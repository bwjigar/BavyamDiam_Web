using iTextSharp.text;
using iTextSharp.text.pdf;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace Sunrise.Services.Models
{
    public class PdfTemplate
    {
        public static string GetHTMLString(List<SearchStone> stones, DataRow SummaryRow, string CurrentRequestURL)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"
				<html>
				<head>
					<link href='{0}/Content/PdfAssets/Style.css' rel='stylesheet' />
				</head>
				<body>
				<p class='p1 ft1'>SUNRISE DIAMONDS LTD.</p>
				<table cellpadding=0 cellspacing=0 class='t0'>
				<tr class='tr3'>
					<td class='td0'><p  class='p2 ft2'>SrNo</p></td>
					<td class='td1'><p  class='p2 ft2'>Stock ID</p></td>
					<td class='td2'><p  class='p2 ft2'>Location</p></td>
					<td class='td3'><p  class='p2 ft3'>Lab</p></td>
					<td class='td4'><p  class='p2 ft2'>Shape</p></td>
					<td class='td5'><p  class='p2 ft2'>Pointer</p></td>
					<td class='td6'><p  class='p2 ft2'>Certi No.</p></td>
					<td class='td7'><p  class='p2 ft3'>Color</p></td>
					<td class='td8'><p  class='p2 ft2'>Clarity</p></td>
					<td class='td9'><p  class='p2 ft3'>Cts</p></td>
					<td class='td10'><p class='p2 ft3'>Rap Price($)</p></td>
					<td class='td11'><p class='p2 ft3'>Rap Amt($)</p></td>
					<td class='td12'><p class='p2 ft2'>Price/Cts($)</p></td>
					<td class='td5'><p  class='p2 ft2'>Disc(%)</p></td>
					<td class='td11'><p class='p2 ft3'>Net Amt($)</p></td>
					<td class='td3'><p  class='p2 ft3'>Cut</p></td>
					<td class='td13'><p class='p2 ft2'>Polish</p></td>
					<td class='td13'><p class='p2 ft2'>Symm</p></td>
					<td class='td3'><p  class='p2 ft3'>Fls</p></td>
					<td class='td14'><p class='p2 ft2'>Length</p></td>
					<td class='td13'><p class='p2 ft2'>Width</p></td>
					<td class='td9'><p  class='p2 ft2'>Depth</p></td>
					<td class='td11'><p class='p2 ft2'>Depth(%)</p></td>
					<td class='td15'><p class='p2 ft2'>Table(%)</p></td>
					<td class='td16'><p class='p2 ft2'>Status</p></td>
				</tr>", CurrentRequestURL);

            foreach (var stone in stones)
            {
                sb.AppendFormat(@"
				<tr class='tr1'>
					<td class='td34'><p class='p14 ft6'>{0}</p></td>
					<td class='td35'><p class='p14 ft6'><NOBR>{1}</NOBR></p></td>
					<td class='td36'><p class='p14 ft6'>{2}</p></td>
					<td class='td37'><p class='p14 ft6'>{3}</p></td>
					<td class='td38'><p class='p14 ft6'>{4}</p></td>
					<td class='td39'><p class='p14 ft6'><NOBR>{5}</NOBR></p></td>
					<td class='td40'><p class='p14 ft6'>{6}</p></td>
					<td class='td41'><p class='p14 ft6'>{7}</p></td>
					<td class='td42'><p class='p14 ft6'>{8}</p></td>
					<td class='td43'><p class='p14 ft6'>{9}</p></td>
					<td class='td44'><p class='p14 ft6'>{10}</p></td>
					<td class='td45'><p class='p14 ft6'>{11}</p></td>
					<td class='td46'><p class='p14 ft9'>{12}</p></td>
					<td class='td39'><p class='p14 ft9'>{13}</p></td>
					<td class='td45'><p class='p14 ft9'>{14}</p></td>
					<td class='td37'><p class='p14 ft6'>{15}</p></td>
					<td class='td47'><p class='p14 ft6'>{16}</p></td>
					<td class='td47'><p class='p14 ft6'>{17}</p></td>
					<td class='td37'><p class='p14 ft6'>{18}</p></td>
					<td class='td48'><p class='p14 ft6'>{19}</p></td>
					<td class='td47'><p class='p14 ft6'>{20}</p></td>
					<td class='td43'><p class='p14 ft6'>{21}</p></td>
					<td class='td45'><p class='p14 ft6'>{22}</p></td>
					<td class='td49'><p class='p14 ft6'>{23}</p></td>
			 		<td class='td50'><p class='p14 ft6'>{24}</p></td>
                </tr>", stone.Sr, stone.stone_ref_no, stone.Location, stone.lab.ToUpper(),
                        stone.shape.ToUpper(), stone.pointer, stone.certi_no, stone.color,
                        stone.clarity, stone.cts, stone.cur_rap_rate.ToString("#,##0.00"), stone.rap_amount.ToString("#,##0.00"),
                        stone.price_per_cts.ToString("#,##0.00"), stone.sales_disc_per.ToString("#,##0.00"), stone.net_amount.ToString("#,##0.00"), stone.cut,
                        stone.polish, stone.symm, stone.fls, stone.length, stone.width,
                        stone.depth, stone.depth_per, stone.table_per, stone.status
                );
            }

            sb.AppendFormat(@"
				<tr class='tr3'>
					<td colspan=2 class='td85'><P class='p2 ft16'>Total:- {0}</P></td>
					<td class='td86'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td87'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td88'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td89'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td90'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td91'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td25'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td26'><P class='p14 ft12'>{1}</P></td>
					<td class='td27'><P class='p14 ft12'>{2}</P></td>
					<td class='td28'><P class='p14 ft12'>{3}</P></td>
					<td class='td29'><P class='p14 ft12'>{4}</P></td>
					<td class='td22'><P class='p14 ft12'><NOBR>{5}</NOBR></P></td>
					<td class='td28'><P class='p14 ft12'>{6}</P></td>
					<td class='td20'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td30'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td30'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td20'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td31'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td30'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td26'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td28'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td32'><P class='p12 ft4'>&nbsp;</P></td>
					<td class='td33'><P class='p12 ft4'>&nbsp;</P></td>
				</tr>", SummaryRow["stone_ref_no"],
                            SummaryRow["CTS"],
                            SummaryRow["cur_rap_rate"],
                            SummaryRow["RAP_AMOUNT"],
                            SummaryRow["PRICE_PER_CTS"],
                            SummaryRow["SALES_DISC_PER"],
                            SummaryRow["NET_AMOUNT"]);



            sb.Append(@"</table></body></html>");

            return sb.ToString();
        }

        public static void ExportToPdf(DataTable myDataTable, string FileNamePath)
        {
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 0, 0, 10f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(FileNamePath, FileMode.Create));

                PDFEvent PageEventHandler = new PDFEvent();
                writer.PageEvent = PageEventHandler;

                pdfDoc.Open();
                Chunk chunkS = new Chunk("  Sunrise Diamonds Ltd.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#83CAFF"))));

                Paragraph p = new Paragraph();

                p.Add(chunkS);

                pdfDoc.Add(p);

                Font font8 = FontFactory.GetFont("ARIAL", 7);
                //--- Add new Line ------------
                Phrase phrase1 = new Phrase(Environment.NewLine);
                pdfDoc.Add(phrase1);

                //-------------------------------

                DataTable dt = myDataTable;
                if (dt != null)
                {
                    //---- Add Result of DataTable to PDF file With Header -----
                    //PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
                    PdfPTable pdfTable = new PdfPTable(28);
                    //relative col widths in proportions - 1/3 and 2s/3

                    pdfTable.DefaultCell.FixedHeight = 80f;

                    pdfTable.TotalWidth = 825f;

                    //fix the absolute width of the table
                    pdfTable.LockedWidth = true;
                    //relative col widths in proportions - 1/3 and 2/3

                    float[] widths = new float[] { 1.4f, 3.1f, 1.8f, 3.2f, 3f, 3.8f, 2.1f, 2.3f, 2.1f, 2.8f,3f, 3f, 2.7f, 3.2f, 2f, 2.3f,
                        2.1f, 2.1f, 2.5f, 2.1f, 2.2f, 2.3f, 2.1f, 2f, 1.9f, 2f, 1.9f, 3f };
                    pdfTable.SetWidths(widths);

                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 100; // percentage
                    pdfTable.DefaultCell.BorderWidth = 0.1f;
                    pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "sh_name")
                        {
                            pdfTable.AddCell(new PdfPCell(new Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#003e7e"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f, Padding = 3, BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#83CAFF")) });
                        }
                    }

                    pdfTable.HeaderRows = 1; // this is the end of the table header
                    pdfTable.DefaultCell.BorderWidth = 0.1f;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Columns[j].ColumnName != "sh_name")
                            {
                                if (i == dt.Rows.Count - 1)
                                {
                                    //if (j == 11)
                                    //{
                                    //    pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f, Padding = 3, BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#83CAFF")) });
                                    //}
                                    //else
                                    //{
                                    pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#003e7e"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f, BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#83CAFF")) });
                                    //}
                                }
                                else if (j == 2)
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#156DC7"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                }
                                else if (j == 11 || j == 12 || j == 13)
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i][j]).ToString("#,##0.00"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                }
                                else if (j == 8)
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                }
                                else if (j == 9 || j == 10)
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i][j]).ToString("#,##0.00"), FontFactory.GetFont(FontFactory.HELVETICA, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                }
                                else if (j == 14 || j == 15 || j == 16)
                                {
                                    if (dt.Rows[i][14].ToString() == "3EX")
                                    {
                                        pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                    }
                                    else
                                    {
                                        pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                    }
                                }
                                else if (j == 9)
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                }
                                else
                                {
                                    pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i][j]).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 7, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                                }
                            }
                        }
                    }

                    pdfDoc.Add(pdfTable);
                }
                //string Page = pdfDoc.PageNumber.ToString();
                //iTshEvent PageEventHandler = new iTshEvent();
                //writer.PageEvent = PageEventHandler;
                //PageEventHandler.Title = Title;
                //PageEventHandler.HeaderFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, Font.BOLD);
                //PageEventHandler.HeaderLeft = "Group";
                //PageEventHandler.HeaderRight = "1";

                pdfDoc.Close();


                // File.ReadAllBytes(FileNamePath);
            }
            catch (DocumentException de)
            {
                //System.Web.HttpContext.Current.Response.Write(de.Message);
            }
            catch (IOException ioEx)
            {
                //System.Web.HttpContext.Current.Response.Write(ioEx.Message);
            }
            catch (Exception ex)
            {
                //System.Web.HttpContext.Current.Response.Write(ex.Message);
            }
            //if (FileNamePath.Length > 0)
            //    if (System.IO.File.Exists(FileNamePath))
            //        System.IO.File.Delete(FileNamePath);

        }

        public static void CartExportToPdf(DataTable myDataTable, string FileNamePath)
        {
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 0, 0, 10f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(FileNamePath, FileMode.Create));

                PDFEvent PageEventHandler = new PDFEvent();
                writer.PageEvent = PageEventHandler;

                pdfDoc.Open();
                Chunk chunkS = new Chunk("Sunrise Diamonds Ltd.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#83CAFF"))));

                Paragraph p = new Paragraph();

                p.Add(chunkS);

                pdfDoc.Add(p);

                Font font8 = FontFactory.GetFont("ARIAL", 7);
                //--- Add new Line ------------
                Phrase phrase1 = new Phrase(Environment.NewLine);
                pdfDoc.Add(phrase1);

                //-------------------------------

                DataTable dt = myDataTable;
                if (dt != null)
                {
                    //---- Add Result of DataTable to PDF file With Header -----
                    PdfPTable pdfTable = new PdfPTable(28);
                    pdfTable.DefaultCell.FixedHeight = 80f;
                    pdfTable.TotalWidth = 825f;

                    //fix the absolute width of the table
                    pdfTable.LockedWidth = true;

                    float[] widths = new float[] { 1.4f, 3.1f, 1.8f, 3.2f, 3f, 3.8f, 2.1f, 2.3f, 2.1f, 2.8f,3f, 3f, 2.7f, 3.2f, 2f, 2.3f,
                        2.1f, 2.1f, 2.5f, 2.1f, 2.2f, 2.3f, 2.1f, 2f, 1.9f, 2f, 1.9f, 3f };
                    pdfTable.SetWidths(widths);

                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 100;
                    pdfTable.DefaultCell.BorderWidth = 0.1f;
                    pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7,
                        new BaseColor(System.Drawing.ColorTranslator.FromHtml("#003e7e")));
                    BaseColor bColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#83CAFF"));

                    Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 7,
                        new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000")));

                    Font cellFontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7,
                        new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000")));

                    Font cellFontRed = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.RED);

                    #region Add headers

                    pdfTable.AddCell(new PdfPCell(new Phrase("Sr.", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Ref. No.", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Lab", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Shape", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Pointer", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Certi", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Color", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Clarity", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Cts", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Rap Price($)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Rap Amt($)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Price/Cts($)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Disc(%)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Net Amt($)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Cut", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Polish", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Symm", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Fls", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Length", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Width", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Depth", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Depth(%)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Table(%)", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Cr Ang", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Cr Ht", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Pav Ang", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Pav Ht", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.AddCell(new PdfPCell(new Phrase("Status", headerFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BorderWidth = 0.1f,
                        Padding = 3,
                        BackgroundColor = bColor
                    });

                    pdfTable.HeaderRows = 1;
                    pdfTable.DefaultCell.BorderWidth = 0.1f;

                    #endregion Add headers
                    int i = 0;
                    string status = "";
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;
                    for (; i < dt.Rows.Count; i++)
                    {
                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["sr"]).ToString(), cellFont)) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["stone_ref_no"]).ToString(), cellFont)) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i]["Lab"].ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 6, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#156DC7"))))) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["Shape"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["Pointer"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["certi_no"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["color"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["clarity"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(dt.Rows[i]["Cts"].ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i]["cur_rap_rate"]).ToString("#,##0.00"), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i]["rap_amount"]).ToString("#,##0.00"), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i]["price_per_cts"]).ToString("#,##0.00"), cellFontRed)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i]["sales_disc_per"]).ToString("#,##0.00"), cellFontRed)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase(Convert.ToDouble(dt.Rows[i]["net_amount"]).ToString("#,##0.00"), cellFontRed)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        if (dt.Rows[i]["cut"].ToString() == "3EX")
                        {
                            pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["cut"]).ToString(), cellFontBold)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                            pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["polish"]).ToString(), cellFontBold)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                            pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["symm"]).ToString(), cellFontBold)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                        }
                        else
                        {
                            pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["cut"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                            pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["polish"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                            pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["symm"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                        }

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["fls"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["length"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["width"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["depth"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["depth_per"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["table_per"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["crown_angle"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["crown_height"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["pav_angle"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        pdfTable.AddCell(new PdfPCell(new Phrase((dt.Rows[i]["pav_height"]).ToString(), cellFont)){ HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });

                        status = asTitleCase.ToTitleCase((dt.Rows[i]["Stock_Staus"]).ToString().ToLower());
                        pdfTable.AddCell(new PdfPCell(new Phrase(status, cellFont)) 
                        { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0.1f });
                    }

                    pdfDoc.Add(pdfTable);
                }
                pdfDoc.Close();
            }
            catch (DocumentException de)
            {
                //System.Web.HttpContext.Current.Response.Write(de.Message);
            }
            catch (IOException ioEx)
            {
                //System.Web.HttpContext.Current.Response.Write(ioEx.Message);
            }
            catch (Exception ex)
            {
                //System.Web.HttpContext.Current.Response.Write(ex.Message);
            }
        }

        public static void GeneratePDF(List<SearchStone> stones, DataRow SummaryRow, string FileNamePath)
        {
            Font fntTableFontHdr = FontFactory.GetFont("Verdana", 8, Font.BOLD, BaseColor.BLACK);
            Font fntTableFont = FontFactory.GetFont("Verdana", 8, Font.NORMAL, BaseColor.BLACK);

            Document doc = new Document(PageSize.A4, 5, 5, 10, 20);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(FileNamePath, FileMode.Create));
            doc.Open();
            PdfPTable myTable = new PdfPTable(24);
            // Table size is set to 100% of the page
            myTable.WidthPercentage = 100;
            //Left aLign
            myTable.HorizontalAlignment = 0;
            myTable.SpacingAfter = 10;

            #region  Set columns Width
            float[] sglTblHdWidths = new float[24];
            sglTblHdWidths[0] = 15f;
            sglTblHdWidths[1] = 200f;
            sglTblHdWidths[2] = 385f;
            #endregion

            // Set the column widths on table creation. Unlike HTML cells cannot be sized.
            myTable.SetWidths(sglTblHdWidths);
            PdfPCell CellOneHdr = new PdfPCell(new Phrase(" ", fntTableFontHdr));
            myTable.AddCell(CellOneHdr);
            PdfPCell CellTwoHdr = new PdfPCell(new Phrase("cell 2 Hdr", fntTableFontHdr));
            myTable.AddCell(CellTwoHdr);
            PdfPCell CellTreeHdr = new PdfPCell(new Phrase("cell 3 Hdr", fntTableFontHdr));
            myTable.AddCell(CellTreeHdr);
            PdfPCell CellOne = new PdfPCell(new Phrase("R1 C1", fntTableFont));
            CellOne.Rotation = -90;
            myTable.AddCell(CellOne);
            PdfPCell CellTwo = new PdfPCell(new Phrase("R1 C2", fntTableFont));
            myTable.AddCell(CellTwo);
            PdfPCell CellTree = new PdfPCell(new Phrase("R1 C3", fntTableFont));
            myTable.AddCell(CellTree);
            PdfPCell CellOneR2 = new PdfPCell(new Phrase("R2 C1", fntTableFont));
            CellOneR2.Rotation = -90;
            myTable.AddCell(CellOneR2);
            PdfPCell CellTwoR2 = new PdfPCell(new Phrase("R2 C2", fntTableFont));
            myTable.AddCell(CellTwoR2);
            PdfPCell CellTreeR2 = new PdfPCell(new Phrase("R2 C3", fntTableFont));
            myTable.AddCell(CellTreeR2);
            doc.Add(myTable);
            doc.Close();
        }
    }
}
