using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using OfficeOpenXml;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;
using System.Text.RegularExpressions;
using System.IO;
using DAL;
using System.Data;

namespace EpExcelExportLib
{
    public class GridViewEpExcelExport: EpExcelExportLib.EpExcelExport
    {
        private Boolean Exporting = false;
        public GridView _gridView;
        public const string HTML_TAG_PATTERN = "<.*?>";
        SortedList<string, UInt32> combineStyleList;

        public GridViewEpExcelExport(System.Web.UI.WebControls.GridView gridView, string sheetname, string TableName)
            : base(sheetname, TableName)
        {
            _gridView = gridView;
        }

        protected void GridView1_Unload(object sender, EventArgs e)
        {
            if (Exporting == false)
            {
                return;
            }
            this.Save();
        }

        public override void CreateExcel(System.IO.Stream ms, String TempFolderPath)
        {
            Exporting = true;
            Directory.CreateDirectory(TempFolderPath);
            this.PrepareExcel(ms, TempFolderPath);
            this.Save();
        }

        private String GetCellValue(System.Web.UI.Control gvcell, ref  String Hyperlink)
        {
            string cv = "";
            if (gvcell.Controls.Count > 0)
            {
                string val = "";
                for (int i = 0; i < gvcell.Controls.Count; i++)
                {
                    if (gvcell.Controls[i].GetType() == typeof(HyperLink))
                    {
                        val = val + (gvcell.Controls[i] as HyperLink).Text;
                        Hyperlink = (gvcell.Controls[i] as HyperLink).NavigateUrl;
                    }
                    else if (gvcell.Controls[i].GetType().BaseType == typeof(LinkButton))
                    {
                        val = val + (gvcell.Controls[i] as LinkButton).Text;
                        Hyperlink = (gvcell.Controls[i] as LinkButton).PostBackUrl;
                    }
                    else if (gvcell.Controls[i].GetType() == typeof(ImageButton))
                    {
                        val = val + (gvcell.Controls[i] as ImageButton).ImageUrl;
                        Hyperlink = (gvcell.Controls[i] as ImageButton).ImageUrl;
                    }
                    else if (gvcell.Controls[i].GetType() == typeof(CheckBox))
                    {
                        val = val + ((gvcell.Controls[i] as CheckBox).Checked ? "True" : "False");
                    }
                    else if (gvcell.Controls[i].GetType() == typeof(LiteralControl))
                    {
                        val = val + (gvcell.Controls[i] as LiteralControl).Text;
                    }
                    else //if (gvcell.Controls[i].HasControls())
                    {
                        val = val + GetCellValue(gvcell.Controls[i], ref Hyperlink);
                    }
                }

                return val.Replace("\r", "").Replace("\n", "").Trim();
            }
            else
            {
                if (gvcell.GetType() == typeof(DataControlFieldCell))
                {
                    cv = (gvcell as DataControlFieldCell).Text;
                    if (cv.ToString() == "&nbsp;")
                    {
                        cv = "";
                    }
                }
                else if (gvcell.GetType() == typeof(LiteralControl))
                {
                    cv = (gvcell as LiteralControl).Text;

                    if (cv.ToString() == "&nbsp;")
                    {
                        cv = "";
                    }
                }
                else if (gvcell.GetType() == typeof(DataBoundLiteralControl))
                {
                    cv = (gvcell as DataBoundLiteralControl).Text;
                    if (cv.ToString() == "&nbsp;")
                    {
                        cv = "";
                    }
                }
                else if (gvcell.GetType() == typeof(DataControlFieldHeaderCell))
                {
                    cv = (gvcell as DataControlFieldHeaderCell).Text;

                    if (cv.ToString() == "&nbsp;")
                    {
                        cv = "";
                    }
                }

                string retStr = Regex.Replace(cv.ToString(), HTML_TAG_PATTERN, string.Empty);

                return retStr.ToString().Replace("\r", "").Replace("\n", "").Trim();
            }
        }

