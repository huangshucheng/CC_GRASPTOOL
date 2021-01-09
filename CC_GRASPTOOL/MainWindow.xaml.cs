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
using System.Windows.Threading;
using System.ComponentModel;

namespace CC_GRASPTOOL
{
    public partial class MainWindow : Window
    {
        Dictionary<string, string> _cooDic               = new Dictionary<string, string>();
        Dictionary<string, string> _reqHeaderDic         = new Dictionary<string, string>();
        ObservableCollection<DataInfo> _dataInfoList     = new ObservableCollection<DataInfo>();
        ObservableCollection<DataReturn> _dataReturnList = new ObservableCollection<DataReturn>();
        BackgroundWorker _bgMeet = new BackgroundWorker();
        private enum ReqMethod { GET, POST };
        private ReqMethod _reqMethod    = ReqMethod.POST;
        static string _cookieHeader     = string.Empty;
        static int _responseIndex       = 0;

        string _confReqStr  = "confReqStr";
        string _readConfStr = "readConfStr";
        string _handReqStr  = "handReqStr";
        string _doThingStr  = string.Empty;

        string _reqNumStr       = string.Empty;
        string _reqBodyStr      = string.Empty;
        string _reqUrlAddStr    = string.Empty;
        string _reqUrlSourceStr = string.Empty;
        string _reqCookieStr    = string.Empty;

        string _web  = "http://www.baidu.com";
        string _web1 = "http://www.chenkaihua.com";
        string _web2 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears";
        string _web3 = "https://wx.vivatech.cn/app/index.php";
        string _web5 = "http://api.androidhive.info/volley/person_object.json";  //for test json response
        string _web6 = "http://eservice.datcent.com/psbchn/active?code=0219l3u72fgwfL0tvHt72Ei6u729l3uP&state=123";


        public MainWindow()
        {
            InitializeComponent();
            InitBackGroundWorker();
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
            ui_listview_ck.ItemsSource  = _dataInfoList;

            var myForm = new Receiver();
            myForm.Show();
        }


        private void InitBackGroundWorker()
        {
            _bgMeet = new BackgroundWorker();
            _bgMeet.WorkerReportsProgress = true;
            _bgMeet.WorkerSupportsCancellation = true;
            _bgMeet.DoWork += new DoWorkEventHandler(bgMeetDo_ReadConf);
            _bgMeet.DoWork += new DoWorkEventHandler(bgMeetDo_ConfReq);
            _bgMeet.DoWork += new DoWorkEventHandler(bgMeetDo_handReq);
            _bgMeet.ProgressChanged += new ProgressChangedEventHandler(bgMeet_ProgressChanged);  //更新UI
            _bgMeet.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgMeet_RunWorkerCompleted); //更新UI
        }
        //清除
        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            _dataInfoList.Clear();
            _dataReturnList.Clear();
            ui_rtext_return.Text = string.Empty;
            _responseIndex = 0;

