using Autofac;
using BCC.Pharm.App.Services;
using BCC.Pharm.App.ViewModels;

namespace BCC.Pharm.App
{
    public static class DependencyConfiguration
    {
        public static void RegisterViewModels(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DefaultWindowDialogService>().As<IWindowDialogService>();
            containerBuilder.RegisterType<MainViewModel>();
        }
    }
}