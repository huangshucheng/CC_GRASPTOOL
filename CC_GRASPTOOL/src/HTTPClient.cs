using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CC_GRASPTOOL
{
    class HTTPClient
    {
        //域名
        //public static string domainName = "http://localhost:8080";
        //public static string domainName = "http://113.240.97.7:80";
        // public static string domainName = "http://115.29.136.127:80";
        // public static string domainName = "http://192.168.1.200:8080";
        //保存cookie
        private static CookieCollection cookies = null;
        //超时时间
        //private static int defaultTimeout = 5;
        //默认编码格式
        private static Encoding defaultEncoding = Encoding.UTF8;
        //默认referer
        private static string defaultReferer = "/Yj4PcServer/func_question/teacher";
        //默认 accept
        private static string defaultAccept = "*/*";
        //默认语言
        private static string defaultLanguage = "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3";
        //默认字符集
        private static string defaultCharset = "utf-8;q=0.7,*;q=0.3";
        //默认用户代理
        private static string defaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";
        //默认保持连接方式
        private static bool defaultKeepAlive = false;
        //默认保持连接时长
        //private static int defaultTimeout = 10;
        //默认格式
        private static string defaultContentType = "application/json; charset=UTF-8";
        public static string FormContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        public HTTPClient()
        {
            Console.WriteLine("HTTPClient...");
        }
        public void testHttp()
        {
            Console.WriteLine("testHttp...");
        }
        /// 将webresponse转成string
        public static string GetStringFromResponse(HttpWebResponse response, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = defaultEncoding;
            }
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse, encoding);
            Char[] readBuff = new Char[256];
            int count = streamRead.Read(readBuff, 0, 256);
            //content为http响应所返回的字符流  
            StringBuilder content = new StringBuilder();
            while (count > 0)
            {
                content.Append(readBuff, 0, count);
                count = streamRead.Read(readBuff, 0, 256);
            }
            streamRead.Close();
            return content.ToString();
        }

        /// 登陆到服务器，并保存cookies（sessionid）
        public static HttpWebResponse Get(string url, string reqContentType)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "GET";
            request.CookieContainer = new CookieContainer();
            //request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = defaultKeepAlive;
            request.UserAgent = defaultUserAgent;
            //request.Timeout = defaultTimeout;
            if (string.IsNullOrEmpty(reqContentType))
            {
                request.ContentType = defaultContentType;
            }
            else
            {
                request.ContentType = reqContentType;
            }
            HttpWebResponse rep = request.GetResponse() as HttpWebResponse;
            //cookies = request.CookieContainer.GetCookies(new Uri(referer));
            return rep;
        }
        /// 登陆到服务器，并保存cookies（sessionid）
        public static HttpWebResponse Login(string url, byte[] data, string reqContentType, string referer)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.CookieContainer = new CookieContainer();
            request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = defaultKeepAlive;
            request.UserAgent = defaultUserAgent;
            //request.Timeout = defaultTimeout;
            if (string.IsNullOrEmpty(reqContentType))
            {
                request.ContentType = defaultContentType;
            }
            else
            {
                request.ContentType = reqContentType;
            }
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse rep = request.GetResponse() as HttpWebResponse;
            cookies = request.CookieContainer.GetCookies(new Uri(referer));
            return rep;
        }
        /// 登陆到服务器，并保存cookies（sessionid）
        public static HttpWebResponse KeepAlive(string url, byte[] data, string reqContentType, string referer)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                throw new Exception("未登陆服务器");
            }
            request.CookieContainer = new CookieContainer();
            request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = defaultKeepAlive;
            request.UserAgent = defaultUserAgent;
            //request.Timeout = defaultTimeout;
            if (string.IsNullOrEmpty(reqContentType))
            {
                request.ContentType = defaultContentType;
            }
            else
            {
                request.ContentType = reqContentType;
            }
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse rep = request.GetResponse() as HttpWebResponse;
            cookies = request.CookieContainer.GetCookies(new Uri(referer));
            return rep;
        }
        /// 登出服务器，并清空cookies
        public static void Logout(string url, string reqContentType)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                throw new Exception("未登陆服务器");
            }

            request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = false;
            request.UserAgent = defaultUserAgent;

            if (string.IsNullOrEmpty(reqContentType))
            {
                request.ContentType = defaultContentType;
            }
            else
            {
                request.ContentType = reqContentType;
            }

            request.GetResponse();
            cookies = null;
        }
        /// 用get方式发送请求并获取response（sessionid为登陆后获取到的sessionid）
        public static HttpWebResponse CreateGetHttpResponse(string url, byte[] data, string reqContentType)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                throw new Exception("未登陆服务器");
            }
            request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = defaultKeepAlive;
            request.UserAgent = defaultUserAgent;
            //request.Timeout = defaultTimeout;
            if (string.IsNullOrEmpty(reqContentType))
            {
                request.ContentType = defaultContentType;
            }
            else
            {
                request.ContentType = reqContentType;
            }
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// 用POST方式发送请求并获取response（sessionid为登陆后获取到的sessionid）
        public static HttpWebResponse CreatePostHttpResponse(string url, byte[] data, string reqContentType)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                throw new Exception("未登陆服务器");
            }
            request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = defaultKeepAlive;
            request.UserAgent = defaultUserAgent;
            //request.Timeout = defaultTimeout;
            if (string.IsNullOrEmpty(reqContentType))
            {
                request.ContentType = defaultContentType;
            }
            else
            {
                request.ContentType = reqContentType;
            }
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        /// 用post方式上传文件并获取response（sessionid为登陆后获取到的sessionid）
        public static HttpWebResponse CreateUploadFileHttpResponse(string url, string fileName, Dictionary<String, Object> paras)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                throw new Exception("未登陆服务器");
            }
            request.Referer = defaultReferer;
            request.Accept = defaultAccept;
            request.Headers["Accept-Language"] = defaultLanguage;
            request.Headers["Accept-Charset"] = defaultCharset;
            request.UserAgent = defaultUserAgent;
            request.KeepAlive = defaultKeepAlive;
            request.UserAgent = defaultUserAgent;
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = defaultEncoding.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endbytes = defaultEncoding.GetBytes("\r\n--" + boundary + "--\r\n");
            //1.HttpWebRequest
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetRequestStream())
            {
                //1.1 key/value
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                if (null != paras && paras.Count > 0)
                {
                    List<String> keys = paras.Keys.ToList<String>();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, keys[i], paras[keys[i]]);
                        byte[] formitembytes = defaultEncoding.GetBytes(formitem);
                        stream.Write(formitembytes, 0, formitembytes.Length);
                    }
                }
                //1.2 file
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                stream.Write(boundarybytes, 0, boundarybytes.Length);
                string header = string.Format(headerTemplate, "file", Path.GetFileName(fileName));
                byte[] headerbytes = defaultEncoding.GetBytes(header);
                stream.Write(headerbytes, 0, headerbytes.Length);
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }
                }
                //1.3 form end
                stream.Write(endbytes, 0, endbytes.Length);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }
}
