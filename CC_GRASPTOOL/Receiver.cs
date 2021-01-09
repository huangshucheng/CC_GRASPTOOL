using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CC_GRASPTOOL
{
    public partial class Receiver : Form
    {
        public Receiver()
        {
            InitializeComponent();
        }

        //COPYDATASTRUCT结构
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        public const int WM_COPYDATA = 0x004A;

        //捕获消息
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    //处理消息
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                    string content = mystr.lpData;
                    Console.WriteLine("hcc>>recvCopyData: ", content);
                    this.label1.Text = this.label1.Text + content;
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
