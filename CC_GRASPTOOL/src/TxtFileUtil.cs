using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace CC_GRASPTOOL
{
    public class TxtFileUtil
    {
        private static string _txtName = "\\cookies.txt";
        private static string _filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + _txtName;
        private 　List<string> _cookieList = new List<string>();
        private 　List<string> _urlList = new List<string>();
        private 　Dictionary<string, string> _ck_urlDic = new Dictionary<string, string>();
        //定义一个delegate委托
        public 　delegate void TxtReturnHandler(object sender , DataInfo data);
        public   event TxtReturnHandler OnTxtReturn;
        int _ck_count = 0;
        public async void readFileToList()
        {
            _cookieList.Clear();
            _urlList.Clear();
            _ck_urlDic.Clear();
            if (string.IsNullOrEmpty(_filePath))
            {
                return;
            }

            if (System.IO.File.Exists(_filePath))
            {
                Console.WriteLine("存在：" + _filePath);
                try {
                    FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                    StreamReader streamReader = new StreamReader(fs,Encoding.UTF8);
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string filestr = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    //Console.WriteLine("读取内容：\r\n" + filestr);
                    paraseTxtString(filestr);
                }catch(Exception e){

                    Console.WriteLine("读取文件出错:" + e.Message);
                }
            }
            else
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(_filePath);
                fileInfo.Create();
                Console.WriteLine("不存在：" + _filePath);
            }
        }

        private void paraseTxtString(string txtString)
        { 
            if(string.IsNullOrEmpty(txtString)){
                return;
            }
            string[] sArray = Regex.Split(txtString, "cookiePath=", RegexOptions.IgnoreCase);
            int i = 0;
            foreach (string str in sArray)
            {
                if(!string.IsNullOrEmpty(str))
                {
                    string[] subArr = Regex.Split(EasyHttpUtils.RemoveSpace(str), "fullUrlPath=", RegexOptions.IgnoreCase);
                    foreach (string sstr in subArr)
                    {
                        if(!string.IsNullOrEmpty(sstr)){
                            i++;
                            var restr = EasyHttpUtils.ReplaceNewline(sstr, string.Empty);
                            if (i % 2 == 0)
                                _urlList.Add(restr);
                            else
                                _cookieList.Add(restr);
                        }
                    }
                }
            }

            foreach(string cookies in _cookieList)
            {
              //  Console.WriteLine("cookies = " + cookies.ToString());
            }

            foreach (string url in _urlList)
            {
               // Console.WriteLine("url = " + url.ToString());
            }

            for (int ct = 0; ct < _cookieList.Count; ct++ )
            {
                _ck_urlDic.Add(_cookieList[ct],_urlList[ct]);
            }

            foreach(var obj in _ck_urlDic){
                Console.WriteLine("ck: " + obj.Key + " ,url: " + obj.Value);
                _ck_count++;
                if (OnTxtReturn != null)
                {
                    OnTxtReturn.Invoke(this, new DataInfo(Convert.ToString(_ck_count), obj.Key, "result", "state"));
                }
                else {
                    Console.WriteLine("OnTextReturn == null");
                }
            }
        }

    }
}
