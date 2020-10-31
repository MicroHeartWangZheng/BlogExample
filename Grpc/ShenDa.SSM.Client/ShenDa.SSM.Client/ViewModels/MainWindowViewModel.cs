using GalaSoft.MvvmLight;
using Grpc.Core;
using Grpc.Net.Client;
using ShenDa.SSM.Client.Factory;
using ShenDa.SSM.Common;
using ShenDa.SSM.Grpc;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static ShenDa.SSM.Grpc.SsmService;

namespace ShenDa.SSM.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region 字段 属性
        private string loadingContent;
        private int progressValue;

        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                RaisePropertyChanged();
            }
        }
        public string LoadingContent
        {
            get { return loadingContent; }
            set
            {
                loadingContent = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public void InitSystemParam()
        {
            LoadingContent = "开始系统初始化...";
            SetProgressValue(0);
            AddUser();
            ////1.连接服务器
            //var result = ConnServer();
            //if (!result.Success)
            //{
            //    LoadingContent = result.Message;
            //    return;
            //}
            //Thread.Sleep(300);
            //SetProgressValue(20);

            ////2.同步服务器端参数
            //result = GetServerParam();
            //if (!result.Success)
            //{
            //    LoadingContent = result.Message;
            //    return;
            //}
            //Thread.Sleep(300);
            //SetProgressValue(40);



            //LoadingContent = "获取服务器票务信息";
            //LoadingContent = "初始化银联支付设备";
            //LoadingContent = "完成初始化，启动系统!";
            //SoundPlayerHelper.PlaySound("002");

        }

        private (bool Success, string Message) ConnServer()
        {
            LoadingContent = "连接服务器中......";
            var client = ClientFactory.GetClient();
            var response = client.Health(new EmptyRequest());
            if (!response.Success)
                return (false, "服务器连接失败.");
            return (true, string.Empty);
        }

        private void AddUser()
        {
            var client = ClientFactory.GetClient();

            var cts = new CancellationTokenSource(5000);
            var userAdd = client.User_Add();

            for (var i = 0; i < 10; i++)
            {
                userAdd.RequestStream.WriteAsync(new UserAddRequest()
                {
                    Id = i,
                    Age = "18",
                    Name = $"Name-{i}"
                }); ;
            }

            //处理返回
            Task.Run(async () =>
            {
                await foreach (var item in userAdd.ResponseStream.ReadAllAsync())
                {
                    //todo
                }
            });

        }

        private (bool Success, string Message) GetServerParam()
        {
            LoadingContent = "同步服务器端参数......";
            var client = ClientFactory.GetClient();

            var terminalCode = ConfigurationHelper.GetByKey<string>("SystemConfig:TerminalCode");
            var request = new TermiParamSearchRequest()
            {
                TerminalCode = terminalCode
            };
            var response = client.TermiParm_GetList(request);
            if (!response.Success)
                return (false, "服务器连接失败.");
            return (true, string.Empty);
        }

        private void SetProgressValue(int value)
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            progressValue = value;
        }
    }
}
