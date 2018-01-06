using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CC_GRASPTOOL
{
    public class DataReturn : INotifyPropertyChanged
    {
        private string _return_id = "";
        private string _return_data = "";
        public event PropertyChangedEventHandler PropertyChanged;
        public DataReturn(string id, string data)
        {
            _return_id = id;
            _return_data = data;
        }
        public string return_id  
        {
            get { return _return_id; }
            set {
                _return_id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("return_id")); 
            }  
        }
        public string return_data
        {
            get { return _return_data; }
            set
            {
                _return_data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("return_data"));
            }  
        }
    }
}
