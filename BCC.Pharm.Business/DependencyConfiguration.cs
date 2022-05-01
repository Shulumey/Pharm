using Autofac;
using BCC.Pharm.Business.Import;
using BCC.Pharm.Shared.Contracts;
using BCC.Pharm.Shared.Contracts.Business;
using BCC.Pharm.Shared.Dtos;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace BCC.Pharm.Business
{
    /// <summary>
    /// Регистрация зависимостей.
    /// </summary>
    public static class DependencyConfiguration
    {
        public static void RegisterBusinessLayer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterMediatR(typeof(DependencyConfiguration).Assembly);

            containerBuilder.RegisterType<XmlImportDataFile>().As<IMedicationsImporter>();
            containerBuilder.RegisterType<MedicationsComparer>().As<IObjectsComparer<MedicationDto>>();
        }
    }
}