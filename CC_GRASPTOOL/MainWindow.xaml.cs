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
using System.Collections.ObjectModel;


namespace CC_GRASPTOOL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> _cooDic = new Dictionary<string, string>();
        Dictionary<string, string> _reqHeaderDic = new Dictionary<string, string>();
        ObservableCollection<DataInfo> _dataInfoList = new ObservableCollection<DataInfo>();
        string _cookieHeader = string.Empty;
        int _index = 0;
        string _web  = "http://www.baidu.com";
        string _web1 = "http://www.chenkaihua.com";
        string _web2 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears";
        string _web3 = "https://wx.vivatech.cn/app/index.php";
        string _web5 = "http://api.androidhive.info/volley/person_object.json";  //for test json response

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow。。。。。。");
           // _reqHeaderDic.Add("Host", "wx.vivatech.cn");              //自动计算，不用添加也没关系 
            //_reqHeaderDic.Add("Origin", "https://wx.vivatech.cn");    //不会自动添加，不用也没关系
            //_reqHeaderDic.Add("Content-Length", "9");                 //自动计算
            //Referer :服务端用来验证页面来源，不用添加也没关系
            //_reqHeaderDic.Add("Referer", "https://wx.vivatech.cn/app/index.php?i=2&c=entry&do=index&m=viva_njfh_4thyears&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O");
            _reqHeaderDic.Add("Accept", "application/json, text/javascript,text/html,application/xhtml+xml,application/xml, */*; q=0.01");//客户端希望接收到的数据格式
            _reqHeaderDic.Add("Proxy-Connection", "keep-alive");
            _reqHeaderDic.Add("X-Requested-With", "XMLHttpRequest");
            _reqHeaderDic.Add("Accept-Encoding", "gzip, deflate");
            _reqHeaderDic.Add("Accept-Language", "zh-cn");
            _reqHeaderDic.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");      //发送的数据格式
            _reqHeaderDic.Add("Connection", "keep-alive");
            _reqHeaderDic.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_3 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Mobile/13G34 MicroMessenger/6.6.1 NetType/WIFI Language/zh_CN");
            //设置cookies
            //_cookieHeader = "PHPSESSID=483677829f2800a2dfdb19e8fba35575;CNZZDATA1271740069=1766148731-1514894551-%7C1515151149;PHPSESSID=483677829f2800a2dfdb19e8fba35575;UM_distinctid=160b6e1daae7f7-0170cb6d9-7f560309-3d10d-160b6e1daaf6b8";
            //_cookieHeader = "PHPSESSID=6ec48f3714a2f2e62babbce694cfc3b7; CNZZDATA1271740069=1766148731-1514894551-%7C1515165502; PHPSESSID=6ec48f3714a2f2e62babbce694cfc3b7; UM_distinctid=160b6e1daae7f7-0170cb6d9-7f560309-3d10d-160b6e1daaf6b8";
            _cookieHeader = "PHPSESSID=6ec48f3714a2f2e62babbce694cfc3b7;";
            //_dataInfoList.Add(new DataInfo("1", "cookie--ajdkfljdfjkdjfjdjfkld", "result", "state"));
            //_dataInfoList.Add(new DataInfo("2", "cookies2--hjfkjdk", "result2", "state2"));
            ui_listview.ItemsSource = _dataInfoList;  
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _index++;
            EasyHttp http = EasyHttp.With(_web2);
            if(http == null){
                return;
            }
            http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
            var html = http.GetForString();
            _cooDic = http.Cookies();
            var cookiesCount = _cooDic.Count();
            if(cookiesCount ==  0)
                Console.WriteLine("------------->没有cookies");
            else
                Console.WriteLine("---------------->有cookies");
            //foreach (var item in _cooDic){
            //    Console.WriteLine("cookie:----->" + item.Key + " : " + item.Value);
            //}
            string ckurl = http.CookieHeader();
            if (!string.IsNullOrEmpty(ckurl))
            {
                _dataInfoList.Add(new DataInfo(_index.ToString(), ckurl, "result", "state"));
            }
            else
            {
                _dataInfoList.Add(new DataInfo(_index.ToString(), "empty", "result", "state"));
            }
            //Console.WriteLine("ckurl_0: " + ckurl);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EasyHttp http = EasyHttp.With(_web2);
            if (http == null) return;
            http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
            //TODO:文本输入
            http.Data("code","6422");
            //添加请求头
            http.AddHeadersByDic(_reqHeaderDic);   
            //设置cookie
            http.SetCookieHeader(_cookieHeader);
            //请求
            string respStr = http.PostForString();
            Console.WriteLine("\n");
            Console.WriteLine("网页返回值:\n" + respStr);
            //text_response.Inlines.Add(new Run(respStr));
            /*
            foreach (DataInfo _data in _dataInfoList)
            {
                var id = _data.ck_id;
                var co = _data.ck_cookie;
                var re = _data.ck_result;
                var st = _data.ck_state;
                Console.WriteLine("id:" + id + " ,co:" + co + " ,re:"+re + " ,st:" + st);
                if(id.Equals("1")){
                    _data.ck_cookie = "huangshucheng";
                }
            }
             * */
            //var gbk = Encoding.GetEncoding("gbk");
            var gbk = Encoding.GetEncoding("gb2312");
            Console.WriteLine("bgk:" + gbk.CodePage);
        }
    }
}
