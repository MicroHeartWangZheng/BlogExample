using ShenDa.SSM.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShenDa.SSM.Client.Views
{
    /// <summary>
    /// TopContent.xaml 的交互逻辑
    /// </summary>
    public partial class TopContentView : Page
    {
        public TopContentViewModel viewModel;
        public TopContentView()
        {
            InitializeComponent();
            this.viewModel = new TopContentViewModel();
            this.DataContext = viewModel;
        }
    }
}
