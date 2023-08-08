using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Sunrise.Services.Models
{
    public class PushMessageAndroid
    {
        public PushMessageAndroid() { }

        public static string pushMessage(string WebsiteName, string UDID, string Message)
        {
            //DNK iPad - d55cafdcdbcb26c20424a18a213a5039a67780e5530abf1fccb0455c22a8f0a7

            try
            {

                //RegiId - DEVICE ID

                //DLL dl = new DLL();
                //BLL bl = new BLL();

                // string Regi = "";
                DataSet dsregID = new DataSet();
                // bl.RegId = RegiId;
                string text = string.Empty;
                //dsregID = dl.GetAndroidRegIds(bl);

                var applicationID = "AIzaSyCsqPSlHh34A_uHCGXd945bygTanZvN1Vo";      //DIFFERENT FOR APP RJOLD
                //var applicationID = "AIzaSyBGmGZgA8rNCUABCABeMVJEFMnp5-W5nOA";		//DIFFERENT FOR APP

                // var applicationID = "AIzaSyAPrjC2hseI1aM3telU_dYVRUUm8sPGaBE";

                //var SENDER_ID = "972643036257";						
                //RJOLD
                //var SENDER_ID = "503405990653";						//DIFFERENT FOR APP
                var SENDER_ID = "504488549245";

                switch (WebsiteName)
                {
                    case "SHAIRU":
                        applicationID = "AIzaSyAzyHl95fbHFVJy_m47XZO8yxJGTf0ZKFA"; //"AIzaSyCsqPSlHh34A_uHCGXd945bygTanZvN1Vo";
                        SENDER_ID = "504488549245";
                        break;
                    case "SUNRISE":
                        //applicationID = "AIzaSyACRfjGc6FIx-LdCanduxWpWWWsSqX9RQQ"; // "AIzaSyD6BULynUjC5WrdxVdSUpJEuIOqeZGYZjQ";
                        //SENDER_ID = "810970496424";
                        applicationID = "AIzaSyA2HPb5tVMF3wGF5aWdNBCHtNS13cs4BY4";
                        SENDER_ID = "300946530292";
                        break;
                    case "ATIT":
                        applicationID = "AIzaSyDSOa0I0BHRyhzxbU257qa54Vr0_H6xTBo";
                        SENDER_ID = "640423368212";
                        break;
                    default:
                        applicationID = "AIzaSyD6BULynUjC5WrdxVdSUpJEuIOqeZGYZjQ";
                        SENDER_ID = "810970496424";
                        break;
                }

                var value = Message;
                HttpWebRequest tRequest;
                //tRequest = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                // tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                // tRequest.ContentType = " application/json;";

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                String collaspeKey = Guid.NewGuid().ToString("n");
                //  string json = "{ \"registration_ids\": [ \"" + RegiId + "\" ], \"data\": { \"message\": \"" + HttpUtility.UrlEncode(value).ToString() + "\"}}";

                //string json = string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", RegiId, HttpUtility.UrlEncode(value), HttpUtility.UrlEncode(collaspeKey));
                string json = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&registration_id=" + UDID + "";

                Console.WriteLine(json);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                //  Byte[] byteArray = Encoding.Default.GetBytes(json); ;
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);
                // tRequest.Proxy = GlobalProxySelection.GetEmptyWebProxy();
                dataStream.Close();

                tRequest.Credentials = CredentialCache.DefaultCredentials;
                try
                {
                    WebResponse Response = tRequest.GetResponse();
                    HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                    if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                    {
                        text = "error=Unauthorized - need new token";


                    }
                    else if (!ResponseCode.Equals(HttpStatusCode.OK))
                    {
                        text = "error=Response from web service isn't OK";
                    }

                    StreamReader Reader = new StreamReader(Response.GetResponseStream());
                    string responseLine = Reader.ReadToEnd();
                    Reader.Close();

                    return responseLine;
                }
                catch (Exception)
                {

                }
                return "error";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private static string GetPostStringFrom(NameValueCollection postFieldNameValue)
        {
            //throw new NotImplementedException();
            List<string> items = new List<string>();

            foreach (String name in postFieldNameValue)
                items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(postFieldNameValue[name])));

            return String.Join("&", items.ToArray());
        }
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
    }
}