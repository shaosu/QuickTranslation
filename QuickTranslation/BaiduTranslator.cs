using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

using Himesyo;
using Himesyo.Check;
using Himesyo.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QuickTranslation
{
    public sealed class BaiduTranslator
    {
        private static readonly Random random = new Random();

        private DateTime upTranTime = DateTime.Now.AddDays(-1);

        [XmlIgnore]
        private static Uri UriHttp { get; }
          = new Uri(@"http://api.fanyi.baidu.com/api/trans/vip/translate");

        public string AppID { get; set; }
        public string SecretKey { get; set; }

        public string Password { get; set; }

        public int Interval { get; set; } = 1200;

        public bool VerifyPassword(string password)
        {
            string pass = PasswordHash(AppID, string.Empty);
            if (pass != Password)
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    return false;
                }
                pass = PasswordHash(AppID, password?.Trim());
                return pass == Password;
            }
            return true;
        }

        public void Wait()
        {
            TimeSpan span = DateTime.Now - upTranTime;
            int ms = (int)Math.Floor(span.TotalMilliseconds);
            if (ms >= 0 && Interval > ms)
            {
                Thread.Sleep(Interval - ms);
            }
        }

        public bool TryTran(string text, out string[] result, out string code, out string message)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                result = Array.Empty<string>();
                code = "52000";
                message = null;
                return true;
            }

            string requestResult;
            string salt = random.Next().ToString();
            string requestBody = string.Format(
                        "q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}"
                        , Uri.EscapeDataString(text)
                        , "en"
                        , "zh"
                        , AppID
                        , salt
                        , $"{AppID}{text}{salt}{SecretKey}".ComputeMD5().ToShow());

            try
            {
                HttpWebRequest web = (HttpWebRequest)WebRequest.Create(UriHttp);
                web.Method = WebRequestMethods.Http.Post;
                web.ContentType = "application/x-www-form-urlencoded";
                web.UserAgent = null;
                using (Stream requestStream = web.GetRequestStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(requestBody);
                    requestStream.Write(data, 0, data.Length);
                }
                upTranTime = DateTime.Now;
                using (WebResponse response = web.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    requestResult = reader.ReadToEnd();
                }
                if (string.IsNullOrWhiteSpace(requestResult))
                {
                    result = null;
                    code = "";
                    message = "服务器返回空值";
                    return false;
                }
                else
                {
                    JObject json = JObject.Parse(requestResult);
                    if (json.TryGetValue("error_code", out JToken value) && (code = value.ToString()) != "52000")
                    {
                        json.TryGetValue("error_msg", out JToken errorMessage);
                        result = null;
                        //code = value.ToString();
                        message = $"服务器返回错误。{errorMessage}";
                        return false;
                    }
                    else
                    {
                        result = json["trans_result"]?
                            .Select(token => token.Value<string>("dst"))
                            .ToArray();
                        code = "52000";
                        message = null;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                code = "400";
                message = $"网络错误。{ex.Message}";
                return false;
            }
        }

        public static BaiduTranslator Load(string path, string password)
        {
            BaiduTranslator translator = AppConfig.Load<BaiduTranslator>(path);
            if (translator == null)
            {
                translator = new BaiduTranslator();
                translator.Password = PasswordHash(string.Empty, string.Empty);
                return translator;
            }
            password = password?.Trim();
            if (!translator.VerifyPassword(password))
            {
                translator.Password = string.Empty;
                return translator;
            }
            if (string.IsNullOrWhiteSpace(translator.SecretKey))
            {
                return new BaiduTranslator()
                {
                    AppID = translator.AppID,
                    Interval = translator.Interval,
                    Password = translator.Password
                };
            }
            string pass;
            if (string.IsNullOrWhiteSpace(password))
            {
                pass = $"Himesyo@QuickTranslation#7AA2F154";
            }
            else
            {
                pass = $"{password}@QuickTranslation#A9C8F49D";
            }
            byte[] data = Convert.FromBase64String(translator.SecretKey.Trim());
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = pass.ComputeSHA256();
                aes.IV = "Himesyo".ComputeMD5();
                using (ICryptoTransform transform = aes.CreateDecryptor())
                {
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    string key = Encoding.Unicode.GetString(result);
                    return new BaiduTranslator()
                    {
                        AppID = translator.AppID,
                        Interval = translator.Interval,
                        SecretKey = key,
                        Password = translator.Password
                    };
                }
            }
        }

        public void Save(string path, string password)
        {
            BaiduTranslator translator = new BaiduTranslator();
            translator.AppID = AppID?.Trim();
            translator.Interval = Interval;
            string key = SecretKey?.Trim();
            string pass;
            if (string.IsNullOrWhiteSpace(password))
            {
                password = string.Empty;
                pass = $"Himesyo@QuickTranslation#7AA2F154";
            }
            else
            {
                password = password.Trim();
                pass = $"{password}@QuickTranslation#A9C8F49D";
            }
            translator.Password = PasswordHash(translator.AppID, password);
            if (!string.IsNullOrWhiteSpace(key))
            {
                byte[] data = Encoding.Unicode.GetBytes(key);
                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = pass.ComputeSHA256();
                    aes.IV = "Himesyo".ComputeMD5();
                    using (ICryptoTransform transform = aes.CreateEncryptor())
                    {
                        byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                        translator.SecretKey = Convert.ToBase64String(result);
                    }
                }
            }
            Password = translator.Password;
            AppConfig.Save(translator, path);
        }

        private static string PasswordHash(string appid, string password)
        {
            string hash = $"{appid}|{password}@QuickTranslation#9D9BBB42".ComputeSHA256().ToShow();
            return hash;
        }
    }
}
