using Lib.Models;
using Newtonsoft.Json;
using SunriseWeb.Helper;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace SunriseWeb.Data
{
    public class API
    {
        public string CallAPI(string MethodName, string InputPara)
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"];
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", "Bearer " + SessionFacade.TokenNo);
            client.Headers.Add("Content-type", "application/json");
            client.Encoding = Encoding.UTF8;
            string json = string.Empty;
            try
            {
                 json = client.UploadString(apiUrl + MethodName, "POST", InputPara);
            }
            catch (WebException ex)
            {
                if (ex.Message.ToLower().Contains("401"))
                {
                    HttpContext.Current.Response.Redirect("~/login", true);
                }
            }
            return json;
        }
        public string CallAPIWithoutToken(string MethodName, string InputPara)
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"];
            WebClient client = new WebClient();
            client.Headers.Add("Content-type", "application/json");
            client.Encoding = Encoding.UTF8;
            string json = string.Empty;
            try
            {
                json = client.UploadString(apiUrl + MethodName, "POST", InputPara);
            }
            catch (WebException ex)
            {
                if (ex.Message.ToLower().Contains("401"))
                {
                    HttpContext.Current.Response.Redirect("~/login", true);
                }
            }
            return json;
        }

        public string CallAPIUrlEncoded(string MethodName, string InputPara)
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"];
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
            client.Encoding = Encoding.UTF8;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string json;

            try
            {
                json = client.UploadString(apiUrl + MethodName, InputPara);
                return json;
            }
            catch (WebException ex)
            {
                //if (ex.Status)
                var webException = ex as WebException;
                if ((Convert.ToString(webException.Status)).ToUpper() == "PROTOCOLERROR")
                {
                    OAuthErrorMsg error =
                        JsonConvert.DeserializeObject<OAuthErrorMsg>(
                       API.ExtractResponseString(webException));
                    json = API.ExtractResponseString(webException);
                    return json;
                }
                //json = ex.Message;
                return json = "";
            }
            catch (Exception ex)
            {
                json = ex.Message;
                return json;
            }
        }

        public string CallAPIUrlEncodedWithWebReq(string MethodName, string InputPara)
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"];
            WebRequest request = WebRequest.Create(apiUrl + MethodName);
            request.Method = "POST";
            request.Timeout = 7200000; //2 Hour in milliseconds
            byte[] byteArray = Encoding.UTF8.GetBytes(InputPara);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            string json;

            //Here is the Business end of the code...
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                //and here is the response.
                WebResponse response = request.GetResponse();

                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                json = reader.ReadToEnd();
                Console.WriteLine(json);
                reader.Close();
                dataStream.Close();
                response.Close();

                return json;
            }
            catch (WebException ex)
            {
                //if (ex.Status)
                var webException = ex as WebException;
                if ((Convert.ToString(webException.Status)).ToUpper() == "PROTOCOLERROR")
                {
                    OAuthErrorMsg error =
                        JsonConvert.DeserializeObject<OAuthErrorMsg>(
                       API.ExtractResponseString(webException));
                    json = API.ExtractResponseString(webException);
                    return json;
                }
                //json = ex.Message;
                return json = "";
            }
            catch (Exception ex)
            {
                json = ex.Message;
                return json;
            }
        }


        public static string ExtractResponseString(WebException webException)
        {
            if (webException == null || webException.Response == null)
                return null;

            var responseStream =
                webException.Response.GetResponseStream() as MemoryStream;

            if (responseStream == null)
                return null;

            var responseBytes = responseStream.ToArray();

            var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
    }
}