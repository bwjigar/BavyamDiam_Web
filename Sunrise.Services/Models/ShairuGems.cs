using DAL;
using EpExcelExportLib;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Sunrise.Services.Models
{
    public class ShairuGems
    {
        public ShairuGems() { }


        public static String EmailHeader()
        {
            return @"<html><head><style type=""text/css"">body{font-family: Verdana,'sans-serif';font-size:12px;}p{text-align:justify;margin:10px 0 !important;}
                a{color:#1a4e94;text-decoration:none;font-weight:bold;}a:hover{color:#3c92fe;}table td{font-family: Verdana,'sans-serif' !important;font-size:12px;padding:3px;border-bottom:1px solid #dddddd;}
                </style></head><body>
                <div style=""width:100%; margin:5px auto;font-family: Verdana,'sans-serif';font-size:12px;line-height:20px; background-color:#f2f2f2;"">
                <img alt=""Shairu Gems "" src=""http://s436045357.onlinehome.us/Images/email-head.png"" width=""750px"" />
                <div style=""padding:10px;"">";
        }

        public static String EmailSignature()
        {
            return @"<p>Please do let us know if you have any questions. Email us on <a href=""mailto:sales@shairugems.net"">sales@shairugems.net</a></p>
                <p>Thanks and Regards,<br />Shairu Gems Team,<br />DE-9012A(Entrance from DC),<br/>Bharat Diamond Bourse,<br/>Bandra Kurla Complex,<br/>Bandra(E),Mumbai 400 051</br>India<br/>
                <a href=""http://shairugems.net"">www.shairugems.net</a></p>
                </div></div></body></html>";
        }

        public static bool EmailNewRegistration(string fsToAdd, string fsName, string fsUsername, string fsPassword, string Lang)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();

                if (!string.IsNullOrEmpty(Lang) && Lang == "cn")
                {
                    loSb.Append(EmailHeader());
                    loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">亲 " + fsName + ",</p>");
                    loSb.Append(@"<p>欢迎来到我们的世界。</p>");
                    loSb.Append(@"<p>感谢您向我们注册。 您的个人资料目前正在筛选中。 这仅是为了验证客户与我们进行业务的真诚和真诚，并避免任何与钻石交易有关的非法活动。 我们会尽快通知您您的帐户状态。 最多需要3个工作日，并且我们的代表可能会与您联系以提供您用于验证目的的联系信息。</p>");
                    loSb.Append(@"<p>请存储以下信息，以便进一步沟通。<br />");
                    loSb.Append(@"<b>用户名: </b>" + fsUsername + "<br />");
                    loSb.Append("<b>密码: </b>" + fsPassword + "<br /></p>");
                    loSb.Append(EmailSignature());

                    SendMail(fsToAdd, "欢迎来到Shairu Gems –新注册–" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                        Convert.ToString(loSb), null, null, null);
                }
                else
                {
                    loSb.Append(EmailHeader());
                    loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
                    loSb.Append(@"<p>Welcome to our world.</p>");
                    loSb.Append(@"<p>Thanks for registering with us. Your profile is currently under screening. This is just for verifying that customer is genuine and sincere for business with us and avoid any illegal activity regarding to diamond trade. We will get back soon with notification of your account status. It will take   maximum 3 working Days and can be possible that our representative will be contact you with contact information you given for verification purpose.</p>");
                    loSb.Append(@"<p>Please store below information for further communication.<br />");
                    loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                    loSb.Append("<b>Password: </b>" + fsPassword + "<br /></p>");
                    loSb.Append(EmailSignature());

                    SendMail(fsToAdd, "Welcome to Shairu Gems – New Registration – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                        Convert.ToString(loSb), null, null, null);
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EmailNewRegistrationToAdmin(string fsToAdd, string fsUsername, string fsFirstName, string fsLastName,
            string fsMobile, string fsCompName, string fsCompAdd, string fsCompCity, string fsCompCountry, string fsCompMob1,
            string fsCompPhone1, string fsCompEmail, string fsStarus, int liUserId)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Admin,</p>");
                loSb.Append(@"<p>New user registered with Shairu Gems.</p>");
                loSb.Append(@"<p>Customer profile is under verification.</p>");
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                loSb.Append(@"<tr><td colspan=""2""><b>Personal Detail</b></td></tr>");
                loSb.Append(@"<tr><td width=""170px"">Username:</td><td>" + fsUsername + "</td></tr>");
                loSb.Append(@"<tr><td>First Name:</td><td>" + fsFirstName + "</td></tr>");
                loSb.Append(@"<tr><td>Last Name:</td><td>" + fsLastName + "</td></tr>");
                loSb.Append(@"<tr><td>Mobile:</td><td>" + fsMobile + "</td></tr>");
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
                loSb.Append(@"<p><a href=""http://s436045357.onlinehome.us/Login.aspx?refUrl=sgAdmin/ViewOrder.aspx?Id=" + liUserId.ToString()
                    + @""">Click here</a> to know your current order Status.</p>");
                loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Shairu Gems – New Registration – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool EmailNewOrder(int fiUserCode, string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
                string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, DateTime? orderDate, string bcc = null, string cc = null, string who = null)
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
            if (who == "Admin")
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
            if (who == "Admin")
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
                    // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
                }
            }
            if (who == "Admin")
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
                    // loSb.Append(@"<tr><td colspan=""2"">" + dtUserDetail.Rows[0]["sWeChatId1"].ToString() + "</td></tr>");
                }
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
            para.Add(db.CreateParam("iUserId", System.Data.DbType.Int32, System.Data.ParameterDirection.Input, fiUserCode));

            System.Data.DataTable dt = db.ExecuteSP("OrderDet_SelectAllByOrderId_Email", para.ToArray(), false);

            //if (who == "Customer")
            //{
            //    dt.Columns.Remove("Supplier");
            //    dt.Columns.Remove("Supplier Stone No");
            //}
            //else if (who == "Admin")
            //{
            //    dt.Columns.Remove("Supplier");
            //}

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
                    //if (column.ColumnName.ToString() == "Disc %" || column.ColumnName.ToString() == "Net Amt($)")
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

            //Table end.
            loSb.Append("</table>");

            loSb.Append(@"<p>Thank you for placing order from our website www.sunrisediamonds.com.hk</p>");

            loSb.Append(EmailSignature());

            if (cc == null && bcc == null)
            {
                SendMail(fsToAdd, "Shairu Gems – Order Confirmation – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(),
           Convert.ToString(loSb), null, fiOrderNo, fiUserCode, who);
            }
            else
            {
                ConfirmOrderSendMail(fsToAdd, "Shairu Gems – Order Confirmation – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(),
           Convert.ToString(loSb), bcc, cc, fiOrderNo, fiUserCode, who);
            }


            //return Convert.ToString(loSb);
            return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

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

        //    loSb.Append(@"<p><a href=""http://s436045357.onlinehome.us/SgAdmin/ViewOrder.aspx/Id=" + fiOrderNo.ToString() + @""">Click here</a> to change order status.</p>");
        //    loSb.Append(EmailSignature());

        //    SendMail(fsToAdd, "Shairu Gems – Order Confirmation – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss") + " - " + fiOrderNo.ToString(),
        //    Convert.ToString(loSb), null, fiOrderNo, fiUserCode);
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
                loSb.Append("<b>Password: </b>" + fsPassword + "<br /></p>");

                loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Shairu Gems – Forgot Password – " + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb), null, null, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, int? fiOrderId, int? fiUserCode, string who = null)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");
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
                        loMail.CC.Add(fsCCAdd);
                }


                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                    loMail.CC.Add(ConfigurationManager.AppSettings["BCCEmail"]);

                loMail.Bcc.Add("hardik@brainwaves.co.in");
                
                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                // Add Attechment (Customer Order)
                if (fiOrderId != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                    {
                        System.IO.MemoryStream ms = ExportToStreamEpPlus(Convert.ToInt32(fiOrderId), Convert.ToInt32(fiUserCode), who);
                        Attachment attachFile = new Attachment(ms, "SG_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), who);
                        Attachment attachFile = new Attachment(ms, "SG_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                loSmtp.Send(loMail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                loMail.Dispose();
                loSmtp = null;
            }
        }

        public static void ConfirmOrderSendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsBCCAdd, string fsCCAdd, int? fiOrderId, int? fiUserCode, string who = null)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Shairu Gems");
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
                        loMail.CC.Add(fsCCAdd);
                }
                
                if (!string.IsNullOrEmpty(fsBCCAdd))
                    loMail.Bcc.Add(fsBCCAdd);

                //if (!string.IsNullOrEmpty(fsCCAdd))
                //    loMail.CC.Add(fsCCAdd);

                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                //    loMail.CC.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                //    loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                loMail.Bcc.Add("hardik@brainwaves.co.in");
                
                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                // Add Attechment (Customer Order)
                if (fiOrderId != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["UseEPPlus"] == "Y")
                    {
                        System.IO.MemoryStream ms = ExportToStreamEpPlus(Convert.ToInt32(fiOrderId), Convert.ToInt32(fiUserCode), who);
                        Attachment attachFile = new Attachment(ms, "SG_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        loMail.Attachments.Add(attachFile);
                    }
                    else
                    {
                        System.IO.MemoryStream ms = ExportToStream(Convert.ToInt32(fiOrderId), who);
                        Attachment attachFile = new Attachment(ms, "SG_Order_" + Lib.Models.Common.GetHKTime().ToString("dd-MMM-yyyy") + ".xls", "application/vnd.ms-excel");
                        loMail.Attachments.Add(attachFile);
                    }
                }
                loSmtp.Send(loMail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                loMail.Dispose();
                loSmtp = null;
            }
        }


        private static System.IO.MemoryStream ExportToStream(int fiOrderId, string who = null)
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

            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Offer Disc.(%)"; //"Disc(%)";
            if (dlTotalRapAmount == 0)
                cDisc.FooterText = @"0.00";
            else
                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");

            cDisc.ItemStyle.CssClass = "twoDigit-red";
            cDisc.FooterStyle.CssClass = "twoDigit-red";
            gvData.Columns.Add(cDisc);


            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Offer Value($)"; //"Net Amt($)";
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

            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            gvData.Columns.Add(cCertiNo);

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

        private static System.IO.MemoryStream ExportToStreamEpPlus(int fiOrderId, int fiUserCode, string who = null)
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

            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Offer Disc.(%)"; //"Disc(%)";
            if (dlTotalRapAmount == 0)
                cDisc.FooterText = @"0.00";
            else
                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");

            cDisc.ItemStyle.CssClass = "twoDigit-red";
            cDisc.FooterStyle.CssClass = "twoDigit-red";
            gvData.Columns.Add(cDisc);


            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Offer Value($)"; //"Net Amt($)";
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

            

            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            gvData.Columns.Add(cCertiNo);

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

            GridViewEpExcelExport ep_ge;
            ep_ge = new GridViewEpExcelExport(gvData, "Order", "Order");

            ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
            ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
            ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;

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

        private static UInt32 DiscNormalStyleindex;
        private static UInt32 OfferDisc_OfferValue_Index;
        private static UInt32 WebDisc_Index;
        private static UInt32 FinalValue_FinalDisc_Index;
        private static UInt32 CutNormalStyleindex;

        private static void Ep_FillingWorksheetEventHandler(object sender, ref EpExcelExport.FillingWorksheetEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            EpExcelExport.ExcelFormat format = new EpExcelExport.ExcelFormat();
            Color _SKY_BLUE = System.Drawing.ColorTranslator.FromHtml("#ade0e9");
            Color _PISTA = System.Drawing.ColorTranslator.FromHtml("#ddeedf");
            Color _LIGHT_YELLOW = System.Drawing.ColorTranslator.FromHtml("#fdfdc1");

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

            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            DiscNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex = ee.AddStyle(format);
        }

        private static void Ep_BeforeCreateColumnEventHandler(object sender, ref EpExcelExport.ExcelHeader e)
        {

            //if (e.ColName == "iSr")
            if (e.Caption == "Sr.")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 4;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "sRefNo")
            if (e.Caption == "Ref. No.")
            {
                e.ColDataType = eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 7.5;
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
            if (e.Caption == "Carats")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 5.71;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dRepPrice")
            if (e.Caption == "Rap Price($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 10.86;
                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "dRepAmount")
            if (e.Caption == "Rap Amount($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 15.71;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0";
            }
            if (e.Caption == "Offer Disc.(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Offer Value($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";
            }
            //if (e.ColName == "dDisc")
            if (e.Caption == "Disc(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 6.86;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Net Amt($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";

            }
            if (e.Caption == "Web Disc.($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#0.00";
            }
            if (e.Caption == "Final Disc.(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.NumFormat = "#0.00";
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "-(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Final Value", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*100";

            }
            if (e.Caption == "Final Value")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#0.00";
            }
            if (e.Caption == "Offer Value($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 14;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
            //if (e.ColName == "dNetPrice")
            if (e.Caption == "Net Amt($)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
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
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 6.70;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dWidth")
            if (e.Caption == "Width")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 6.14;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dDepth")
            if (e.Caption == "Depth")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 6.14;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dDepthPer")
            if (e.Caption == "Depth(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 8.43;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dTablePer")
            if (e.Caption == "Table(%)")
            {
                e.ColDataType = eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                e.Width = 8.43;
                e.NumFormat = "#0.00";
            }
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

            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
                //e.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                if (e.ColumnName == "Disc(%)" || e.ColumnName == "Offer Disc.(%)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
            }

        }


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

    }
}