        protected override void CreateHeader(FillingWorksheetEventArgs e)
        {
            e.StartrowCount = 0;
            base.CreateHeader(e);
        }

        protected override void CreateDetail(FillingWorksheetEventArgs e)
        {
            base.CreateDetail(e);

            combineStyleList = new SortedList<string, UInt32>();

            foreach (GridViewRow gvRow in this._gridView.Rows)
            {

                if (gvRow.Visible == false)
                {
                    continue;
                }

                Int32 CellColNo = 0;
                CurrentRowCount += 1;

                //Row row = GetRow(e.Data, (uint)CurrentRowCount);

                for (int Cellno = 0; Cellno < gvRow.Cells.Count; Cellno++)
                {

                    ExcelHeader h = this.AllColumns[Cellno];

                    if (h.visible == true)
                    {
                        CellColNo++;

                        string colname = GetColumnAlphaIndex(CellColNo);

                        //BeforeCreateCellEventArgs CellArgs = new BeforeCreateCellEventArgs();
                        //CellArgs.ColDataType = h.ColDataType;
                        //CellArgs.ta = TableArea.Detail;

                        //RaiseBeforeCreateCellEvent(CellArgs);

                        //Cell c = this.InsertCellInRow(e.Data, row, colname + (CurrentRowCount).ToString());
                        ExcelRange c = this._worksheet.Cells[colname + (CurrentRowCount).ToString()];

                        ExcelCellFormat cf = new ExcelCellFormat();
                        //cf.ul = UnderlineValues.None;
                        //c.DataType = CellArgs.ColDataType;

                        string prevText = "";

                        FormatCell(c, gvRow.Cells[Cellno], ref cf, ref prevText);

                        cf.tableArea = TableArea.Detail;
                        cf.RowInd = (int)CurrentRowCount;
                        cf.ColInd = CellColNo;
                        cf.HeaderStartRow = TableHeaderStartRow;
                        cf.DetailStartRow = TableDetailStartRow;
                        cf.ColumnName = h.Caption;

                        cf.Text = prevText;
                        cf.GridRow = gvRow;


                        RaiseAfterCreateCellEvent(ref cf);
                        if (cf.url != "")
                            //priyanka on date[24-oct-16] add cf.formula
                           SetCellHyperlink(this._worksheet, c.Address, cf.url, cf.Text, cf.Formula);
                        ////priyanka on date[24-oct-16] end//
                        cf.Format = h.NumFormat;
                        cf.HorizontalAllign = h.HorizontalAllign;
                        cf.VerticalAllign = h.VerticalAllign;

                        if (cf.OverrideDefault)
                        {
                            ApplyStyle(e.Worksheet, c, cf);
                        }
                        else
                        {
                            UInt32 combineIndex;
                            UInt32 UserIndex = DefaultStyleindex;

                            if (cf.StyleInd != null)
                            {
                                UserIndex = (UInt32)cf.StyleInd;

                            }

                            String IsHyperlink = "";
                            if (cf.url.Length > 0)
                            {
                                IsHyperlink = "H";
                               
                            }


                            if (combineStyleList.ContainsKey(UserIndex + ":" + cf.ColInd.ToString() + IsHyperlink))
                            {
                                combineIndex = combineStyleList[UserIndex + ":" + cf.ColInd.ToString() + IsHyperlink];
                            }
                            else
                            {
                                combineIndex = GetCombineStyle(UserIndex, h.NumFormat, h.HorizontalAllign, h.VerticalAllign);

                                if (IsHyperlink == "H")
                                {
                                    combineIndex = GetCombineHyperlinkStyle(combineIndex);
                                }

                                combineStyleList.Add(UserIndex + ":" + cf.ColInd + IsHyperlink, combineIndex);

                            }
                            ApplyStyle(c, (uint)combineIndex);
                        }

                        if (cf.Formula != null && cf.Formula.Length > 0)
                        {
                            //cf.Formula = ReplaceFormulaWithAlphaIndexDetail(cf.Formula);
                            //CellFormula cfor = new CellFormula();
                            //cfor.FormulaType = CellFormulaValues.Normal;
                            //cfor.Text = cf.Formula;
                            //AddFormulaCalculationChain(c.CellReference);
                            //c.InsertBefore<OpenXmlElement>(cfor, c.FirstChild);
                            cf.Formula = ReplaceFormulaWithAlphaIndexDetail(cf.Formula);
                            //RJCHANGE
                            c.Formula = cf.Formula;
                            //SetCellValue(c, "=" + cf.Formula);
                        }
                        else if (cf.Text != null)
                        {
                            SetCellValue(c, cf.Text);
                        }

                    }

                }

            }

        }

