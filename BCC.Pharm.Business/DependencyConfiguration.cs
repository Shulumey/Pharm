using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace BCC.Pharm.Business
{
    public static class DependencyConfiguration
    {
        public static void RegisterBusinessLayer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterMediatR(typeof(DependencyConfiguration).Assembly);
        }
    }
}