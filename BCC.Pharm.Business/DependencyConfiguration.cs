using Autofac;
using BCC.Pharm.Business.Export;
using BCC.Pharm.Business.Import;
using BCC.Pharm.Shared;
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

            containerBuilder.RegisterType<XmlImportDataFile>().As<IMedicationsImporter>();
            containerBuilder.RegisterType<MedicationsComparer>().As<IObjectsComparer<MedicationDto>>();
            containerBuilder.RegisterType<JsonMedicationsExporter>().Keyed<IMedicationsExporter>(ExportFormat.Json);
            containerBuilder.RegisterType<XmlMedicationsExporter>().Keyed<IMedicationsExporter>(ExportFormat.Xml);
        }
    }
}