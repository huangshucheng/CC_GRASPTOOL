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
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow。。。。。。");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _web = "http://www.baidu.com";
            string _web1 = "http://www.chenkaihua.com";
            string _web2 = "https://wx.vivatech.cn/app/index.php?i=2&c=entry&fromuser=ot7eUuOEL5zSTiEWKEaf7eqeth_s&sign=bb33QB3lZbVmVl1kTc02VlMnNbXgpO0O0OTO0O0OVGlFV0tFYWY3ZXFldGhfcwO0O0OO0O0O&do=compare&m=viva_njfh_4thyears";
            string _web3 = "https://wx.vivatech.cn/app/index.php";
           // string html = EasyHttp.With("http://www.baidu.com").Data("key", "value").GetForString();
           // Console.WriteLine("hcc--->html: {0}",html);
            /*
            string html_1 = EasyHttp.With(_web)
                .Cookie("sessionId", "cookieValue")
                .GetForString();
            */
            EasyHttp http = EasyHttp.With(_web3);
            http.LogLevel(EasyHttp.EasyHttpLogLevel.None);
            var html = http.GetForString();
            var cookiesDic = http.Cookies();
            var cookiesCount = cookiesDic.Count();
            if(cookiesCount ==  0)
                Console.WriteLine("no cookies------------->hcc");
            else
                Console.WriteLine("have cookies---------------->hcc");
            foreach (var item in cookiesDic)
            {
                Console.WriteLine("key: " + item.Key + " ,value: " + item.Value);
            }
        }
    }
}
