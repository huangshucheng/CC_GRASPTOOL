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

//using Fidder;


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
        static string _cookieHeader = string.Empty;
        static int _responseIndex = 0;
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
            //_reqHeaderDic.Add("Content-Length", "9");                 //自动计算，不用也没关系
            //Referer :服务端用来验证页面来源，不用添加也没关系
            //_reqHeaderDic.Add("Referer", "");
            _reqHeaderDic.Add("Accept", "application/json, text/javascript,text/html,application/xhtml+xml,application/xml, */*; q=0.01");//客户端希望接收到的数据格式
            _reqHeaderDic.Add("Proxy-Connection", "keep-alive");
            _reqHeaderDic.Add("X-Requested-With", "XMLHttpRequest");
            _reqHeaderDic.Add("Accept-Encoding", "gzip, deflate");
            _reqHeaderDic.Add("Accept-Language", "zh-cn");
            _reqHeaderDic.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");      //发送的数据格式
            _reqHeaderDic.Add("Connection", "keep-alive");
            _reqHeaderDic.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_3 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Mobile/13G34 MicroMessenger/6.6.1 NetType/WIFI Language/zh_CN");
            
            //设置cookies
            //_cookieHeader = "PHPSESSID=6ec48f3714a2f2e62babbce694cfc3b7;";
            //_dataInfoList.Add(new DataInfo("2", "cookies2--hjfkjdk", "result2", "state2"));
            //_dataReturnList.Add(new DataReturn("1", "return"));

            ui_listview_ck.ItemsSource      = _dataInfoList;
            //ui_listview_return.ItemsSource  = _dataReturnList;
        }
        //清除
        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            _dataInfoList.Clear();
            _dataReturnList.Clear();
            ui_rtext_return.Text = string.Empty;
            _responseIndex = 0;
        }
        //手动请求
        private void Button_Click_HandReq(object sender, RoutedEventArgs e)
        {
            try
            {
                ParameterizedThreadStart ts = new ParameterizedThreadStart(HandReqThread);
                Thread tmpThread = new Thread(ts);
                //System.Threading.Thread.Sleep(100);
                tmpThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Button_Click_HandReqError:" + ex.Message);
            }
        }
        //用配置请求
        private void Button_Click_ConfigReq(object sender, RoutedEventArgs e)
        {
            try
            {
                ParameterizedThreadStart ts = new ParameterizedThreadStart(ConfReqThread);
                Thread tmpThread = new Thread(ts);
                //System.Threading.Thread.Sleep(100);
                tmpThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Button_Click_ConfigReqError:" + ex.Message);
            }

        }
        //读取配置
        private void Button_Click_ReadConf(object sender, RoutedEventArgs e)
        {
            try
            {
                ParameterizedThreadStart ts = new ParameterizedThreadStart(ReadConfigThread);
                Thread tmpThread = new Thread(ts);
                //System.Threading.Thread.Sleep(100);
                tmpThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Button_Click_ReadConfError:" + ex.Message);
            }
            
        }
        //手动请求线程
        private void HandReqThread(object param)
        {
             Dispatcher.BeginInvoke((Action)delegate() {
                 string tmpweb = "";
                 string tmpparam = "";
                 string tmpck = _cookieHeader;
                 string ckstr = ui_rtext_ckinput.Text;
                 string urlstr = ui_rtext_urlinput.Text;
                 string paramstr = ui_text_paraminput.Text;

                 if (!string.IsNullOrEmpty(ckstr))
                 {
                     tmpck = ckstr;
                 }

                 if (!string.IsNullOrEmpty(urlstr) && EasyHttpUtils.CheckIsUrlFormat(urlstr))
                 {
                     tmpweb = urlstr;
                 }

                 if (!string.IsNullOrEmpty(paramstr))
                 {
                     tmpparam = paramstr;    //TODO  验证请求参数
                 }

                 int recount = getReqCount();
                 Console.WriteLine("请求次数：{0}", recount);
                 for (int i = 0; i < recount; ++i)
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
             });
             //System.Threading.Thread.Sleep(100);
        }

        //读配置线程
        private void ReadConfigThread(object sender)
        {
            Dispatcher.BeginInvoke((Action)delegate()
            {
                TxtFileUtil t = new TxtFileUtil();
                t.readFileToList();
                t.OnTxtReturn += new TxtFileUtil.TxtReturnHandler(addTxtReturn);
            });
            //System.Threading.Thread.Sleep(100);
        }

        //配置请求线程
        private void ConfReqThread(object param)
        {

            Dispatcher.BeginInvoke((Action)delegate() {
                if (_dataInfoList.Count <= 0) { return; }
                int recount = getReqCount();
                for (int ct = 0; ct < recount; ++ct)
                {
                    for (int i = 0; i < _dataInfoList.Count; ++i)
                    {
                        var url = _dataInfoList[i].ck_url;
                        var body = _dataInfoList[i].ck_body;
                        var cookie = _dataInfoList[i].ck_cookie;
                        EasyHttp http = EasyHttp.With(url);
                        if (http == null) return;
                        http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
                        //请求内容
                        //http.Data("code", "9405");
                        //添加请求头
                        http.AddHeadersByDic(_reqHeaderDic);
                        //设置cookie
                        http.SetCookieHeader(cookie);
                        //请求
                        http.PostForStringAsyc(body);   //请求内容放在这里也可
                        http.OnDataReturn += new EasyHttp.DataReturnHandler(addDataReturn);
                    }
                }
            });
            //System.Threading.Thread.Sleep(100);
        }

        //请求结果显示到UI
        private void addDataReturn(object sender,DataReturn data)
        {
            /*
            _responseIndex++;
            //Console.WriteLine("hcc--->{0},{1}" ,data.return_id , data.return_data);
            var tmpStr = data.return_data;
            if (tmpStr.Length > 200)
            {
                tmpStr = tmpStr.Substring(0, 200);
            }
            _dataReturnList.Add(new DataReturn(_responseIndex.ToString(),tmpStr));
            ui_listview_return.Items.MoveCurrentToLast();
            ui_listview_return.ScrollIntoView(ui_listview_return.Items.CurrentItem);
             */
            
            if(data == null){
                return;
            }
            _responseIndex++;
            var tmpStr ="[" + _responseIndex + "]," +  "[" + DateTime.Now.ToLongTimeString().ToString() + "]:  " + data.return_data;
            if (tmpStr.Length > 200)
            {
                tmpStr = tmpStr.Substring(0, 200);
            }
            ui_rtext_return.AppendText(tmpStr + "\r\n");
            ui_rtext_return.ScrollToEnd();
        }
        //读取配置，写如UI
        private void addTxtReturn(object sender, DataInfo data)
        {
            _dataInfoList.Add(new DataInfo(data.ck_id, data.ck_cookie, data.ck_body, data.ck_url));
            ui_listview_ck.Items.MoveCurrentToLast();
            ui_listview_ck.ScrollIntoView(ui_listview_ck.Items.CurrentItem);
        }
        //控制请求次数
        private int getReqCount()
        {
            int reqcount = 1;
            int nstr = 0;
            string cstr = ui_text_numinput.Text;
            if(string.IsNullOrEmpty(cstr)){
                return reqcount;            
            }
            try
            {
                nstr = int.Parse(cstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("error:" + e.Message);
                return reqcount;
            }
            if(nstr > 0){
                reqcount = nstr;
            }
            if (reqcount >= 100)
                reqcount = 100;      //最多请求100次
            return reqcount;
        }
    }
}
