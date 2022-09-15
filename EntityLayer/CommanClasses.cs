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

namespace EntityLayer
{
    public static class CommanClasses
    {

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
        public static bool isAuthorize(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public static string encryption(String password)
        //{
        //    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //    byte[] encrypt;
        //    UTF8Encoding encode = new UTF8Encoding();
        //    //encrypt the given password string into Encrypted data  
        //    encrypt = md5.ComputeHash(encode.GetBytes(password));
        //    StringBuilder encryptdata = new StringBuilder();
        //    //Create a new string by using the encrypted data  
        //    for (int i = 0; i < encrypt.Length; i++)
        //    {
        //        encryptdata.Append(encrypt[i].ToString());
        //    }
        //    return encryptdata.ToString();
        //}
        //public static string Decrpytion(string Email)
        //{
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(Email);
        //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charCount];
        //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    string result = new String(decoded_char);
        //    return result;
        //}
        public static string Decrypt(string encryptText)
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
        public static string Encrypt(string inputText)
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

        public static string SendEmail(string emailTo, string suject, string body)
        {
            try
            {
                //Reading sender Email credential from web.config file  

                string HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                string FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
                string Pass = ConfigurationManager.AppSettings["Password"].ToString();

                //creating the object of MailMessage  
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
                mailMessage.To.Add(new MailAddress(emailTo));
                mailMessage.Subject = suject; //Subject of Email  
                mailMessage.Body = body; //body or message of Email  
                mailMessage.IsBodyHtml = true;

                string[] emails = emailTo.Split(',');

                foreach (string email in emails)
                {
                    mailMessage.Bcc.Add(new MailAddress(email)); //Adding Multiple BCC email Id  
                }
                SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

                //network and security related credentials  

                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Pass;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage); //sending Email   
                return "Mail sent successfully.";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
            return "";
        }

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

        public static DateTime CurrentDateTime()
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            return indianTime;
        }

        public static T FromXML<T>(this string obj)
        {
            System.IO.TextReader sw = new StringReader(obj);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(sw);
        }

        public static int checkParse(string input)
        {

            try
            {
                // declaring Int32 variable
                int val;
                // getting parsed value
                val = Int32.Parse(input);
                return val;
            }

            catch (FormatException)
            {
                return 0;
            }
        }

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

    }
}
