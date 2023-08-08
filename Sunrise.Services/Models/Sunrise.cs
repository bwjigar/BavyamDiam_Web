using DAL;
using EpExcelExportLib;
using OfficeOpenXml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

namespace Sunrise.Services.Models
{

    /// <summary>
    /// Summary description for Sunrise
    /// </summary>
    public class Sunrise
    {
        public Sunrise()
        { }


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

        public static bool EmailNewAdminRegistration(string UserName, string Password, string FirstName, string LastName, string CompEmail, string CompMobile, string IsActive)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
                loSb.Append(@"<p>Now you part of our team, and below your useful information and credentials.</p>");
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                loSb.Append(@"<tr><td colspan=""2""><b>Personal Detail</b></td></tr>");
                loSb.Append(@"<tr><td width=""170px"">Username:</td><td>" + UserName + "</td></tr>");
                loSb.Append(@"<tr><td width=""170px"">Password:</td><td>" + Password + "</td></tr>");
                loSb.Append(@"<tr><td>First Name:</td><td>" + FirstName + "</td></tr>");
                loSb.Append(@"<tr><td>Last Name:</td><td>" + LastName + "</td></tr>");
                loSb.Append(@"<tr><td>Email Address:</td><td>" + CompEmail + "</td></tr>");
                loSb.Append(@"<tr><td>Mobile No.:</td><td>" + CompMobile.Replace("-", " ") + "</td></tr>");
                loSb.Append(@"<tr><td>Account Status:</td><td>" + (IsActive == "true" ? "Active" : "In-Active") + "</td></tr>");
                loSb.Append("</table>");
                loSb.Append(@"<p>" + (IsActive == "true" ? "" : "After Activate your Account ") + "Kindly change the password frequently and ensure confidentiality of the credentials.<br />");
                loSb.Append(@"<br />");

                loSb.Append(EmailSignature());

