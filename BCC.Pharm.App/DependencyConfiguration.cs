using Autofac;
using BCC.Pharm.App.ViewModels;

namespace BCC.Pharm.App
{
    public static class DependencyConfiguration
    {
        public static void RegisterViewModels(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MainViewModel>();
        }
    }
}