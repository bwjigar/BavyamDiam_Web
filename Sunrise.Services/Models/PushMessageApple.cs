using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Sunrise.Services.Models
{
    public class PushMessageApple
    {
        public PushMessageApple()
        { }

        public static string pushMessage(string WebsiteName, string UDID, string Source, string IS_IPHONE, string IS_DEVELOPMENT, string Message)
        {
            //DNK iPad - d55cafdcdbcb26c20424a18a213a5039a67780e5530abf1fccb0455c22a8f0a7

            try
            {
                // Help File for Mac to generate certificate file and how to send notification code:
                // http://www.raywenderlich.com/32960/apple-push-notification-services-in-ios-6-tutorial-part-1

                int port = 2195;
                String hostname;
                String certificatePath;
                string certificatePassword;

                if (IS_DEVELOPMENT.Trim() == "0" || IS_DEVELOPMENT.Trim().ToLower() == "no")
                {
                    // Live Apple store after App upload to server
                    hostname = "gateway.push.apple.com";

                    if (IS_IPHONE.Trim() == "1" || IS_IPHONE.Trim().ToLower() == "yes")
                    {
                        if (WebsiteName.ToUpper() == "SHAIRU")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPhone\\ShairuGems_APNSKey_Distribution_iPhone.p12"); //working ok
                        else if (WebsiteName.ToUpper() == "ATIT")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPhone\\atit_APNSKey_Distribution_iPhone.p12"); //working ok
                        else
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPhone\\sunrise_APNSKey_Distribution_iPhone.p12"); //working ok

                    }
                    else
                    {
                        if (WebsiteName.ToUpper() == "SHAIRU")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPad\\ShairuGems_APNSKey_Distribution_iPad.p12"); //working ok
                        else if (WebsiteName.ToUpper() == "ATIT")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPad\\atit_APNSKey_Distribution_iPad.p12"); //working ok
                        else
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPad\\sunrise_APNSKey_Distribution_iPad.p12"); //working ok
                    }

                    certificatePassword = "dnk@123";
                }
                else
                {
                    // Local development for testing
                    hostname = "gateway.sandbox.push.apple.com";

                    if (IS_IPHONE.Trim() == "1" || IS_IPHONE.Trim().ToLower() == "yes")
                    {
                        if (WebsiteName.ToUpper() == "SHAIRU")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPhone\\ShairuGems_APNSKey_Development_iPhone.p12"); //working ok
                        else if (WebsiteName.ToUpper() == "ATIT")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPhone\\atit_APNSKey_Development_iPhone.p12"); //working ok
                        else
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPhone\\sunrise_APNSKey_Development_iPhone.p12"); //working ok
                    }
                    else
                    {
                        if (WebsiteName.ToUpper() == "SHAIRU")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPad\\ShairuGems_APNSKey_Development_iPad.p12"); //working ok
                        else if (WebsiteName.ToUpper() == "ATIT")
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPad\\atit_APNSKey_Development_iPad.p12"); //working ok
                        else
                            certificatePath = HttpContext.Current.Server.MapPath("~\\ApplePushCertificate\\iPad\\sunrise_APNSKey_Development_iPad.p12"); //working ok
                    }

                    certificatePassword = "dnk@123";
                }

                X509Certificate2 clientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(certificatePath), certificatePassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);
                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                try
                {
                    sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Tls, false);

                    MemoryStream memoryStream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(memoryStream);

                    writer.Write((byte)0);  //The command
                    writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
                    writer.Write((byte)32); //The deviceId length (big-endian second byte)

                    writer.Write(StringToByteArray(UDID.ToUpper()));
                    //String payload = "{\"aps\":{\"alert\":\"" + Mesaj + "\",\"badge\":0,\"sound\":\"default\",\"content-available\":1}}";
                    String payload = "{\"aps\":{\"alert\":\"" + Message + "\",\"sound\":\"default\",\"content-available\":1}}";
                    writer.Write((byte)0);
                    writer.Write((byte)payload.Length);
                    byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                    writer.Write(b1);
                    writer.Flush();
                    byte[] array = memoryStream.ToArray();
                    sslStream.Write(array);
                    sslStream.Flush();
                    client.Close();
                }
                catch (Exception e)
                {
                    client.Close();
                    return e.Message;
                }

                return "Message sent";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers. 
            return false;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }
    }
}