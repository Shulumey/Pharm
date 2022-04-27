using System.Windows;
using Autofac;
using BCC.Pharm.App.ViewModels;

namespace BCC.Pharm.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.DependencyContainer.Resolve<MainViewModel>();
        }
    }
}
