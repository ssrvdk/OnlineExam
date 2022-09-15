using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataLayer
{
    public static class CommonControl
    {
        #region Encryption method
        /// <summary>
        ///  Convert hashed byte array into string        
        /// </summary>
        /// <param name="arrInput">byte array to convert into string</param>
        public static string SHA256Encryption(string strtext)
        {
            // Create a SHA256
            SHA256 sha256 = SHA256.Create();
            // ComputeHash - returns byte array
            byte[] _bytePassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(strtext));

            // Convert byte array to a string   
            int i;
            StringBuilder sbOutput = new StringBuilder(_bytePassword.Length);
            for (i = 0; i < _bytePassword.Length; i++)
            {
                //build string array from byte array
                sbOutput.Append(_bytePassword[i].ToString("X2"));//"X2" is used to convert byte array to string
            }
            return sbOutput.ToString();
        }
        #endregion

        #region Send Email
        public static string SendEmail(String bcc, String Subj, string Message,string host, string fromMail, string password)
        {
           try
            {
                //Reading sender Email credential from web.config file  

                //string HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                //string FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
                //string Pass = ConfigurationManager.AppSettings["Password"].ToString();

                //creating the object of MailMessage  
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromMail); //From Email Id  
                mailMessage.To.Add(new MailAddress(bcc));
                mailMessage.Subject = Subj; //Subject of Email  
                mailMessage.Body = Message; //body or message of Email  
                mailMessage.IsBodyHtml = true;

                string[] bccid = bcc.Split(',');

                foreach (string bccEmailId in bccid)
                {
                    mailMessage.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id  
                }
                SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                smtp.Host = host;              //host of emailaddress for example smtp.gmail.com etc  

                //network and security related credentials  

                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage); //sending Email   
                return "Please check your email for set password";
            }
            catch (Exception e)
            {
                return e.Message.ToString();

            }

        }
        #endregion

        #region XML Control
        public static string ToXML(this object obj)
        {
            string xml = string.Empty;
            try
            {
                System.IO.TextWriter sw = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                xml = sw.ToString();
                sw.Close();
                return xml;
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static T FromXML<T>(this string obj)
        {
            System.IO.TextReader sw = new StringReader(obj);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(sw);
        }
        #endregion

        #region Encryption/Decryption
        public static string Decrypt(string encryptText)
        {
            try
            {
                if (!string.IsNullOrEmpty(encryptText))
                {
                    string encryptionkey = "SAUW193BX628TD57";
                    byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
                    RijndaelManaged rijndaelCipher = new RijndaelManaged();
                    byte[] encryptedData = Convert.FromBase64String(encryptText.Replace(" ", "+"));
                    PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
                    using (ICryptoTransform decryptrans = rijndaelCipher.CreateDecryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
                    {
                        using (MemoryStream mstrm = new MemoryStream(encryptedData))
                        {
                            using (CryptoStream cryptstm = new CryptoStream(mstrm, decryptrans, CryptoStreamMode.Read))
                            {
                                byte[] plainText = new byte[encryptedData.Length];
                                int decryptedCount = cryptstm.Read(plainText, 0, plainText.Length);
                                return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                            }
                        }
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string Encrypt(string inputText)
        {
            try
            {
                string encryptionkey = "SAUW193BX628TD57";
                byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                byte[] plainText = Encoding.Unicode.GetBytes(inputText);
                PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
                using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
                {
                    using (MemoryStream mstrm = new MemoryStream())
                    {
                        using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                        {
                            cryptstm.Write(plainText, 0, plainText.Length);
                            cryptstm.Close();
                            return Convert.ToBase64String(mstrm.ToArray());
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region SendSMS
        public static string SendSMS(string message, string numbers)
        {
            string responseString;
            // return "";
            //message = "Dear Vipul,\nGreetings from Hotel Maheshwari Avenue ujjain. Your booking for quadruple room with breakfast on 12 Aug for 4 guest is confirmed. your tariff is Rs 4000 and you prepaid Rs 3000. Please contact on 7978051335 for more details.";
            //using (var wb = new WebClient())
            //{
            //    byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
            //    {
            //    {"apikey" , "yourapiKey"},
            //    {"numbers" , numbers},
            //    {"message" , message},
            //    {"sender" , "TRVNTS"}
            //    });
            //    string result = System.Text.Encoding.UTF8.GetString(response);
            //    return result;
            //}

            //Your authentication key
            string authKey = "9249A2ppEkLt5b8531b4";
            //Multiple mobiles numbers separated by comma
            string mobileNumber = numbers;
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderId = "TRVNTS";
            //Your message to send, Add URL encoding here.
            //string message = HttpUtility.UrlEncode(message);
            string Route = ConfigurationManager.AppSettings["route"].ToString();
            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&route={0}", Route);

            try
            {
                //Call Send SMS API
                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                return ex.Message.ToString();
            }
            return responseString;
        }
        #endregion

        #region CurrentDateTime
        public static DateTime CurrentDateTime()
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            return indianTime;
        }
        #endregion

        public static string GenerateRandomNumber(int length)
        {
            //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "5124348754964675060";
            string characters = numbers;
            //characters += alphabets + small_alphabets + numbers;           
            string UniqueNumber = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (UniqueNumber.IndexOf(character) != -1);
                UniqueNumber += character;
            }
            return UniqueNumber;
        }

        public static string GetAdminUrl()
        {
            string AdminUrl = ConfigurationManager.AppSettings["AdminUrl"].ToString();
            return AdminUrl;
        }

        public static string GetImagesUrlAdmin()
        {
            string ImagePathAdmin = ConfigurationManager.AppSettings["ImagePathAdmin"].ToString();
            return ImagePathAdmin;
        }
    }
}
