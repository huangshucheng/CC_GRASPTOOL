﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;

namespace CC_GRASPTOOL
{
    public class TxtFileUtil
    {
        private static string _txtName       = "\\cookies.txt";
        private static string _filePath      = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + _txtName;
        public 　List<string> _cookieList   = new List<string>();
        public 　List<string> _urlList      = new List<string>();
        public   List<string> _bodyList     = new List<string>();
        //定义一个delegate委托
        public 　delegate void TxtReturnHandler(object sender , DataInfo data);
        public   event TxtReturnHandler OnTxtReturn;

        public TxtFileUtil()
        {
        }

        public List<DataInfo> readFileToList()
        {
            List<DataInfo> dataInfoList = new List<DataInfo>();
            _cookieList.Clear();
            _urlList.Clear();
            _bodyList.Clear();
            if (string.IsNullOrEmpty(_filePath))
            {
                return dataInfoList;
            }

            if (System.IO.File.Exists(_filePath))
            {
                Console.WriteLine("存在：" + _filePath);
                try {
                    FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
                    StreamReader streamReader = new StreamReader(fs,Encoding.UTF8);
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string filestr = streamReader.ReadToEnd();
                    //string filestr = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    dataInfoList = paraseTxtString(filestr);
                }catch(Exception e){
                    Console.WriteLine("读取文件出错:" + e.Message);
                }
            }
            else
            {
                Console.WriteLine("不存在：" + _filePath);
                try {
                    FileStream fs = File.Create(_filePath);
                    fs.Close();
                }catch(Exception e){
                    Console.WriteLine("创建文件出错:" + e.Message);
                }
            }
            return dataInfoList;
        }

        private List<DataInfo> paraseTxtString(string txtString)
        {
            List<DataInfo> dataInfoList = new List<DataInfo>();
            if(string.IsNullOrEmpty(txtString)){
                return dataInfoList;
            }

            var __tmpstr = EasyHttpUtils.RemoveSpace(EasyHttpUtils.ReplaceNewline(txtString, string.Empty));
            //Console.WriteLine("去换行:"+ __tmpstr);
            string[] sArray = Regex.Split(__tmpstr, "hcc_cookiePath=", RegexOptions.IgnoreCase);
            int i = 0;
            int j = 0;
            foreach (string str_1 in sArray)
            {
                if(!string.IsNullOrEmpty(str_1))
                {
                    i++;
                    var cookieStr = string.Empty;
                    var urlStr = string.Empty;
                    var bodyStr = string.Empty;

                    //Console.WriteLine("分割(one) " + i.ToString()+ "  :" + str_1);
                    string[] sArray_1 = Regex.Split(str_1, "hcc_fullUrlPath=", RegexOptions.IgnoreCase);
                    foreach (string str_2 in sArray_1) 
                    {
                        //Console.WriteLine("分割(two) " + i.ToString() + "  :" + str_2);
                        string[] sArray_2 = Regex.Split(str_2, "hcc_reqBodyPath=", RegexOptions.IgnoreCase);
                        foreach (string str_3 in sArray_2)
                        {
                            j++;
                            //Console.WriteLine("分割(three) " + i.ToString() + "  :" + str_3);
                            //Console.WriteLine("分割(three) " + j.ToString() + "  :" + str_3);
                            if(j%3==0){
                                _bodyList.Add(str_3);
                            }else if(j%3==1){
                                _cookieList.Add(str_3);
                            }else if(j%3==2){
                                _urlList.Add(str_3);
                            }
                        }
                    }
                }
            }

            for (int ct = 0; ct < _cookieList.Count; ct++ )
            {
                dataInfoList.Add(new DataInfo(Convert.ToString(ct + 1), _cookieList[ct], _bodyList[ct], _urlList[ct]));
                if (OnTxtReturn != null){
                    //OnTxtReturn.Invoke(this, new DataInfo(Convert.ToString(ct+1), _cookieList[ct], _bodyList[ct],_urlList[ct]));
                }
                else{
                    //Console.WriteLine("OnTextReturn == null");
                }
            }
            return dataInfoList;
     }

    }
}
