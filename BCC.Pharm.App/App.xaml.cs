using System.Configuration;
using System.Windows;
using Autofac;
using BCC.Pharm.Business;
using BCC.Pharm.DataAccess;
using System.Reactive.Linq;
using ReactiveUI;

namespace BCC.Pharm.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer DependencyContainer { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterDataAccess(ConfigurationManager.ConnectionStrings["PharmDbConnection"].ConnectionString);
            containerBuilder.RegisterBusinessLayer();
            containerBuilder.RegisterViewModels();
            
            DependencyContainer = containerBuilder.Build();

            RxApp.DefaultExceptionHandler = new AppDefaultExceptionHandler();
            
            base.OnStartup(e);
        }
    }
}
