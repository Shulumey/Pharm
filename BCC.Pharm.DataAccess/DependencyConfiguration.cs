using Autofac;

namespace BCC.Pharm.DataAccess
{
    public static class DependencyConfiguration
    {
        public static void ConfigureDataAccess(this ContainerBuilder containerBuilder, string connectionString)
        {
            containerBuilder
                .RegisterType<PharmDbContext>()
                .WithParameter(new TypedParameter(typeof(string), connectionString));
        }
    }
}