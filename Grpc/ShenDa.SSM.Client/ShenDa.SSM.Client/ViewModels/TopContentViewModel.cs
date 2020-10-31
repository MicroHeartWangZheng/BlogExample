
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;
using ShenDa.SSM.Common;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;

namespace ShenDa.SSM.Client.ViewModels
{
    public class TopContentViewModel : ViewModelBase
    {
        #region 字段 属性
        private string systemChName;
        private string systemEnName;
        private string servicePhone;
        private string salePhone;
        private string time;
        private string day;

        /// <summary>
        /// 时分秒
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 当前日期
        /// </summary>
        public string Day
        {
            get { return day; }
            set { day = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 系统中文名称
        /// </summary>
        public string SystemChName
        {
            get { return systemChName; }
            set { systemChName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 系统英文名称
        /// </summary>
        public string SystemEnName
        {
            get { return systemEnName; }
            set { systemEnName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 客服电话
        /// </summary>
        public string ServicePhone
        {
            get { return servicePhone; }
            set { servicePhone = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 售票处电话
        /// </summary>
        public string SalePhone
        {
            get { return salePhone; }
            set { salePhone = value; RaisePropertyChanged(); }
        }
        #endregion

        Timer timer = new Timer();


        public TopContentViewModel()
        {
            IntervalInvoke();
            Init();
        }

        public void Init()
        {
            var appSettings = ConfigurationHelper.Get<AppSettings>("SystemConfig");
            SystemChName = appSettings.SystemName;
            SystemEnName = appSettings.SystemEnName;
            SalePhone = appSettings.Department;
            ServicePhone = appSettings.ContactPhone;
        }

        public void IntervalInvoke()
        {
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler((s, t) =>
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    var now = DateTime.Now;
                    Day = now.ToString("yyyy-MM-dd");
                    Time = now.ToString("HH-mm-ss");
                }));
            });
            timer.Start();
        }
    }
}