        private UInt32 GetCombineHyperlinkStyle(UInt32 StyleInd)
        {
            ExcelFormat combineFormat = (ExcelFormat)StyleList[StyleInd].Clone();
            combineFormat.forColorArgb = KnownHexValue.Blue;
            combineFormat.ul = OfficeOpenXml.Style.ExcelUnderLineType.Single;

            return AddStyle(combineFormat);


        }

        private UInt32 GetCombineStyle(UInt32 StyleInd, String NumFormat, OfficeOpenXml.Style.ExcelHorizontalAlignment ha, OfficeOpenXml.Style.ExcelVerticalAlignment va)
        {
            ExcelFormat combineFormat = (ExcelFormat)StyleList[StyleInd].Clone();

            combineFormat.Format = NumFormat;
            combineFormat.HorizontalAllign = ha;
            combineFormat.VerticalAllign = va;

            return AddStyle(combineFormat);


        }

        public void InsertCellInRow(Int32 RowIndex, Int32 ColIndex, Object Val)
        {
            ExcelRange x = this._worksheet.Cells[RowIndex, ColIndex];
            this._worksheet.Cells[RowIndex, ColIndex].Value = Val;
        }

        protected override void AddColumnDef(List<ExcelHeader> AllColumns)
        {
            if (_gridView.HeaderRow != null)
            {
                CurrentRowCount++;

                int CellColNo = 0;
                UInt32 rn = 0;
                bool HeaderAdded = false;

                for (rn = CurrentRowCount; rn < CurrentRowCount + 1; rn++)
                {
                    for (int Cellno = 0; Cellno < _gridView.HeaderRow.Cells.Count; Cellno++)
                    {
                        if (!HeaderAdded)
                        {
                            ExcelHeader h = new ExcelHeader();

                            string hperlink = "";

                            h.Caption = GetCellValue(_gridView.HeaderRow.Cells[Cellno], ref hperlink);

                            if (h.Caption.Length > 20)
                            {
                                h.Caption = "";
                            }

                            System.Web.UI.WebControls.DataControlFieldHeaderCell hc;
                            hc = (System.Web.UI.WebControls.DataControlFieldHeaderCell)_gridView.HeaderRow.Cells[Cellno];

                            if (hc.ContainingField != null)
                            {
                                if (h.Caption.Length == 0)
                                {
                                    h.Caption = hc.ClientID;
                                }

                                if (hc.ContainingField.Visible == false)
                                {
                                    h.visible = false;
                                }
                                else
                                {
                                    CellColNo++;
                                    h.visible = true;
                                    string colname = GetColumnAlphaIndex(CellColNo);
                                    h.ColInd = CellColNo;
                                    //h.ColDataType = CellValues.String;
                                    h.ColDataType = eDataTypes.String;

                                }
                            }

                            RaiseBeforeCreateColumn(ref h);

                            AllColumns.Add(h);
                        }
                    }
                }
            }
            base.AddColumnDef(AllColumns);
        }

        protected override void AddColumnDef(List<ExcelHeader> AllColumns, int iTransId)
        {
            List<ApiColSettings> columnsSettings = new List<ApiColSettings>();
            if (_gridView.HeaderRow != null)
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, Convert.ToInt64(iTransId)));
                DataTable dt = db.ExecuteSP("ApiDet_Select", para.ToArray(), false);

                if (dt.Rows.Count > 0)
                {
                    columnsSettings = DTExtension.ToList<ApiColSettings>(dt);
                }

