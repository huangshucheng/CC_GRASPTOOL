using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CC_GRASPTOOL
{
    public class DataInfo:INotifyPropertyChanged 
    {
        private string  _ck_id;
        private string  _ck_cookie;
        private string  _ck_body;
        private string  _ck_url;
        public event PropertyChangedEventHandler PropertyChanged;
        public DataInfo(string id, string cookie, string body, string url)
        {
            _ck_id = id;
            _ck_cookie = cookie;
            _ck_body = body;
            _ck_url = url;
        }
        public string ck_id  
        {
            get { return _ck_id; }
            set { 
                _ck_id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ck_id")); 
            }  
        }  
        public string ck_cookie  
        {
            get { return _ck_cookie; }
            set { 
                _ck_cookie = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ck_cookie"));  
            }  
        }
        public string ck_body 
        {
            get { return _ck_body; }
            set {
                _ck_body = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ck_body")); 
            }  
        }  
        public string ck_url  
        {
            get { return _ck_url; }
            set {
                _ck_url = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ck_url")); 
            }  
        }
    }
}
