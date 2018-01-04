using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace CC_GRASPTOOL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> _cooDic = new Dictionary<string, string>();
        Dictionary<string, string> _reqHeaderDic = new Dictionary<string, string>();

        string _web = "http://www.baidu.com";
        string _web1 = "http://www.chenkaihua.com";
        string _web2 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears";
        string _web3 = "https://wx.vivatech.cn/app/index.php";
        string _web4 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears";
        string _web5 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears ";
        
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow。。。。。。");
            _reqHeaderDic.Add("Host", "wx.vivatech.cn");
            _reqHeaderDic.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            _reqHeaderDic.Add("Proxy-Connection", "keep-alive");
            _reqHeaderDic.Add("X-Requested-With", "XMLHttpRequest");
            _reqHeaderDic.Add("Accept-Encoding", "gzip, deflate");
            _reqHeaderDic.Add("Accept-Language", "zh-cn");
            _reqHeaderDic.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            _reqHeaderDic.Add("Origin", "https://wx.vivatech.cn");
            _reqHeaderDic.Add("Content-Length", "9");
            _reqHeaderDic.Add("Connection", "keep-alive");
            _reqHeaderDic.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_3 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Mobile/13G34 MicroMessenger/6.6.1 NetType/WIFI Language/zh_CN");
            _reqHeaderDic.Add("Referer", "https://wx.vivatech.cn/app/index.php?i=2&c=entry&do=index&m=viva_njfh_4thyears&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&wxref=mp.weixin.qq.com");
            _reqHeaderDic.Add("Cookie", "PHPSESSID=9eeceebdb96c6067b0223bab25c1a714; CNZZDATA1271740069=1766148731-1514894551-%7C1515076656; PHPSESSID=9eeceebdb96c6067b0223bab25c1a714; UM_distinctid=160b6e1daae7f7-0170cb6d9-7f560309-3d10d-160b6e1daaf6b8");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EasyHttp http = EasyHttp.With(_web5);
            if(http == null){
                return;
            }
            http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
            var html = http.GetForString();
            //http.Data("code","4833");       //TODO:文本输入
            //var html = http.PostForString();
            var cookiesDic = http.Cookies();
            _cooDic = cookiesDic;
            //var ckurl = http.CookieHeader();
            //Console.WriteLine("ckurl_0: " + ckurl);
            var cookiesCount = cookiesDic.Count();
            if(cookiesCount ==  0)
                Console.WriteLine("------------->no cookies");
            else
                Console.WriteLine("---------------->have cookies");
            foreach (var item in cookiesDic)
            {
                Console.WriteLine("cookie:----->" + item.Key + " : " + item.Value);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EasyHttp http = EasyHttp.With(_web5);
            if (http == null) return;
            http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
            http.Data("code","2908");       //TODO:文本输入
            //http.HeaderCustome("X-Requested-With", "XMLHttpRequest");           //设置请求头
            //http.HeaderCustome("Accept-Language", "zh-cn");           //设置请求头
            http.AddHeadersByDic(_reqHeaderDic);
            foreach (var item in _cooDic)
            {
                http.Cookie(item.Key,item.Value);
            }
            var html = http.PostForString();
        }
    }
}
