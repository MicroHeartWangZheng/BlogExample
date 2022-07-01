using GoHomeWindow.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoHomeWindow
{
    public partial class MainForm : Form
    {
        private Timer textTimer;
        private Timer cpuTimer;
        private Timer catTimer;

        /// <summary>
        /// 右下角图标
        /// </summary>
        private NotifyIcon notifyIcon;
        private PerformanceCounter cpu;
        private List<Icon> icons = new List<Icon>();

        private int index = 0;

        private float cpuValue = 0f;
        public MainForm()
        {
            InitializeComponent();

            cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuValue = cpu.NextValue();

            textTimer = new Timer();
            textTimer.Tick += TextTimer_Tick;
            textTimer.Interval = 1000;
            textTimer.Start();
            //拆分下

            InitNotifyIcon();

            //300最慢的  30
            catTimer = new Timer();
            catTimer.Tick += ChangeIcon;
            catTimer.Interval = 300;
            catTimer.Start();


            cpuTimer = new Timer();
            cpuTimer.Tick += Cpu_Tick;
            cpuTimer.Interval = 3000;
            cpuTimer.Start();

        }



        private void InitNotifyIcon()
        {
            var rm = Resources.ResourceManager;
            for (int i = 0; i < 5; i++)
            {
                icons.Add((Icon)rm.GetObject($"dark_cat_{i}"));
            }
            var contextMenuStrip = new ContextMenuStrip(new Container());
            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("显示", null, Show),
                new ToolStripMenuItem("退出", null, Exit)
            });

            notifyIcon = new NotifyIcon()
            {
                Icon = icons[0],
                ContextMenuStrip = contextMenuStrip,
                Text = "淞哥专属",
                Visible = true,
            };
        }

        private void Exit(object sender, EventArgs e)
        {
            cpu.Close();
            catTimer.Stop();
            textTimer.Stop();
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void Show(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }

        }

        private void TextTimer_Tick(object sender, EventArgs e)
        {
            var date = new DateTime(2022, 2, 6);

            var span = date - DateTime.Now;

            if (span.Days < 0)
            {
                label1.Text = "2021已经完了,2022已经到来，希望淞哥新的一年里 快快乐乐";
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                return;
            }


            label1.Text = $"{span.Days}天";
            label2.Text = $"{span.Hours}时";
            label3.Text = $"{span.Minutes}分";
            label4.Text = $"{span.Seconds}秒";
        }

        public void ChangeIcon(object sender, EventArgs e)
        {
            var rm = Resources.ResourceManager;

            if (index >= 5)
                index = 0;

            //var cpuValue = cpu.NextValue();
            //notifyIcon.Text = $"CPU: {cpuValue:f1}%";
            //if (cpuValue > 80)
            //{

            //}
            notifyIcon.Icon = (Icon)rm.GetObject($"dark_cat_{index}");

            index++;
        }

        public void Cpu_Tick(object sender, EventArgs e)
        {
            cpuValue = cpu.NextValue();
            var a = ((int)cpuValue / 10) == 0 ? 1 : ((int)cpuValue / 10);

            catTimer.Interval = 300 / a;
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //取消关闭窗口
            e.Cancel = true;
            //最小化主窗口
            this.WindowState = FormWindowState.Minimized;
            //不在系统任务栏显示主窗口图标
            this.ShowInTaskbar = false;
        }
    }
}
