using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpExcelExportLib
{
    public class DataTableEpExcelExport : EpExcelExportLib.EpExcelExport
    {

        SortedList<string, UInt32> combineStyleList;
        //GridView _gridView;
        public System.Data.DataTable _dt;  //change by kaushal date[07-04-2018]
        const string HTML_TAG_PATTERN = "<.*?>";

        public DataTableEpExcelExport(System.Data.DataTable dt, string sheetname, string TableName)
            : base(sheetname, TableName)
        {
            //_gridView = gridView;
            _dt = dt;

        }


        private Boolean Exporting = false;

        public override void CreateExcel(System.IO.Stream ms, String TempFolderPath)
        {

            Exporting = true;

            this.PrepareExcel(ms, TempFolderPath);

            this.Save();

        }

        public void SetCellHyperlink(ExcelWorksheet worksheet, String Address, string linkValue, String Display, String Formula)
        {
            if (Formula != null && Formula != "")
            { }
            else if (linkValue.Length > 0)
            {
                this._worksheet.Cells[Address].Formula = @"=HYPERLINK(""" + linkValue + @""",""" + Display + @""")";
            }

        }

        private void FormatCell(ExcelRange c, String gvcellValue, String hLink, ref ExcelCellFormat cf, ref String Value)
        {

            string hyperlink = hLink;

            Value = gvcellValue;
            //SetCellValue(c, Value);

            //if (hyperlink.Length > 0)
            //{
            //    //SetCellHyperlink(this._worksheet, c.Address, hyperlink, c.Value.ToString());
            //    SetCellHyperlink(this._worksheet, c.Address, hyperlink, Value.ToString());
            //}

            cf.url = hyperlink;

        }


        private String GetCellValue(System.Web.UI.Control gvcell, ref String Hyperlink)
        {
            return "";
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

        protected override void CreateDetail(FillingWorksheetEventArgs e)
        {
            base.CreateDetail(e);

            combineStyleList = new SortedList<string, UInt32>();

            int cnt = 0; //add by kaushal date[07-04-2018]

            foreach (System.Data.DataRow drRow in this._dt.Rows)
            {

                //if (gvRow.Visible == false)
                //{
                //    continue;
                //}

                Int32 CellColNo = 0;
                CurrentRowCount += 1;

                //Row row = GetRow(e.Data, (uint)CurrentRowCount);

                for (int Cellno = 0; Cellno < this.AllColumns.Count; Cellno++)
                {

                    ExcelHeader h = this.AllColumns[Cellno];

                    if (h.visible == true)
                    {
                        CellColNo++;

                        string colname = GetColumnAlphaIndex(CellColNo);

                        BeforeCreateCellEventArgs CellArgs = new BeforeCreateCellEventArgs();
                        CellArgs.ColDataType = h.ColDataType;
                        CellArgs.ta = TableArea.Detail;

                        RaiseBeforeCreateCellEvent(CellArgs);

                        //Cell c = this.InsertCellInRow(e.Data, row, colname + (CurrentRowCount).ToString());
                        ExcelRange c = this._worksheet.Cells[colname + (CurrentRowCount).ToString()];

                        ExcelCellFormat cf = new ExcelCellFormat();
                        //cf.ul = OfficeOpenXml.Style.eUnderLineType.None;
                        //c.DataType = CellArgs.ColDataType;

                        string prevText = "";

                        String hLink = "";
                        if (h.HyperlinkColName != null && h.HyperlinkColName.ToString() != "")
                        {
                            hLink = drRow[h.HyperlinkColName.ToString()].ToString();
                        }

                        //FormatCell(e.WorksheetPart, e.Worksheet, e.Style, c, drRow[h.ColName].ToString(), hLink, ref cf, ref prevText);
                        FormatCell(c, drRow[h.ColName].ToString(), hLink, ref cf, ref prevText);

                        cf.tableArea = TableArea.Detail;
                        cf.RowInd = Convert.ToInt32(CurrentRowCount);
                        cf.ColInd = CellColNo;
                        cf.HeaderStartRow = TableHeaderStartRow;
                        cf.DetailStartRow = TableDetailStartRow;
                        cf.ColumnName = h.Caption;

                        cf.Text = prevText;
                        cf.GridRow = drRow;
                        cf.RowIndex = cnt;  //add by kaushal date[07-04-2018]

                        RaiseAfterCreateCellEvent(ref cf);

                        if (hLink != "")
                        {
                            SetCellHyperlink(this._worksheet, c.Address, hLink, prevText, c.Formula);
                        }


                        cf.Format = h.NumFormat;
                        cf.HorizontalAllign = h.HorizontalAllign;
                        cf.VerticalAllign = h.VerticalAllign;

                        if (cf.OverrideDefault)
                        {
                            //ApplyStyle(e.Worksheet, e.Style, c, cf);
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
                            //CellFormula cfor = new CellFormula();
                            //cfor.FormulaType = CellFormulaValues.Normal;
                            //cfor.Text = cf.Formula;
                            //AddFormulaCalculationChain(c.CellReference);
                            //c.Append(cfor);
                            cf.Formula = ReplaceFormulaWithAlphaIndexDetail(cf.Formula);
                            c.Formula = cf.Formula;
                        }
                        else if (cf.Text != null)
                        {
                            SetCellValue(c, cf.Text);
                        }

                        //if (cf.Text != null && cf.Text != prevText)
                        //{
                        //    SetCellValue(c, cf.Text);
                        //}


                        //if (_gridView.HeaderRow.Cells[CellColNo - 1].ColumnSpan > 1)
                        //{
                        //    ExpandCellToRight(e.Worksheet, e.Data, c, gvRow.Cells[CellColNo - 1].ColumnSpan - 1, CurrentRowCount, CellColNo);
                        //    CellColNo = CellColNo + gvRow.Cells[CellColNo - 1].ColumnSpan - 1;
                        //}

                    }

                }


            }

        }

        protected override void AddColumnDef(List<ExcelHeader> AllColumns)
        {
            CurrentRowCount++;
            UInt32 rn = 0;
            for (rn = CurrentRowCount; rn < CurrentRowCount + 1; rn++)
            {
                List<ExcelHeader> tmpAllColumns = new List<ExcelHeader>();


                for (int i = 0; i < _dt.Columns.Count; i++)
                {
                    ExcelHeader h = new ExcelHeader();
                    h.ColName = _dt.Columns[i].ColumnName;
                    h.visible = true;
                    h.Caption = _dt.Columns[i].ColumnName;
                    h.ColDataType = eDataTypes.String;
                    if (h.Caption.Length > 20)
                    {
                        h.Caption = "";
                    }

                    RaiseBeforeCreateColumn(ref h);

                    //AllColumns.Add(h);
                    tmpAllColumns.Add(h);
                }

                var sortedWords =
                from w in tmpAllColumns
                orderby w.ColInd
                select w;

                foreach (EpExcelExportLib.EpExcelExport.ExcelHeader item in sortedWords)
                {
                    AllColumns.Add(item);
                }

            }

            base.AddColumnDef(AllColumns);

        }

        protected override void CreateHeader(FillingWorksheetEventArgs e)
        {
            e.StartrowCount = 0;
            base.CreateHeader(e);
        }

    }
}