                CurrentRowCount++;

                int CellColNo = 0;
                UInt32 rn = 0;
                bool HeaderAdded = false;

                for (rn = CurrentRowCount; rn < CurrentRowCount + 1; rn++)
                {
                    for (int Cellno = 0; Cellno < _gridView.HeaderRow.Cells.Count; Cellno++)
                    {
                        if (!HeaderAdded)
                        {
                            ExcelHeader h = new ExcelHeader();

                            string hperlink = "";

                            h.Caption = GetCellValue(_gridView.HeaderRow.Cells[Cellno], ref hperlink);

                            if (h.Caption.Length > 20)
                            {
                                h.Caption = "";
                            }

                            System.Web.UI.WebControls.DataControlFieldHeaderCell hc;
                            hc = (System.Web.UI.WebControls.DataControlFieldHeaderCell)_gridView.HeaderRow.Cells[Cellno];

                            if (hc.ContainingField != null)
                            {
                                if (h.Caption.Length == 0)
                                {
                                    h.Caption = hc.ClientID;
                                }

                                if (hc.ContainingField.Visible == false)
                                {
                                    h.visible = false;
                                }
                                else
                                {
                                    CellColNo++;
                                    h.visible = true;
                                    string colname = GetColumnAlphaIndex(CellColNo);
                                    h.ColInd = CellColNo;
                                    //h.ColDataType = CellValues.String;
                                    h.ColDataType = eDataTypes.String;

                                }
                            }

                            RaiseBeforeCreateColumn1(ref h, columnsSettings);

                            AllColumns.Add(h);
                        }
                    }
                }
            }
            base.AddColumnDef(AllColumns, iTransId);
        }
        //private void FormatCell(WorksheetPart wsp, Worksheet ws, Stylesheet stylesheet1, Cell c, System.Web.UI.Control gvcell, ref ExcelCellFormat cf, ref String Value)
        //{

        //    string hyperlink = "";

        //    Value = GetCellValue(gvcell, ref hyperlink);
        //    SetCellValue(c, Value);

        //    if (hyperlink.Length > 0)
        //    {
        //        //c.StyleIndex = GetHyperlinkStyle(ws, stylesheet1, cf);

        //        SetCellHyperlink(ws, wsp, c.CellReference, hyperlink, (c.CellValue as CellValue).Text);

        //    }

        //    cf.url = hyperlink;

        //}

        private void FormatCell(ExcelRange c, System.Web.UI.Control gvcell, ref ExcelCellFormat cf, ref String Value)
        {

            string hyperlink = "";

            Value = GetCellValue(gvcell, ref hyperlink);
            //SetCellValue(c, Value);
            //priyanka on date[24-oct-16]
            //if (hyperlink.Length > 0)
            //{
            //    //SetCellHyperlink(this._worksheet, c.Address, hyperlink, c.Value.ToString());
            //    SetCellHyperlink(this._worksheet, c.Address, hyperlink, Value.ToString());
            //}
            //priyanka on date[24-oct-16] end
            cf.url = hyperlink;

        }

        public void SetCellHyperlink(ExcelWorksheet worksheet, String Address, string linkValue, String Display, string Formula)
        {//priyanka on date[24-oct-16]
            //if (linkValue.Length > 0)
            //{
            //    if (this._worksheet.Cells[Address].Hyperlink == null || this._worksheet.Cells[Address].Hyperlink.ToString() != linkValue)
            //    {

            //            this._worksheet.Cells[Address].Hyperlink = new Uri(linkValue);
            //    }
            //}
            if (linkValue.Length > 0)
            {
                if (Formula != "" && Formula!=null)
                { }
                else if (this._worksheet.Cells[Address].Hyperlink == null || this._worksheet.Cells[Address].Hyperlink.ToString() != linkValue)
                {

                    this._worksheet.Cells[Address].Hyperlink = new Uri(linkValue);
                }
            }
            ////priyanka on date[24-oct-16] end

        }

        

    }

}
