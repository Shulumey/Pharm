using Autofac;
using AutoMapper;
using BCC.Pharm.DataAccess.DataProviders;
using BCC.Pharm.DataAccess.Mappers;
using BCC.Pharm.Shared.Contracts.Data;

namespace BCC.Pharm.DataAccess
{
    public static class DependencyConfiguration
    {
        public static void RegisterDataAccess(this ContainerBuilder containerBuilder, string connectionString)
        {
            containerBuilder.RegisterType<PharmDbContext>()
                .WithParameter(new TypedParameter(typeof(string), connectionString));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new EntityMapperProfile()));
            mapperConfiguration.AssertConfigurationIsValid();

            containerBuilder.RegisterInstance(mapperConfiguration.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            containerBuilder.RegisterType<MedicationsDataProvider>().As<IMedicationsDataProvider>();
        }
    }
}