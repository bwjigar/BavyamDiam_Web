using DAL;
using Sunrise.Services.Models.WhatsApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Sunrise.Services.Models
{
    public class ServicesCommon
    {
        private static User user;
        private static User user2;

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


        // Added By Jubin Shah 23-01-2020
        public static bool EmailNewRegistration_New(string fsToAdd, string fsName, string fsUsername, string fsPassword, string scompname,string Lang)
        {
            return Sunrise.EmailNewRegistration(fsToAdd, fsName, fsUsername, fsPassword, scompname, Lang);
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

        public static bool EmailNewOrder(int fiUserCode, string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
            string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote, DateTime? orderDate, string bcc = null, string cc = null)
        {
            if (ConfigurationManager.AppSettings["Location"] == "H")
                return Sunrise.EmailNewOrder(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote, orderDate, bcc, cc);
            else
                return ShairuGems.EmailNewOrder(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote, orderDate, bcc, cc);
        }

        //public static bool EmailNewOrderToAdmin(int fiUserCode, string fsToAdd, int fiOrderNo, DateTime fdtOrderDate, byte fiOrderStauts, string fsFullname,
        //string fsCompName, string fsAddress, string fsPhoneNo, string fsMobile, string fsEmail, string fsCustomerNote)
        //{
        //    if (ConfigurationManager.AppSettings["Location"] == "H")
        //        return Sunrise.EmailNewOrderToAdmin(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsCompName, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote);
        //    else
        //        return ShairuGems.EmailNewOrderToAdmin(fiUserCode, fsToAdd, fiOrderNo, fdtOrderDate, fiOrderStauts, fsFullname, fsCompName, fsAddress, fsPhoneNo, fsMobile, fsEmail, fsCustomerNote);
        //}

        public static void Whatsapp(int? UserId, string Name, string ContactNo, string CompName, int? OrderId, bool IsPurchaseConfirm, string Other, bool Login)
        {
            #region Start By Hitesh as per Doc No [1152] on [21-10-2015]
            string sWhatsappno = ConfigurationManager.AppSettings["WhatsAppNo"];
            string sWhatsapppassword = ConfigurationManager.AppSettings["WhatsAppPassword"];
            string sTejash = ConfigurationManager.AppSettings["Tejash"];
            string sJignesh = ConfigurationManager.AppSettings["Jignesh"];
            #endregion End By Hitesh as per Doc No [1152] on [21-10-2015]

            if (!CheckLogin(sWhatsappno, sWhatsapppassword))
            {
                return;
            }
            else
            {
                string Msg;
                if (Login == true)
                {
                    Msg = Name + " [" + CompName + "] has tried to login on our website [www.sunrisediamonds.com.hk]. As per our criteria his/her Account is suspended.";
                    user = User.UserExists(sJignesh, "Jignesh");
                    WhatSocket.Instance.SendMessage(user.WhatsUser.GetFullJid(), Msg);
                }
                else if (Other != "Register")
                {
                    Database db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>
                    {
                        db.CreateParam("p_for_userid", DbType.String, ParameterDirection.Input, Convert.ToInt32(UserId))
                    };

                    DataTable dt = db.ExecuteSP("get_assist_by_emp", para.ToArray(), false);

                    if (dt.Rows.Count > 0)
                    {


                        if (dt.Rows[0]["sMobileNo1"].ToString() != "")
                        {
                            Msg = "(" + CompName + " ; Contact No = " + ContactNo + " ) has send request of order no ";
                            if (IsPurchaseConfirm == true)
                                Msg = "(" + CompName + " ; Contact No = " + ContactNo + " ) has place purchase order ";
                            user = User.UserExists(Convert.ToString(dt.Rows[0]["sMobileNo1"].ToString().Replace("-", "")).Trim(), Convert.ToString(dt.Rows[0]["name"]).Trim());
                            WhatSocket.Instance.SendMessage(user.WhatsUser.GetFullJid(), Name + Msg + OrderId + ".");
                            user2 = User.UserExists(sTejash, Convert.ToString(dt.Rows[0]["name"]).Trim());
                            WhatSocket.Instance.SendMessage(user2.WhatsUser.GetFullJid(), Name + Msg + OrderId + ".");
                        }
                    }
                }
                else
                {
                    Msg = "[" + Name + "] [" + CompName + "] has made registration with us.";
                    //Jignesh = SG.User.UserExists("919978799126", "Jignesh");
                    user = User.UserExists(sJignesh, "Jignesh");
                    WhatSocket.Instance.SendMessage(user.WhatsUser.GetFullJid(), Msg + ".");
                }
            }
        }

        protected static bool CheckLogin(string user, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                    return false;

                WhatSocket.Create(user, pass, "Sunrise Diamonds", true);
                WhatSocket.Instance.Connect();
                WhatSocket.Instance.Login();
                //check login status
                if (WhatSocket.Instance.ConnectionStatus == WhatsAppApi.WhatsApp.CONNECTION_STATUS.LOGGEDIN)
                {
                    return true;
                }
            }
            catch (Exception)
            { }
            return false;
        }
    }
}