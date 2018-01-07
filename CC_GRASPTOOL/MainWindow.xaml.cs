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
    /// //Window mainwin = Application.Current.MainWindow;  //获取主窗口
    public partial class MainWindow : Window
    {
        Dictionary<string, string> _cooDic = new Dictionary<string, string>();
        Dictionary<string, string> _reqHeaderDic = new Dictionary<string, string>();
        ObservableCollection<DataInfo> _dataInfoList = new ObservableCollection<DataInfo>();
        ObservableCollection<DataReturn> _dataReturnList = new ObservableCollection<DataReturn>();
        string _cookieHeader = string.Empty;
        int _responseIndex = 0;
        int _requestIndex = 0;
        string _web  = "http://www.baidu.com";
        string _web1 = "http://www.chenkaihua.com";
        string _web2 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears";
        string _web3 = "https://wx.vivatech.cn/app/index.php";
        string _web5 = "http://api.androidhive.info/volley/person_object.json";  //for test json response
        string _web6 = "http://eservice.datcent.com/psbchn/active?code=0219l3u72fgwfL0tvHt72Ei6u729l3uP&state=123";

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
            //_cookieHeader = "PHPSESSID=6ec48f3714a2f2e62babbce694cfc3b7;";
            //_cookieHeader = "CNZZDATA1271992096=129419380-1515220033-%7C1515220033; PHPSESSID=lpnqqqkfj1n0a96ldhicjg6s86; UM_distinctid=160ca72d4886ff-0f00fd2f2-7f560309-3d10d-160ca72d4893d2";
            //_cookieHeader = "PHPSESSID=52b9e106731a0b2f5d284fb034a6d7f0; CNZZDATA1271740069=1766148731-1514894551-%7C1515221887; PHPSESSID=52b9e106731a0b2f5d284fb034a6d7f0; UM_distinctid=160b6e1daae7f7-0170cb6d9-7f560309-3d10d-160b6e1daaf6b8";
            //_cookieHeader = "openid=oYIFj1OeZn_GIyx5QZuuGUh5J7Wc";
            //openid=oYIFj1OeZn_GIyx5QZuuGUh5J7Wc

            //_dataInfoList.Add(new DataInfo("1", "cookie--ajdkfljdfjkdjfjdjfkld", "result", "state"));
            //_dataInfoList.Add(new DataInfo("2", "cookies2--hjfkjdk", "result2", "state2"));
            //_dataReturnList.Add(new DataReturn("1", "return"));
            //_dataReturnList.Add(new DataReturn("1", "return"));

            ui_listview_ck.ItemsSource = _dataInfoList;
            ui_listview_return.ItemsSource = _dataReturnList;
        }
        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            _dataInfoList.Clear();
            _dataReturnList.Clear();
            _responseIndex = 0;
            _requestIndex = 0;
            /*
            _requestIndex++;
            EasyHttp http = EasyHttp.With(_web3);
            if(http == null)return;
            http.LogLevel(EasyHttp.EasyHttpLogLevel.None);
            http.GetForString();
            _cooDic = http.Cookies();
            var cookiesCount = _cooDic.Count();
            if(cookiesCount ==  0)
                Console.WriteLine("------------->没有cookies");
            else
                Console.WriteLine("---------------->有cookies");
            string ckurl = http.CookieHeader();
            if (!string.IsNullOrEmpty(ckurl))
            {
                _dataInfoList.Add(new DataInfo(_requestIndex.ToString(), ckurl, "result", "state"));
            }
            else
            {
                _dataInfoList.Add(new DataInfo(_requestIndex.ToString(), "empty", "result", "state"));
            }
        */
            //var ckipt = StringFromRichTextBox(ui_text_numinput);
            Console.WriteLine("ckipt--->{0}", ui_text_numinput.Text);
            int reqCount = getReqCount();
            Console.WriteLine("rec--->{0}", reqCount);
            string sss = "jfkdljklfsjklfj\u8be5jkdslfjskldj";
            Console.WriteLine("uni--->{0}", EasyHttpUtils.UnicodeDencode(sss));
            
        }
        //btn2
        private void Button_Click_Request(object sender, RoutedEventArgs e)
        {
            string tmpweb       = "";
            string tmpparam     = "";
            string tmpck        = _cookieHeader;
            string ckstr        = ui_rtext_ckinput.Text;
            string urlstr       = ui_rtext_urlinput.Text;
            string paramstr     = ui_text_paraminput.Text;

            if(!string.IsNullOrEmpty(ckstr)){
                tmpck = ckstr;
            }

            if (!string.IsNullOrEmpty(urlstr) && EasyHttpUtils.CheckIsUrlFormat(urlstr)){
                tmpweb = urlstr;
            }

            if(!string.IsNullOrEmpty(paramstr)){
                tmpparam = paramstr;    //TODO  验证
            }

            int recount = getReqCount();
            Console.WriteLine("请求次数：{0}", recount);
            for (int i = 0; i < recount;++i)
            {
                EasyHttp http = EasyHttp.With(tmpweb);
                if (http == null) return;
                http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
                //请求内容表单
                //http.Data("code", "9405");
                //添加请求头
                http.AddHeadersByDic(_reqHeaderDic);   
                //设置cookie
                http.SetCookieHeader(tmpck);
                //请求
                http.PostForStringAsyc(tmpparam);
                http.OnDataReturn += new EasyHttp.DataReturnHandler(addDataReturn);
            }
        }

        private void Button_Click_Cookie(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("开始套cookie");
        }

        private void addDataReturn(object sender,DataReturn data)
        {
            _responseIndex++;
            //Console.WriteLine("hcc--->{0},{1}" ,data.return_id , data.return_data);
            var tmpStr = data.return_data;
            if (tmpStr.Length > 200)
            {
                tmpStr = tmpStr.Substring(0, 200);
            }
            _dataReturnList.Add(new DataReturn(_responseIndex.ToString(),tmpStr));    
        }

        private int getReqCount()
        {
            int reqcount = 1;
            int nstr = 0;
            string cstr = ui_text_numinput.Text;
            if(string.IsNullOrEmpty(cstr)){
                return reqcount;            
            }
            try{
                nstr = int.Parse(cstr);
            }
            catch (Exception e){
                Console.WriteLine("error:"+e.Message);
            }
            if(nstr > 0){
                reqcount = nstr;
            }
            if (reqcount > 50)
                reqcount = 50;      //最多请求50次
            return reqcount;
        }
    }
}
