using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CC_GRASPTOOL
{
    class DataInfo:INotifyPropertyChanged 
    {
        private string  _ck_id;
        private string  _ck_cookie;
        private string  _ck_result;
        private string  _ck_state;
        public event PropertyChangedEventHandler PropertyChanged;
        public DataInfo(string id, string cookie, string result, string state)
        {
            _ck_id = id;
            _ck_cookie = cookie;
            _ck_result = result;
            _ck_state = state;
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
        public string ck_result  
        {
            get { return _ck_result; }
            set {
                _ck_result = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ck_result")); 
            }  
        }  
        public string ck_state  
        {
            get { return _ck_state; }
            set { 
                _ck_state = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ck_state")); 
            }  
        }
    }
}
