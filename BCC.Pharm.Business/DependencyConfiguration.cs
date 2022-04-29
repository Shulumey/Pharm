using Autofac;
using BCC.Pharm.Business.Import;
using BCC.Pharm.Shared.Contracts;
using BCC.Pharm.Shared.Contracts.Business;
using BCC.Pharm.Shared.Dtos;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace BCC.Pharm.Business
{
    public static class DependencyConfiguration
    {
        public static void RegisterBusinessLayer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterMediatR(typeof(DependencyConfiguration).Assembly);

            containerBuilder.RegisterType<XmlImportDataFile>().As<IImportDataFile>();
            containerBuilder.RegisterType<MedicationsComparer<MedicationDto>>().As<IObjectsComparer<MedicationDto>>();
        }
    }
}