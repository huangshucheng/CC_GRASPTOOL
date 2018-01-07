using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace CC_GRASPTOOL
{
  public  class EasyHttpUtils
    {
        public static string NameValuesToQueryParamString(NameValueCollection nameValueCollection)
        {
            StringBuilder builder = new StringBuilder();
            //string nameValue = nameValueCollection["s"];
            if (nameValueCollection == null || nameValueCollection.Count == 0)
            {
                return string.Empty;
            }

            foreach (string key in nameValueCollection.Keys)
            {
                var value = nameValueCollection[key];
                builder.Append(key).Append('=').Append(nameValueCollection[key]).Append("&");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public static void CopyHttpHeader(HttpWebRequest fromRequest,HttpWebRequest defaultRequest, HttpWebRequest toRequest)
        {
            //设置头部信息
            if (string.IsNullOrEmpty(fromRequest.Accept)) 
                toRequest.Accept = defaultRequest.Accept;
            else
                toRequest.Accept = fromRequest.Accept;

            if (string.IsNullOrEmpty(fromRequest.ContentType))
                toRequest.ContentType = defaultRequest.ContentType;
            else 
                toRequest.ContentType = fromRequest.ContentType;

            if (string.IsNullOrEmpty(fromRequest.Referer))
                toRequest.Referer = defaultRequest.Referer;
            else
                toRequest.Referer = fromRequest.Referer;

            if (string.IsNullOrEmpty(fromRequest.UserAgent))
                toRequest.UserAgent = defaultRequest.UserAgent;
            else
                toRequest.UserAgent = fromRequest.UserAgent;

            if (toRequest.AutomaticDecompression != fromRequest.AutomaticDecompression)
                toRequest.AutomaticDecompression = fromRequest.AutomaticDecompression;
            else
                toRequest.AutomaticDecompression = defaultRequest.AutomaticDecompression;

            toRequest.ClientCertificates = fromRequest.ClientCertificates;
            toRequest.Connection = fromRequest.Connection;
            toRequest.AllowWriteStreamBuffering = fromRequest.AllowWriteStreamBuffering;
            toRequest.ContinueDelegate = fromRequest.ContinueDelegate;
            toRequest.Credentials = fromRequest.Credentials;
            toRequest.UseDefaultCredentials = fromRequest.UseDefaultCredentials;
            toRequest.Expect = fromRequest.Expect;
            toRequest.IfModifiedSince = fromRequest.IfModifiedSince;

            if (toRequest.KeepAlive != fromRequest.KeepAlive)
                toRequest.KeepAlive = fromRequest.KeepAlive;
            else 
                toRequest.KeepAlive = defaultRequest.KeepAlive;

            toRequest.TransferEncoding = fromRequest.TransferEncoding;
            if (toRequest.AllowAutoRedirect != fromRequest.AllowAutoRedirect)
                toRequest.AllowAutoRedirect = fromRequest.AllowAutoRedirect;
            else 
                toRequest.AllowAutoRedirect = defaultRequest.AllowAutoRedirect;

            if (toRequest.Timeout != fromRequest.Timeout) 
                toRequest.Timeout = fromRequest.Timeout;
            else 
                toRequest.Timeout = defaultRequest.Timeout;

            if (toRequest.ServicePoint.Expect100Continue != fromRequest.ServicePoint.Expect100Continue)
                toRequest.ServicePoint.Expect100Continue = fromRequest.ServicePoint.Expect100Continue;
            else
                toRequest.ServicePoint.Expect100Continue = defaultRequest.ServicePoint.Expect100Continue;
        }

        public static string NameValuesToQueryParamString(List<KeyValue> nameValueCollection)
        {
            StringBuilder builder = new StringBuilder();
            //string nameValue = nameValueCollection["s"];
            if (nameValueCollection == null || nameValueCollection.Count == 0)
            {
                return string.Empty;
            }
            foreach (KeyValue keyValue in nameValueCollection)
            {
                builder.Append(keyValue.Key).Append('=').Append(keyValue.Value).Append('&');
            }
            if (builder.Length > 0)

                builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
        
        public static string ReadAllAsString(Stream stream, Encoding encoding)
        {
            if(stream == null || encoding == null){
                return null;
            }
            string html = string.Empty;
            using (var responseStream = new StreamReader(stream, encoding))
            {
                html = responseStream.ReadToEnd();
            }
            return html;
        }

        public static long ReadAllAsFile(Stream stream, long length, string filePath)
        {
            long currentTotal = 0;
            byte[] buffer = null;
            //判断文件大小，如果大于1m的，则buffer大小为10k，否则为1k
            if (length > 1 * 1024)
            {
                buffer = new byte[10 * 1024];
            }
            else
            {
                buffer = new byte[1024];
            }

            using (BinaryReader reader = new BinaryReader(stream,Encoding.UTF8,true))
            {
                using (FileStream lxFS = new FileStream(filePath, FileMode.Create))
                {
                    int size = -1;
                    while ((size = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        lxFS.Write(buffer, 0, size);
                        currentTotal += size;
                    }
                }
            }
            return currentTotal;
        }
        public static void WriteFileToRequest(HttpWebRequest request, List<KeyValue> nvc)
        {
            //   log.Debug(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = request;
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            using (var rs = wr.GetRequestStream())
            {
                // 普通参数模板
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                //带文件的参数模板
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                foreach (KeyValue keyValue in nvc)
                {
                    //如果是普通参数
                    if (keyValue.FilePath == null)
                    {
                        rs.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, keyValue.Key, keyValue.Value);
                        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                        rs.Write(formitembytes, 0, formitembytes.Length);
                    }
                    //如果是文件参数,则上传文件
                    else
                    {
                        rs.Write(boundarybytes, 0, boundarybytes.Length);

                        string header = string.Format(headerTemplate, keyValue.Key, keyValue.FilePath, keyValue.ContentType);
                        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                        rs.Write(headerbytes, 0, headerbytes.Length);

                        using (var fileStream = new FileStream(keyValue.FilePath, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead = 0;
                            long total = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {

                                rs.Write(buffer, 0, bytesRead);
                                total += bytesRead;
                            }
                        }
                    }

                }
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }
        }
        /// <summary>
        /// 检测串值是否为合法的网址格式
        /// </summary>
        /// <param name="strValue">要检测的String值</param>
        /// <returns>成功返回true 失败返回false</returns>
        public static bool CheckIsUrlFormat(string strValue)
        {
            //return CheckIsFormat(@"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", strValue);
            string pattern = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            //string pattern = @"(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$";
            return CheckIsFormat(pattern, strValue);
        }
        public static string CheckIsWithHttp(string Value)
        {
            if(Value.Length < 8){
                return Value;
            }
            if ((Value.Substring(0, 7) != "http://") && (Value.Substring(0, 8) != "https://"))
            {
                Value = "http://" + Value;
            }
            return Value;
        }
        /// <summary>
        /// 检测串值是否为合法的格式
        /// </summary>
        /// <param name="strRegex">正则表达式</param>
        /// <param name="strValue">要检测的String值</param>
        /// <returns>成功返回true 失败返回false</returns>
        public static bool CheckIsFormat(string strRegex,string strValue)
        {
            if(strValue != null && strValue.Trim() != "")
            {               
                Regex re = new Regex(strRegex);
                if (re.IsMatch(strValue))
                    return true;
                else
                    return false;
            }
            return false;
        }
        
      public static string toJson(string content)
      {
          if (string.IsNullOrEmpty(content)){
              return string.Empty;
          }
          return content;
      }

      public static string toXml(string content)
      {
          if (string.IsNullOrEmpty(content)){
              return string.Empty;
          }
          try
          {
              XmlDocument xd = new XmlDocument();
              xd.LoadXml(content);
              StringBuilder sb = new StringBuilder();
              StringWriter sw = new StringWriter(sb);
              XmlTextWriter xtw = null;
              try
              {
                  xtw = new XmlTextWriter(sw);
                  xtw.Formatting = System.Xml.Formatting.Indented;
                  xtw.Indentation = 4;
                  xtw.IndentChar = ' ';
                  xd.WriteTo(xtw);
              }
              finally
              {
                  if (xtw != null)
                      xtw.Close();
              }
              return sb.ToString();
          }
          catch (Exception e)
          {
              return e.Message;
          }
      }

      public static string toHtml(string content)
      {
          if(string.IsNullOrEmpty(content)){
              return string.Empty;
          }
          Regex re = new Regex("(\r*\n[ \t\r\n]*\n){1,}", RegexOptions.Compiled);
          content = re.Replace(content, "\n");
          return content;
      }
      //unicode 转中文
      public static string UnicodeDencode(string str)
      {
          if (string.IsNullOrWhiteSpace(str))
              return str;
          string tmpstr = str;
          try {
              tmpstr = Regex.Unescape(str);
          }
          catch(Exception e){
              Console.WriteLine("UnicodeDencode error:{0}",e.Message);
          }
          return tmpstr;
      }
      //中文转unicode
      public static string UnicodeEncode(string str)
      {
          if (string.IsNullOrWhiteSpace(str))
              return str;
          StringBuilder strResult = new StringBuilder();
          if (!string.IsNullOrEmpty(str))
          {
              for (int i = 0; i < str.Length; i++)
              {
                  strResult.Append("\\u");
                  strResult.Append(((int)str[i]).ToString("x4"));
              }
          }
          return strResult.ToString();
      }
      //去除空格字符
      public static string RemoveSpace(string content)
      {
          return new Regex(@"\s").Replace(content + "", string.Empty);
      }
      //替换换行
      public static string ReplaceNewline(string content, string newValue)
      {
          return (content + "").Replace("\n\r", newValue).Replace("\r\n", newValue).Replace("\r", newValue).Replace("\n", newValue).Replace("\t", newValue);
      }
    }
}
