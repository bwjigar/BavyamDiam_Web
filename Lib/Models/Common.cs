using DAL;
using EpExcelExportLib;
using OfficeOpenXml;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
namespace Lib.Models
{
    public class Common
    {
        private static long? Code;
        static string toSupplDisc = "0.00";
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
        // Change By Hitesh on [25-03-2017] as Per [Doc No 670]
        public static bool CheckExists(string url)
        {
            Uri uri = new Uri(url);
            //Uri uri = new Uri("http://cdn1.brainwaves.co.in/vfiles/Imaged/13597153/0.js");

            if (uri.IsFile) // File is local
                return System.IO.Directory.Exists(uri.LocalPath);

            try
            {
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.Method = "HEAD"; // No need to download the whole thing

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return (response.StatusCode == HttpStatusCode.OK); // Return true if the file exists
            }
            catch
            {

                return false; // URL does not exist
            }
        }
        public static string GetStatus(byte fiStatus)
        {
            string lsStatus;
            switch (fiStatus)
            {
                case 1:
                    lsStatus = "Received";
                    break;
                case 2:
                    lsStatus = "In Process";
                    break;
                case 3:
                    lsStatus = "Delivered";
                    break;
                case 4:
                    lsStatus = "Cancel";
                    break;
                default:
                    lsStatus = null;
                    break;
            }
            return lsStatus;
        }
        // Change By Hitesh on [29-05-2017] as per rahul bcoz only order mail can not sent tejashbhai
        private static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsBccAdd, int? fiOrderId, bool AdminMail, Int64? UserId, string MailFrom, bool bIsOrder)
        {
            Order objorder = new Order();
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam");
                loMail.To.Add(fsToAdd);
                if (!string.IsNullOrEmpty(fsBccAdd))
                    loMail.Bcc.Add(fsBccAdd);
                if (bIsOrder == false)
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                }
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
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), UserId);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                //PRIAYNKA ON DATE [15-Jun-2015] AS PER SAID BY [TJ]
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

        //-- By Aniket [24-06-15]
        // Change By Hitesh on [29-05-2017] as per rahul bcoz only order mail can not sent tejashbhai
        private static void ReSendOrderMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, int? fiOrderId, DateTime? orderDate, bool AdminMail, Int64? UserId, bool bIsOrder)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();

            try
            {
                string[] date = orderDate.ToString().Split(' ');
                string dtOrder = Convert.ToDateTime(date[0]).ToString("dd-MM-yyyy");


                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);
                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.Bcc.Add(fsCCAdd);

                if (bIsOrder == false)
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                }

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
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + dtOrder + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + dtOrder + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), UserId);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + dtOrder + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + dtOrder + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                //PRIAYNKA ON DATE [15-Jun-2015] AS PER SAID BY [TJ]
                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
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
        //-- Over [24-06-15]

        public static String EmailHeader()
        {
            return @"<html><head><style type=""text/css"">body{font-family: Verdana,'sans-serif';font-size:12px;}p{text-align:justify;margin:10px 0 !important;}
                a{color:#1a4e94;text-decoration:none;font-weight:bold;}a:hover{color:#3c92fe;}table td{font-family: Verdana,'sans-serif' !important;font-size:12px;padding:3px;border-bottom:1px solid #dddddd;}
                </style></head><body>
                <div style=""width:100%; margin:5px auto;font-family: Verdana,'sans-serif';font-size:12px;line-height:20px; background-color:#f2f2f2;"">
                <div style=""padding:10px;"">";
        }

        public static String EmailSignature()
        {
            return @"<p>Thanks and Regards,<br />Bhavyam Diam Team
                <br/><a href=""http://www.bhavyamdiam.com/"">www.bhavyamdiam.com</a></p></div></div></body></html>";
        }

        public static bool EmailNewRegistration(string fsToAdd, string fsName, string fsUsername, string fsPassword, DateTime? registerDate, int UserId)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
                loSb.Append(@"<p>Welcome to our world.</p>");
                loSb.Append(@"<p>Thanks for registering with us. Your profile is currently under screening. This is just for varifying that customer is genuine and sincere for business with us and avoid any illegal activity regarding to diamond trade. We will get back soon with notification of your account status. It will take   maximum 3 working Days and can be possible that our representative will be contact you with contact information you given for verification purpose.</p>");
                loSb.Append(@"<p>Please store below information for further communication.<br />");
                loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                loSb.Append("<b>Password: </b>" + fsPassword + "<br /></p>");

                loSb.Append(EmailSignature());

                //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
                //--By Aniket [24-06-15] coz added Date parameter
                if (registerDate != null)
                    SendMail(fsToAdd, "Welcome to Sunrise Diamonds  New Registration  " + Convert.ToDateTime(registerDate).ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), null, null, false, UserId, null, false);
                else
                    SendMail(fsToAdd, "Welcome to Sunrise Diamonds  New Registration  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), null, null, false, UserId, null, false);
                //--Over [24-06-15]

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailNewRegistrationToAdmin(string fsToAdd, string fsUsername, string fsFirstName, string fsLastName,
            string fsAddress, string fsCity, string fsCountry, string fsMobile, string fsEmail, string fsCompName, string fsCompAdd,
            string fsCompCity, string fsCompCountry, string fsCompMob1, string fsCompPhone1, string fsCompEmail, string fsStarus, int liUserId, DateTime? registrationDate)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
                loSb.Append(@"<p>New user registered with Sunrise Diamonds.</p>");
                loSb.Append(@"<p>Customer profile is under verification.</p>");
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                loSb.Append(@"<tr><td colspan=""2""><b>Personal Detail</b></td></tr>");
                loSb.Append(@"<tr><td width=""170px"">Username:</td><td>" + fsUsername + "</td></tr>");
                loSb.Append(@"<tr><td>First Name:</td><td>" + fsFirstName + "</td></tr>");
                loSb.Append(@"<tr><td>Last Name:</td><td>" + fsLastName + "</td></tr>");
                loSb.Append(@"<tr><td>Address:</td><td>" + fsAddress + "</td></tr>");
                loSb.Append(@"<tr><td>City:</td><td>" + fsCity + "</td></tr>");
                loSb.Append(@"<tr><td>Country:</td><td>" + fsCountry + "</td></tr>");
                loSb.Append(@"<tr><td>Mobile:</td><td>" + fsMobile + "</td></tr>");
                loSb.Append(@"<tr><td>Email Address:</td><td>" + fsEmail + "</td></tr>");
                loSb.Append(@"<tr><td colspan=""2"">&nbsp;</td></tr>");
                loSb.Append(@"<tr><td colspan=""2""><b>Company Detail</b></td></tr>");
                loSb.Append(@"<tr><td>Company Name:</td><td>" + fsCompName + "</td></tr>");
                loSb.Append(@"<tr><td>Address:</td><td>" + fsCompAdd + "</td></tr>");
                loSb.Append(@"<tr><td>City:</td><td>" + fsCompCity + "</td></tr>");
                loSb.Append(@"<tr><td>Country:</td><td>" + fsCompCountry + "</td></tr>");
                loSb.Append(@"<tr><td>Mobile 1:</td><td>" + fsCompMob1 + "</td></tr>");
                loSb.Append(@"<tr><td>Office Phone 1:</td><td>" + fsCompPhone1 + "</td></tr>");
                loSb.Append(@"<tr><td>Email Address:</td><td>" + fsCompEmail + "</td></tr>");
                loSb.Append(@"<tr><td>Account Status:</td><td>" + fsStarus + "</td></tr>");
                loSb.Append("</table>");
                loSb.Append(@"<p><a href=""https://sunrisediamonds.com.hk/Login.aspx?refUrl=sgAdmin/ViewOrder.aspx?Id=" + liUserId.ToString()
                    + @""">Click here</a> to know your current order Status.</p>");
                loSb.Append(EmailSignature());

                //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
                //--By Aniket [24-06-15] coz added Date parameter
                if (registrationDate != null)
                    SendMail(fsToAdd, "Sunrise Diamonds  New Registration  " + Convert.ToDateTime(registrationDate).ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), null, null, false, liUserId, null, false);
                else
                    SendMail(fsToAdd, "Sunrise Diamonds  New Registration  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), null, null, false, liUserId, null, false);
                //--Over [24-06-15] 

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #region change by hitesh on [17-12-2015] as per Doc No [30]
        //public static bool EmailChangeProfileToAdmin(string fsToAdd, string fsUsername, string fsFirstName, string prefsFirstName, string fsLastName,string prefsLastName,
        //  string fsAddress, string fsCity, string fsCountry, string fsMobile, string fsEmail, string fsCompName, string fsCompAdd,
        //  string fsCompCity, string fsCompCountry, string fsCompMob1, string fsCompPhone1, string fsCompEmail, string fsCompEmail2, int liUserId, DateTime? registrationDate)

        public static bool EmailChangeProfileToAdmin(string fsToAdd, string fsUsername, string fsCompanyname, string fsFirstName, string prefsFirstName, string fsLastName, string prefsLastName,
            string prvCompAddress1, string fsCompAddress1, string prvCompAddress2, string fsCompAddress2, string prvCompAddress3, string fsCompAddress3, string prvCity, string fsCity,
            string prvZipcode, string fsZipcode, string prvState, string fsState, string prvCountry, string fsCountry, string prvMobile1, string fsMobile1, string prvMobile2, string fsMobile2,
            string prvPhone1, string fsPhone1, string prvPhone2, string fsPhone2, string prvFax, string fsFax, string prvEmail1, string fsEmail1, string prvEmail2, string fsEmail2,
            string prvRapID, string fsRapID, string prvRegNo, string fsRegNO, int liUserId)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear,</p>");
                loSb.Append(@"<p><b> " + fsUsername + "</b> [ " + fsCompanyname + " ] has changed below profile detail</p>");
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                loSb.Append(@"<tr><td colspan=""1"" width=""170px""><b>Personal Detail</b></td><td ><b>Previous Detail</b></td><td ><b>Current Detail</b></td></tr>");

                //loSb.Append(@"<tr><td width=""170px"">Username:</td><td>" + fsUsername + "</td> <td>" + fsUsername + "</td></tr>");
                if (Convert.ToString(prefsFirstName.ToString().Trim()) == Convert.ToString(fsFirstName.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">First Name:</td><td valign=""top"">" + prefsFirstName + @"</td><td valign=""top"">" + fsFirstName + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">First Name:</td><td valign=""top"">" + prefsFirstName + @"</td><td style=""color:red;"" valign=""top"">" + fsFirstName + "</td></tr>");
                }
                if (Convert.ToString(prefsLastName.ToString().Trim()) == Convert.ToString(fsLastName.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Last Name:</td><td valign=""top"">" + prefsLastName + @"</td><td valign=""top"">" + fsLastName + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Last Name:</td><td valign=""top"">" + prefsLastName + @"</td><td style=""color:red;"" valign=""top"">" + fsLastName + "</td></tr>");
                }
                if (Convert.ToString(prvCompAddress1.ToString().Trim()) == Convert.ToString(fsCompAddress1.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Address1:</td><td valign=""top"">" + prvCompAddress1 + @"</td><td valign=""top"">" + fsCompAddress1 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Address1:</td><td valign=""top"">" + prvCompAddress1 + @"</td><td style=""color:red;"" valign=""top"">" + fsCompAddress1 + "</td></tr>");
                }
                if (Convert.ToString(prvCompAddress2.ToString().Trim()) == Convert.ToString(fsCompAddress2.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Address2:</td><td valign=""top"">" + prvCompAddress2 + @"</td><td valign=""top"">" + fsCompAddress2 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Address2:</td><td valign=""top"">" + prvCompAddress2 + @"</td><td style=""color:red;"" valign=""top"">" + fsCompAddress2 + "</td></tr>");
                }
                if (Convert.ToString(prvCompAddress3.ToString().Trim()) == Convert.ToString(fsCompAddress3.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Address3:</td><td valign=""top"">" + prvCompAddress3 + @"</td><td valign=""top"">" + fsCompAddress3 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Address3:</td><td valign=""top"">" + prvCompAddress3 + @"</td><td style=""color:red;"" valign=""top"">" + fsCompAddress3 + "</td></tr>");
                }
                if (Convert.ToString(prvCity.ToString().Trim()) == Convert.ToString(fsCity.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">City:</td><td valign=""top"">" + prvCity + @"</td><td valign=""top"">" + fsCity + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">City:</td><td valign=""top"">" + prvCity + @"</td><td style=""color:red;"" valign=""top"">" + fsCity + "</td></tr>");
                }
                if (Convert.ToString(prvZipcode.ToString().Trim()) == Convert.ToString(fsZipcode.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">ZipCode:</td><td valign=""top"">" + prvZipcode + @"</td><td valign=""top"">" + fsZipcode + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">ZipCode:</td><td valign=""top"">" + prvZipcode + @"</td><td style=""color:red;"" valign=""top"">" + fsZipcode + "</td></tr>");
                }
                if (Convert.ToString(prvState.ToString().Trim()) == Convert.ToString(fsState.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">State:</td><td valign=""top"">" + prvState + @"</td><td valign=""top"">" + fsState + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">State:</td><td valign=""top"">" + prvState + @"</td><td style=""color:red;"" valign=""top"">" + fsState + "</td></tr>");
                }

                if (Convert.ToString(prvCountry.ToString().Trim()) == Convert.ToString(fsCountry.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Country:</td><td valign=""top"">" + prvCountry + @"</td><td valign=""top"">" + fsCountry + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Country:</td><td valign=""top"">" + prvCountry + @"</td><td style=""color:red;"" valign=""top"">" + fsCountry + "</td></tr>");
                }
                if (Convert.ToString(prvMobile1.ToString().Trim()) == Convert.ToString(fsMobile1.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Mobile1:</td><td valign=""top"">" + prvMobile1 + @"</td><td valign=""top"">" + fsMobile1 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Mobile1:</td><td valign=""top"">" + prvMobile1 + @"</td><td style=""color:red;"" valign=""top"">" + fsMobile1 + "</td></tr>");
                }
                if (Convert.ToString(prvMobile2.ToString().Trim()) == Convert.ToString(fsMobile2.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Mobile2:</td><td valign=""top"">" + prvMobile2 + @"</td><td valign=""top"">" + fsMobile2 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Mobile2:</td><td valign=""top"">" + prvMobile2 + @"</td><td style=""color:red;"" valign=""top"">" + fsMobile2 + "</td></tr>");
                }
                if (Convert.ToString(prvPhone1.ToString().Trim()) == Convert.ToString(fsPhone1.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Office Phone 1:</td><td valign=""top"">" + prvPhone1 + @"</td><td valign=""top"">" + fsPhone1 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Office Phone 1:</td><td valign=""top"">" + prvPhone1 + @"</td><td style=""color:red;"" valign=""top"">" + fsPhone1 + "</td></tr>");
                }
                if (Convert.ToString(prvPhone2.ToString().Trim()) == Convert.ToString(fsPhone2.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Office Phone 2:</td><td valign=""top"">" + prvPhone2 + @"</td><td valign=""top"">" + fsPhone2 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Office Phone 2:</td><td valign=""top"">" + prvPhone2 + @"</td><td style=""color:red;"" valign=""top"">" + fsPhone2 + "</td></tr>");
                }
                if (Convert.ToString(prvFax.ToString().Trim()) == Convert.ToString(fsFax.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Fax:</td><td valign=""top"">" + prvFax + @"</td><td valign=""top"">" + fsFax + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Fax:</td><td valign=""top"">" + prvFax + @"</td><td style=""color:red;"" valign=""top"">" + fsFax + "</td></tr>");
                }

                if (Convert.ToString(prvEmail1.ToString().Trim()) == Convert.ToString(fsEmail1.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Email Address:</td><td valign=""top"">" + prvEmail1 + @"</td><td valign=""top"">" + fsEmail1 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Email Address:</td><td valign=""top"">" + prvEmail1 + @"</td><td style=""color:red;"" valign=""top"">" + fsEmail1 + "</td></tr>");
                }
                if (Convert.ToString(prvEmail2.ToString().Trim()) == Convert.ToString(fsEmail2.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Email Address2:</td><td valign=""top"">" + prvEmail2 + @"</td><td valign=""top"">" + fsEmail2 + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Email Address2:</td><td valign=""top"">" + prvEmail2 + @"</td><td style=""color:red;"" valign=""top"">" + fsEmail2 + "</td></tr>");
                }
                if (Convert.ToString(prvRapID.ToString().Trim()) == Convert.ToString(fsRapID.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Rap ID:</td><td valign=""top"">" + prvRapID + @"</td><td valign=""top"">" + fsRapID + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Rap ID:</td><td valign=""top"">" + prvRapID + @"</td><td style=""color:red;"" valign=""top"">" + fsRapID + "</td></tr>");
                }
                if (Convert.ToString(prvRegNo.ToString().Trim()) == Convert.ToString(fsRegNO.ToString().Trim()))
                {
                    //loSb.Append(@"<tr><td valign=""top"">Registration No:</td><td valign=""top"">" + prvRegNo + @"</td><td valign=""top"">" + fsRegNO + "</td></tr>");
                }
                else
                {
                    loSb.Append(@"<tr><td valign=""top"">Registration No:</td><td valign=""top"">" + prvRegNo + @"</td><td style=""color:red;"" valign=""top"">" + fsRegNO + "</td></tr>");
                }
                loSb.Append("</table>");

                //loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Sunrise Diamonds  User Profile Change  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), null, null, false, liUserId, null, false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        public static bool EmailNewOrder(string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
             string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, Int64? UserCode, DateTime? orderDate, string BCCEmailId = null, string CCEmailId = null)
        {
            Database db1 = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para1;
            para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para1.Clear();
            para1.Add(db1.CreateParam("iiUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(UserCode)));
            System.Data.DataTable dtUserDetail = db1.ExecuteSP("UserMas_SelectOne", para1.ToArray(), false);

            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());
            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
            if (dtUserDetail.Rows[0]["sCompName"] != null && dtUserDetail.Rows[0]["sCompName"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Company Name:</td><td>" + dtUserDetail.Rows[0]["sCompName"].ToString() + "(" + dtUserDetail.Rows[0]["sCompMobile"].ToString() + ")</td></tr>");
                //  loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sCompName"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["iUserType"] != null && dtUserDetail.Rows[0]["iUserType"].ToString() == "3")
            {
                string Fname = "", Lname = "";
                if (dtUserDetail.Rows[0]["sFirstName"] != null && dtUserDetail.Rows[0]["sFirstName"].ToString() != "")
                {
                    Fname = dtUserDetail.Rows[0]["sFirstName"].ToString();
                }
                if (dtUserDetail.Rows[0]["sLastName"] != null && dtUserDetail.Rows[0]["sLastName"].ToString() != "")
                {
                    Lname = dtUserDetail.Rows[0]["sLastName"].ToString();
                }
                loSb.Append(@"<tr><td>Buyer:</td><td>" + Fname + " " + Lname + "</td></tr>");
                // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["sCompAddress"] != null && dtUserDetail.Rows[0]["sCompAddress"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Address:</td><td>" + dtUserDetail.Rows[0]["sCompAddress"].ToString() + "</td></tr>");
                //  loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sCompName"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["AssistByEmpName"] != null && dtUserDetail.Rows[0]["AssistByEmpName"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Sales Person:</td><td>" + dtUserDetail.Rows[0]["AssistByEmpName"].ToString() + "</td></tr>");
                //loSb.Append(@"<tr><td colspan=""2"">(Sales Person):" + dtUserDetail.Rows[0]["AssistBy1"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["AssistByMobile"] != null && dtUserDetail.Rows[0]["AssistByMobile"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Mobile/Whatsapp:</td><td>" + dtUserDetail.Rows[0]["AssistByMobile"].ToString() + "</td></tr>");
            }
            //loSb.Append(@"<tr><td colspan=""2"">" + fsMobile + "</td></tr>");
            if (dtUserDetail.Rows[0]["AssistByWechat"] != null && dtUserDetail.Rows[0]["AssistByWechat"].ToString() != "")
            {
                loSb.Append(@"<tr><td>WeChat ID:</td><td>" + dtUserDetail.Rows[0]["AssistByWechat"].ToString() + "</td></tr>");
                // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["AssistByEmail"] != null && dtUserDetail.Rows[0]["AssistByEmail"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["AssistByEmail"].ToString() + "</td></tr>");
                // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
            }
            if (orderDate != null)
                loSb.Append(@"<tr><td>Order Date:</td><td>" + Convert.ToDateTime(orderDate).ToString("dd-MMM-yyyy") + "</td></tr>");
            else
                loSb.Append(@"<tr><td>Order Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
            loSb.Append(@"<tr><td width=""170px"">Order No:</td><td>" + fiOrderNo.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td width=""170px"">Customer Note:</td><td>" + fsCustomerNote.ToString() + "</td></tr>");
            if (dtUserDetail.Rows[0]["iUserType"] != null && dtUserDetail.Rows[0]["iUserType"].ToString() != "3")
            {
                string Fname = "", Lname = "";
                if (dtUserDetail.Rows[0]["sFirstName"] != null && dtUserDetail.Rows[0]["sFirstName"].ToString() != "")
                {
                    Fname = dtUserDetail.Rows[0]["sFirstName"].ToString();
                }
                if (dtUserDetail.Rows[0]["sLastName"] != null && dtUserDetail.Rows[0]["sLastName"].ToString() != "")
                {
                    Lname = dtUserDetail.Rows[0]["sLastName"].ToString();
                }
                loSb.Append(@"<tr><td>Employee Name:</td><td>" + Fname + " " + Lname + "</td></tr>");
                // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
            }
            loSb.Append("</table>");
            loSb.Append("<br/> <br/>");

            // Added By Jubin Shah 24-02-2020
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiOrderNo));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, UserCode));

            System.Data.DataTable dt = db.ExecuteSP("OrderDet_SelectAllByOrderId_Email", para.ToArray(), false);

            //Building an HTML string.

            //Table start.
            loSb.Append("<table border = '1' style='overflow-x:scroll !important; width:1500px !important;'>");

            //Building the Header row.
            loSb.Append("<tr>");

            string _strfont = "\"font-size: 12px; font-family: Tahoma;text-align:center; background-color: #83CAFF;\"";
            foreach (DataColumn column in dt.Columns)
            {
                loSb.Append("<th style = " + _strfont + ">");
                loSb.Append(column.ColumnName);
                loSb.Append("</th>");
            }
            loSb.Append("</tr>");

            _strfont = "\"font-size: 10px; font-family: Tahoma;text-align:center;white-space: nowrap; \"";
            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                loSb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    string _strcheck = "";
                    if (column.ColumnName.ToString() == "Disc %" || column.ColumnName.ToString() == "Net Amt($)")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;color: #FF0000;white-space: nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Status" && row["Stock Id"].ToString() != "Total")
                    {
                        if (row["Status"].ToString().ToLower() == "confirmed")
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #c6ffbe;white-space: nowrap;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }
                        else
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: yellow;color:red;white-space: nowrap;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }
                    }
                    else if (column.ColumnName.ToString() == "Location" && row["Location"].ToString() == "Upcoming")
                    {
                        string _strstyle = "\"font-weight:bold;background-color: #c4e3fb;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Cut" || column.ColumnName.ToString() == "Polish" || column.ColumnName.ToString() == "Symm")
                    {
                        loSb.Append("<td style = " + _strfont + ">");
                        if (row["Cut"].ToString() == "3EX" && row["Polish"].ToString() == "EX" && row["Symm"].ToString() == "EX")
                        {
                            loSb.Append("<b>" + row[column.ColumnName] + "<b>");
                            _strcheck = "Y";
                        }
                    }
                    else
                        loSb.Append("<td style = " + _strfont + ">");

                    if (_strcheck != "Y")
                    {
                        if (column.ColumnName.ToString() == "Rap Price($)" || column.ColumnName.ToString() == "Rap Amount($)" || column.ColumnName.ToString() == "Net Amt($)")
                        {
                            if (row[column.ColumnName].ToString() != "")
                            {

                                if (column.ColumnName.ToString() == "Rap Price($)")
                                {
                                    loSb.Append(Convert.ToInt32(row[column.ColumnName]).ToString("C", new CultureInfo("en-US")).Replace("$", "").Replace("(", "").Replace(")", "").Replace(".00", ""));
                                }
                                else
                                {
                                    loSb.Append(Convert.ToDecimal(row[column.ColumnName]).ToString("C", new CultureInfo("en-US")).Replace("(", "").Replace(")", "").Replace("$", ""));
                                }
                            }
                            else
                            {
                                loSb.Append(row[column.ColumnName]);
                            }
                        }
                        else if (column.ColumnName.ToString() == "Cts")
                        {
                            loSb.Append(String.Format("{0:0.00}", Convert.ToDecimal(row[column.ColumnName])));
                        }
                        else if (column.ColumnName.ToString() == "image")
                        {
                            if (row["image"].ToString() != "")
                            {
                                loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Image</a>"));
                            }
                        }

                        else if (column.ColumnName.ToString() == "video")
                        {
                            if (row["video"].ToString() != "")
                            {
                                loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Video</a>"));
                            }
                        }

                        else if (column.ColumnName.ToString() == "dna")
                        {
                            if (row["dna"].ToString() != "")
                            {
                                loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Dna</a>"));
                            }
                        }
                        else
                        {
                            loSb.Append(row[column.ColumnName]);
                        }
                    }
                    loSb.Append("</td>");

                }
                loSb.Append("</tr>");
            }

            //Table end.
            loSb.Append("</table>");

            Code = UserCode;
            loSb.Append(@"<p>Thank you for placing order from our website www.sunrisediamonds.com.hk</p>");
            //loSb.Append(@"<p>Thank you for interesting and ordering with us.<br />");
            //loSb.Append(@"<a href=""https://sunrisediamonds.com.hk/ViewOrder.aspx/Id=" + fiOrderNo.ToString() + @""">Click here</a> to know your current order status.</p>");
            ////priyanka on date [28-05-15]
            //loSb.Append(@"<p>For not available stone contact your KAM.");

            //////
            loSb.Append(EmailSignature());

            //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
            //--By Aniket [24-06-15]
            if (CCEmailId == null && BCCEmailId == null)
            {
                if (orderDate != null)
                    ReSendOrderMail(fsToAdd, "Sunrise Diamonds – Order Confirmation – " + Convert.ToDateTime(orderDate).ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(), Convert.ToString(loSb), null, fiOrderNo, orderDate, false, UserCode, true);
                else
                    SendMail(fsToAdd, "Sunrise Diamonds – Order Confirmation – " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(), Convert.ToString(loSb), null, fiOrderNo, false, UserCode, "CUST", true);
                //--Over [24-06-15]
            }
            else
            {
                if (orderDate != null)
                    ConfirmOrderReSendOrderMail(fsToAdd, "Sunrise Diamonds – Order Confirmation – " + Convert.ToDateTime(orderDate).ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(), Convert.ToString(loSb), BCCEmailId, CCEmailId, fiOrderNo, orderDate, false, UserCode, true);
                else
                    ConfirmOrderSendMail(fsToAdd, "Sunrise Diamonds – Order Confirmation – " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(), Convert.ToString(loSb), BCCEmailId, CCEmailId, fiOrderNo, false, UserCode, "CUST", true);
            }


            //return Convert.ToString(loSb);
            return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }


        //public static bool EmailNewOrderToAdmin(string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
        //    string fsCompName, string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, DateTime? orderDate, Int64? UserCode)
        //{
        //    //try
        //    //{
        //    StringBuilder loSb = new StringBuilder();
        //    loSb.Append(EmailHeader());

        //    loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
        //    loSb.Append(@"<p>New order arrived from " + fsFullname + " (" + fsCompName + ")</p>");
        //    loSb.Append(@"<p>Below are the Order details.</p>");

        //    loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
        //    loSb.Append(@"<tr><td width=""170px"">Order No:</td><td>" + fiOrderNo.ToString() + "</td></tr>");
        //    //--By Aniket [24-06-15]
        //    if (orderDate != null)
        //        loSb.Append(@"<tr><td>Order Date:</td><td>" + Convert.ToDateTime(orderDate).ToString("dd-MMM-yyyy") + "</td></tr>");
        //    else
        //        loSb.Append(@"<tr><td>Order Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
        //    //--Over [24-06-15]

        //    //loSb.Append(@"<tr><td>Order Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
        //    loSb.Append(@"<tr><td>Order Status:</td><td>" + getStatus(fiOrderStauts) + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2""><b>&nbsp;</b></td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2""><b>Billing Information</b></td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + fsFullname + " (" + fsCompName + ")</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + fsAddress + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">Phone No: " + fsPhoneNo + " Mobile: " + fsMobile + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">Email Address: " + fsEmail + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">&nbsp;</td></tr>");
        //    loSb.Append(@"<tr><td>Customer Note:</td><td>" + fsCustomerNote + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">&nbsp;</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2""><b>Diamond Details</b></td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + "Please find Order Detail in attached file." + "</td></tr>");
        //    loSb.Append("</table>");

        //    loSb.Append(@"<p><a href=""https://sunrisediamonds.com.hk/SgAdmin/ViewOrder.aspx/Id=" + fiOrderNo.ToString() + @""">Click here</a> to change order status.</p>");
        //    loSb.Append(EmailSignature());

        //    //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
        //    //--By Aniket [24-06-15]
        //    if (orderDate != null)
        //        ReSendOrderMail(fsToAdd, "Sunrise Diamonds  Order Confirmation  " + Convert.ToDateTime(orderDate).ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(), Convert.ToString(loSb), null, fiOrderNo, orderDate, true, UserCode, true);
        //    else
        //        SendMail(fsToAdd, "Sunrise Diamonds  Order Confirmation  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(), Convert.ToString(loSb), null, fiOrderNo, true, UserCode, "ADMIN", true);
        //    //--Over [24-06-15]

        //    return true;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //}
        //}

        public static bool EmailChangeOrderStatus(string fsToAdd, string fiOrderNo, string fdtOrderDate, byte fiOrderStauts, string fsFullname, int UserCode)
        {
            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());

            loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsFullname + ",</p>");
            loSb.Append(@"<p>You have placed below order on date " + fdtOrderDate + ". Your current status of order is</p>");

            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
            loSb.Append(@"<tr><td width=""170px"">Order No:</td><td>" + fiOrderNo + "</td></tr>");
            loSb.Append(@"<tr><td>Order Date:</td><td>" + fdtOrderDate + "</td></tr>");
            loSb.Append(@"<tr><td>Order Status:</td><td>" + GetStatus(fiOrderStauts) + "</td></tr>");
            loSb.Append("</table>");
            loSb.Append(EmailSignature());

            //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
            SendMail(fsToAdd, "Sunrise Diamonds  Order Status  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                Convert.ToString(loSb), null, null, false, UserCode, null, true);
            return true;
        }

        public static bool EmailMemberActiveStatus(string fsToAdd, string fsCCAdd, string fsName, string fsUsername, string fCompName, DateTime? modifiedDate, int UserId, string AssistDetail)
        {
            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());

            loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
            loSb.Append(@"<p>Thanks for registering with us. Now your account has been activated successfully.</p>");
            loSb.Append(@"<p>In future you can communicate with us using below detail.<br />");
            loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
            if (!string.IsNullOrEmpty(fCompName))
            {
                loSb.Append(@"<b>Company Name: </b>" + fCompName + "<br />");
            }
            loSb.Append(@"<br />"); 
            loSb.Append(EmailSignature());

            SendMail(fsToAdd, "Bhavyam Diam Account Activation " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), fsCCAdd, null, false, UserId, null, false);
            return true;
        }

        public static bool EmailTradeFairRegistration(string fsToAdd, string fsEventName, string fsCompanyName, string fsName,
            string fsSurname, string fsCountry, string fsPhone, string fsEmail, int UserId)
        {
            //try
            //{
            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());

            loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
            loSb.Append(@"<p>User registered in Trade Fair of Sunrise Diamonds.</p>");
            loSb.Append(@"<p>User detail.</p>");
            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
            loSb.Append(@"<tr><td width=""170px"">Event/Trade Fair:</td><td>" + fsEventName + "</td></tr>");
            loSb.Append(@"<tr><td>Company Name:</td><td>" + fsCompanyName + "</td></tr>");
            loSb.Append(@"<tr><td>Name:</td><td>" + fsName + "</td></tr>");
            loSb.Append(@"<tr><td>Surname:</td><td>" + fsSurname + "</td></tr>");
            loSb.Append(@"<tr><td>Country:</td><td>" + fsCountry + "</td></tr>");
            loSb.Append(@"<tr><td>Phone:</td><td>" + fsPhone + "</td></tr>");
            loSb.Append(@"<tr><td>Email Address:</td><td>" + fsEmail + "</td></tr>");
            loSb.Append("</table>");
            loSb.Append(EmailSignature());

            //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
            SendMail(fsToAdd, "Sunrise Diamonds  Trade Fair Registration " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                Convert.ToString(loSb), null, null, false, UserId, null, false);
            return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        public static bool EmailForgotPassword(string fsToAdd, string fsName, string fsUsername, string fsPassword, Int64? UserId)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
                loSb.Append(@"<p>Thank you for requesting your account detail.</p>");
                loSb.Append(@"<p>Please store below information for further communication.<br />");
                loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                loSb.Append("<b>Password: </b>" + fsPassword + "<br /></p>");

                loSb.Append(EmailSignature());

                //Chnage from GetHKTime() to GetHkTime() On dated 05/11/2014 By [RJ] According to [TJ]
                SendMail(fsToAdd, "Sunrise Diamonds  Forgot Password  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, false, UserId, null, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailUploadConfirmation(string fsToAdd, string fsMethodName, string fsRecCountMessage, int UserId)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
                loSb.Append(@"<p>Method <b>" + fsMethodName + "</b> execute successfully for Sunrise Diamonds.</p>");
                loSb.Append(@"<b>Date: </b>" + GetHKTime() + "<br />");
                loSb.Append(@"<b>Total Stock: </b>" + fsRecCountMessage + "<br />");

                loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Sunrise  " + fsMethodName + " successfully(" + fsRecCountMessage + ")  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, false, UserId, null, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static System.IO.MemoryStream ExportToStream(int fiOrderId, Int64? UserId)
        {
            //OrderDetDataContext loDC = new OrderDetDataContext();
            //List<OrderDet_SelectAllByOrderIdResult> loOrderDet = loDC.OrderDet_SelectAllByOrderId(fiOrderId, UserId).ToList();
            Order objorder = new Order();
            DataTable loOrderDet = objorder.OrderDet_SelectAllByOrderId(fiOrderId, UserId);

            GridView gvData = new GridView
            {
                AutoGenerateColumns = false,
                ShowFooter = true
            };

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

        private static System.IO.MemoryStream ExportToStreamEpPlus(int fiOrderId, bool AdminMail, Int64? UserId)
        {
            Order objorder = new Order();
            DataTable loOrderDet = objorder.OrderDet_SelectAllByOrderId(fiOrderId, UserId);
            //OrderDetDataContext loDC = new OrderDetDataContext();
            //List<OrderDet_SelectAllByOrderIdResult> loOrderDet = loDC.OrderDet_SelectAllByOrderId(fiOrderId, UserId).ToList();

            GridView gvData = new System.Web.UI.WebControls.GridView();
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

                                toSupplDisc = loOrderDet.Select().Where(p => p["sSupplDisc"] != DBNull.Value).Select(c => Convert.ToDecimal(c["sSupplDisc"])).Sum().ToString("#,##0.00");
                                BoundField sSupplNetVal = new BoundField(); sSupplNetVal.DataField = "sSupplNetVal"; sSupplNetVal.HeaderText = "Org Value($)";
                                sSupplNetVal.FooterText = toSupplDisc; //Convert.ToDecimal(loOrderDet.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00"); ;//loOrderDet.Sum(r => r.sSupplDisc).Value.ToString("#,##0.00");
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
                    sSupplNetVal.FooterText = loOrderDet.Select().Where(p => p["sSupplDisc"] != DBNull.Value).Select(c => Convert.ToDecimal(c["sSupplDisc"])).Sum().ToString("#,##0.00"); //Convert.ToDecimal(loOrderDet.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00");//loOrderDet.Sum(r => r.sSupplDisc).Value.ToString("#,##0.00");
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

        //        private static System.IO.MemoryStream ExportToStreamOpenXml(int fiOrderId)
        //        {
        //            OrderDetDataContext loDC = new OrderDetDataContext();
        //            List<OrderDet_SelectAllByOrderIdResult> loOrderDet = loDC.OrderDet_SelectAllByOrderId(fiOrderId).ToList();

        //            GridView gvData = new GridView();
        //            gvData.AutoGenerateColumns = false;
        //            gvData.ShowFooter = true;

        //            //gvData.ShowHeader = true;
        //            //gvData.GridLines = GridLines.Both;
        //            //gvData.AllowPaging = false;
        //            //gvData.AlternatingRowStyle.CssClass = "";

        //            CustomColDataContext ColResDet = new CustomColDataContext();
        //            List<ColumnConfDet_SelectResult> ColRes = ColResDet.ColumnConfDet_Select(Convert.ToInt16(Code)).ToList();

        //            if (ColRes.Count > 0)
        //            {
        //                BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
        //                gvData.Columns.Add(cSr);


        //                for (int k = 0; k < ColRes.Count; k++)
        //                {
        //                    //HyperLink hl = (gvData.Controls[k].Controls[1] as HyperLink);

        //                    decimal dlTotalRapAmount = loOrderDet.Sum(r => r.dRapAmount).Value;
        //                    switch (ColRes[k].sColumnName)
        //                    {

        //                        case "bImage":
        //                            //BoundField cImage = new BoundField(); cImage.DataField = "bImage"; cImage.HeaderText = "Image";
        //                            //gvData.Columns.Add(cImage);
        //                            break;
        //                        case "bHDMovie":
        //                            //BoundField cHd = new BoundField(); cHd.DataField = "bHDMovie"; cHd.HeaderText = "HD";
        //                            //gvData.Columns.Add(cHd);
        //                            break;
        //                        case "sRefNo":
        //                            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
        //                            gvData.Columns.Add(cRefNo);
        //                            break;

        //                        case "sLab":
        //                            BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
        //                            gvData.Columns.Add(cLab);
        //                            break;

        //                        case "sShape":
        //                            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
        //                            gvData.Columns.Add(cShape);
        //                            break;
        //                        case "sPointer":
        //                            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointers";
        //                            gvData.Columns.Add(cPointer);
        //                            break;
        //                        case "sCertiNo":
        //                            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
        //                            gvData.Columns.Add(cCertiNo);
        //                            break;
        //                        case "sColor":
        //                            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
        //                            cColor.FooterText = "Pcs";
        //                            gvData.Columns.Add(cColor);
        //                            break;

        //                        case "sClarity":
        //                            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
        //                            cClarity.FooterText = loOrderDet.Count.ToString();
        //                            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
        //                            gvData.Columns.Add(cClarity);
        //                            break;
        //                        case "dCts":
        //                            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Carats";
        //                            cCarats.FooterText = loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
        //                            cCarats.ItemStyle.CssClass = "twoDigit";
        //                            cCarats.FooterStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cCarats);
        //                            break;

        //                        case "dRepPrice":

        //                            BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
        //                            cRepPrice.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cRepPrice);
        //                            break;
        //                        case "dRapAmount":
        //                            BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
        //                            cRepAmount.ItemStyle.CssClass = "twoDigit";
        //                            cRepAmount.FooterStyle.CssClass = "twoDigit";

        //                            cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
        //                            gvData.Columns.Add(cRepAmount);
        //                            break;
        //                        case "dDisc":
        //                            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
        //                            if (dlTotalRapAmount == 0)
        //                                cDisc.FooterText = @"0.00";
        //                            else
        //                                cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
        //                            cDisc.ItemStyle.CssClass = "twoDigit-red";
        //                            cDisc.FooterStyle.CssClass = "twoDigit-red";
        //                            gvData.Columns.Add(cDisc);

        //                            break;
        //                        case "dNetPrice":
        //                            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
        //                            cNetPrice.FooterText = loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
        //                            cNetPrice.ItemStyle.CssClass = "twoDigit";
        //                            cNetPrice.FooterStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cNetPrice);
        //                            break;
        //                        case "sCut":
        //                            BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
        //                            cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
        //                            gvData.Columns.Add(cCut);
        //                            break;
        //                        case "sPolish":
        //                            BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
        //                            gvData.Columns.Add(cPolish);
        //                            break;
        //                        case "sSymm":
        //                            BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
        //                            gvData.Columns.Add(cSymm);
        //                            break;
        //                        case "sFls":
        //                            BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
        //                            gvData.Columns.Add(cFls);
        //                            break;
        //                        case "dLength":
        //                            BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
        //                            cLength.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cLength);
        //                            break;
        //                        case "dWidth":

        //                            BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
        //                            cWidth.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cWidth);
        //                            break;

        //                        case "dDepth":

        //                            BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
        //                            cDepth.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cDepth);
        //                            break;
        //                        case "dDepthPer":
        //                            BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
        //                            cDepthPer.ItemStyle.CssClass = "oneDigit";
        //                            gvData.Columns.Add(cDepthPer);
        //                            break;
        //                        case "dTablePer":
        //                            BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
        //                            gvData.Columns.Add(cTablePer);
        //                            break;
        //                        case "sSymbol":
        //                            BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
        //                            gvData.Columns.Add(cSymbol);
        //                            break;
        //                        case "sInclusion":
        //                            BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Inclusion";
        //                            gvData.Columns.Add(cInclusion);
        //                            break;
        //                        case "sTableNatts":
        //                            BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
        //                            gvData.Columns.Add(cTableNatts);
        //                            break;
        //                        case "sShade":
        //                            BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
        //                            gvData.Columns.Add(cShade);
        //                            break;
        //                        case "dCrAng":
        //                            BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
        //                            cCrAng.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cCrAng);
        //                            break;
        //                        case "dCrHt":
        //                            BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
        //                            cCrHt.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cCrHt);
        //                            break;
        //                        case "dPavAng":
        //                            BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
        //                            cPavAng.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cPavAng);
        //                            break;
        //                        case "dPavHt":
        //                            BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
        //                            cPavHt.ItemStyle.CssClass = "twoDigit";
        //                            gvData.Columns.Add(cPavHt);
        //                            break;
        //                        case "sGirdle":
        //                            BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdle"; cGirdle.HeaderText = "Girdle";
        //                            gvData.Columns.Add(cGirdle);
        //                            break;
        //                        case "sStatus":
        //                            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
        //                            gvData.Columns.Add(sstatus);
        //                            break;
        //                        case "sSideNatts":
        //                            BoundField cSideNatts = new BoundField(); cSideNatts.HeaderText = "Side Natts"; cSideNatts.DataField = "sSideNatts";
        //                            gvData.Columns.Add(cSideNatts);
        //                            break;
        //                        case "sCulet":
        //                            BoundField sCulet = new BoundField(); sCulet.HeaderText = "Culet"; sCulet.DataField = "sCulet";
        //                            gvData.Columns.Add(sCulet);
        //                            break;
        //                        case "dTableDepth":
        //                            //BoundField cTableDepth = new BoundField(); cTableDepth.HeaderText = "Table Depth"; cTableDepth.DataField = "dTableDepth";
        //                            //gvData.Columns.Add(cTableDepth);
        //                            break;
        //                        case "sHNA":
        //                            BoundField cHNA = new BoundField(); cHNA.HeaderText = "HNA"; cHNA.DataField = "sHNA";
        //                            gvData.Columns.Add(cHNA);
        //                            break;
        //                        case "sSideFtr":
        //                            BoundField sSideFtr = new BoundField(); sSideFtr.HeaderText = "Side Ftr"; sSideFtr.DataField = "sSideFtr";
        //                            gvData.Columns.Add(sSideFtr);
        //                            break;
        //                        case "sTableFtr":
        //                            BoundField sTableFtr = new BoundField(); sTableFtr.HeaderText = "Table Ftr"; sTableFtr.DataField = "sTableFtr";
        //                            gvData.Columns.Add(sTableFtr);
        //                            break;

        //                    }




        //                }
        //            }
        //            else
        //            {

        //                BoundField cSr = new BoundField(); cSr.DataField = "iSr"; cSr.HeaderText = "Sr.";
        //                gvData.Columns.Add(cSr);
        //                decimal dlTotalRapAmount = loOrderDet.Sum(r => r.dRapAmount).Value;
        //                BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
        //                gvData.Columns.Add(cRefNo);

        //                BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
        //                gvData.Columns.Add(cLab);

        //                BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
        //                gvData.Columns.Add(cShape);

        //                BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointers";
        //                gvData.Columns.Add(cPointer);

        //                BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
        //                gvData.Columns.Add(cCertiNo);

        //                BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
        //                cColor.FooterText = "Pcs";
        //                gvData.Columns.Add(cColor);

        //                BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
        //                cClarity.FooterText = loOrderDet.Count.ToString();
        //                cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
        //                gvData.Columns.Add(cClarity);

        //                BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Carats";
        //                cCarats.FooterText = loOrderDet.Sum(r => r.dCts).Value.ToString("#,##0.00");
        //                cCarats.ItemStyle.CssClass = "twoDigit";
        //                cCarats.FooterStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cCarats);


        //                BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
        //                cRepPrice.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cRepPrice);

        //                BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
        //                cRepAmount.ItemStyle.CssClass = "twoDigit";
        //                cRepAmount.FooterStyle.CssClass = "twoDigit";

        //                cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
        //                gvData.Columns.Add(cRepAmount);

        //                BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Disc(%)";
        //                if (dlTotalRapAmount == 0)
        //                    cDisc.FooterText = @"0.00";
        //                else
        //                    cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
        //                cDisc.ItemStyle.CssClass = "twoDigit-red";
        //                cDisc.FooterStyle.CssClass = "twoDigit-red";
        //                gvData.Columns.Add(cDisc);


        //                BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Net Amt($)";
        //                cNetPrice.FooterText = loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
        //                cNetPrice.ItemStyle.CssClass = "twoDigit";
        //                cNetPrice.FooterStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cNetPrice);

        //                BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
        //                cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
        //                gvData.Columns.Add(cCut);

        //                BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
        //                gvData.Columns.Add(cPolish);

        //                BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
        //                gvData.Columns.Add(cSymm);

        //                BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
        //                gvData.Columns.Add(cFls);

        //                BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
        //                cLength.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cLength);

        //                BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
        //                cWidth.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cWidth);

        //                BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
        //                cDepth.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cDepth);

        //                BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
        //                cDepthPer.ItemStyle.CssClass = "oneDigit";
        //                gvData.Columns.Add(cDepthPer);

        //                BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
        //                gvData.Columns.Add(cTablePer);

        //                BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
        //                gvData.Columns.Add(cSymbol);

        //                BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Inclusion";
        //                gvData.Columns.Add(cInclusion);

        //                BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
        //                gvData.Columns.Add(cTableNatts);

        //                BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
        //                gvData.Columns.Add(cShade);

        //                BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
        //                cCrAng.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cCrAng);

        //                BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
        //                cCrHt.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cCrHt);

        //                BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
        //                cPavAng.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cPavAng);

        //                BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
        //                cPavHt.ItemStyle.CssClass = "twoDigit";
        //                gvData.Columns.Add(cPavHt);

        //                BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdle"; cGirdle.HeaderText = "Girdle";
        //                gvData.Columns.Add(cGirdle);

        //                BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
        //                gvData.Columns.Add(sstatus);
        //            }

        //            gvData.DataSource = loOrderDet;
        //            gvData.DataBind();

        //            gvData.FooterStyle.Font.Bold = true;
        //            gvData.HeaderStyle.Font.Bold = true;

        //            ExcelExportLib.GridViewExcelExport ge;
        //            ge = new ExcelExportLib.GridViewExcelExport(gvData, "ConfirmOrder", "ConfirmOrder");
        //            ge.BeforeCreateColumnEvent += BeforeCreateColumnEventHandler;
        //            ge.AfterCreateCellEvent += AfterCreateCellEventHandler;
        //            ge.FillingWorksheetEvent += FillingWorksheetEventHandler;
        //            ge.AddHeaderEvent += AddHeaderEventHandler;

        //            MemoryStream ms = new MemoryStream();
        //            ge.CreateExcel(ms);


        //            System.IO.MemoryStream memn = new System.IO.MemoryStream();

        //            byte[] byteDatan = ms.ToArray();
        //            memn.Write(byteDatan, 0, byteDatan.Length);
        //            memn.Flush();
        //            memn.Position = 0;
        //            //memn.Close();
        //            return memn;



        //            System.IO.StringWriter sw = new System.IO.StringWriter();
        //            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

        //            System.Web.UI.Page objPage = new System.Web.UI.Page();
        //            objPage.Controls.Add(gvData);
        //            System.Web.UI.HtmlControls.HtmlForm hForm = new System.Web.UI.HtmlControls.HtmlForm();
        //            gvData.Parent.Controls.Add(hForm);
        //            hForm.Attributes["runat"] = "server";
        //            hForm.Controls.Add(gvData);
        //            hForm.RenderControl(hw);

        //            string style = @"<style>.twoDigit{mso-number-format:""\#,\#\#0\.00"";} .bold{font-weight:""bold"";}
        //                                    .twoDigit-red{mso-number-format:""\#,\#\#0\.00""; font-weight:""bold"";color:""#ff0000"";}
        //                                    .oneDigit{mso-number-format:""0\.0"";}</style>";

        //            System.IO.MemoryStream mem = new System.IO.MemoryStream();
        //            byte[] byteData = Encoding.Default.GetBytes(style);
        //            mem.Write(byteData, 0, byteData.Length);

        //            string content = sw.ToString();
        //            byteData = Encoding.Default.GetBytes(content);

        //            mem.Write(byteData, 0, byteData.Length);
        //            mem.Flush();
        //            mem.Position = 0; //reset position to the begining of the stream
        //            return mem;
        //        }


        public static bool EmailError(Exception ex, System.Web.HttpRequest Request, string UserName, Int32 UserId, string ErrorFrom)
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
            ErrorLog objErrorLog = new ErrorLog();
            objErrorLog.ErrorLog_Insert(GetHKTime(), UserId, Request.UserHostAddress.ToString(), ex.StackTrace.ToString(), ex.Message, ErrorFrom, ErrPage);

            //--Over [22-04-2015] 

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

                loMail.Subject = "Error - Sunrise  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
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
        
        //--Start [26-10-15] by Aniket Doc-1165.
        public static bool EmailOfSuspendedUser(string subject, string ToEmail, string BccEmail, string Username, string CompName)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p><b>As per our company policy his/her account is suspended.</b></p>");
                loSb.Append("<br/>");
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                loSb.Append(@"<tr><td style=""width: 25%;"">Company Name: </td><td>" + CompName.ToString() + "</td></tr>");
                loSb.Append(@"<tr><td style=""width: 25%;"">User Name: </td><td>" + Username.ToString() + "</td></tr>");
                loSb.Append("</table>");
                loSb.Append("<br/>");

                loSb.Append(EmailSignature());
                
                SendMail(ToEmail, subject, Convert.ToString(loSb), BccEmail, null, false, 0, "SuspendedUser", false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool EmailOfAdminSuspendedUser(string subject, string ToEmail, string bodyMsg)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(bodyMsg);

                loSb.Append(EmailSignature());

                SendMail(ToEmail, subject, Convert.ToString(loSb), null, null, false, 0, "SuspendedUser", false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //--over [26-10-15] by Aniket Doc-1165.

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
            ErrorLog objErrorLog = new ErrorLog();
            objErrorLog.ErrorLog_Insert(GetHKTime(), UserId, Request.UserHostAddress.ToString(), ex.StackTrace.ToString(), ex.Message, ErrorFrom, ErrPage);

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

                loMail.Subject = "Error - Sunrise  " + GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
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

        //Confirm Order with cc bcc value from parameter added by rajeshri on 3/3/2020

        private static void ConfirmOrderSendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsBccAdd, string fsCCAdd, int? fiOrderId, bool AdminMail, Int64? UserId, string MailFrom, bool bIsOrder)
        {
            Order objorder = new Order();
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);

                if (!string.IsNullOrEmpty(fsBccAdd))
                    loMail.Bcc.Add(fsBccAdd);
                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.CC.Add(fsCCAdd);

                //if (!string.IsNullOrEmpty(fsCCAdd))
                //    loMail.Bcc.Add(fsCCAdd);
                //if (bIsOrder == false)
                //{
                //    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                //        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                //    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                //        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                //}
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
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), UserId);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                //PRIAYNKA ON DATE [15-Jun-2015] AS PER SAID BY [TJ]
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

        //-- By Aniket [24-06-15]
        // Change By Hitesh on [29-05-2017] as per rahul bcoz only order mail can not sent tejashbhai
        private static void ConfirmOrderReSendOrderMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsBccAdd, string fsCCAdd, int? fiOrderId, DateTime? orderDate, bool AdminMail, Int64? UserId, bool bIsOrder)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();

            try
            {
                string[] date = orderDate.ToString().Split(' ');
                string dtOrder = Convert.ToDateTime(date[0]).ToString("dd-MM-yyyy");


                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);
                if (!string.IsNullOrEmpty(fsBccAdd))
                    loMail.Bcc.Add(fsBccAdd);
                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.CC.Add(fsCCAdd);

                //if (bIsOrder == false)
                //{
                //    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                //        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                //    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                //        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                //}

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
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + dtOrder + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + dtOrder + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), UserId);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + dtOrder + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + dtOrder + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                //PRIAYNKA ON DATE [15-Jun-2015] AS PER SAID BY [TJ]
                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
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

    }

}
