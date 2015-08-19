using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace 边角触发
{

    public partial class Form1 : Form
    {
        bool b = true;
        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {




        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            SetVisibleCore(false);
            this.Hide();
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。  
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Alt, Keys.F2);
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt, Keys.F1);
            this.notifyIcon1.ShowBalloonTip(100, "边角触发", "边角触发已开启，按alt+f2关闭",ToolTipIcon.Info);
        }

        private void textBoxX_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (b)
            {
                Point pointToScreen = Control.MousePosition;
                int x = pointToScreen.X, y = pointToScreen.Y;
                textBoxX.Text = x.ToString();
                textBoxY.Text = y.ToString();
                if (x == 0 & y == 0)
                {

                    keybd_event(0x5b, 0, 0, 0);
                    keybd_event(9, 0, 0, 0);
                    keybd_event(0x5b, 0, 2, 0);
                    keybd_event(9, 0, 2, 0);
                    Thread.Sleep(1000);
                }

            }
        }
        [DllImport("user32.dll", EntryPoint = "keybd_event")]

        public static extern void keybd_event(

            byte bVk, //虚拟键值

            byte bScan,// 一般为0

            int dwFlags, //这里是整数类型 0 为按下，2为释放

            int dwExtraInfo //这里是整数类型 一般情况下设成为 0

  );

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            SetVisibleCore(true);
            this.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("%{F1}");
        }

        private void 关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("%{F2}");
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键  
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 101:     //按下的是Alt+F2
                           //此处填写快捷键响应代码  
                            b = false;
                            this.notifyIcon1.ShowBalloonTip(100, "边角触发", "边角触发已关闭，按alt+f1开启", ToolTipIcon.Info);
                            break;
                        case 102:     //按下的是Alt+F1  
                            //此处填写快捷键响应代码  
                            b = true;
                            this.notifyIcon1.ShowBalloonTip(100, "边角触发", "边角触发已开启，按alt+f2关闭", ToolTipIcon.Info);
                            break;
                       
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        protected override void SetVisibleCore(bool values)
        {
            base.SetVisibleCore(values);
        }



    }
}
