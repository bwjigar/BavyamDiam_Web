using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Sunrise.Services.Models
{
    public class IPadCommon
    {
        public IPadCommon() { }

        private static String EmailHeader()
        {
            if (ConfigurationManager.AppSettings["Location"] == "H")
                return Sunrise.EmailHeader();
            else
                return ShairuGems.EmailHeader();
        }

        private static String EmailSignature()
        {
            if (ConfigurationManager.AppSettings["Location"] == "H")
                return Sunrise.EmailSignature();
            else
                return ShairuGems.EmailSignature();
        }

        public static bool EmailNewAdminRegistration(string UserName, string Password, string FirstName, string LastName, string CompEmail, string CompMobile, string IsActive)
        {
            return Sunrise.EmailNewAdminRegistration(UserName, Password, FirstName, LastName, CompEmail, CompMobile, IsActive);
        }

        // Added By Jubin Shah 23-01-2020
        public static bool EmailNewRegistration_New(string fsToAdd, string fsName, string fsUsername, string fsPassword, string scompname)
        {
            return Sunrise.EmailNewRegistration(fsToAdd, fsName, fsUsername, fsPassword, scompname, "en");
        }

        public static bool EmailNewRegistrationToAdmin(string whichuser, string fsToAdd, string fsUsername, string fsFirstName, string fsLastName,
            string fsAddress, string fsCity, string fsCountry, string fsMobile, string fsEmail, string fsCompName, string fsCompAdd,
            string fsCompCity, string fsCompCountry, string fsCompMob1, string fsCompPhone1, string fsCompEmail, string fsStarus, int liUserId)
        {

            return Sunrise.EmailNewRegistrationToAdmin(whichuser, fsToAdd, fsUsername, fsFirstName, fsLastName,
                       fsAddress, fsCity, fsCountry, fsMobile, fsEmail, fsCompName, fsCompAdd,
                       fsCompCity, fsCompCountry, fsCompMob1, fsCompPhone1, fsCompEmail, fsStarus, liUserId);
        }

        public static void EmailForgotPassword(string fsToAdd, string fsName, string fsUsername, string fsPassword)
        {
            if (ConfigurationManager.AppSettings["Location"] == "H")
                Sunrise.EmailForgotPassword(fsToAdd, fsName, fsUsername, fsPassword);
            else
                ShairuGems.EmailForgotPassword(fsToAdd, fsName, fsUsername, fsPassword);
        }
        // Change By Hitesh on [29-05-2017] as per rahul bcoz only order mail can not sent tejashbhai
        private static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, int? fiOrderId, int? fiUserCode, bool bIsOrder)
        {
            if (ConfigurationManager.AppSettings["Location"] == "H")
                Sunrise.SendMail(fsToAdd, fsSubject, fsMsgBody, fsCCAdd, fiOrderId, fiUserCode, false, bIsOrder);
            else
                ShairuGems.SendMail(fsToAdd, fsSubject, fsMsgBody, fsCCAdd, fiOrderId, fiUserCode);
        }

        public static bool EmailNewOrder(int fiUserCode, string fsToAdd, long fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
            string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, DateTime? orderDate, string bcc = null, string cc = null)
        {
            return Sunrise.EmailNewOrder(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote, orderDate, bcc, cc);
        }
        public static bool EmailNewDemandNotification(int iUserId, int Id, string ToEmail)
        {
            return Sunrise.EmailNewDemandNotification(iUserId, Id, ToEmail);
        }
        public static bool EmailNewHoldRelease(string MailType, int fiUserCode, int LoginUserid, string fsToAdd, string StoneID, DateTime fdtOrderDate, string cc = null, string fsCustomerNote = null, string _HoldCompany = null)
        {
            return Sunrise.EmailNewHoldRelease(MailType, fiUserCode, LoginUserid, fsToAdd, StoneID, fdtOrderDate, cc, fsCustomerNote, _HoldCompany);
        }

        //public static bool EmailNewOrderToAdmin(int fiUserCode, string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
        //string fsCompName, string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote)
        //{
        //    if (ConfigurationManager.AppSettings["Location"] == "H")
        //        return Sunrise.EmailNewOrderToAdmin(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsCompName, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote);
        //    else
        //        return ShairuGems.EmailNewOrderToAdmin(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsCompName, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote);
        //}

        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            }
        }

        public static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public static bool EmailPurchaseOrder(string type, string when, string custMail, DateTime fdtOrderDate, string assistbyemail = null, string OrderId = null, string Refno = null, string Price = null, string Comments = null, string CompanyName = null, string UserName = null, string SupplierStatus = null, string SunriseStatus = null)
        {

            return Sunrise.EmailPurchaseOrder(type, when, custMail, fdtOrderDate, assistbyemail, OrderId, Refno, Price, Comments, CompanyName, UserName, SupplierStatus, SunriseStatus);

        }
    }
}