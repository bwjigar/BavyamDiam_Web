using System;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Web;
using System.Threading;
using System.Web.UI.WebControls;
using EpExcelExportLib;
using System.IO;
using OfficeOpenXml;
using System.Data;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Collections.Generic;

namespace DAL
{
    public static class Common
    {
        public static DateTime GetHKTime()
        {
            DateTime dt = DateTime.Now.ToUniversalTime();
            dt = TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            return dt;
        }

        public static DateTime GetGMTime(DateTime ust)
        {
            DateTime dt;//= ust.ToUniversalTime();
            dt = ust.AddHours(5);
            return dt;
        }

        public static DateTime GetHKTime(DateTime ust)
        {
            DateTime dt;
            dt = GetGMTime(ust);
            dt = dt.AddHours(8);// (TimeZoneInfo.FindSystemTimeZoneById("China Standard Time").BaseUtcOffset);

            return dt;
        }

        public static bool EmailError(Exception ex, System.Web.HttpRequest Request, string UserName, int? UserId, string ErrorFrom)
        {
            //-- Start [29-10-15] By Aniket Doc-1151.
            string[] PName = Request.Url.ToString().Split('/');
            string ErrPage = null;
            if (PName.Length > 0)
            {
                for (int i = 0; i < PName.Length; i++)
                {
                    if (PName[i].ToString().Contains(".aspx"))
                    {
                        ErrPage = PName[i].ToString();
                        if (ErrPage.Contains('?'))
                        {
                            string[] temp = ErrPage.Split('?');
                            ErrPage = temp[0].ToString();
                        }
                    }
                }
            }

            //ErrorLogDataContext errDataContext = new ErrorLogDataContext();
            //errDataContext.ErrorLog_Insert(GetHKTime(), UserId, Request.UserHostAddress.ToString(), ex.StackTrace.ToString(), ex.Message, ErrorFrom, ErrPage);

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, GetHKTime()));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, UserId));
            para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, Request.UserHostAddress.ToString()));
            para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.StackTrace.ToString()));
            para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.Message));
            para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrorFrom));
            para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrPage));
            db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);



            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p>Error raise from Sunrise Diamond.</p>");
                loSb.Append(@"<b>Date Time: </b>" + GetHKTime().ToString() + "<br />");
                loSb.Append(@"<b>Location: </b>" + Request.Url.ToString() + "<br />");
                loSb.Append(@"<b>Message: </b>" + ex.Message + "<br />");
                loSb.Append(@"<b>Trace: </b>" + ex.StackTrace.ToString() + "<br />");
                if (ex.InnerException != null)
                {
                    loSb.Append(@"<b>Inner Messsag: </b>" + ex.InnerException.Message.ToString() + "<br />");
                    loSb.Append(@"<b>Inner Trace: </b>" + ex.InnerException.StackTrace.ToString() + "<br />");
                }
                if (UserName != "" && UserName != null)
                    loSb.Append(@"<b>Username: </b>" + UserName + "<br />");
                else
                    loSb.Append(@"<b>Username: </b>" + HttpContext.Current.User.Identity.Name + "<br />");

                loSb.Append(@"<b>IP: </b>" + Request.UserHostAddress.ToString() + "<br />");
                loSb.Append(@"<b>DNS: </b>" + Request.UserHostName.ToString() + "<br />");
                loSb.Append(@"<b>Browser: </b>" + Request.Browser.Type.ToString() + "<br />");
                loSb.Append(@"<b>Browser Version: </b>" + Request.Browser.Version.ToString() + "<br />");
                loSb.Append(@"<b>Agent: </b>" + Request.UserAgent.ToString() + "<br />");

                loSb.Append(EmailSignature());

                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);

                loMail.Subject = "Error - Sunrise – " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(Convert.ToString(loSb), null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                loSmtp.Send(loMail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailError(Exception ex, System.Web.HttpRequest Request, string UserName, string fsSender, string fsExtraText, int UserId, string ErrorFrom)
        {
            //-- Start [29-10-15] By Aniket Doc-1151.
            string[] PName = Request.Url.ToString().Split('/');
            string ErrPage = null;
            if (PName.Length > 0)
            {
                for (int i = 0; i < PName.Length; i++)
                {
                    if (PName[i].ToString().Contains(".aspx"))
                    {
                        ErrPage = PName[i].ToString();
                        if (ErrPage.Contains('?'))
                        {
                            string[] temp = ErrPage.Split('?');
                            ErrPage = temp[0].ToString();
                        }
                    }
                }
            }
            //-- Over [29-10-15]

            //--By Aniket on [22-04-2015] To Store Error log (added UserId and ErrorFrom Parameters above).
            //ErrorLogDataContext errDataContext = new ErrorLogDataContext();
            //errDataContext.ErrorLog_Insert(GetHKTime(), UserId, Request.UserHostAddress.ToString(), ex.StackTrace.ToString(), ex.Message, ErrorFrom, ErrPage);

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, GetHKTime()));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int64, System.Data.ParameterDirection.Input, UserId));
            para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, Request.UserHostAddress.ToString()));
            para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.StackTrace.ToString()));
            para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.Message));
            para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrorFrom));
            para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, ErrPage));
            db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);

            //--Over [22-04-2015] 

            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p>Error raise from Sunrise Diamond.</p>");
                loSb.Append(@"<p><b>" + fsExtraText + "</b></p>");
                loSb.Append(@"<b>Date Time: </b>" + GetHKTime().ToString() + "<br />");
                loSb.Append(@"<b>Location: </b>" + Request.Url.ToString() + "<br />");
                loSb.Append(@"<b>Message: </b>" + ex.Message + "<br />");
                loSb.Append(@"<b>Trace: </b>" + ex.StackTrace.ToString() + "<br />");
                if (ex.InnerException != null)
                {
                    loSb.Append(@"<b>Inner Messsag: </b>" + ex.InnerException.Message.ToString() + "<br />");
                    loSb.Append(@"<b>Inner Trace: </b>" + ex.InnerException.StackTrace.ToString() + "<br />");
                }

                if (UserName != "" && UserName != null)
                    loSb.Append(@"<b>Username: </b>" + UserName + "<br />");
                else
                    loSb.Append(@"<b>Username: </b>" + HttpContext.Current.User.Identity.Name + "<br />");

                loSb.Append(@"<b>IP: </b>" + Request.UserHostAddress.ToString() + "<br />");
                loSb.Append(@"<b>DNS: </b>" + Request.UserHostName.ToString() + "<br />");
                loSb.Append(@"<b>Browser: </b>" + Request.Browser.Type.ToString() + "<br />");
                loSb.Append(@"<b>Browser Version: </b>" + Request.Browser.Version.ToString() + "<br />");
                loSb.Append(@"<b>Agent: </b>" + Request.UserAgent.ToString() + "<br />");

                loSb.Append(EmailSignature());

                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsSender);
                loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);

                loMail.Subject = "Error - Sunrise – " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(Convert.ToString(loSb), null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                loSmtp.Send(loMail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private static String EmailHeader()
        {
            return @"<html><head><style type=""text/css"">body{font-family: Verdana,'sans-serif';font-size:12px;}p{text-align:justify;margin:10px 0 !important;}
                a{color:#1a4e94;text-decoration:none;font-weight:bold;}a:hover{color:#3c92fe;}table td{font-family: Verdana,'sans-serif' !important;font-size:12px;padding:3px;border-bottom:1px solid #dddddd;}
                </style></head><body>
                <div style=""width:750px; margin:5px auto;font-family: Verdana,'sans-serif';font-size:12px;line-height:20px; background-color:#f2f2f2;"">
                <img alt=""Sunrise Diamonds Ltd"" src=""https://sunrisediamonds.com.hk/Images/email-head.png"" width=""750px"" />
                <div style=""padding:10px;"">";
        }

        private static String EmailSignature()
        {
            return @"<p>Please do let us know if you have any questions. Email us on <a href=""mailto:support@sunrisediamonds.com.hk"">support@sunrisediamonds.com.hk</a></p>
                <p>Thanks and Regards,<br />Sunrise Diamond Team,<br />
                <a href=""https://sunrisediamonds.com.hk"">www.sunrisediamonds.com.hk</a></p>
                </div></div></body></html>";
        }
        public static bool EmailOfSuspendedUser(string ToAssEmail, string Name, string Username, string CompName)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + Name + ",</p>");
                loSb.Append(@"<p>" + Username + " [ " + CompName + " ] has tried to login on our website [www.sunrisediamonds.com.hk].<br />");
                loSb.Append(@" As per our company policy his/her account is suspended.<br /></p>");

                loSb.Append(EmailSignature());

                SendMail(ToAssEmail, "Unauthorised Login Attemt.", Convert.ToString(loSb), null, null, false, 0, "SuspendedUser");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, int? fiOrderId, bool AdminMail, int UserId, string MailFrom)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            Order objorder = new Order();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);
                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.Bcc.Add(fsCCAdd);

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                    loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);

                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                // Add Attechment (Customer Order)
                if (fiOrderId != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                    {
                        System.IO.MemoryStream ms = ExportToStreamEpPlus(Convert.ToInt32(fiOrderId), AdminMail, UserId);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), UserId);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                    if (fiOrderId != null)
                    {
                        if (MailFrom == "CUST")
                        {

                            //OrderMasDataContext loOrderMasDataContext = new OrderMasDataContext();
                            //loOrderMasDataContext.OrderMas_Update_CustMail_Status(Convert.ToInt32(fiOrderId), true);
                            objorder.OrderMas_Update_CustMail_Status(Convert.ToInt64(fiOrderId), true);
                        }
                        else if (MailFrom == "ADMIN")
                        {
                            //OrderMasDataContext loOrderMasDataContext = new OrderMasDataContext();
                            //loOrderMasDataContext.OrderMas_Update_AdminMail_Status(Convert.ToInt32(fiOrderId), true);
                            objorder.OrderMas_Update_AdminMail_Status(Convert.ToInt64(fiOrderId), true);
                        }
                    }
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {
                    //loMail.Attachments.Dispose();
                    //loMail.AlternateViews.Dispose();
                    //loMail.Dispose();
                    email.Abort();
                }
                ///////////////////////
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static System.IO.MemoryStream ExportToStreamEpPlus(int fiOrderId, bool AdminMail, int UserId)
        {
            Order objorder = new Order();
            DataTable loOrderDet = objorder.OrderDet_SelectAllByOrderId(fiOrderId, UserId);
            //OrderDetDataContext loDC = new OrderDetDataContext();
            //List<OrderDet_SelectAllByOrderIdResult> loOrderDet = loDC.OrderDet_SelectAllByOrderId(fiOrderId, UserId).ToList();

            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = false;
            gvData.ShowFooter = true;

            ColumnMas objcolumn = new ColumnMas();
            DataTable ColRes = objcolumn.ColumnConfDet_Select(UserId);

            //CustomColDataContext ColResDet = new CustomColDataContext();
            //List<ColumnConfDet_SelectResult> ColRes = ColResDet.ColumnConfDet_Select(Convert.ToInt16(Code)).ToList();

            if (ColRes.Rows.Count > 0)
            {
                //BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
                //gvData.Columns.Add(cSr);


                //priyanka on date [28-May-15] as per tj
                BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
                gvData.Columns.Add(sStoneStatus);

                BoundField sSupplLocation = new BoundField(); sSupplLocation.HeaderText = "Location"; sSupplLocation.DataField = "sSupplLocation";
                gvData.Columns.Add(sSupplLocation);

                for (int k = 0; k < ColRes.Rows.Count; k++)
                {
                    //HyperLink hl = (gvData.Controls[k].Controls[1] as HyperLink);

                    decimal dlTotalRapAmount = Convert.ToDecimal(loOrderDet.Compute("sum(dRapAmount)", "")); //loOrderDet.Sum(r => r.dRapAmount).Value;
                    switch (Convert.ToString(ColRes.Rows[k]["sColumnName"]))
                    {

                        case "bImage":
                            //BoundField cImage = new BoundField(); cImage.DataField = "bImage"; cImage.HeaderText = "Image";
                            //gvData.Columns.Add(cImage);
                            break;
                        case "bHDMovie":
                            //BoundField cHd = new BoundField(); cHd.DataField = "bHDMovie"; cHd.HeaderText = "HD";
                            //gvData.Columns.Add(cHd);
                            break;
                        case "sRefNo":
                            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
                            gvData.Columns.Add(cRefNo);
                            break;

                        case "sLab":
                            //BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
                            //gvData.Columns.Add(cLab);

                            HyperLinkField cLab = new HyperLinkField();
                            cLab.HeaderText = "Lab";
                            cLab.DataTextField = "sLab";
                            cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
                            cLab.DataNavigateUrlFormatString = "{0}";
                            gvData.Columns.Add(cLab);

                            break;

                        case "sShape":
                            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
                            gvData.Columns.Add(cShape);
                            break;
                        case "sPointer":
                            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
                            gvData.Columns.Add(cPointer);
                            break;
                        case "sCertiNo":
                            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
                            gvData.Columns.Add(cCertiNo);
                            break;
                        case "sColor":
                            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
                            cColor.FooterText = "Pcs";
                            gvData.Columns.Add(cColor);
                            break;

                        case "sClarity":
                            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
                            cClarity.FooterText = loOrderDet.Rows.Count.ToString();
                            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                            gvData.Columns.Add(cClarity);
                            break;
                        case "dCts":
                            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
                            cCarats.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dCts)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
                            cCarats.ItemStyle.CssClass = "twoDigit";
                            cCarats.FooterStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cCarats);
                            break;

                        case "dRepPrice":

                            BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
                            cRepPrice.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cRepPrice);
                            break;
                        case "dRapAmount":
                            BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
                            cRepAmount.ItemStyle.CssClass = "twoDigit";
                            cRepAmount.FooterStyle.CssClass = "twoDigit";

                            cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
                            gvData.Columns.Add(cRepAmount);
                            break;
                        case "dDisc":
                            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
                            if (dlTotalRapAmount == 0)
                                cDisc.FooterText = @"0.00";
                            else
                                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                                cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                            cDisc.ItemStyle.CssClass = "twoDigit-red";
                            cDisc.FooterStyle.CssClass = "twoDigit-red";
                            gvData.Columns.Add(cDisc);

                            break;
                        case "dNetPrice":
                            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
                            cNetPrice.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
                            cNetPrice.ItemStyle.CssClass = "twoDigit";
                            cNetPrice.FooterStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cNetPrice);
                            if (AdminMail == true)
                            {
                                BoundField sSupplDisc = new BoundField(); sSupplDisc.DataField = "sSupplDisc"; sSupplDisc.HeaderText = "Org Disc(%)";
                                if (dlTotalRapAmount == 0)
                                    sSupplDisc.FooterText = @"0.00";
                                else
                                    //sSupplDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                                    sSupplDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                                sSupplDisc.ItemStyle.CssClass = "twoDigit-red";
                                sSupplDisc.FooterStyle.CssClass = "twoDigit-red";
                                gvData.Columns.Add(sSupplDisc);

                                BoundField sSupplNetVal = new BoundField(); sSupplNetVal.DataField = "sSupplNetVal"; sSupplNetVal.HeaderText = "Org Value($)";
                                sSupplNetVal.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.sSupplDisc).Value.ToString("#,##0.00");
                                sSupplNetVal.ItemStyle.CssClass = "twoDigit";
                                sSupplNetVal.FooterStyle.CssClass = "twoDigit";
                                gvData.Columns.Add(sSupplNetVal);
                            }
                            break;
                        case "sCut":
                            BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
                            cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
                            gvData.Columns.Add(cCut);
                            break;
                        case "sPolish":
                            BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
                            gvData.Columns.Add(cPolish);
                            break;
                        case "sSymm":
                            BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
                            gvData.Columns.Add(cSymm);
                            break;
                        case "sFls":
                            BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
                            gvData.Columns.Add(cFls);
                            break;
                        case "dLength":
                            BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
                            cLength.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cLength);
                            break;
                        case "dWidth":

                            BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
                            cWidth.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cWidth);
                            break;

                        case "dDepth":

                            BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
                            cDepth.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cDepth);
                            break;
                        case "dDepthPer":
                            BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
                            cDepthPer.ItemStyle.CssClass = "oneDigit";
                            gvData.Columns.Add(cDepthPer);
                            break;
                        case "dTablePer":
                            BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
                            gvData.Columns.Add(cTablePer);
                            break;
                        case "sSymbol":
                            BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
                            gvData.Columns.Add(cSymbol);
                            break;
                        case "sInclusion":
                            BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
                            gvData.Columns.Add(cInclusion);
                            break;
                        case "sTableNatts":
                            BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
                            gvData.Columns.Add(cTableNatts);
                            break;
                        //--By Aniket [11-06-15]
                        case "sCrownInclusion":
                            BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
                            gvData.Columns.Add(cCrownInclusion);
                            break;
                        case "sCrownNatts":
                            BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
                            gvData.Columns.Add(cCrownNatts);
                            break;
                        case "sLuster":
                            BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
                            gvData.Columns.Add(cLuster);
                            break;
                        //--Over [11-06-15]
                        case "sShade":
                            BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
                            gvData.Columns.Add(cShade);
                            break;
                        case "dCrAng":
                            BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
                            cCrAng.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cCrAng);
                            break;
                        case "dCrHt":
                            BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
                            cCrHt.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cCrHt);
                            break;
                        case "dPavAng":
                            BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
                            cPavAng.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cPavAng);
                            break;
                        case "dPavHt":
                            BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
                            cPavHt.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cPavHt);
                            break;
                        case "sGirdleType":
                            BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
                            gvData.Columns.Add(cGirdle);
                            break;
                        case "sStatus":
                            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
                            gvData.Columns.Add(sstatus);
                            break;
                        case "sSideNatts":
                            BoundField cSideNatts = new BoundField(); cSideNatts.HeaderText = "Side Natts"; cSideNatts.DataField = "sSideNatts";
                            gvData.Columns.Add(cSideNatts);
                            break;
                        case "sCulet":
                            BoundField sCulet = new BoundField(); sCulet.HeaderText = "Culet"; sCulet.DataField = "sCulet";
                            gvData.Columns.Add(sCulet);
                            break;
                        case "dTableDepth":
                            //BoundField cTableDepth = new BoundField(); cTableDepth.HeaderText = "Table Depth"; cTableDepth.DataField = "dTableDepth";
                            //gvData.Columns.Add(cTableDepth);
                            break;
                        case "sHNA":
                            BoundField cHNA = new BoundField(); cHNA.HeaderText = "HNA"; cHNA.DataField = "sHNA";
                            gvData.Columns.Add(cHNA);
                            break;
                        case "sSideFtr":
                            BoundField sSideFtr = new BoundField(); sSideFtr.HeaderText = "Side Ftr"; sSideFtr.DataField = "sSideFtr";
                            gvData.Columns.Add(sSideFtr);
                            break;
                        case "sTableFtr":
                            BoundField sTableFtr = new BoundField(); sTableFtr.HeaderText = "Table Ftr"; sTableFtr.DataField = "sTableFtr";
                            gvData.Columns.Add(sTableFtr);
                            break;
                        // change by hitesh on [31-03-2016] as per [Doc No 201] 
                        case "sInscription":
                            BoundField SINSCRIPTION = new BoundField(); SINSCRIPTION.HeaderText = "Laser Insc"; SINSCRIPTION.DataField = "sInscription";
                            gvData.Columns.Add(SINSCRIPTION);
                            break;
                            // End by hitesh on [31-03-2016] as per [Doc No 201] 

                    }
                }
            }
            else
            {

                BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
                gvData.Columns.Add(cSr);
                //priyanka on date [28-May-15] as per tj
                BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
                gvData.Columns.Add(sStoneStatus);

                decimal dlTotalRapAmount = Convert.ToDecimal(loOrderDet.Compute("sum(dRapAmount)", "")); //loOrderDet.Sum(r => r.dRapAmount).Value;
                BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
                gvData.Columns.Add(cRefNo);

                //BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
                //gvData.Columns.Add(cLab);

                HyperLinkField cLab = new HyperLinkField();
                cLab.HeaderText = "Lab";
                cLab.DataTextField = "sLab";
                cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
                cLab.DataNavigateUrlFormatString = "{0}";
                gvData.Columns.Add(cLab);

                BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
                gvData.Columns.Add(cShape);

                BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
                gvData.Columns.Add(cPointer);

                BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
                gvData.Columns.Add(cCertiNo);

                BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
                cColor.FooterText = "Pcs";
                gvData.Columns.Add(cColor);

                BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
                cClarity.FooterText = loOrderDet.Rows.Count.ToString();
                cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                gvData.Columns.Add(cClarity);

                BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
                cCarats.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dCts)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
                cCarats.ItemStyle.CssClass = "twoDigit";
                cCarats.FooterStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cCarats);


                BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
                cRepPrice.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cRepPrice);

                BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
                cRepAmount.ItemStyle.CssClass = "twoDigit";
                cRepAmount.FooterStyle.CssClass = "twoDigit";

                cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
                gvData.Columns.Add(cRepAmount);

                BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
                if (dlTotalRapAmount == 0)
                    cDisc.FooterText = @"0.00";
                else
                    //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                    cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.ItemStyle.CssClass = "twoDigit-red";
                cDisc.FooterStyle.CssClass = "twoDigit-red";
                gvData.Columns.Add(cDisc);


                BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
                cNetPrice.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
                cNetPrice.ItemStyle.CssClass = "twoDigit";
                cNetPrice.FooterStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cNetPrice);

                /*priyanka on date [01-Sep-15]*/
                if (AdminMail == true)
                {
                    BoundField sSupplDisc = new BoundField(); sSupplDisc.DataField = "sSupplDisc"; sSupplDisc.HeaderText = "Org Disc(%)";
                    if (dlTotalRapAmount == 0)
                        sSupplDisc.FooterText = @"0.00";
                    else
                        //sSupplDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                        sSupplDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                    sSupplDisc.ItemStyle.CssClass = "twoDigit-red";
                    sSupplDisc.FooterStyle.CssClass = "twoDigit-red";
                    gvData.Columns.Add(sSupplDisc);

                    BoundField sSupplNetVal = new BoundField(); sSupplNetVal.DataField = "sSupplNetVal"; sSupplNetVal.HeaderText = "Org Value($)";
                    sSupplNetVal.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00");//loOrderDet.Sum(r => r.sSupplDisc).Value.ToString("#,##0.00");
                    sSupplNetVal.ItemStyle.CssClass = "twoDigit";
                    sSupplNetVal.FooterStyle.CssClass = "twoDigit";
                    gvData.Columns.Add(sSupplNetVal);
                }
                /***********************************/

                BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
                cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
                gvData.Columns.Add(cCut);

                BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
                gvData.Columns.Add(cPolish);

                BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
                gvData.Columns.Add(cSymm);

                BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
                gvData.Columns.Add(cFls);

                BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
                cLength.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cLength);

                BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
                cWidth.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cWidth);

                BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
                cDepth.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cDepth);

                BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
                cDepthPer.ItemStyle.CssClass = "oneDigit";
                gvData.Columns.Add(cDepthPer);

                BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
                gvData.Columns.Add(cTablePer);

                BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
                gvData.Columns.Add(cSymbol);

                BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
                gvData.Columns.Add(cInclusion);

                BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
                gvData.Columns.Add(cTableNatts);

                //--By Aniket [11-06-15]
                BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
                gvData.Columns.Add(cCrownInclusion);

                BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
                gvData.Columns.Add(cCrownNatts);

                BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
                gvData.Columns.Add(cLuster);
                //-- Over [11-06-15]

                BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
                gvData.Columns.Add(cShade);

                BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
                cCrAng.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cCrAng);

                BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
                cCrHt.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cCrHt);

                BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
                cPavAng.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cPavAng);

                BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
                cPavHt.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cPavHt);

                BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
                gvData.Columns.Add(cGirdle);

                BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
                gvData.Columns.Add(sstatus);

                //change by Hitesh on [31-03-2016] as per [Doc No 2016]
                BoundField SINSCRIPTION = new BoundField(); SINSCRIPTION.HeaderText = "Laser Insc"; SINSCRIPTION.DataField = "sInscription";
                gvData.Columns.Add(SINSCRIPTION);
                // End by Hitesh on [31-03-2016] as per [Doc No 2016]

            }

            gvData.DataSource = loOrderDet;
            gvData.DataBind();

            gvData.FooterStyle.Font.Bold = true;
            gvData.HeaderStyle.Font.Bold = true;

            GridViewEpExcelExport ep_ge;
            ep_ge = new GridViewEpExcelExport(gvData, "Order", "Order");

            ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
            ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
            ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;
            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            ep_ge.AddHeaderEvent += Ep_AddHeaderEventHandler;
            //End by Hitesh on [31-03-2016] as per [Doc No 201]

            MemoryStream ms = new MemoryStream();
            ep_ge.CreateExcel(ms, HttpContext.Current.Server.MapPath("~/Temp/Excel/"));

            System.IO.MemoryStream memn = new System.IO.MemoryStream();

            byte[] byteDatan = ms.ToArray();
            memn.Write(byteDatan, 0, byteDatan.Length);
            memn.Flush();
            memn.Position = 0;
            //memn.Close();
            return memn;
        }
        //private static System.IO.MemoryStream ExportToStreamEpPlus(int fiOrderId, bool AdminMail, int UserId)
        //{
        //    Order objorder = new Order();
        //    DataTable loOrderDet = objorder.OrderDet_SelectAllByOrderId(fiOrderId, UserId);
        //    //OrderDetDataContext loDC = new OrderDetDataContext();
        //    //List<OrderDet_SelectAllByOrderIdResult> loOrderDet = loDC.OrderDet_SelectAllByOrderId(fiOrderId, UserId).ToList();

        //    System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
        //    gvData.AutoGenerateColumns = false;
        //    gvData.ShowFooter = true;

        //    ColumnMas objcolumn = new ColumnMas();
        //    DataTable ColRes = objcolumn.ColumnConfDet_Select(UserId);

        //    //CustomColDataContext ColResDet = new CustomColDataContext();
        //    //List<ColumnConfDet_SelectResult> ColRes = ColResDet.ColumnConfDet_Select(Convert.ToInt16(Code)).ToList();

        //    if (ColRes.Rows.Count > 0)
        //    {
        //        //BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
        //        //gvData.Columns.Add(cSr);


        //        //priyanka on date [28-May-15] as per tj
        //        BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
        //        gvData.Columns.Add(sStoneStatus);

        //        BoundField sSupplLocation = new BoundField(); sSupplLocation.HeaderText = "Location"; sSupplLocation.DataField = "sSupplLocation";
        //        gvData.Columns.Add(sSupplLocation);

        //        for (int k = 0; k < ColRes.Rows.Count; k++)
        //        {
        //            //HyperLink hl = (gvData.Controls[k].Controls[1] as HyperLink);

        //            decimal dlTotalRapAmount = Convert.ToDecimal(loOrderDet.Compute("sum(dRapAmount)", "")); //loOrderDet.Sum(r => r.dRapAmount).Value;
        //            switch (Convert.ToString(ColRes.Rows[k]["sColumnName"]))
        //            {

        //                case "bImage":
        //                    //BoundField cImage = new BoundField(); cImage.DataField = "bImage"; cImage.HeaderText = "Image";
        //                    //gvData.Columns.Add(cImage);
        //                    break;
        //                case "bHDMovie":
        //                    //BoundField cHd = new BoundField(); cHd.DataField = "bHDMovie"; cHd.HeaderText = "HD";
        //                    //gvData.Columns.Add(cHd);
        //                    break;
        //                case "sRefNo":
        //                    BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
        //                    gvData.Columns.Add(cRefNo);
        //                    break;

        //                case "sLab":
        //                    //BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
        //                    //gvData.Columns.Add(cLab);

        //                    HyperLinkField cLab = new HyperLinkField();
        //                    cLab.HeaderText = "Lab";
        //                    cLab.DataTextField = "sLab";
        //                    cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
        //                    cLab.DataNavigateUrlFormatString = "{0}";
        //                    gvData.Columns.Add(cLab);

        //                    break;

        //                case "sShape":
        //                    BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
        //                    gvData.Columns.Add(cShape);
        //                    break;
        //                case "sPointer":
        //                    BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
        //                    gvData.Columns.Add(cPointer);
        //                    break;
        //                case "sCertiNo":
        //                    BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
        //                    gvData.Columns.Add(cCertiNo);
        //                    break;
        //                case "sColor":
        //                    BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
        //                    cColor.FooterText = "Pcs";
        //                    gvData.Columns.Add(cColor);
        //                    break;

        //                case "sClarity":
        //                    BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
        //                    cClarity.FooterText = loOrderDet.Rows.Count.ToString();
        //                    cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
        //                    gvData.Columns.Add(cClarity);
        //                    break;
        //                case "dCts":
        //                    BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
        //                    cCarats.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dCts)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
        //                    cCarats.ItemStyle.CssClass = "twoDigit";
        //                    cCarats.FooterStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cCarats);
        //                    break;

        //                case "dRepPrice":

        //                    BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
        //                    cRepPrice.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cRepPrice);
        //                    break;
        //                case "dRapAmount":
        //                    BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
        //                    cRepAmount.ItemStyle.CssClass = "twoDigit";
        //                    cRepAmount.FooterStyle.CssClass = "twoDigit";

        //                    cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
        //                    gvData.Columns.Add(cRepAmount);
        //                    break;
        //                case "dDisc":
        //                    BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
        //                    if (dlTotalRapAmount == 0)
        //                        cDisc.FooterText = @"0.00";
        //                    else
        //                        //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
        //                        cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
        //                    cDisc.ItemStyle.CssClass = "twoDigit-red";
        //                    cDisc.FooterStyle.CssClass = "twoDigit-red";
        //                    gvData.Columns.Add(cDisc);

        //                    break;
        //                case "dNetPrice":
        //                    BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
        //                    cNetPrice.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
        //                    cNetPrice.ItemStyle.CssClass = "twoDigit";
        //                    cNetPrice.FooterStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cNetPrice);
        //                    if (AdminMail == true)
        //                    {
        //                        BoundField sSupplDisc = new BoundField(); sSupplDisc.DataField = "sSupplDisc"; sSupplDisc.HeaderText = "Org Disc(%)";
        //                        if (dlTotalRapAmount == 0)
        //                            sSupplDisc.FooterText = @"0.00";
        //                        else
        //                            //sSupplDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
        //                            sSupplDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
        //                        sSupplDisc.ItemStyle.CssClass = "twoDigit-red";
        //                        sSupplDisc.FooterStyle.CssClass = "twoDigit-red";
        //                        gvData.Columns.Add(sSupplDisc);

        //                        BoundField sSupplNetVal = new BoundField(); sSupplNetVal.DataField = "sSupplNetVal"; sSupplNetVal.HeaderText = "Org Value($)";
        //                        sSupplNetVal.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.sSupplDisc).Value.ToString("#,##0.00");
        //                        sSupplNetVal.ItemStyle.CssClass = "twoDigit";
        //                        sSupplNetVal.FooterStyle.CssClass = "twoDigit";
        //                        gvData.Columns.Add(sSupplNetVal);
        //                    }
        //                    break;
        //                case "sCut":
        //                    BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
        //                    cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
        //                    gvData.Columns.Add(cCut);
        //                    break;
        //                case "sPolish":
        //                    BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
        //                    gvData.Columns.Add(cPolish);
        //                    break;
        //                case "sSymm":
        //                    BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
        //                    gvData.Columns.Add(cSymm);
        //                    break;
        //                case "sFls":
        //                    BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
        //                    gvData.Columns.Add(cFls);
        //                    break;
        //                case "dLength":
        //                    BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
        //                    cLength.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cLength);
        //                    break;
        //                case "dWidth":

        //                    BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
        //                    cWidth.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cWidth);
        //                    break;

        //                case "dDepth":

        //                    BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
        //                    cDepth.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cDepth);
        //                    break;
        //                case "dDepthPer":
        //                    BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
        //                    cDepthPer.ItemStyle.CssClass = "oneDigit";
        //                    gvData.Columns.Add(cDepthPer);
        //                    break;
        //                case "dTablePer":
        //                    BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
        //                    gvData.Columns.Add(cTablePer);
        //                    break;
        //                case "sSymbol":
        //                    BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
        //                    gvData.Columns.Add(cSymbol);
        //                    break;
        //                case "sInclusion":
        //                    BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
        //                    gvData.Columns.Add(cInclusion);
        //                    break;
        //                case "sTableNatts":
        //                    BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
        //                    gvData.Columns.Add(cTableNatts);
        //                    break;
        //                //--By Aniket [11-06-15]
        //                case "sCrownInclusion":
        //                    BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
        //                    gvData.Columns.Add(cCrownInclusion);
        //                    break;
        //                case "sCrownNatts":
        //                    BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
        //                    gvData.Columns.Add(cCrownNatts);
        //                    break;
        //                case "sLuster":
        //                    BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
        //                    gvData.Columns.Add(cLuster);
        //                    break;
        //                //--Over [11-06-15]
        //                case "sShade":
        //                    BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
        //                    gvData.Columns.Add(cShade);
        //                    break;
        //                case "dCrAng":
        //                    BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
        //                    cCrAng.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cCrAng);
        //                    break;
        //                case "dCrHt":
        //                    BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
        //                    cCrHt.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cCrHt);
        //                    break;
        //                case "dPavAng":
        //                    BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
        //                    cPavAng.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cPavAng);
        //                    break;
        //                case "dPavHt":
        //                    BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
        //                    cPavHt.ItemStyle.CssClass = "twoDigit";
        //                    gvData.Columns.Add(cPavHt);
        //                    break;
        //                case "sGirdleType":
        //                    BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
        //                    gvData.Columns.Add(cGirdle);
        //                    break;
        //                case "sStatus":
        //                    BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
        //                    gvData.Columns.Add(sstatus);
        //                    break;
        //                case "sSideNatts":
        //                    BoundField cSideNatts = new BoundField(); cSideNatts.HeaderText = "Side Natts"; cSideNatts.DataField = "sSideNatts";
        //                    gvData.Columns.Add(cSideNatts);
        //                    break;
        //                case "sCulet":
        //                    BoundField sCulet = new BoundField(); sCulet.HeaderText = "Culet"; sCulet.DataField = "sCulet";
        //                    gvData.Columns.Add(sCulet);
        //                    break;
        //                case "dTableDepth":
        //                    //BoundField cTableDepth = new BoundField(); cTableDepth.HeaderText = "Table Depth"; cTableDepth.DataField = "dTableDepth";
        //                    //gvData.Columns.Add(cTableDepth);
        //                    break;
        //                case "sHNA":
        //                    BoundField cHNA = new BoundField(); cHNA.HeaderText = "HNA"; cHNA.DataField = "sHNA";
        //                    gvData.Columns.Add(cHNA);
        //                    break;
        //                case "sSideFtr":
        //                    BoundField sSideFtr = new BoundField(); sSideFtr.HeaderText = "Side Ftr"; sSideFtr.DataField = "sSideFtr";
        //                    gvData.Columns.Add(sSideFtr);
        //                    break;
        //                case "sTableFtr":
        //                    BoundField sTableFtr = new BoundField(); sTableFtr.HeaderText = "Table Ftr"; sTableFtr.DataField = "sTableFtr";
        //                    gvData.Columns.Add(sTableFtr);
        //                    break;
        //                // change by hitesh on [31-03-2016] as per [Doc No 201] 
        //                case "sInscription":
        //                    BoundField SINSCRIPTION = new BoundField(); SINSCRIPTION.HeaderText = "Laser Insc"; SINSCRIPTION.DataField = "sInscription";
        //                    gvData.Columns.Add(SINSCRIPTION);
        //                    break;
        //                    // End by hitesh on [31-03-2016] as per [Doc No 201] 

        //            }
        //        }
        //    }
        //    else
        //    {

        //        BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
        //        gvData.Columns.Add(cSr);
        //        //priyanka on date [28-May-15] as per tj
        //        BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
        //        gvData.Columns.Add(sStoneStatus);

        //        decimal dlTotalRapAmount = Convert.ToDecimal(loOrderDet.Compute("sum(dRapAmount)", "")); //loOrderDet.Sum(r => r.dRapAmount).Value;
        //        BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
        //        gvData.Columns.Add(cRefNo);

        //        //BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
        //        //gvData.Columns.Add(cLab);

        //        HyperLinkField cLab = new HyperLinkField();
        //        cLab.HeaderText = "Lab";
        //        cLab.DataTextField = "sLab";
        //        cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
        //        cLab.DataNavigateUrlFormatString = "{0}";
        //        gvData.Columns.Add(cLab);

        //        BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
        //        gvData.Columns.Add(cShape);

        //        BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
        //        gvData.Columns.Add(cPointer);

        //        BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
        //        gvData.Columns.Add(cCertiNo);

        //        BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
        //        cColor.FooterText = "Pcs";
        //        gvData.Columns.Add(cColor);

        //        BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
        //        cClarity.FooterText = loOrderDet.Rows.Count.ToString();
        //        cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
        //        gvData.Columns.Add(cClarity);

        //        BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
        //        cCarats.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dCts)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
        //        cCarats.ItemStyle.CssClass = "twoDigit";
        //        cCarats.FooterStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cCarats);


        //        BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
        //        cRepPrice.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cRepPrice);

        //        BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
        //        cRepAmount.ItemStyle.CssClass = "twoDigit";
        //        cRepAmount.FooterStyle.CssClass = "twoDigit";

        //        cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
        //        gvData.Columns.Add(cRepAmount);

        //        BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
        //        if (dlTotalRapAmount == 0)
        //            cDisc.FooterText = @"0.00";
        //        else
        //            //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
        //            cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
        //        cDisc.ItemStyle.CssClass = "twoDigit-red";
        //        cDisc.FooterStyle.CssClass = "twoDigit-red";
        //        gvData.Columns.Add(cDisc);


        //        BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
        //        cNetPrice.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
        //        cNetPrice.ItemStyle.CssClass = "twoDigit";
        //        cNetPrice.FooterStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cNetPrice);

        //        /*priyanka on date [01-Sep-15]*/
        //        if (AdminMail == true)
        //        {
        //            BoundField sSupplDisc = new BoundField(); sSupplDisc.DataField = "sSupplDisc"; sSupplDisc.HeaderText = "Org Disc(%)";
        //            if (dlTotalRapAmount == 0)
        //                sSupplDisc.FooterText = @"0.00";
        //            else
        //                //sSupplDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
        //                sSupplDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
        //            sSupplDisc.ItemStyle.CssClass = "twoDigit-red";
        //            sSupplDisc.FooterStyle.CssClass = "twoDigit-red";
        //            gvData.Columns.Add(sSupplDisc);

        //            BoundField sSupplNetVal = new BoundField(); sSupplNetVal.DataField = "sSupplNetVal"; sSupplNetVal.HeaderText = "Org Value($)";
        //            sSupplNetVal.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00");//loOrderDet.Sum(r => r.sSupplDisc).Value.ToString("#,##0.00");
        //            sSupplNetVal.ItemStyle.CssClass = "twoDigit";
        //            sSupplNetVal.FooterStyle.CssClass = "twoDigit";
        //            gvData.Columns.Add(sSupplNetVal);
        //        }
        //        /***********************************/

        //        BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
        //        cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
        //        gvData.Columns.Add(cCut);

        //        BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
        //        gvData.Columns.Add(cPolish);

        //        BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
        //        gvData.Columns.Add(cSymm);

        //        BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
        //        gvData.Columns.Add(cFls);

        //        BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
        //        cLength.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cLength);

        //        BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
        //        cWidth.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cWidth);

        //        BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
        //        cDepth.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cDepth);

        //        BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
        //        cDepthPer.ItemStyle.CssClass = "oneDigit";
        //        gvData.Columns.Add(cDepthPer);

        //        BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
        //        gvData.Columns.Add(cTablePer);

        //        BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
        //        gvData.Columns.Add(cSymbol);

        //        BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
        //        gvData.Columns.Add(cInclusion);

        //        BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
        //        gvData.Columns.Add(cTableNatts);

        //        //--By Aniket [11-06-15]
        //        BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
        //        gvData.Columns.Add(cCrownInclusion);

        //        BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
        //        gvData.Columns.Add(cCrownNatts);

        //        BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
        //        gvData.Columns.Add(cLuster);
        //        //-- Over [11-06-15]

        //        BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
        //        gvData.Columns.Add(cShade);

        //        BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
        //        cCrAng.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cCrAng);

        //        BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
        //        cCrHt.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cCrHt);

        //        BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
        //        cPavAng.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cPavAng);

        //        BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
        //        cPavHt.ItemStyle.CssClass = "twoDigit";
        //        gvData.Columns.Add(cPavHt);

        //        BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
        //        gvData.Columns.Add(cGirdle);

        //        BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
        //        gvData.Columns.Add(sstatus);

        //        //change by Hitesh on [31-03-2016] as per [Doc No 2016]
        //        BoundField SINSCRIPTION = new BoundField(); SINSCRIPTION.HeaderText = "Laser Insc"; SINSCRIPTION.DataField = "sInscription";
        //        gvData.Columns.Add(SINSCRIPTION);
        //        // End by Hitesh on [31-03-2016] as per [Doc No 2016]

        //    }

        //    gvData.DataSource = loOrderDet;
        //    gvData.DataBind();

        //    gvData.FooterStyle.Font.Bold = true;
        //    gvData.HeaderStyle.Font.Bold = true;

        //    GridViewEpExcelExport ep_ge;
        //    ep_ge = new GridViewEpExcelExport(gvData, "Order", "Order");

        //    ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
        //    ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
        //    ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;
        //    //change by Hitesh on [31-03-2016] as per [Doc No 201]
        //    ep_ge.AddHeaderEvent += Ep_AddHeaderEventHandler;
        //    //End by Hitesh on [31-03-2016] as per [Doc No 201]

        //    MemoryStream ms = new MemoryStream();
        //    ep_ge.CreateExcel(ms, HttpContext.Current.Server.MapPath("~/Temp/Excel/"));

        //    System.IO.MemoryStream memn = new System.IO.MemoryStream();

        //    byte[] byteDatan = ms.ToArray();
        //    memn.Write(byteDatan, 0, byteDatan.Length);
        //    memn.Flush();
        //    memn.Position = 0;
        //    //memn.Close();
        //    return memn;
        //}
        private static UInt32 DiscNormalStyleindex;
        private static UInt32 CutNormalStyleindex;
        //priyanka on date [28-05-15]
        private static UInt32 STatusBkgrndIndx;
        ///
        private static UInt32 InscStyleindex;
        private static void Ep_FillingWorksheetEventHandler(object sender, ref EpExcelExport.FillingWorksheetEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            EpExcelExport.ExcelFormat format = new EpExcelExport.ExcelFormat();

            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            DiscNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex = ee.AddStyle(format);
            //priyanka on date [28-05-15]
            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Yellow.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            STatusBkgrndIndx = ee.AddStyle(format);
            /////////

            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            format = new EpExcelExportLib.EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Blue.ToArgb());
            format.isbold = true;
            InscStyleindex = ee.AddStyle(format);
            //End by Hitesh on [31-03-2016] as per [Doc No 201]
        }

        private static void Ep_BeforeCreateColumnEventHandler(object sender, ref EpExcelExport.ExcelHeader e)
        {

            //if (e.ColName == "iSr")
            if (e.Caption == "Sr.")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 4;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
                e.NumFormat = "#,##0";
            }
            if (e.Caption == "Order Status")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Location")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10;
            }
            //if (e.ColName == "sRefNo")
            if (e.Caption == "Ref. No.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 12;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
            }
            //if (e.ColName == "sLab")
            if (e.Caption == "Lab")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 4;
            }
            //if (e.ColName == "sShape")
            if (e.Caption == "Shape")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
            }
            //if (e.ColName == "sPointer")
            if (e.Caption == "Pointer")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8.14;
            }
            //if (e.ColName == "sCertiNo")
            if (e.Caption == "Certi No")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
            }
            //if (e.ColName == "sColor")
            if (e.Caption == "Color")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6;
            }
            //if (e.ColName == "sClarity")
            if (e.Caption == "Clarity")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6;
            }
            //if (e.ColName == "dCts")
            if (e.Caption == "Cts")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dRepPrice")
            if (e.Caption == "Rap Price($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10.86;
                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "dRepAmount")
            if (e.Caption == "Rap Amount($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15.71;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "dDisc")
            if (e.Caption == "Disc(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Net Amt($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";

            }
            if (e.Caption == "Org Disc(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "if(" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Org Disc(%)", EpExcelExport.TotalsRowFunctionValues.Sum) + "=0,0.00,(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Org Value($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.SumIf, "Org Disc(%)", "<100") + " ))*-100)";
            }

            //if (e.ColName == "dNetPrice")
            if (e.Caption == "Net Amt($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
            if (e.Caption == "Org Value($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
            //if (e.ColName == "sCut")
            if (e.Caption == "Cut")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5;
            }
            //if (e.ColName == "sPolish")
            if (e.Caption == "Polish")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
            }
            //if (e.ColName == "sSymm")
            if (e.Caption == "Symm")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
            }
            //if (e.ColName == "sFls")
            if (e.Caption == "Fls")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
            }
            if (e.Caption == "Length")
            //if (e.ColName == "dLength")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.70;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dWidth")
            if (e.Caption == "Width")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.14;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dDepth")
            if (e.Caption == "Depth")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.14;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dDepthPer")
            if (e.Caption == "Depth(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8.43;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dTablePer")
            if (e.Caption == "Table(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8.43;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "sSymbol")
            if (e.Caption == "Key To Symbol")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 35;
            }
            //if (e.ColName == "sInclusion")
            if (e.Caption == "Table Incl.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //if (e.ColName == "sTableNatts")
            if (e.Caption == "Table Natts")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //--By Aniket [11-06-15]
            //if (e.ColName == "sCrownInclusion")
            if (e.Caption == "Crown Incl.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //if (e.ColName == "sCrownNatts")
            if (e.Caption == "Crown Natts")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }

            //if (e.ColName == "sLuster")
            if (e.Caption == "Luster/Milky")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //-- Over [11-06-15]
            //if (e.ColName == "sShade")
            if (e.Caption == "Shade")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //if (e.ColName == "dCrAng")
            if (e.Caption == "Cr Ang")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dCrHt")
            if (e.Caption == "Cr Ht")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dPavAng")
            if (e.Caption == "Pav Ang")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dPavHt")
            if (e.Caption == "Pav Ht")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "sGirdle")
            if (e.Caption == "Girdle Type")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //if (e.ColName == "sStatus")
            if (e.Caption == "Status")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            if (e.Caption == "DNA")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "HD Movie")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "Image")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "Laser Insc")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //End by Hitesh on [31-03-2016] as per [Doc No 201]
        }

        private static void Ep_AfterCreateCellEventHandler(object sender, ref EpExcelExport.ExcelCellFormat e)
        {
            if (e.tableArea == EpExcelExport.TableArea.Header)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                //e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.isbold = true;
            }
            else if (e.tableArea == EpExcelExport.TableArea.Detail)
            {

                //if (e.ColumnName == "dDisc")
                if (e.ColumnName == "Disc(%)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
                else if (e.ColumnName == "Lab")
                {
                    e.Formula = @"=HYPERLINK(""" + e.url + @""",""" + e.Text + @""")";
                }
                else if (e.ColumnName == "Org Disc(%)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
                //else if (e.ColumnName == "sCut")
                else if (e.ColumnName == "Cut")
                {
                    if (e.Text == "3EX")
                        e.StyleInd = CutNormalStyleindex;
                }//priyanka on date [28-05-15]
                else if (e.ColumnName == "Order Status")
                {
                    if (e.Text == "NOT AVAILABLE" || e.Text == "CHECKING AVAIBILITY")
                        e.StyleInd = STatusBkgrndIndx;
                }//********
                //change by Hitesh on [31-03-2016] as per [Doc No 201]
                else if (e.ColumnName == "Net Amt($)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
                else if (e.ColumnName == "Laser Insc")
                {
                    e.StyleInd = InscStyleindex;
                }
                //End by Hitesh on [31-03-2016] as per [Doc No 201]

            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
                //e.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                if (e.ColumnName == "Disc(%)")
                {
                    //e.StyleInd = DiscNormalStyleindex;
                }
            }

        }

        private static void Ep_AddHeaderEventHandler(object sender, ref EpExcelExportLib.EpExcelExport.AddHeaderEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            ee.AddNewRow("A1");
        }

        private static System.IO.MemoryStream ExportToStream(int fiOrderId, int UserId)
        {
            //OrderDetDataContext loDC = new OrderDetDataContext();
            //List<OrderDet_SelectAllByOrderIdResult> loOrderDet = loDC.OrderDet_SelectAllByOrderId(fiOrderId, UserId).ToList();
            Order objorder = new Order();
            DataTable loOrderDet = objorder.OrderDet_SelectAllByOrderId(fiOrderId, UserId);

            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = false;
            gvData.ShowFooter = true;

            //CustomColDataContext ColResDet = new CustomColDataContext();
            //List<ColumnConfDet_SelectResult> ColRes = ColResDet.ColumnConfDet_Select(Convert.ToInt16(Code)).ToList();

            ColumnMas objcolumn = new ColumnMas();
            DataTable ColRes = objcolumn.ColumnConfDet_Select(UserId);

            if (ColRes.Rows.Count > 0)
            {
                BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
                gvData.Columns.Add(cSr);

                //priyanka on date [28-May-15] as per tj
                BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
                gvData.Columns.Add(sStoneStatus);
                for (int k = 0; k < ColRes.Rows.Count; k++)
                {
                    //HyperLink hl = (gvData.Controls[k].Controls[1] as HyperLink);

                    decimal dlTotalRapAmount = Convert.ToDecimal(loOrderDet.Compute("sum(dRapAmount)", "")); //loOrderDet.Sum(r => r.dRapAmount).Value;
                    switch (Convert.ToString(ColRes.Rows[k]["sColumnName"]))
                    {

                        case "bImage":
                            //BoundField cImage = new BoundField(); cImage.DataField = "bImage"; cImage.HeaderText = "Image";
                            //gvData.Columns.Add(cImage);
                            break;
                        case "bHDMovie":
                            //BoundField cHd = new BoundField(); cHd.DataField = "bHDMovie"; cHd.HeaderText = "HD";
                            //gvData.Columns.Add(cHd);
                            break;
                        case "sRefNo":
                            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
                            gvData.Columns.Add(cRefNo);
                            break;

                        case "sLab":
                            //BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
                            //gvData.Columns.Add(cLab);

                            HyperLinkField cLab = new HyperLinkField();
                            cLab.HeaderText = "Lab";
                            cLab.DataTextField = "sLab";
                            cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
                            cLab.DataNavigateUrlFormatString = "{0}";
                            gvData.Columns.Add(cLab);

                            break;

                        case "sShape":
                            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
                            gvData.Columns.Add(cShape);
                            break;
                        case "sPointer":
                            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
                            gvData.Columns.Add(cPointer);
                            break;
                        case "sCertiNo":
                            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
                            gvData.Columns.Add(cCertiNo);
                            break;
                        case "sColor":
                            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
                            cColor.FooterText = "Pcs";
                            gvData.Columns.Add(cColor);
                            break;

                        case "sClarity":
                            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
                            cClarity.FooterText = loOrderDet.Rows.Count.ToString();
                            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                            gvData.Columns.Add(cClarity);
                            break;
                        case "dCts":
                            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
                            cCarats.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dCts)", "")).ToString("#,##0.00");//loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
                            cCarats.ItemStyle.CssClass = "twoDigit";
                            cCarats.FooterStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cCarats);
                            break;

                        case "dRepPrice":

                            BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
                            cRepPrice.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cRepPrice);
                            break;
                        case "dRapAmount":
                            BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
                            cRepAmount.ItemStyle.CssClass = "twoDigit";
                            cRepAmount.FooterStyle.CssClass = "twoDigit";

                            cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
                            gvData.Columns.Add(cRepAmount);
                            break;
                        case "dDisc":
                            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
                            if (dlTotalRapAmount == 0)
                                cDisc.FooterText = @"0.00";
                            else
                                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                                cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                            cDisc.ItemStyle.CssClass = "twoDigit-red";
                            cDisc.FooterStyle.CssClass = "twoDigit-red";
                            gvData.Columns.Add(cDisc);

                            break;
                        case "dNetPrice":
                            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
                            cNetPrice.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
                            cNetPrice.ItemStyle.CssClass = "twoDigit";
                            cNetPrice.FooterStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cNetPrice);
                            break;
                        case "sCut":
                            BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
                            cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
                            gvData.Columns.Add(cCut);
                            break;
                        case "sPolish":
                            BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
                            gvData.Columns.Add(cPolish);
                            break;
                        case "sSymm":
                            BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
                            gvData.Columns.Add(cSymm);
                            break;
                        case "sFls":
                            BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
                            gvData.Columns.Add(cFls);
                            break;
                        case "dLength":
                            BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
                            cLength.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cLength);
                            break;
                        case "dWidth":

                            BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
                            cWidth.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cWidth);
                            break;

                        case "dDepth":

                            BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
                            cDepth.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cDepth);
                            break;
                        case "dDepthPer":
                            BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
                            cDepthPer.ItemStyle.CssClass = "oneDigit";
                            gvData.Columns.Add(cDepthPer);
                            break;
                        case "dTablePer":
                            BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
                            gvData.Columns.Add(cTablePer);
                            break;
                        case "sSymbol":
                            BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
                            gvData.Columns.Add(cSymbol);
                            break;
                        case "sInclusion":
                            BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
                            gvData.Columns.Add(cInclusion);
                            break;
                        case "sTableNatts":
                            BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
                            gvData.Columns.Add(cTableNatts);
                            break;
                        //--By Aniket [11-06-15]
                        case "sCrownInclusion":
                            BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
                            gvData.Columns.Add(cCrownInclusion);
                            break;
                        case "sCrownNatts":
                            BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
                            gvData.Columns.Add(cCrownNatts);
                            break;
                        case "sLuster":
                            BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
                            gvData.Columns.Add(cLuster);
                            break;
                        //--Over [11-06-15]
                        case "sShade":
                            BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
                            gvData.Columns.Add(cShade);
                            break;
                        case "dCrAng":
                            BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
                            cCrAng.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cCrAng);
                            break;
                        case "dCrHt":
                            BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
                            cCrHt.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cCrHt);
                            break;
                        case "dPavAng":
                            BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
                            cPavAng.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cPavAng);
                            break;
                        case "dPavHt":
                            BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
                            cPavHt.ItemStyle.CssClass = "twoDigit";
                            gvData.Columns.Add(cPavHt);
                            break;
                        case "sGirdleType":
                            BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
                            gvData.Columns.Add(cGirdle);
                            break;
                        case "sStatus":
                            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
                            gvData.Columns.Add(sstatus);
                            break;
                        case "sSideNatts":
                            BoundField cSideNatts = new BoundField(); cSideNatts.HeaderText = "Side Natts"; cSideNatts.DataField = "sSideNatts";
                            gvData.Columns.Add(cSideNatts);
                            break;
                        case "sCulet":
                            BoundField sCulet = new BoundField(); sCulet.HeaderText = "Culet"; sCulet.DataField = "sCulet";
                            gvData.Columns.Add(sCulet);
                            break;
                        case "dTableDepth":
                            //BoundField cTableDepth = new BoundField(); cTableDepth.HeaderText = "Table Depth"; cTableDepth.DataField = "dTableDepth";
                            //gvData.Columns.Add(cTableDepth);
                            break;
                        case "sHNA":
                            BoundField cHNA = new BoundField(); cHNA.HeaderText = "HNA"; cHNA.DataField = "sHNA";
                            gvData.Columns.Add(cHNA);
                            break;
                        case "sSideFtr":
                            BoundField sSideFtr = new BoundField(); sSideFtr.HeaderText = "Side Ftr"; sSideFtr.DataField = "sSideFtr";
                            gvData.Columns.Add(sSideFtr);
                            break;
                        case "sTableFtr":
                            BoundField sTableFtr = new BoundField(); sTableFtr.HeaderText = "Table Ftr"; sTableFtr.DataField = "sTableFtr";
                            gvData.Columns.Add(sTableFtr);
                            break;

                    }




                }
            }
            else
            {

                BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
                gvData.Columns.Add(cSr);
                //priyanka on date [28-May-15] as per tj
                BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
                gvData.Columns.Add(sStoneStatus);
                decimal dlTotalRapAmount = Convert.ToDecimal(loOrderDet.Compute("sum(dRapAmount)", "")); //loOrderDet.Sum(r => r.dRapAmount).Value;
                BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
                gvData.Columns.Add(cRefNo);

                //BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
                //gvData.Columns.Add(cLab);

                HyperLinkField cLab = new HyperLinkField();
                cLab.HeaderText = "Lab";
                cLab.DataTextField = "sLab";
                cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
                cLab.DataNavigateUrlFormatString = "{0}";
                gvData.Columns.Add(cLab);

                BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
                gvData.Columns.Add(cShape);

                BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
                gvData.Columns.Add(cPointer);

                BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
                gvData.Columns.Add(cCertiNo);

                BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
                cColor.FooterText = "Pcs";
                gvData.Columns.Add(cColor);

                BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
                cClarity.FooterText = loOrderDet.Rows.Count.ToString();
                cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                gvData.Columns.Add(cClarity);

                BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
                cCarats.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dCts)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
                cCarats.ItemStyle.CssClass = "twoDigit";
                cCarats.FooterStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cCarats);


                BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
                cRepPrice.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cRepPrice);

                BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
                cRepAmount.ItemStyle.CssClass = "twoDigit";
                cRepAmount.FooterStyle.CssClass = "twoDigit";

                cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
                gvData.Columns.Add(cRepAmount);

                BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
                if (dlTotalRapAmount == 0)
                    cDisc.FooterText = @"0.00";
                else
                    //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                    cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.ItemStyle.CssClass = "twoDigit-red";
                cDisc.FooterStyle.CssClass = "twoDigit-red";
                gvData.Columns.Add(cDisc);


                BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
                cNetPrice.FooterText = Convert.ToDecimal(loOrderDet.Compute("sum(dNetPrice)", "")).ToString("#,##0.00"); //loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
                cNetPrice.ItemStyle.CssClass = "twoDigit";
                cNetPrice.FooterStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cNetPrice);

                BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
                cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
                gvData.Columns.Add(cCut);

                BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
                gvData.Columns.Add(cPolish);

                BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
                gvData.Columns.Add(cSymm);

                BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
                gvData.Columns.Add(cFls);

                BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
                cLength.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cLength);

                BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
                cWidth.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cWidth);

                BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
                cDepth.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cDepth);

                BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
                cDepthPer.ItemStyle.CssClass = "oneDigit";
                gvData.Columns.Add(cDepthPer);

                BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
                gvData.Columns.Add(cTablePer);

                BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
                gvData.Columns.Add(cSymbol);

                BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
                gvData.Columns.Add(cInclusion);

                BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
                gvData.Columns.Add(cTableNatts);

                //--By Aniket [11-06-15]
                BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
                gvData.Columns.Add(cCrownInclusion);

                BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
                gvData.Columns.Add(cCrownNatts);

                BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
                gvData.Columns.Add(cLuster);
                //-- Over [11-06-15]

                BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
                gvData.Columns.Add(cShade);

                BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
                cCrAng.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cCrAng);

                BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
                cCrHt.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cCrHt);

                BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
                cPavAng.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cPavAng);

                BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
                cPavHt.ItemStyle.CssClass = "twoDigit";
                gvData.Columns.Add(cPavHt);

                BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
                gvData.Columns.Add(cGirdle);

                BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
                gvData.Columns.Add(sstatus);

                //BoundField cSideNatts = new BoundField(); cSideNatts.HeaderText = "Side Natts"; cSideNatts.DataField = "sSideNatts";
                //gvData.Columns.Add(cSideNatts);

                //BoundField sCulet = new BoundField(); sCulet.HeaderText = "Culet"; sCulet.DataField = "sCulet";
                //gvData.Columns.Add(sCulet);

                //BoundField cTableDepth = new BoundField(); cTableDepth.HeaderText = "Table Depth"; cTableDepth.DataField = "dTableDepth";
                //gvData.Columns.Add(cTableDepth);

                //BoundField cHNA = new BoundField(); cHNA.HeaderText = "HNA"; cHNA.DataField = "sHNA";
                //gvData.Columns.Add(cHNA);

                //BoundField sSideFtr = new BoundField(); sSideFtr.HeaderText = "Side Ftr"; sSideFtr.DataField = "sSideFtr";
                //gvData.Columns.Add(sSideFtr);

                //BoundField sTableFtr = new BoundField(); sTableFtr.HeaderText = "Table Ftr"; sTableFtr.DataField = "sTableFtr";
                //gvData.Columns.Add(sTableFtr);


            }

            //BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
            //gvData.Columns.Add(cSr);
            //BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
            //gvData.Columns.Add(cRefNo);
            //BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            //gvData.Columns.Add(cCertiNo);
            //BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
            //gvData.Columns.Add(cShape);
            //BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointers";
            //gvData.Columns.Add(cPointer);
            //BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
            //cColor.FooterText = "Pcs";
            //gvData.Columns.Add(cColor);

            //BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
            //cClarity.FooterText = loOrderDet.Count.ToString();
            //cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvData.Columns.Add(cClarity);

            //BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Carats";
            //cCarats.FooterText = loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
            //cCarats.ItemStyle.CssClass = "twoDigit";
            //cCarats.FooterStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cCarats);

            //BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
            //cRepPrice.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cRepPrice);
            //BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
            //cRepAmount.ItemStyle.CssClass = "twoDigit";
            //cRepAmount.FooterStyle.CssClass = "twoDigit";
            //decimal dlTotalRapAmount = loOrderDet.Sum(r => r.dRapAmount).Value;
            //cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
            //gvData.Columns.Add(cRepAmount);

            //BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
            //if (dlTotalRapAmount == 0)
            //    cDisc.FooterText = @"0.00";
            //else
            //    cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
            //cDisc.ItemStyle.CssClass = "twoDigit-red";
            //cDisc.FooterStyle.CssClass = "twoDigit-red";
            //gvData.Columns.Add(cDisc);

            //BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
            //cNetPrice.FooterText = loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
            //cNetPrice.ItemStyle.CssClass = "twoDigit";
            //cNetPrice.FooterStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cNetPrice);

            //BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
            //cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
            //gvData.Columns.Add(cCut);

            //BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
            //gvData.Columns.Add(cPolish);
            //BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
            //gvData.Columns.Add(cSymm);
            //BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
            //gvData.Columns.Add(cFls);
            //BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
            //cLength.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cLength);
            //BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
            //cWidth.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cWidth);
            //BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
            //cDepth.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cDepth);
            //BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
            //cDepthPer.ItemStyle.CssClass = "oneDigit";
            //gvData.Columns.Add(cDepthPer);
            //BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
            //gvData.Columns.Add(cTablePer);
            //BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
            //gvData.Columns.Add(sstatus);


            gvData.DataSource = loOrderDet;
            gvData.DataBind();

            gvData.FooterStyle.Font.Bold = true;
            gvData.HeaderStyle.Font.Bold = true;

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

            System.Web.UI.Page objPage = new System.Web.UI.Page();
            objPage.Controls.Add(gvData);
            System.Web.UI.HtmlControls.HtmlForm hForm = new System.Web.UI.HtmlControls.HtmlForm();
            gvData.Parent.Controls.Add(hForm);
            hForm.Attributes["runat"] = "server";
            hForm.Controls.Add(gvData);
            hForm.RenderControl(hw);

            string style = @"<style>.twoDigit{mso-number-format:""\#,\#\#0\.00"";} .bold{font-weight:""bold"";}
                                    .twoDigit-red{mso-number-format:""\#,\#\#0\.00""; font-weight:""bold"";color:""#ff0000"";}
                                    .oneDigit{mso-number-format:""0\.0"";}</style>";

            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            byte[] byteData = Encoding.Default.GetBytes(style);
            mem.Write(byteData, 0, byteData.Length);

            string content = sw.ToString();
            byteData = Encoding.Default.GetBytes(content);

            mem.Write(byteData, 0, byteData.Length);
            mem.Flush();
            mem.Position = 0; //reset position to the begining of the stream
            return mem;
        }

        public static Boolean InsertErrorLog(Exception ex, string Message, HttpRequestMessage Request)
        {
            

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, GetHKTime()));
            
            if (ex != null)
                para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.ToString()));
            else
                para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            if (ex != null)
                para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.Message.ToString() + Message));
            else if (Message != "")
                para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, Message));
            else
                para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

            if (Request != null)
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                List<Claim> claims = principal.Claims.ToList();

                if (claims.Count > 0)
                {
                    para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(claims.Where(cl => cl.Type == "UserID").FirstOrDefault().Value)));
                    para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(claims.Where(cl => cl.Type == "IpAddress").FirstOrDefault().Value)));
                    para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(claims.Where(cl => cl.Type == "DeviseType").FirstOrDefault().Value)));
                }
            }
            para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

            db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);
            return true;
        }
    }
}
