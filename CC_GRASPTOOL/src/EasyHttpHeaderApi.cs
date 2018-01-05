using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CC_GRASPTOOL
{
 public   partial class EasyHttp
    {

        /*
         * 受限制的标头包括︰
               接受
               连接
               内容长度
               内容类型
               日期
               期望
               Host
               如果-修改-自
               范围
               引用站点
               传送编码
               用户代理
               代理连接
         //http://blog.csdn.net/u011127019/article/details/52571317
         */
        #region 设置头信息
        /// <summary>
        /// 设置自定义头部键值对
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EasyHttp HeaderCustome(string name, string value)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)){
                return this;
            }
            _headers.Add(name, value);
            return this;
        }

        public EasyHttp AddHeadersByDic(Dictionary<string, string> dic)
        {
            if(dic.Count() == 0 || dic == null){
                return this;
            }
            foreach (var item in dic)
            {
                HeaderCustome(item.Key, item.Value);
            }
            return this;
        }

        /// <summary>
        /// 设置一个默认头信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public EasyHttp DefaultHeaderCustome(string name, string value)
        {
            _defaultHeaders.Add(name, value);
            return this;
        }

        /// <summary>
        /// 设置当前请求的UserAgent头为指定的值
        /// </summary>
        /// <param name="userAgent">UserAgent值</param>
        /// <returns></returns>
        public EasyHttp UserAgent(string userAgent)
        {
            _tempRequest.UserAgent = userAgent;
            return this;
        }

        /// <summary>
        /// 设置默认的UserAgent头，当没有设置UserAgent时(<see cref="UserAgent"/>),使用的UserAgent值
        /// </summary>
        /// <param name="userAgent">UserAgent值</param>
        /// <returns></returns>
        public EasyHttp DefaultUserAgent(string userAgent)
        {
            _defaultHeaderRequest.UserAgent = userAgent;
            return this;
        }

        /// <summary>
        /// 设置Http请求中的Refer行值
        /// </summary>
        /// <param name="referer">Refer行的值</param>
        /// <returns></returns>
        public EasyHttp Referer(string referer)
        {
            _tempRequest.Referer = referer;
            return this;
        }

        /// <summary>
        /// 默认Refer值
        /// </summary>
        /// <param name="referer">Refer行的值</param>
        /// <returns></returns>
        public EasyHttp DefaultReferer(string referer)
        {
            _defaultHeaderRequest.Referer = referer;
            return this;
        }
        /// <summary>
        /// 设置请求的<c>Accept-Encoding</c>行的值
        /// </summary>
        /// <param name="acceptEncoding"><c>Accept-Encoding</c>行的值</param>
        /// <returns></returns>
        public EasyHttp AcceptEncoding(string acceptEncoding)
        {
            _headers.Add("Accept-Encoding", acceptEncoding);
            return this;
        }

        /// <summary>
        /// 设置请求的<c>Accept-Encoding</c>行的值
        /// </summary>
        /// <param name="acceptEncoding"><c>Accept-Encoding</c>行的值</param>
        /// <returns></returns>
        public EasyHttp DefaultAcceptEncoding(string acceptEncoding)
        {
            _defaultHeaders.Add("Accept-Encoding", acceptEncoding);
            return this;
        }
        /// <summary>
        /// 设置请求的<c>Accept-Language</c>的值
        /// </summary>
        /// <param name="acceptLanguage"><c>Accept-Language</c>的值</param>
        /// <returns></returns>
        public EasyHttp AcceptLanguage(string acceptLanguage)
        {
            _headers.Add("Accept-Language", acceptLanguage);
            return this;
        }
        /// <summary>
        /// 设置默认的<c>Accept-Language</c>的值
        /// </summary>
        /// <param name="acceptLanguage"><c>Accept-Language</c>的值</param>
        /// <returns></returns>
        public EasyHttp DefaultAcceptLanguage(string acceptLanguage)
        {
            _defaultHeaders.Add("Accept-Language", acceptLanguage);
            return this;
        }

        /// <summary>
        /// 设置<c>Accept</c>值
        /// </summary>
        /// <param name="accept"><c>Accept</c>值</param>
        /// <returns></returns>
        public EasyHttp Accept(string accept)
        {
            _tempRequest.Accept = accept;
            return this;
        }

        /// <summary>
        /// 设置默认<c>Accept</c>值
        /// </summary>
        /// <param name="accept">默认<c>Accept</c>值</param>
        /// <returns></returns>
        public EasyHttp DefaultAccept(string accept)
        {
            _defaultHeaderRequest.Accept = accept;
            return this;
        }

        /// <summary>
        /// 设置请求的<c>Content-Type</c>值
        /// </summary>
        /// <param name="contentType"><c>Content-Type</c>值</param>
        /// <returns></returns>
        public EasyHttp ContentType(string contentType)
        {
            _tempRequest.ContentType = contentType;
            return this;
        }
        /// <summary>
        /// 设置默认请求的<c>Content-Type</c>值
        /// </summary>
        /// <param name="contentType"><c>Content-Type</c>值</param>
        /// <returns></returns>
        public EasyHttp DefaultContentType(string contentType)
        {
            _defaultHeaderRequest.ContentType = contentType;
            return this;
        }

        /// <summary>
        /// 设置请求是否KeepAlive
        /// </summary>
        /// <param name="keepAlive"></param>
        /// <returns></returns>
        public EasyHttp KeepAlive(bool keepAlive)
        {
            _tempRequest.KeepAlive = keepAlive;
            return this;
        }
        /// <summary>
        /// 设置默认请求是否KeepAlive
        /// </summary>
        /// <param name="keepAlive"></param>
        /// <returns></returns>
        public EasyHttp DefaultKeepAlive(bool keepAlive)
        {
            _defaultHeaderRequest.KeepAlive = keepAlive;
            return this;
        }
        /// <summary>
        /// 设置请求是否带上<c>Expect100Continue</c>
        /// </summary>
        /// <param name="expect100Continue"></param>
        /// <returns></returns>
        public EasyHttp Expect100Continue(bool expect100Continue)
        {
            _tempRequest.ServicePoint.Expect100Continue = expect100Continue;
            return this;
        }
        /// <summary>
        /// 设置默认请求是否带上<c>Expect100Continue</c>
        /// </summary>
        /// <param name="expect100Continue"></param>
        /// <returns></returns>
        public EasyHttp DefaultExpect100Continue(bool defaultExpect100Continue)
        {
            _defaultHeaderRequest.ServicePoint.Expect100Continue = defaultExpect100Continue;
            return this;
        }
        public EasyHttp AutomaticDecompression(DecompressionMethods decompressionMethods)
        {
            _tempRequest.AutomaticDecompression = decompressionMethods;
            return this;
        }
        public EasyHttp DefaultAutomaticDecompression(DecompressionMethods decompressionMethods)
        {
            _defaultHeaderRequest.AutomaticDecompression = decompressionMethods;
            return this;
        }
        #endregion
    }
}