                SendMail(CompEmail, "BHAVYAM DIAM : Intimation of Account Activation – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, null, false, false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailNewRegistration(string fsToAdd, string fsName, string fsUsername, string fsPassword, string scompname, string Lang)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + "&nbsp;&nbsp;[" + scompname + "]</p>");
                loSb.Append(@"<p>We thank you for registering with us.<br /> Your account details are as follows:</p>");
                loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                loSb.Append(@"</p> <br />");

                loSb.Append(@"The password has been set by you at the time of registration.");
                loSb.Append(@"<p>Kindly change the password frequently and ensure confidentiality of the credentials.<br />");
                loSb.Append(@"<p>Use our online web based system to get the inventory details. As a part of improving your experience, we would always appreciate your valuable feedback.<br />");
                loSb.Append(@"<p>Looking forward to a long term relationship.<br /><br /></p>");

                loSb.Append(EmailSignature());

                SendMail(fsToAdd, "BHAVYAM DIAM : Intimation of Account Activation – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                Convert.ToString(loSb), null, null, null, false, false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailNewRegistrationToAdmin(string whichuser, string fsToAdd, string fsUsername, string fsFirstName, string fsLastName,
            string fsAddress, string fsCity, string fsCountry, string fsMobile, string fsEmail, string fsCompName, string fsCompAdd,
            string fsCompCity, string fsCompCountry, string fsCompMob1, string fsCompPhone1, string fsCompEmail, string fsStarus, int liUserId)
        {

            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
                loSb.Append(@"<p>New " + whichuser + " registered with Bhavyam Diam.</p>");
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                loSb.Append(@"<tr><td colspan=""2""><b>Personal Detail</b></td></tr>");
                loSb.Append(@"<tr><td width=""170px"">Username:</td><td>" + fsUsername + "</td></tr>");
                loSb.Append(@"<tr><td>First Name:</td><td>" + fsFirstName + "</td></tr>");
                loSb.Append(@"<tr><td>Last Name:</td><td>" + fsLastName + "</td></tr>");
                loSb.Append(@"<tr><td>Email Address:</td><td>" + fsCompEmail + "</td></tr>");
                loSb.Append(@"<tr><td>Mobile No.:</td><td>" + fsCompMob1.Replace("-", " ") + "</td></tr>");
                loSb.Append(@"<tr><td>Account Status:</td><td>" + (fsStarus == "true" ? "Active" : "In-Active") + "</td></tr>");
                loSb.Append("</table>");
                loSb.Append(@"<br />");
                loSb.Append(EmailSignature());

                SendMail(fsToAdd, "BHAVYAM DIAM – New Registration – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, null, false, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailNewOrder(int fiUserCode, string fsToAdd, long fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
               string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, DateTime? orderDate, string bcc = null, string cc = null)
        {

            Database db1 = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para1;
            para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para1.Clear();
            para1.Add(db1.CreateParam("iiUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(fiUserCode)));
            System.Data.DataTable dtUserDetail = db1.ExecuteSP("UserMas_SelectOne", para1.ToArray(), false);

            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());
            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
            if (dtUserDetail.Rows[0]["sCompName"] != null && dtUserDetail.Rows[0]["sCompName"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Company Name:</td><td>" + dtUserDetail.Rows[0]["sCompName"].ToString() + "</td></tr>");
            }

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

            if (dtUserDetail.Rows[0]["sCompEmail"] != null && dtUserDetail.Rows[0]["sCompEmail"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["sCompEmail"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["sCompMobile"] != null && dtUserDetail.Rows[0]["sCompMobile"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Mobile No / WhatsApp No:</td><td>" + dtUserDetail.Rows[0]["sCompMobile"].ToString().Replace("-", " ") + "</td></tr>");
            }

            loSb.Append(@"<tr><td>Order Date:</td><td>" + DateTime.Now.ToString("dd-MMM-yyyy") + "</td></tr>");
            loSb.Append(@"<tr><td width=""170px"">Order No:</td><td>" + fiOrderNo.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td width=""170px"">Remark:</td><td>" + fsCustomerNote.ToString() + "</td></tr>");

            loSb.Append("</table>");
            loSb.Append("<br/> <br/>");

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("iOrderId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiOrderNo));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiUserCode));

            System.Data.DataTable dt = db.ExecuteSP("OrderDet_SelectAllByOrderId_Email", para.ToArray(), false);

            dt.Columns.Remove("iSr");

            loSb.Append("<table border = '1' style='overflow-x:scroll !important; width:1250px !important;'>");

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

            foreach (DataRow row in dt.Rows)
            {
                loSb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    string _strcheck = "";
                    if (column.ColumnName.ToString() == "Offer Disc.(%)" || column.ColumnName.ToString() == "Offer Value($)")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #ade0e9;color:red;white-space: nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Web Disc.($)")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #ddeedf;color:blue;white-space: nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Final Disc.(%)" || column.ColumnName.ToString() == "Final Value")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #fdfdc1;color:blue;white-space: nowrap;\"";
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
                    else if (column.ColumnName.ToString() == "Certi No")
                    {
                        string _strstyle = "";
                        //if (row["Certi No"].ToString().Contains("_TCPG"))
                        //{
                        //    _strstyle = "\"font-size:10px;font-family:Tahoma;text-align:center;white-space:nowrap;background-color:#fff2cc;\"";
                        //}
                        //else
                        //{
                        //    _strstyle = "\"font-size:10px;font-family:Tahoma;text-align:center;white-space:nowrap;\"";
                        //}
                        _strstyle = "\"font-size:10px;font-family:Tahoma;text-align:center;white-space:nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else
                    {
                        loSb.Append("<td style = " + _strfont + ">");
                    }

                    if (_strcheck != "Y")
                    {
                        if (column.ColumnName.ToString() == "Rap Price($)" || column.ColumnName.ToString() == "Rap Amount($)" || column.ColumnName.ToString() == "Net Amt($)" || column.ColumnName.ToString() == "Offer Value($)" || column.ColumnName.ToString() == "Final Value")
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
                        else if (column.ColumnName.ToString() == "Certi No")
                        {
                            loSb.Append(row[column.ColumnName].ToString().Replace("_TCPG", ""));
                        }
                        else if (column.ColumnName.ToString() == "Cts")
                        {
                            loSb.Append(String.Format("{0:0.00}", Convert.ToDecimal(row[column.ColumnName])));
                        }
                        else if (column.ColumnName.ToString() == "Image")
                        {
                            if (row["Image"].ToString() != "")
                            {
                                loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Image</a>"));
                            }
                        }

                        else if (column.ColumnName.ToString() == "Video")
                        {
                            if (row["Video"].ToString() != "")
                            {
                                loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Video</a>"));
                            }
                        }

                        else if (column.ColumnName.ToString() == "Certi")
                        {
                            if (row["Certi"].ToString() != "")
                            {
                                loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Certi</a>"));
                            }
                        }

                        else if (column.ColumnName.ToString() == "DNA")
                        {
                            if (row["DNA"].ToString() != "")
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

            loSb.Append("</table>");

            loSb.Append(@"<p>This Stone(s) are Subject to Avaibility</p>");

            db = new Database();
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiUserCode));
            System.Data.DataTable dt1 = db.ExecuteSP("UserLogin_Where", para.ToArray(), false);

            if (dt1.Rows[0]["LoginType"] != null && dt1.Rows[0]["LoginType"].ToString() != "")
            {
                if (dt1.Rows[0]["LoginType"].ToString().ToUpper() == "WEB")
                {
                    loSb.Append(@"<p>Thank you for placing order from our website www.bhavyamdiam.com</p>");
                }
                else
                {
                    loSb.Append(@"<p>Thank you for placing order from our application</p>");
                }
            }

            loSb.Append(EmailSignature());

            ConfirmOrderSendMail(fsToAdd, "Bhavyam Diam – Order Confirmation – " + Convert.ToDateTime(fdtOrderDate).ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(),
                 Convert.ToString(loSb), bcc, cc, fiOrderNo, fiUserCode, false, true);

            return true;
        }


        public static bool EmailNewDemandNotification(int iUserId, int Id, string ToEmail)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para1;
            para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para1.Clear();
            para1.Add(db.CreateParam("iiUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(iUserId)));
            System.Data.DataTable dtUserDetail = db.ExecuteSP("UserMas_SelectOne", para1.ToArray(), false);

            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());
            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""65%"">");
            if (dtUserDetail.Rows[0]["sCompName"] != null && dtUserDetail.Rows[0]["sCompName"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Company Name:</td><td>" + dtUserDetail.Rows[0]["sCompName"].ToString() + "</td></tr>");
            }

            if (dtUserDetail.Rows[0]["sUsername"] != null && dtUserDetail.Rows[0]["sUsername"].ToString() != "")
            {
                loSb.Append(@"<tr><td>User Name:</td><td>" + dtUserDetail.Rows[0]["sUsername"].ToString() + "</td></tr>");
            }
            string Fname = "", Lname = "";
            if (dtUserDetail.Rows[0]["sFirstName"] != null && dtUserDetail.Rows[0]["sFirstName"].ToString() != "")
            {
                Fname = dtUserDetail.Rows[0]["sFirstName"].ToString();
            }
            if (dtUserDetail.Rows[0]["sLastName"] != null && dtUserDetail.Rows[0]["sLastName"].ToString() != "")
            {
                Lname = dtUserDetail.Rows[0]["sLastName"].ToString();
            }
            loSb.Append(@"<tr><td>Full Name:</td><td>" + Fname + " " + Lname + "</td></tr>");

            if (dtUserDetail.Rows[0]["sCompEmail"] != null && dtUserDetail.Rows[0]["sCompEmail"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["sCompEmail"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["sCompMobile"] != null && dtUserDetail.Rows[0]["sCompMobile"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Mobile No / WhatsApp No:</td><td>" + dtUserDetail.Rows[0]["sCompMobile"].ToString().Replace("-", " ") + "</td></tr>");
            }

            loSb.Append(@"<tr><td>Validity Date:</td><td>" + DateTime.Now.ToString("dd-MMM-yyyy") + "</td></tr>");

            loSb.Append("</table>");
            loSb.Append("<br/> <br/>");


            db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("iId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, Id));

            System.Data.DataTable dt = db.ExecuteSP("SearchStock_NoFoundDet_Get", para.ToArray(), false);

            loSb.Append("<table border = '1' style='overflow-x:scroll !important; width:900px !important;'>");

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

            foreach (DataRow row in dt.Rows)
            {
                loSb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    string _strcheck = "";
                    if (column.ColumnName.ToString() == "Cut" || column.ColumnName.ToString() == "Polish" || column.ColumnName.ToString() == "Symm")
                    {
                        loSb.Append("<td style = " + _strfont + ">");
                        if (row["Cut"].ToString() == "3EX" && row["Polish"].ToString() == "EX" && row["Symm"].ToString() == "EX")
                        {
                            loSb.Append("<b>" + row[column.ColumnName] + "<b>");
                            _strcheck = "Y";
                        }
                    }
                    else
                    {
                        loSb.Append("<td style = " + _strfont + ">");
                    }

                    if (_strcheck != "Y")
                    {
                        loSb.Append(row[column.ColumnName]);
                    }
                    loSb.Append("</td>");

                }
                loSb.Append("</tr>");
            }

            loSb.Append("</table>");

            loSb.Append("<br/> <br/>");

            loSb.Append(EmailSignature());

            ConfirmOrderSendMail(ToEmail, "Bhavyam Diam – Demand Notification – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), Convert.ToString(loSb), null, null, null, null, false, true);

            return true;
        }
        public static bool EmailNewHoldRelease(string MailType, int fiUserCode, int LoginUserid, string fsToAdd, string StoneID, DateTime fdtOrderDate, string cc = null, string fsCustomerNote = null, string HoldCompany = null)
        {

            Database db1 = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para1;
            para1 = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para1.Clear();
            para1.Add(db1.CreateParam("iiUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(LoginUserid)));
            System.Data.DataTable dtUserDetail = db1.ExecuteSP("UserMas_SelectOne", para1.ToArray(), false);

            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());
            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
            if (dtUserDetail.Rows[0]["sCompName"] != null && dtUserDetail.Rows[0]["sCompName"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Company Name:</td><td>" + dtUserDetail.Rows[0]["sCompName"].ToString() + "(" + dtUserDetail.Rows[0]["sCompMobile"].ToString() + ")</td></tr>");
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
            }
            if (dtUserDetail.Rows[0]["sCompAddress"] != null && dtUserDetail.Rows[0]["sCompAddress"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Address:</td><td>" + dtUserDetail.Rows[0]["sCompAddress"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["AssistByEmpName"] != null && dtUserDetail.Rows[0]["AssistByEmpName"].ToString() != "")
            {
                loSb.Append(@"<tr><td>Sales Person:</td><td>" + dtUserDetail.Rows[0]["AssistByEmpName"].ToString() + "</td></tr>");
            }
            if (dtUserDetail.Rows[0]["iUserType"].ToString() == "1")
            {
                if (dtUserDetail.Rows[0]["sCompMobile"] != null && dtUserDetail.Rows[0]["sCompMobile"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>Mobile/Whatsapp:</td><td>" + dtUserDetail.Rows[0]["sCompMobile"].ToString() + "</td></tr>");
                }
            }
            else
            {
                if (dtUserDetail.Rows[0]["AssistByMobile"] != null && dtUserDetail.Rows[0]["AssistByMobile"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>Mobile/Whatsapp:</td><td>" + dtUserDetail.Rows[0]["AssistByMobile"].ToString() + "</td></tr>");
                }
            }
            if (dtUserDetail.Rows[0]["iUserType"].ToString() == "1")
            {
                if (dtUserDetail.Rows[0]["sWeChatId"] != null && dtUserDetail.Rows[0]["sWeChatId"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>WeChat ID:</td><td>" + dtUserDetail.Rows[0]["sWeChatId"].ToString() + "</td></tr>");
                }
            }
            else
            {
                if (dtUserDetail.Rows[0]["AssistByWechat"] != null && dtUserDetail.Rows[0]["AssistByWechat"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>WeChat ID:</td><td>" + dtUserDetail.Rows[0]["AssistByWechat"].ToString() + "</td></tr>");
                }
            }
            if (dtUserDetail.Rows[0]["iUserType"].ToString() == "1")
            {
                if (dtUserDetail.Rows[0]["sCompEmail"] != null && dtUserDetail.Rows[0]["sCompEmail"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["sCompEmail"].ToString() + "</td></tr>");
                }
            }
            else
            {
                if (dtUserDetail.Rows[0]["AssistByEmail"] != null && dtUserDetail.Rows[0]["AssistByEmail"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["AssistByEmail"].ToString() + "</td></tr>");
                }
            }

            if (MailType == "HOLD")
            {
                loSb.Append(@"<tr><td>Hold Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
            }
            else if (MailType == "RELEASE")
            {
                loSb.Append(@"<tr><td>Release Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
            }

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
            }

            if (MailType == "HOLD")
            {
                if (HoldCompany != null && HoldCompany.ToString() != "")
                {
                    loSb.Append(@"<tr><td>Hold Party Name:</td><td>" + HoldCompany.ToString() + "</td></tr>");
                }

                loSb.Append(@"<tr><td width=""170px"">Hold Note:</td><td>" + fsCustomerNote.ToString() + "</td></tr>");
            }

            loSb.Append("</table>");
            loSb.Append("<br/> <br/>");

            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, StoneID));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiUserCode));

            System.Data.DataTable dt = db.ExecuteSP("HoldDet_SelectAllByRefNo_Email", para.ToArray(), false);

            dt.Columns.Remove("iSr");

            loSb.Append("<table border = '1' style='overflow-x:scroll !important; width:1500px !important;'>");

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

            foreach (DataRow row in dt.Rows)
            {
                loSb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    string _strcheck = "";
                    if (column.ColumnName.ToString() == "Offer Disc.(%)" || column.ColumnName.ToString() == "Offer Value($)")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #ade0e9;color:red;white-space: nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Web Disc.($)")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #ddeedf;color:blue;white-space: nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Final Disc.(%)" || column.ColumnName.ToString() == "Final Value")
                    {
                        string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #fdfdc1;color:blue;white-space: nowrap;\"";
                        loSb.Append("<td style = " + _strstyle + ">");
                    }
                    else if (column.ColumnName.ToString() == "Stone Status" && row["Stock Id"].ToString() != "Total")
                    {
                        if (row["Stone Status"].ToString().ToLower() == "available")
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma; text-align: center; font-weight: bold; background-color: #008000; white-space: nowrap; color: white;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }
                        else if (row["Stone Status"].ToString().ToLower() == "new")
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma; text-align: center; font-weight: bold; background-color: #fcaec6; white-space: nowrap; color: black;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }
                        else if (row["Stone Status"].ToString().ToLower() == "available offer")
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma; text-align: center; font-weight: bold; background-color: #CCFFFF; white-space: nowrap; color: black;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }
                        else if (row["Stone Status"].ToString().ToLower() == "buss. process")
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma; text-align: center; font-weight: bold; background-color: #ff7f00; white-space: nowrap; color: white;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }
                        else
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma; text-align: center; font-weight: bold; background-color: #008000; white-space: nowrap; color: white;\"";
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
                    {
                        loSb.Append("<td style = " + _strfont + ">");
                    }

                    if (_strcheck != "Y")
                    {
                        if (column.ColumnName.ToString() == "Rap Price($)" || column.ColumnName.ToString() == "Rap Amount($)" || column.ColumnName.ToString() == "Net Amt($)" || column.ColumnName.ToString() == "Offer Value($)" || column.ColumnName.ToString() == "Final Value")
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

            loSb.Append("</table>");

            db = new Database();
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
            para.Add(db.CreateParam("UserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, LoginUserid));
            System.Data.DataTable dt1 = db.ExecuteSP("UserLogin_Where", para.ToArray(), false);

            if (dt1.Rows[0]["LoginType"] != null && dt1.Rows[0]["LoginType"].ToString() != "")
            {
                if (dt1.Rows[0]["LoginType"].ToString().ToUpper() == "WEB")
                {
                    if (MailType == "HOLD")
                    {
                        loSb.Append(@"<p>Thank you for hold stone from our website www.sunrisediamonds.com.hk</p>");
                    }
                    else if (MailType == "RELEASE")
                    {
                        loSb.Append(@"<p>Thank you for release stone from our website www.sunrisediamonds.com.hk</p>");
                    }
                }
                else
                {
                    if (MailType == "HOLD")
                    {
                        loSb.Append(@"<p>Thank you for hold stone from our application</p>");
                    }
                    else if (MailType == "RELEASE")
                    {
                        loSb.Append(@"<p>Thank you for release stone from our application</p>");
                    }
                }
            }

            loSb.Append(EmailSignature());

            if (fsToAdd != null)
            {
                string sub = "";
                if (MailType == "HOLD")
                {
                    sub = "Sunrise Diamonds – Hold Stone – ";
                }
                else if (MailType == "RELEASE")
                {
                    sub = "Sunrise Diamonds – Release Stone – ";
                }
                SendHoldReleaseMail(MailType, fsToAdd, sub + Convert.ToDateTime(fdtOrderDate).ToString("dd-MMM-yyyy hh:mm:ss"),
                Convert.ToString(loSb), cc, StoneID, fiUserCode, LoginUserid);
            }
            return true;
        }

        //start old emailneworder function commented on 04/03/2020 as per TJ

        //public static bool EmailNewOrder(int fiUserCode, string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
        //        string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, string bcc = null, string cc = null)
        //{

        //    Database db = new Database();
        //    System.Collections.Generic.List<System.Data.IDbDataParameter> para;
        //    para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

        //    para.Clear();
        //    para.Add(db.CreateParam("iiUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(fiUserCode)));
        //    System.Data.DataTable dtUserDetail = db.ExecuteSP("UserMas_SelectOne", para.ToArray(), false);

        //    string _AssistBy = "";
        //    string mob_AssistBy = "";
        //    string Email_AssistBy = "";

        //    if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["AssistBy1"].ToString()) || !string.IsNullOrEmpty(dtUserDetail.Rows[0]["AssistBy2"].ToString()))
        //    {
        //        if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["AssistBy1"].ToString()))
        //        {
        //            _AssistBy = dtUserDetail.Rows[0]["AssistBy1"].ToString();
        //        }

        //        if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["AssistBy2"].ToString()))
        //        {
        //            if (_AssistBy != "")
        //                _AssistBy = _AssistBy + " & " +dtUserDetail.Rows[0]["AssistBy2"].ToString();
        //            else
        //                _AssistBy = dtUserDetail.Rows[0]["AssistBy2"].ToString();
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["mob_AssistBy1"].ToString()) || !string.IsNullOrEmpty(dtUserDetail.Rows[0]["mob_AssistBy2"].ToString()))
        //    {
        //        if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["mob_AssistBy1"].ToString()))
        //        {
        //            mob_AssistBy = dtUserDetail.Rows[0]["mob_AssistBy1"].ToString();
        //        }

        //        if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["mob_AssistBy2"].ToString()))
        //        {
        //            if (mob_AssistBy != "")
        //                mob_AssistBy = mob_AssistBy + " & " + dtUserDetail.Rows[0]["mob_AssistBy2"].ToString();
        //            else
        //                mob_AssistBy = dtUserDetail.Rows[0]["mob_AssistBy2"].ToString();
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["Email_AssistBy1"].ToString()) || !string.IsNullOrEmpty(dtUserDetail.Rows[0]["Email_AssistBy2"].ToString()))
        //    {
        //        if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["Email_AssistBy1"].ToString()))
        //        {
        //            Email_AssistBy = dtUserDetail.Rows[0]["Email_AssistBy1"].ToString();
        //        }

        //        if (!string.IsNullOrEmpty(dtUserDetail.Rows[0]["Email_AssistBy2"].ToString()))
        //        {
        //            if (Email_AssistBy != "")
        //                Email_AssistBy = Email_AssistBy + " & " + dtUserDetail.Rows[0]["Email_AssistBy2"].ToString();
        //            else
        //                Email_AssistBy = dtUserDetail.Rows[0]["Email_AssistBy2"].ToString();
        //        }
        //    }


        //    //try
        //    //{
        //    StringBuilder loSb = new StringBuilder();
        //   // loSb.Append(EmailHeader());

        //    loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsFullname + ",</p>");
        //    loSb.Append(@"<p>We thank you for your order at SUNRISE DIAMONDS.</p>");

        //    loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
        //    loSb.Append(@"<tr><td width=""170px"">Order No:</td><td>" + fiOrderNo.ToString() + "</td></tr>");
        //    loSb.Append(@"<tr><td>Order Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
        //    loSb.Append(@"<tr><td>Order Status:</td><td>" + getStatus(fiOrderStauts) + "</td></tr>");
        //    if (!string.IsNullOrEmpty(_AssistBy))
        //        loSb.Append(@"<tr><td>Key Account Manager:</td><td>" + _AssistBy + "</td></tr>");
        //    if (!string.IsNullOrEmpty(mob_AssistBy))
        //        loSb.Append(@"<tr><td>Contact Number:</td><td>" + mob_AssistBy + "</td></tr>");
        //    if (!string.IsNullOrEmpty(Email_AssistBy))
        //        loSb.Append(@"<tr><td>Email ID:</td><td>" + Email_AssistBy + "</td></tr>");

        //    loSb.Append(@"<tr><td colspan=""2""><br/> <br/> Please find the details of your order in the attachment.</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + "Our representative will get in touch with you to further the order process." + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + "We, at Sunrise Diamonds value our relationship with you." + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + "We thank you for your business." + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + "<br>Thanking you," + "</td></tr>");
        //    loSb.Append(@"<tr><td colspan=""2"">" + "The Sunrise Diamonds Team" + "</td></tr>");
        //    loSb.Append("</table>");

        //    //loSb.Append(@"<p>Thank you for interesting and ordering with us.<br />");
        //    //loSb.Append(@"<a href=""https://sunrisediamonds.com.hk/ViewOrder.aspx/Id=" + fiOrderNo.ToString() + @""">Click here</a> to know your current order status.</p>");
        //   // loSb.Append(EmailSignature());
        //    if (cc == null && bcc == null)
        //    {
        //        SendMail(fsToAdd, "SUNRISE DIAMONDS : Acknowledgement of Receipt of Order – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
        //        Convert.ToString(loSb), null, fiOrderNo, fiUserCode, false, true);
        //    }
        //    else
        //    {
        //       ConfirmOrderSendMail(fsToAdd, "SUNRISE DIAMONDS : Acknowledgement of Receipt of Order – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
        //        Convert.ToString(loSb), bcc,cc, fiOrderNo, fiUserCode, false, true);
        //    }
        //    //return Convert.ToString(loSb);
        //    return true;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //}
        //}

        //end old emailneworder function commented on 04/03/2020 as per TJ

        //public static bool EmailNewOrderToAdmin(int fiUserCode, string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
        //    string fsCompName, string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote)
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
        //    loSb.Append(@"<tr><td>Order Date:</td><td>" + fdtOrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");
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

        //    SendMail(fsToAdd, "Sunrise Diamonds – Order Confirmation – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(),
        //    Convert.ToString(loSb), null, fiOrderNo, fiUserCode, true, true);
        //    return true;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //}
        //}

        public static bool EmailForgotPassword(string fsToAdd, string fsName, string fsUsername, string fsPassword)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
                loSb.Append(@"<p>Thank you for requesting your account detail.</p>");
                loSb.Append(@"<p>Please store below information for further communication.<br />");
                loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                loSb.Append("<b>Password: </b>" + fsPassword + "<br /><br /></p>");

                loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Bhavyam Diam – Forgot Password – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, null, false, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool EmailError(string Message, string StackTrace, string UserName, string DeviceType)
        {
            try
            {

                MailMessage loMail = new MailMessage();
                SmtpClient loSmtp = new SmtpClient();
                try
                {
                    StringBuilder loSb = new StringBuilder();
                    loSb.Append(EmailHeader());

                    loSb.Append(@"<p>Error raise from Sunrise Diamond.</p>");
                    loSb.Append(@"<b>Date Time: </b>" + Lib.Models.Common.GetHKTime().ToString() + "<br />");
                    loSb.Append(@"<b>User Name: </b>" + UserName + "<br />");
                    loSb.Append(@"<b>DeviceType: </b>" + DeviceType + "<br />");
                    loSb.Append(@"<b>Message: </b>" + Message + "<br />");
                    loSb.Append(@"<b>Trace: </b>" + StackTrace + "<br />");

                    if (UserName != "" && UserName != null)
                        loSb.Append(@"<b>Username: </b>" + UserName + "<br />");
                    else
                        loSb.Append(@"<b>Username: </b>" + HttpContext.Current.User.Identity.Name + "<br />");

                    loSb.Append(EmailSignature());

                    loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                    loMail.To.Add(ConfigurationManager.AppSettings["BCCEmail"]);

                    loMail.Subject = "Error - Sunrise  " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss");
                    loMail.IsBodyHtml = true;

                    AlternateView av = AlternateView.CreateAlternateViewFromString(Convert.ToString(loSb), null, MediaTypeNames.Text.Html);
                    loMail.AlternateViews.Add(av);

                    loSmtp.Send(loMail);
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        // Change By Hitesh on [29-05-2017] as per rahul bcoz only order mail can not sent tejashbhai
        public static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, int? fiOrderId, int? fiUserCode, bool AdminMail, bool bIsOrder, string who = null)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam");

                if (who == "Admin")
                {
                    loMail.To.Add(fsCCAdd);
                }
                else if (who == "Customer")
                {
                    loMail.To.Add(fsToAdd);
                }
                else
                {
                    loMail.To.Add(fsToAdd);

                    if (!string.IsNullOrEmpty(fsCCAdd))
                        loMail.Bcc.Add(fsCCAdd);
                }

                if (bIsOrder == false)
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                }
                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                //// Add Attechment (Customer Order)
                if (fiOrderId != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                    {
                        System.IO.MemoryStream ms = ExportToStreamEpPlus(Convert.ToInt32(fiOrderId), Convert.ToInt32(fiUserCode), AdminMail);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), AdminMail);
                        Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xls", "application/vnd.ms-excel");
                        //Attachment attachFile = new Attachment(ms, "SD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }

                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                    if (fiOrderId != null)
                    {
                        if (AdminMail == false)
                        {
                            //OrderMasDataContext loOrderMasDataContext = new OrderMasDataContext();
                            DBObjects dbObj = ApplicationData.GetDataObjects(); //For adding prifix of SP.                    
                            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(dbObj.GetProcedureNamePrefix() + "OrderMas_Update_CustMail_Status", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@iOrderId", Convert.ToInt32(fiOrderId));
                                    cmd.Parameters.AddWithValue("@bCustMailFlag", true);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }


                            // loOrderMasDataContext.OrderMas_Update_CustMail_Status(Convert.ToInt32(fiOrderId), true);
                        }
                        else if (AdminMail == true)
                        {
                            //OrderMasDataContext loOrderMasDataContext = new OrderMasDataContext();
                            //loOrderMasDataContext.OrderMas_Update_AdminMail_Status(Convert.ToInt32(fiOrderId), true);
                            DBObjects dbObj = ApplicationData.GetDataObjects(); //For adding prifix of SP.               
                            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(dbObj.GetProcedureNamePrefix() + "OrderMas_Update_AdminMail_Status", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@iOrderId", Convert.ToInt32(fiOrderId));
                                    cmd.Parameters.AddWithValue("@bAdminMailFlag", true);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {

                    email.Abort();
                }
                ///////////////////////
            }


            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static void SendHoldReleaseMail(string MailType, string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, string StoneId, int? fiUserCode, int? LoginUserid)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");

                loMail.To.Add(fsToAdd);

                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.CC.Add(fsCCAdd);

                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                if (StoneId != null)
                {
                    System.IO.MemoryStream ms = HoldRelease_ExportToStreamEpPlus(StoneId, fiUserCode, LoginUserid);
                    string sub = "";
                    if (MailType == "HOLD")
                    {
                        sub = "SD_HoldStone_";
                    }
                    else if (MailType == "RELEASE")
                    {
                        sub = "SD_ReleaseStone_";
                    }
                    Attachment attachFile = new Attachment(ms, sub + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    loMail.Attachments.Add(attachFile);
                }
                loSmtp.Send(loMail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConfirmOrderSendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsBCCAdd, string fsCCAdd, long? fiOrderId, int? fiUserCode, bool AdminMail, bool bIsOrder)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Bhavyam Diam");

                loMail.To.Add(fsToAdd);

                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.CC.Add(fsCCAdd);

                if (!string.IsNullOrEmpty(fsBCCAdd))
                    loMail.Bcc.Add(fsBCCAdd);

                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                if (fiOrderId != null)
                {
                    System.IO.MemoryStream ms = ExportToStreamEpPlus(Convert.ToInt32(fiOrderId), Convert.ToInt32(fiUserCode), AdminMail);
                    Attachment attachFile = new Attachment(ms, "BD_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + "-" + fiOrderId + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    loMail.Attachments.Add(attachFile);
                }

                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                    if (fiOrderId != null)
                    {
                        if (AdminMail == false)
                        {
                            DBObjects dbObj = ApplicationData.GetDataObjects();
                            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(dbObj.GetProcedureNamePrefix() + "OrderMas_Update_CustMail_Status", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@iOrderId", Convert.ToInt32(fiOrderId));
                                    cmd.Parameters.AddWithValue("@bCustMailFlag", true);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                        else if (AdminMail == true)
                        {
                            DBObjects dbObj = ApplicationData.GetDataObjects();
                            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(dbObj.GetProcedureNamePrefix() + "OrderMas_Update_AdminMail_Status", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@iOrderId", Convert.ToInt32(fiOrderId));
                                    cmd.Parameters.AddWithValue("@bAdminMailFlag", true);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {
                    email.Abort();
                }
            }
            catch (Exception ex)
            {
                DAL.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }

        private static System.IO.MemoryStream ExportToStream(int fiOrderId, bool AdminMail, string who = null)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("iOrderId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(fiOrderId)));
            System.Data.DataTable dtOrderDetail = db.ExecuteSP("OrderDet_SelectAllByOrderId", para.ToArray(), false);


            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = false;
            gvData.ShowFooter = true;

            BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
            gvData.Columns.Add(sStoneStatus);

            BoundField sSupplLocation = new BoundField(); sSupplLocation.HeaderText = "Location"; sSupplLocation.DataField = "sSupplLocation";
            gvData.Columns.Add(sSupplLocation);

            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
            gvData.Columns.Add(sstatus);

            decimal dlTotalRapAmount = Convert.ToDecimal(dtOrderDetail.Compute("sum(dRapAmount)", ""));
            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
            gvData.Columns.Add(cRefNo);

            HyperLinkField cImage = new HyperLinkField();
            cImage.HeaderText = "Image";
            cImage.DataTextField = "img";
            cImage.DataNavigateUrlFields = new String[] { "image_url" };
            cImage.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cImage);

            HyperLinkField cHd = new HyperLinkField();
            cHd.HeaderText = "Video";
            cHd.DataTextField = "movie";
            cHd.DataNavigateUrlFields = new String[] { "movie_url" };
            cHd.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cHd);

            BoundField cBGM = new BoundField(); cBGM.DataField = "BGM"; cBGM.HeaderText = "BGM";
            gvData.Columns.Add(cBGM);

            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
            gvData.Columns.Add(cShape);

            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
            cColor.FooterText = "Pcs";
            gvData.Columns.Add(cColor);

            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
            cClarity.FooterText = dtOrderDetail.Compute("COUNT(sClarity)", "").ToString();
            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            gvData.Columns.Add(cClarity);

            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            gvData.Columns.Add(cCertiNo);

            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
            gvData.Columns.Add(cPointer);

            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
            cCarats.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(dCts)", "")).ToString("#,##0.00");
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

            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Offer Disc.(%)"; // "Disc(%)";
            if (dlTotalRapAmount == 0)
                cDisc.FooterText = @"0.00";
            else
                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");

            cDisc.ItemStyle.CssClass = "twoDigit-red";
            cDisc.FooterStyle.CssClass = "twoDigit-red";
            gvData.Columns.Add(cDisc);


            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Offer Value($)"; // "Net Amt($)";
            //cNetPrice.FooterText = loOrderDet.Sum(r => r.dNetPrice).Value.ToString("#,##0.00");
            cNetPrice.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", "")).ToString("#,##0.00");
            cNetPrice.ItemStyle.CssClass = "twoDigit";
            cNetPrice.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cNetPrice);

            BoundField Web_Benefit = new BoundField(); Web_Benefit.DataField = "Web_Benefit"; Web_Benefit.HeaderText = "Web Disc.($)";
            Web_Benefit.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(Web_Benefit)", "")).ToString("#,##0.00");
            Web_Benefit.ItemStyle.CssClass = "twoDigit";
            Web_Benefit.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(Web_Benefit);

            BoundField Net_Value = new BoundField(); Net_Value.DataField = "Net_Value"; Net_Value.HeaderText = "Final Value";
            Net_Value.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(Net_Value)", "")).ToString("#,##0.00");
            Net_Value.ItemStyle.CssClass = "twoDigit";
            Net_Value.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(Net_Value);

            decimal _Net_Value = Convert.ToDecimal(dtOrderDetail.Compute("sum(Net_Value)", ""));
            decimal _dRapAmount = Convert.ToDecimal(dtOrderDetail.Compute("sum(dRapAmount)", ""));
            decimal _F_Final_Disc = -(1 - (_Net_Value / _dRapAmount)) * 100;

            BoundField Final_Disc = new BoundField(); Final_Disc.DataField = "Final_Disc"; Final_Disc.HeaderText = "Final Disc.(%)";
            Final_Disc.FooterText = Convert.ToDecimal(_F_Final_Disc).ToString("#,##0.00");
            Final_Disc.ItemStyle.CssClass = "twoDigit";
            Final_Disc.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(Final_Disc);

            HyperLinkField cLab = new HyperLinkField();
            cLab.HeaderText = "Lab";
            cLab.DataTextField = "sLab";
            cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
            cLab.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cLab);

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

            BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
            gvData.Columns.Add(cCrownInclusion);

            BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
            gvData.Columns.Add(cTableNatts);

            //--By Aniket [11-06-15]          

            BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
            gvData.Columns.Add(cCrownNatts);


            //--Over [11-06-15]


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

            //if (who == "Admin")
            //{
            //    BoundField Supplier = new BoundField(); Supplier.HeaderText = "Supplier"; Supplier.DataField = "Supplier";
            //    gvData.Columns.Add(Supplier);

            //    BoundField Supplier_Stone_No = new BoundField(); Supplier_Stone_No.HeaderText = "Supplier Stone No"; Supplier_Stone_No.DataField = "Supplier_Stone_No";
            //    gvData.Columns.Add(Supplier_Stone_No);
            //}

            gvData.DataSource = dtOrderDetail;
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

        private static System.IO.MemoryStream ExportToStreamEpPlus(int fiOrderId, int fiUserCode, bool AdminMail)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("iOrderId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(fiOrderId)));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(fiUserCode)));
            System.Data.DataTable dtOrderDetail = db.ExecuteSP("OrderDet_SelectAllByOrderId_Sunrise", para.ToArray(), false);

            //string iUserType = dtOrderDetail.Rows[0]["iUserType"].ToString();

            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = false;
            gvData.ShowFooter = true;

            //BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
            //gvData.Columns.Add(sStoneStatus);

            //BoundField sSupplLocation = new BoundField(); sSupplLocation.HeaderText = "Location"; sSupplLocation.DataField = "sSupplLocation";
            //gvData.Columns.Add(sSupplLocation);

            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
            gvData.Columns.Add(sstatus);

            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
            gvData.Columns.Add(cRefNo);

            //HyperLinkField cImage = new HyperLinkField();
            //cImage.HeaderText = "Image";
            //cImage.DataTextField = "img";
            //cImage.DataNavigateUrlFields = new String[] { "image_url" };
            //cImage.DataNavigateUrlFormatString = "{0}";
            //gvData.Columns.Add(cImage);

            HyperLinkField cImg = new HyperLinkField();
            cImg.HeaderText = "Image";
            cImg.DataTextField = "img";
            cImg.DataNavigateUrlFields = new String[] { "image_url" };
            cImg.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cImg); 
            
            HyperLinkField cVdo = new HyperLinkField();
            cVdo.HeaderText = "Video";
            cVdo.DataTextField = "movie";
            cVdo.DataNavigateUrlFields = new String[] { "movie_url" };
            cVdo.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cVdo);

            HyperLinkField cCert = new HyperLinkField();
            cCert.HeaderText = "Certi";
            cCert.DataTextField = "certi";
            cCert.DataNavigateUrlFields = new String[] { "certi_url" };
            cCert.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cCert);

            HyperLinkField cDNA = new HyperLinkField();
            cDNA.HeaderText = "DNA";
            cDNA.DataTextField = "dna";
            cDNA.DataNavigateUrlFields = new String[] { "view_dna" };
            cDNA.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cDNA);

            //BoundField cBGM = new BoundField(); cBGM.DataField = "BGM"; cBGM.HeaderText = "BGM";
            //gvData.Columns.Add(cBGM);

            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
            gvData.Columns.Add(cShape);

            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
            cColor.FooterText = "Pcs";
            gvData.Columns.Add(cColor);

            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
            cClarity.FooterText = dtOrderDetail.Compute("COUNT(sClarity)", "").ToString();
            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            gvData.Columns.Add(cClarity);

            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            gvData.Columns.Add(cCertiNo);

            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
            gvData.Columns.Add(cPointer);

            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
            cCarats.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(dCts)", "")).ToString("#,##0.00");
            cCarats.ItemStyle.CssClass = "twoDigit";
            cCarats.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cCarats);


            BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
            cRepPrice.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cRepPrice);

            //decimal dlTotalRapAmount = Convert.ToDecimal(dtOrderDetail.Compute("sum(dRapAmount)", ""));
            decimal dlTotalRapAmount = Convert.ToDecimal((dtOrderDetail.Compute("sum(dRapAmount)", "").ToString() != "" && dtOrderDetail.Compute("sum(dRapAmount)", "").ToString() != null ? dtOrderDetail.Compute("sum(dRapAmount)", "") : 0));
            decimal dlTotaldNetPrice = Convert.ToDecimal((dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != "" && dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != null ? dtOrderDetail.Compute("sum(dNetPrice)", "") : 0));

            BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
            cRepAmount.ItemStyle.CssClass = "twoDigit";
            cRepAmount.FooterStyle.CssClass = "twoDigit";

            cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
            gvData.Columns.Add(cRepAmount);

            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Offer Disc.(%)"; //"Disc(%)";
            if (dlTotalRapAmount == 0)
                cDisc.FooterText = @"0.00";
            else
                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                //cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.FooterText = ((dlTotalRapAmount - dlTotaldNetPrice) * -100 / dlTotalRapAmount).ToString("0.00");

            cDisc.ItemStyle.CssClass = "twoDigit-red";
            cDisc.FooterStyle.CssClass = "twoDigit-red";
            gvData.Columns.Add(cDisc);


            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Offer Value($)"; //"Net Amt($)";
            //cNetPrice.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", "")).ToString("#,##0.00");
            cNetPrice.FooterText = Convert.ToDecimal((dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != "" && dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != null ? dtOrderDetail.Compute("sum(dNetPrice)", "") : 0)).ToString("#,##0.00");
            cNetPrice.ItemStyle.CssClass = "twoDigit";
            cNetPrice.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cNetPrice);

            //BoundField Web_Benefit = new BoundField(); Web_Benefit.DataField = "Web_Benefit"; Web_Benefit.HeaderText = "Web Disc.($)";
            ////Web_Benefit.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(Web_Benefit)", "")).ToString("#,##0.00");
            //Web_Benefit.FooterText = Convert.ToDecimal((dtOrderDetail.Compute("sum(Web_Benefit)", "").ToString() != "" && dtOrderDetail.Compute("sum(Web_Benefit)", "").ToString() != null ? dtOrderDetail.Compute("sum(Web_Benefit)", "") : 0)).ToString("#,##0.00");
            //Web_Benefit.ItemStyle.CssClass = "twoDigit";
            //Web_Benefit.FooterStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(Web_Benefit);

            //BoundField Net_Value = new BoundField(); Net_Value.DataField = "Net_Value"; Net_Value.HeaderText = "Final Value";
            ////Net_Value.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(Net_Value)", "")).ToString("#,##0.00");
            //Net_Value.FooterText = Convert.ToDecimal((dtOrderDetail.Compute("sum(Net_Value)", "").ToString() != "" && dtOrderDetail.Compute("sum(Net_Value)", "").ToString() != null ? dtOrderDetail.Compute("sum(Net_Value)", "") : 0)).ToString("#,##0.00");
            //Net_Value.ItemStyle.CssClass = "twoDigit";
            //Net_Value.FooterStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(Net_Value);

            ////decimal _Net_Value = Convert.ToDecimal(dtOrderDetail.Compute("sum(Net_Value)", ""));
            //decimal _Net_Value = Convert.ToDecimal((dtOrderDetail.Compute("sum(Net_Value)", "").ToString() != "" && dtOrderDetail.Compute("sum(Net_Value)", "").ToString() != null ? dtOrderDetail.Compute("sum(Net_Value)", "") : 0));
            ////decimal _dRapAmount = Convert.ToDecimal(dtOrderDetail.Compute("sum(dRapAmount)", ""));
            //decimal _dRapAmount = Convert.ToDecimal((dtOrderDetail.Compute("sum(dRapAmount)", "").ToString() != "" && dtOrderDetail.Compute("sum(dRapAmount)", "").ToString() != null ? dtOrderDetail.Compute("sum(dRapAmount)", "") : 0));
            ////decimal _F_Final_Disc = -(1 - (_Net_Value / _dRapAmount)) * 100;
            //decimal _F_Final_Disc = (_dRapAmount != 0 ? -(1 - (_Net_Value / _dRapAmount)) * 100 : 0);

            //BoundField Final_Disc = new BoundField(); Final_Disc.DataField = "Final_Disc"; Final_Disc.HeaderText = "Final Disc.(%)";
            //Final_Disc.FooterText = Convert.ToDecimal(_F_Final_Disc).ToString("#,##0.00");
            //Final_Disc.ItemStyle.CssClass = "twoDigit";
            //Final_Disc.FooterStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(Final_Disc);



            //HyperLinkField cLab = new HyperLinkField();
            //cLab.HeaderText = "Lab";
            //cLab.DataTextField = "sLab";
            //cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
            //cLab.DataNavigateUrlFormatString = "{0}";
            //gvData.Columns.Add(cLab);

            BoundField cLab = new BoundField(); cLab.DataField = "sLab"; cLab.HeaderText = "Lab";
            gvData.Columns.Add(cLab);

            BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
            cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
            gvData.Columns.Add(cCut);

            BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
            gvData.Columns.Add(cPolish);

            BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
            gvData.Columns.Add(cSymm);

            BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
            gvData.Columns.Add(cFls);

            BoundField cMeasurement = new BoundField(); cMeasurement.DataField = "Measurement"; cMeasurement.HeaderText = "Measurement";
            gvData.Columns.Add(cMeasurement);

            //BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
            //cLength.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cLength);

            //BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
            //cWidth.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cWidth);

            //BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
            //cDepth.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cDepth);

            BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
            gvData.Columns.Add(cTablePer);

            BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
            cDepthPer.ItemStyle.CssClass = "oneDigit";
            gvData.Columns.Add(cDepthPer);



            //BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
            //gvData.Columns.Add(cSymbol);

            //BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
            //gvData.Columns.Add(cInclusion);

            //BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
            //gvData.Columns.Add(cCrownInclusion);

            //BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
            //gvData.Columns.Add(cTableNatts);

            ////--By Aniket [11-06-15]          

            //BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
            //gvData.Columns.Add(cCrownNatts);


            ////--Over [11-06-15]


            //BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
            //cCrAng.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cCrAng);

            //BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
            //cCrHt.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cCrHt);

            //BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
            //cPavAng.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cPavAng);

            //BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
            //cPavHt.ItemStyle.CssClass = "twoDigit";
            //gvData.Columns.Add(cPavHt);

            ////if (iUserType == "3")
            ////{
            //BoundField Table_Open = new BoundField(); Table_Open.DataField = "Table_Open"; Table_Open.HeaderText = "Table Open";
            //gvData.Columns.Add(Table_Open);

            //BoundField Crown_Open = new BoundField(); Crown_Open.DataField = "Crown_Open"; Crown_Open.HeaderText = "Crown Open";
            //gvData.Columns.Add(Crown_Open);

            //BoundField Pav_Open = new BoundField(); Pav_Open.DataField = "Pav_Open"; Pav_Open.HeaderText = "Pav Open";
            //gvData.Columns.Add(Pav_Open);

            //BoundField Girdle_Open = new BoundField(); Girdle_Open.DataField = "Girdle_Open"; Girdle_Open.HeaderText = "Girdle Open";
            //gvData.Columns.Add(Girdle_Open);
            ////}

            //BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
            //gvData.Columns.Add(cGirdle);

            ////if (iUserType == "3")
            ////{
            //BoundField sInscription = new BoundField(); sInscription.DataField = "sInscription"; sInscription.HeaderText = "Laser Insc.";
            //gvData.Columns.Add(sInscription);
            ////}

            //if (who == "Admin")
            //{
            //    BoundField Supplier = new BoundField(); Supplier.HeaderText = "Supplier"; Supplier.DataField = "Supplier";
            //    gvData.Columns.Add(Supplier);

            //    BoundField Supplier_Stone_No = new BoundField(); Supplier_Stone_No.HeaderText = "Supplier Stone No"; Supplier_Stone_No.DataField = "Supplier_Stone_No";
            //    gvData.Columns.Add(Supplier_Stone_No);
            //}
            ///////////////////////////////////////////////////////////////////////////////

            //BoundField cShade = new BoundField(); cShade.DataField = "sShade"; cShade.HeaderText = "Shade";
            //gvData.Columns.Add(cShade);





            ////--By Aniket [20-08-15]
            //if (AdminMail == true)
            //{
            //    BoundField sSupplDisc = new BoundField(); sSupplDisc.DataField = "sSupplDisc"; sSupplDisc.HeaderText = "Org Disc(%)";
            //    if (dlTotalRapAmount == 0)
            //        sSupplDisc.FooterText = @"0.00";
            //    else

            //        sSupplDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
            //    sSupplDisc.ItemStyle.CssClass = "twoDigit-red";
            //    sSupplDisc.FooterStyle.CssClass = "twoDigit-red";
            //    gvData.Columns.Add(sSupplDisc);

            //    BoundField sSupplNetVal = new BoundField(); sSupplNetVal.DataField = "sSupplNetVal"; sSupplNetVal.HeaderText = "Org Value($)";
            //    string SuppDisc = null;
            //    if (dtOrderDetail.Compute("sum(sSupplDisc)", "").ToString() == "")
            //    {
            //        sSupplNetVal.FooterText = @"0.00";
            //    }
            //    else
            //    {
            //        SuppDisc = Convert.ToDecimal(dtOrderDetail.Compute("sum(sSupplDisc)", "")).ToString("#,##0.00");
            //        sSupplNetVal.FooterText = SuppDisc;
            //    }
            //    sSupplNetVal.ItemStyle.CssClass = "twoDigit";
            //    sSupplNetVal.FooterStyle.CssClass = "twoDigit";
            //    gvData.Columns.Add(sSupplNetVal);
            //}
            ////--Over [20-08-15]


            //BoundField cLuster = new BoundField(); cLuster.DataField = "sLuster"; cLuster.HeaderText = "Luster/Milky";
            //gvData.Columns.Add(cLuster);



            ////change by Hitesh on [31-03-2016] as per [Doc No 201]
            //BoundField SINSCRIPTION = new BoundField(); SINSCRIPTION.HeaderText = "Laser Insc"; SINSCRIPTION.DataField = "sInscription";
            //gvData.Columns.Add(SINSCRIPTION);
            ////change by Hitesh on [31-03-2016] as per [Doc No 201]


            gvData.DataSource = dtOrderDetail;

            gvData.DataBind();

            foreach (GridViewRow gvr in gvData.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    var data = gvr.DataItem as DataRowView;
                    //if (data.DataView.Table.Columns["Cut"] != null)
                    //{
                    int cutIndex = GetColumnIndexByName(gvr, "sCut");
                    string cellContent = gvr.Cells[cutIndex].Text;
                    if (cellContent.ToLower() == "3ex")
                    {
                        int polishIndex = GetColumnIndexByName(gvr, "sPolish");
                        gvr.Cells[polishIndex].CssClass = "bold";

                        int symmIndex = GetColumnIndexByName(gvr, "sSymm");
                        gvr.Cells[symmIndex].CssClass = "bold";
                        // }
                    }
                }
            }
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
            string parentpath = HttpContext.Current.Server.MapPath("~/Temp/Excel/");
            if (System.Configuration.ConfigurationManager.AppSettings["ConnMode"] == "Oracle")
                parentpath = @"C:\inetpub\wwwroot\Temp\";
            ep_ge.CreateExcel(ms, parentpath);

            System.IO.MemoryStream memn = new System.IO.MemoryStream();

            byte[] byteDatan = ms.ToArray();
            memn.Write(byteDatan, 0, byteDatan.Length);
            memn.Flush();
            memn.Position = 0;
            //memn.Close();
            return memn;
        }
        private static System.IO.MemoryStream HoldRelease_ExportToStreamEpPlus(string StoneId, int? fiUserCode, int? LoginUserid)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("sRefNo", System.Data.DbType.String, System.Data.ParameterDirection.Input, StoneId));
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiUserCode));

            System.Data.DataTable dtHoldDetail = db.ExecuteSP("HoldDet_SelectAllByRefNo_Sunrise", para.ToArray(), false);

            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = false;
            gvData.ShowFooter = true;

            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
            gvData.Columns.Add(cRefNo);

            BoundField sSupplLocation = new BoundField(); sSupplLocation.HeaderText = "Location"; sSupplLocation.DataField = "sSupplLocation";
            gvData.Columns.Add(sSupplLocation);

            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Stone Status"; sstatus.DataField = "sStatus";
            gvData.Columns.Add(sstatus);

            HyperLinkField cImage = new HyperLinkField();
            cImage.HeaderText = "Image";
            cImage.DataTextField = "img";
            cImage.DataNavigateUrlFields = new String[] { "image_url" };
            cImage.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cImage);

            HyperLinkField cHd = new HyperLinkField();
            cHd.HeaderText = "Video";
            cHd.DataTextField = "movie";
            cHd.DataNavigateUrlFields = new String[] { "movie_url" };
            cHd.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cHd);

            BoundField cBGM = new BoundField(); cBGM.DataField = "BGM"; cBGM.HeaderText = "BGM";
            gvData.Columns.Add(cBGM);

            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
            gvData.Columns.Add(cShape);

            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
            cColor.FooterText = "Pcs";
            gvData.Columns.Add(cColor);

            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
            cClarity.FooterText = dtHoldDetail.Compute("COUNT(sClarity)", "").ToString();
            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            gvData.Columns.Add(cClarity);

            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            gvData.Columns.Add(cCertiNo);

            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
            gvData.Columns.Add(cPointer);

            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
            cCarats.FooterText = Convert.ToDecimal(dtHoldDetail.Compute("sum(dCts)", "")).ToString("#,##0.00");
            cCarats.ItemStyle.CssClass = "twoDigit";
            cCarats.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cCarats);

            BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
            cRepPrice.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cRepPrice);

            decimal dlTotalRapAmount = Convert.ToDecimal((dtHoldDetail.Compute("sum(dRapAmount)", "").ToString() != "" && dtHoldDetail.Compute("sum(dRapAmount)", "").ToString() != null ? dtHoldDetail.Compute("sum(dRapAmount)", "") : 0));
            decimal dlTotaldNetPrice = Convert.ToDecimal((dtHoldDetail.Compute("sum(dNetPrice)", "").ToString() != "" && dtHoldDetail.Compute("sum(dNetPrice)", "").ToString() != null ? dtHoldDetail.Compute("sum(dNetPrice)", "") : 0));

            BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
            cRepAmount.ItemStyle.CssClass = "twoDigit";
            cRepAmount.FooterStyle.CssClass = "twoDigit";

            cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
            gvData.Columns.Add(cRepAmount);

            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Offer Disc.(%)"; //"Disc(%)";
            if (dlTotalRapAmount == 0)
                cDisc.FooterText = @"0.00";
            else
                cDisc.FooterText = ((dlTotalRapAmount - dlTotaldNetPrice) * -100 / dlTotalRapAmount).ToString("0.00");

            cDisc.ItemStyle.CssClass = "twoDigit-red";
            cDisc.FooterStyle.CssClass = "twoDigit-red";
            gvData.Columns.Add(cDisc);


            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Offer Value($)"; //"Net Amt($)";
            cNetPrice.FooterText = Convert.ToDecimal((dtHoldDetail.Compute("sum(dNetPrice)", "").ToString() != "" && dtHoldDetail.Compute("sum(dNetPrice)", "").ToString() != null ? dtHoldDetail.Compute("sum(dNetPrice)", "") : 0)).ToString("#,##0.00");
            cNetPrice.ItemStyle.CssClass = "twoDigit";
            cNetPrice.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cNetPrice);

            BoundField Web_Benefit = new BoundField(); Web_Benefit.DataField = "Web_Benefit"; Web_Benefit.HeaderText = "Web Disc.($)";
            Web_Benefit.FooterText = Convert.ToDecimal((dtHoldDetail.Compute("sum(Web_Benefit)", "").ToString() != "" && dtHoldDetail.Compute("sum(Web_Benefit)", "").ToString() != null ? dtHoldDetail.Compute("sum(Web_Benefit)", "") : 0)).ToString("#,##0.00");
            Web_Benefit.ItemStyle.CssClass = "twoDigit";
            Web_Benefit.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(Web_Benefit);

            BoundField Net_Value = new BoundField(); Net_Value.DataField = "Net_Value"; Net_Value.HeaderText = "Final Value";
            Net_Value.FooterText = Convert.ToDecimal((dtHoldDetail.Compute("sum(Net_Value)", "").ToString() != "" && dtHoldDetail.Compute("sum(Net_Value)", "").ToString() != null ? dtHoldDetail.Compute("sum(Net_Value)", "") : 0)).ToString("#,##0.00");
            Net_Value.ItemStyle.CssClass = "twoDigit";
            Net_Value.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(Net_Value);

            decimal _Net_Value = Convert.ToDecimal((dtHoldDetail.Compute("sum(Net_Value)", "").ToString() != "" && dtHoldDetail.Compute("sum(Net_Value)", "").ToString() != null ? dtHoldDetail.Compute("sum(Net_Value)", "") : 0));
            decimal _dRapAmount = Convert.ToDecimal((dtHoldDetail.Compute("sum(dRapAmount)", "").ToString() != "" && dtHoldDetail.Compute("sum(dRapAmount)", "").ToString() != null ? dtHoldDetail.Compute("sum(dRapAmount)", "") : 0));
            decimal _F_Final_Disc = (_dRapAmount != 0 ? -(1 - (_Net_Value / _dRapAmount)) * 100 : 0);

            BoundField Final_Disc = new BoundField(); Final_Disc.DataField = "Final_Disc"; Final_Disc.HeaderText = "Final Disc.(%)";
            Final_Disc.FooterText = Convert.ToDecimal(_F_Final_Disc).ToString("#,##0.00");
            Final_Disc.ItemStyle.CssClass = "twoDigit";
            Final_Disc.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(Final_Disc);

            HyperLinkField cLab = new HyperLinkField();
            cLab.HeaderText = "Lab";
            cLab.DataTextField = "sLab";
            cLab.DataNavigateUrlFields = new String[] { "view_dna" };
            cLab.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cLab);

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

            BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
            gvData.Columns.Add(cCrownInclusion);

            BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
            gvData.Columns.Add(cTableNatts);

            BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
            gvData.Columns.Add(cCrownNatts);

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

            BoundField Table_Open = new BoundField(); Table_Open.DataField = "Table_Open"; Table_Open.HeaderText = "Table Open";
            gvData.Columns.Add(Table_Open);

            BoundField Crown_Open = new BoundField(); Crown_Open.DataField = "Crown_Open"; Crown_Open.HeaderText = "Crown Open";
            gvData.Columns.Add(Crown_Open);

            BoundField Pav_Open = new BoundField(); Pav_Open.DataField = "Pav_Open"; Pav_Open.HeaderText = "Pav Open";
            gvData.Columns.Add(Pav_Open);

            BoundField Girdle_Open = new BoundField(); Girdle_Open.DataField = "Girdle_Open"; Girdle_Open.HeaderText = "Girdle Open";
            gvData.Columns.Add(Girdle_Open);

            BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
            gvData.Columns.Add(cGirdle);

            BoundField sInscription = new BoundField(); sInscription.DataField = "sInscription"; sInscription.HeaderText = "Laser Insc.";
            gvData.Columns.Add(sInscription);

            gvData.DataSource = dtHoldDetail;

            gvData.DataBind();

            foreach (GridViewRow gvr in gvData.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    var data = gvr.DataItem as DataRowView;
                    int cutIndex = GetColumnIndexByName(gvr, "sCut");
                    string cellContent = gvr.Cells[cutIndex].Text;
                    if (cellContent.ToLower() == "3ex")
                    {
                        int polishIndex = GetColumnIndexByName(gvr, "sPolish");
                        gvr.Cells[polishIndex].CssClass = "bold";

                        int symmIndex = GetColumnIndexByName(gvr, "sSymm");
                        gvr.Cells[symmIndex].CssClass = "bold";
                    }
                }
            }
            gvData.FooterStyle.Font.Bold = true;
            gvData.HeaderStyle.Font.Bold = true;

            GridViewEpExcelExport ep_ge;
            ep_ge = new GridViewEpExcelExport(gvData, "Order", "Order");

            ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
            ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
            ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;
            ep_ge.AddHeaderEvent += Ep_AddHeaderEventHandler;

            MemoryStream ms = new MemoryStream();
            string parentpath = HttpContext.Current.Server.MapPath("~/Temp/Excel/");
            if (System.Configuration.ConfigurationManager.AppSettings["ConnMode"] == "Oracle")
                parentpath = @"C:\inetpub\wwwroot\Temp\";
            ep_ge.CreateExcel(ms, parentpath);

            System.IO.MemoryStream memn = new System.IO.MemoryStream();

            byte[] byteDatan = ms.ToArray();
            memn.Write(byteDatan, 0, byteDatan.Length);
            memn.Flush();
            memn.Position = 0;
            return memn;
        }

        private static int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }

        private static UInt32 DiscNormalStyleindex;
        private static UInt32 CutNormalStyleindex;
        private static UInt32 NAOrderStatusIndex;
        private static UInt32 OfferDisc_OfferValue_Index;
        private static UInt32 WebDisc_Index;
        private static UInt32 FinalValue_FinalDisc_Index;
        private static UInt32 InscStyleindex;
        private static UInt32 TCPG_Index;
        private static void Ep_FillingWorksheetEventHandler(object sender, ref EpExcelExport.FillingWorksheetEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            EpExcelExport.ExcelFormat format = new EpExcelExport.ExcelFormat();
            Color _SKY_BLUE = System.Drawing.ColorTranslator.FromHtml("#ade0e9");
            Color _PISTA = System.Drawing.ColorTranslator.FromHtml("#ddeedf");
            Color _LIGHT_YELLOW = System.Drawing.ColorTranslator.FromHtml("#fdfdc1");
            Color _LIGHT_ORANGE = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            DiscNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Yellow.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            NAOrderStatusIndex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(_SKY_BLUE.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            OfferDisc_OfferValue_Index = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(_PISTA.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Blue.ToArgb());
            format.isbold = true;
            WebDisc_Index = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(_LIGHT_YELLOW.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Blue.ToArgb());
            format.isbold = true;
            FinalValue_FinalDisc_Index = ee.AddStyle(format);

            //change by Hitesh on 28-03-2016] as per [Doc No 201]
            format = new EpExcelExportLib.EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Blue.ToArgb());
            format.isbold = true;
            InscStyleindex = ee.AddStyle(format);
            //End by Hitesh on 28-03-2016] as per [Doc No 201]

            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(_LIGHT_ORANGE.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Black.ToArgb());
            TCPG_Index = ee.AddStyle(format);

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
            //if (e.ColName == "sRefNo")
            if (e.Caption == "Ref. No.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
            }
            //if (e.ColName == "sLab")
            if (e.Caption == "Lab")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6;
            }
            if (e.Caption == "Location")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
            }
            //if (e.ColName == "sShape")
            if (e.Caption == "Shape")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8;
            }
            //if (e.ColName == "sPointer")
            if (e.Caption == "Pointer")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10;
            }
            //if (e.ColName == "sCertiNo")
            if (e.Caption == "Certi No")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.NumFormat = "#0";
            }
            //if (e.ColName == "sColor")
            if (e.Caption == "Color")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7;
            }
            if (e.Caption == "BGM")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10;
            }
            //if (e.ColName == "sClarity")
            if (e.Caption == "Clarity")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7;
            }
            //if (e.ColName == "dCts")
            if (e.Caption == "Cts")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.2;
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
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "IF(" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + "=0,0," +
                               ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + ")";

                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "dDisc")
            if (e.Caption == "Offer Disc.(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.NumFormat = "#,##0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "IF(" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + "=0,0," +
                                "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Offer Value($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100)";
            }
            if (e.Caption == "Disc(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "IF(" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + "=0,0," +
                                "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Net Amt($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100)";

            }
            if (e.Caption == "Web Disc.($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
            if (e.Caption == "Final Disc.(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.NumFormat = "#,##0.00";
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "IF(" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + "=0,0," +
                                "-(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Final Value", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*100)";

            }
            if (e.Caption == "Final Value")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
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
            if (e.Caption == "Offer Value($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
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
                e.Width = 6;
            }
            //if (e.ColName == "sPolish")
            if (e.Caption == "Polish")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7;
            }
            //if (e.ColName == "sSymm")
            if (e.Caption == "Symm")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6;
            }
            //if (e.ColName == "sFls")
            if (e.Caption == "Fls")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7;
            }
            //if (e.ColName == "Measurement")
            if (e.Caption == "Measurement")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
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
                e.Width = 13;
            }
            //if (e.ColName == "sTableNatts")
            if (e.Caption == "Table Natts")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            //--By Aniket [11-06-15]
            //if (e.ColName == "sInclusion")
            if (e.Caption == "Crown Incl.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            //if (e.ColName == "sTableNatts")
            if (e.Caption == "Crown Natts")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            //if (e.ColName == "sTableNatts")
            if (e.Caption == "Luster/Milky")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //--Over [11-06-15]
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
            if (e.Caption == "Girdle")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "Girdle Type")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //if (e.ColName == "sStatus")
            if (e.Caption == "Status")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
            }
            if (e.Caption == "Stone Status")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Order Status")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 18;
            }
            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            if (e.Caption == "Image")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7.5;
            }
            if (e.Caption == "Video")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7.5;
            }
            if (e.Caption == "Certi")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7.5;
            }
            if (e.Caption == "DNA")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7.5;
            }
            if (e.Caption == "Laser Insc")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //if (e.ColName == "Table_Open")
            if (e.Caption == "Table Open")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            //if (e.ColName == "Crown_Open")
            if (e.Caption == "Crown Open")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            //if (e.ColName == "Pav_Open")
            if (e.Caption == "Pav Open")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            //if (e.ColName == "Girdle_Open")
            if (e.Caption == "Girdle Open")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }
            if (e.Caption == "Laser Insc.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 13;
            }

            //change by Hitesh on [31-03-2016] as per [Doc No 201]
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
                else if (e.ColumnName == "Offer Disc.(%)"
                    || e.ColumnName == "Offer Value($)")
                {
                    e.StyleInd = OfferDisc_OfferValue_Index;
                }
                else if (e.ColumnName == "Web Disc.($)")
                {
                    e.StyleInd = WebDisc_Index;
                }
                else if (e.ColumnName == "Final Value"
                    || e.ColumnName == "Final Disc.(%)")
                {
                    e.StyleInd = FinalValue_FinalDisc_Index;
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
                }
                //else if (e.ColumnName == "Order Status")
                //{
                //    if (e.Text == "NOT AVAILABLE")
                //        e.StyleInd = NAOrderStatusIndex;
                //}
                else if (e.ColumnName == "Order Status")
                {
                    if (e.Text.ToLower() == "not available" || e.Text.ToLower() == "checking avaibility")
                        e.StyleInd = NAOrderStatusIndex;
                }
                //change by Hitesh on [31-03-2016] as per [Doc No 201]
                else if (e.ColumnName == "Net Amt($)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
                else if (e.ColumnName == "Laser Insc")
                {
                    e.StyleInd = InscStyleindex;
                }
                else if (e.ColumnName == "Certi No")
                {
                    //if (e.Text.Contains("_TCPG"))
                    //{
                    //    e.StyleInd = TCPG_Index;
                    //}
                    e.Text = e.Text.Replace("_TCPG", "");
                }
                //else if (e.ColumnName == "Table Open" || e.ColumnName == "Crown Open" || e.ColumnName == "Pav Open" || e.ColumnName == "Girdle Open")
                //{
                //    if (e.Text.Contains("_1"))
                //    {
                //        e.StyleInd = TCPG_Index;
                //    }
                //    e.Text = e.Text.Replace("_1", "");
                //}
                //End by Hitesh on [31-03-2016] as per [Doc No 201]
            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
                //e.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                if (e.ColumnName == "Disc(%)" || e.ColumnName == "Offer Disc.(%)")
                {
                    //e.StyleInd = DiscNormalStyleindex;
                }
            }

        }
        //change by Hitesh on [31-03-2016] as per [Doc No 201]
        private static void Ep_AddHeaderEventHandler(object sender, ref EpExcelExportLib.EpExcelExport.AddHeaderEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            ee.AddNewRow("A1");
        }
        //change by Hitesh on [31-03-2016] as per [Doc No 201]
        public static String getStatus(byte fiStatus)
        {
            String lsStatus;
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

        public static bool EmailPurchaseOrder(string type, string when, string custMail, DateTime fdtOrderDate, string assistbyemail = null, string OrderId = null, string Refno = null, string Price = null, string Comments = null, string CompanyName = null, string UserName = null, string SupplierStatus = null, string SunriseStatus = null)
        {
            StringBuilder loSb = new StringBuilder();
            loSb.Append(EmailHeader());
            loSb.Append("<br/>");
            if (when.ToUpper() == "SUCCESS")
            {
                if (type.ToUpper() == "AUTO")
                {
                    loSb.Append(@"<p><b>Order Placed Successfully (Direct Order)</b></p>");
                }
                else if (type.ToUpper() == "MANUAL")
                {
                    loSb.Append(@"<p><b>Order Placed Successfully</b></p>");
                }
            }
            else if (when.ToUpper() == "ERROR")
            {
                if (type.ToUpper() == "AUTO")
                {
                    loSb.Append(@"<p><b>The Stone is not yet Confirm for you, It's Checking Avaibility (Direct Order)</b></p>");
                }
                else if (type.ToUpper() == "MANUAL")
                {
                    loSb.Append(@"<p><b>The Stone is not yet Confirm for you, It's Checking Avaibility</b></p>");
                }
            }
            loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
            loSb.Append(@"<tr><td>Company Name: </td><td>" + CompanyName.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td>User Name: </td><td>" + UserName.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td>Order ID: </td><td>" + OrderId.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td>Refno: </td><td>" + Refno.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td>Supplier Status: </td><td>" + SupplierStatus.ToString() + "</td></tr>");
            loSb.Append(@"<tr><td>Sunrise Status: </td><td>" + SunriseStatus.ToString() + "</td></tr>");
            //loSb.Append(@"<tr><td>Price : </td><td>" + Price.ToString() + ")</td></tr>");
            loSb.Append(@"<tr><td>Comments: </td><td>" + Comments.ToString() + "</td></tr>");
            //if (!string.IsNullOrEmpty(Comments))
            //{
            //    loSb.Append(@"<tr><td>Comments:</td><td>" + Refno.ToString() + "</td></tr>");
            //}
            loSb.Append("</table>");
            loSb.Append("<br/> <br/>");
            //Building the Data rows.
            loSb.Append(EmailSignature());
            if (custMail.ToString() != "" || assistbyemail != null)
            {
                SendPurchaseMail(custMail, "Sunrise Diamonds – Purchase Order – " + Convert.ToDateTime(fdtOrderDate).ToString("dd-MMM-yyyy hh:mm:ss") + " - " + OrderId.ToString(),
                Convert.ToString(loSb), assistbyemail);
            }

            return true;
        }
        public static void SendPurchaseMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsCCAdd);
                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.Bcc.Add(fsCCAdd);


                loMail.CC.Add("samit@sunrisediam.com");
                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;
                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);
                loMail.Bcc.Add("hardik@brainwaves.co.in");

                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);


                });
                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {
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