            ////test
            var myForm = new Receiver();
            myForm.Show();
        }
        //手动请求
        private void Button_Click_HandReq(object sender, RoutedEventArgs e)
        {
            if (_bgMeet.IsBusy)
            {
                return;
            }
            _bgMeet.RunWorkerAsync(_handReqStr);
        }
        //用配置请求
        private void Button_Click_ConfigReq(object sender, RoutedEventArgs e)
        {
            if (_bgMeet.IsBusy)
            {
                return;
            }
            _bgMeet.RunWorkerAsync(_confReqStr);
        }
        //读取配置
        private void Button_Click_ReadConf(object sender, RoutedEventArgs e)
        {
            if (_bgMeet.IsBusy)
            {
                return;
            }
            _bgMeet.RunWorkerAsync(_readConfStr);
            
        }
        //读配置线程
        private void bgMeetDo_ReadConf(object sender, DoWorkEventArgs e)
        {
            if (!e.Argument.Equals(_readConfStr)){
                Console.WriteLine("is busy----------------------->bgMeetDo_ReadConf");
                return;
            }
            _doThingStr = this._readConfStr;
           
            TxtFileUtil t = new TxtFileUtil();
            var list = t.readFileToList();
            for (int i = 0; i < list.Count; i++)
            {
                _bgMeet.ReportProgress(i, list[i]);
            }
        }
        private void bgMeet_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //百分比，数据
            //Console.WriteLine("进度:" + e.ProgressPercentage.ToString());
            //Console.WriteLine("进度:" + e.ProgressPercentage.ToString() + "   msg:" + e.UserState.ToString());
            //Console.WriteLine("id: " + id + " ,ck: " + cookie + "  ,body: " + body + "  ,url: " + url);
            if (_doThingStr.Equals(this._readConfStr))
            {
                DataInfo info = (DataInfo)e.UserState;
                if(info != null){
                    _dataInfoList.Add(info);
                    ui_listview_ck.Items.MoveCurrentToLast();
                    ui_listview_ck.ScrollIntoView(ui_listview_ck.Items.CurrentItem);
                }
            }else if(_doThingStr.Equals(this._handReqStr)){
                //Console.WriteLine("手动请求。。。。。。。。。。");
                string info = (string)e.UserState;
                if (string.IsNullOrEmpty(info)){
                    info = "返回空";
                }
                var tmpStr = "[" + e.ProgressPercentage.ToString() + "]," + "[" + DateTime.Now.ToLongTimeString().ToString() + "]:  " + info;
                if (tmpStr.Length > 200){
                    tmpStr = tmpStr.Substring(0, 200);
                }
                ui_rtext_return.AppendText(tmpStr + "\r\n");
                ui_rtext_return.ScrollToEnd();
            }else if(_doThingStr.Equals(this._confReqStr)){
                //Console.WriteLine("配置请求。。。。。。。。。。。");
                string info = (string)e.UserState;
                if(string.IsNullOrEmpty(info)){
                    info = "返回空";
                }
                var tmpStr = "[" + e.ProgressPercentage.ToString() + "]," + "[" + DateTime.Now.ToLongTimeString().ToString() + "]:  " + info;
                if (tmpStr.Length > 200){
                    tmpStr = tmpStr.Substring(0, 200);
                }
                ui_rtext_return.AppendText(tmpStr + "\r\n");
                ui_rtext_return.ScrollToEnd();
            }
        }
        void bgMeet_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // Console.WriteLine("完成。。。");
        }
        private void bgMeetDo_ConfReq(object sender, DoWorkEventArgs e)
        {
            if(!e.Argument.Equals(_confReqStr)){
                Console.WriteLine("is busy------------->bgMeetDo_ConfReq");
                return;
            }
            _doThingStr = this._confReqStr;
            if (_dataInfoList.Count <= 0) { return; }
            int recount = getReqCount();
            int allCount = 0;
            for (int ct = 0; ct < recount; ++ct)
            {
                for (int i = 0; i < _dataInfoList.Count; ++i)
                {
                    allCount++;
                    var url         = _dataInfoList[i].ck_url;  
                    var body        = _dataInfoList[i].ck_body;    
                    var cookie      = _dataInfoList[i].ck_cookie;

                    var urlAppend   = string.Empty;
                    var bodyAppend  = this._reqBodyStr;

                    if(!string.IsNullOrEmpty(this._reqUrlAddStr)){
                        urlAppend = "&" + this._reqUrlAddStr;
                    }
                    if (!url.Contains(urlAppend)){
                        url = EasyHttpUtils.RemoveSpace(EasyHttpUtils.ReplaceNewline(url + urlAppend, string.Empty));
                    }

                    if (!string.IsNullOrEmpty(bodyAppend)){
                        body = EasyHttpUtils.RemoveSpace(EasyHttpUtils.ReplaceNewline(bodyAppend, string.Empty));
                    }

                    EasyHttp http = EasyHttp.With(url);
                    if (http == null) {
                        _bgMeet.ReportProgress(allCount, EasyHttpUtils.UnicodeDencode("url出错!"));
                        return;
                    }
                    http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
                    //http.Data("code", "9405");//请求内容
                    http.AddHeadersByDic(_reqHeaderDic);//添加请求头
                    http.SetCookieHeader(cookie);//设置cookie
                    if (_reqMethod == ReqMethod.POST)
                    {
                        //http.PostForStringAsyc(body);
                        var resStr = http.PostForString(body);
                        _bgMeet.ReportProgress(allCount, EasyHttpUtils.UnicodeDencode(resStr));
                    }
                    else
                    {
                        //http.GetForStringAsyc();
                        var resStr = http.GetForString();
                        _bgMeet.ReportProgress(allCount, EasyHttpUtils.UnicodeDencode(resStr));
                    }
                    //http.OnDataReturn += new EasyHttp.DataReturnHandler(addDataReturn);   
                }
            }
        }
        private void bgMeetDo_handReq(object sender, DoWorkEventArgs e)
        {
            if(!e.Argument.Equals(_handReqStr)){
                Console.WriteLine("is busy------------->bgMeetDo_handReq");
                return;
            }
            _doThingStr     = _handReqStr;
            string tmpck    = _cookieHeader;
            string ckstr    = _reqCookieStr;
            string urlstr   = _reqUrlSourceStr;
            string body     = _reqBodyStr;
            var urlAppend   = string.Empty;

            if (string.IsNullOrEmpty(urlstr) || !EasyHttpUtils.CheckIsUrlFormat(urlstr))
            {
                _bgMeet.ReportProgress(0, EasyHttpUtils.UnicodeDencode("url格式不正确:" + urlstr));
                return;
            }
            if (!string.IsNullOrEmpty(this._reqUrlAddStr))
            {
                urlAppend = "&" + this._reqUrlAddStr;
            }
            if (!urlstr.Contains(urlAppend))
            {
                urlstr = EasyHttpUtils.RemoveSpace(EasyHttpUtils.ReplaceNewline(urlstr + urlAppend, string.Empty));
            }

            if (!string.IsNullOrEmpty(ckstr)){
                tmpck = EasyHttpUtils.RemoveSpace(EasyHttpUtils.ReplaceNewline(ckstr, string.Empty));
            }

            if (!string.IsNullOrEmpty(body)){
                body = EasyHttpUtils.RemoveSpace(EasyHttpUtils.ReplaceNewline(body, string.Empty));
            }

            int recount = getReqCount();
            for (int i = 0; i < recount; ++i)
            {
                EasyHttp http = EasyHttp.With(urlstr);
                if (http == null) {
                    _bgMeet.ReportProgress(i, EasyHttpUtils.UnicodeDencode("url格式不正确: " + urlstr));
                    return;
                }
                http.LogLevel(EasyHttp.EasyHttpLogLevel.Header);
                //http.Data("code", "9405");
                http.AddHeadersByDic(_reqHeaderDic);
                http.SetCookieHeader(tmpck);
                if (_reqMethod == ReqMethod.POST)
                {
                    //http.PostForStringAsyc(tmpbody);
                    var resStr = http.PostForString(body);
                    _bgMeet.ReportProgress(i, EasyHttpUtils.UnicodeDencode(resStr));
                }
                else
                {
                    //http.GetForStringAsyc();
                    var resStr = http.GetForString();
                    _bgMeet.ReportProgress(i, EasyHttpUtils.UnicodeDencode(resStr));
                }
                //http.OnDataReturn += new EasyHttp.DataReturnHandler(addDataReturn);
            }
        }
        //请求结果显示到UI
        private void addDataReturn(object sender,DataReturn data)
        {
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
            //UIHelper.DoEvents();
        }
        //读取配置，写如UI
        private void addTxtReturn(object sender, DataInfo data)
        {
            _dataInfoList.Add(new DataInfo(data.ck_id, data.ck_cookie, data.ck_body, data.ck_url));
            ui_listview_ck.Items.MoveCurrentToLast();
            ui_listview_ck.ScrollIntoView(ui_listview_ck.Items.CurrentItem);
            //UIHelper.DoEvents();
        }
        //控制请求次数
        private int getReqCount()
        {
            int reqcount = 1;
            int nstr = 0;
            string cstr = this._reqNumStr;
            if(string.IsNullOrEmpty(cstr)){
                return reqcount;            
            }
            try{
                nstr = int.Parse(cstr);
            }
            catch (Exception e){
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
        public void onUireqtypeSelChenged(object sender, SelectionChangedEventArgs e)
        {
            string selectedContent = ((ComboBoxItem)ui_req_type.SelectedItem).Content.ToString();
            if (selectedContent == "POST")
            {
                Console.WriteLine("POST");
                _reqMethod = ReqMethod.POST;
            }
            if (selectedContent == "GET")
            {
                Console.WriteLine("GET");
                _reqMethod = ReqMethod.GET;
            }
        }
        public void onText_reqNum_changed(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            this._reqNumStr = text;
        }
        private void onText_reqBody_changed(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            this._reqBodyStr = text;
        }
        private void onText_reqUrlAdd_changed(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            this._reqUrlAddStr = text;
        }

        private void onText_reqUrlSource_changed(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            this._reqUrlSourceStr = text;
        }

        private void onText_reqCookie_changed(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            this._reqCookieStr = text;
        }

        private void ui_listview_ck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
