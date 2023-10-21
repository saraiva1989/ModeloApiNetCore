using ModeloNetCoreBiblioteca.Dominio.Usuario;
using ModeloNetCoreBiblioteca.Repositorio.Usuario;

namespace ModeloNetCoreApi.Configuracacoes
{
    public static class ApplicationBuilderConfiguration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            IConfiguration configuration
                = services.BuildServiceProvider()
                          .GetService<IConfiguration>();

            string connectionString = configuration.GetConnectionString("BancoProd");
            //string urlNuuvem = configuration.GetValue<string>("urlNuuvem");

#if DEBUG
            connectionString = configuration.GetConnectionString("BancoDev");
#endif

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>(x => new UsuarioRepositorio(connectionString));
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            //services.AddMemoryCache();
            return services;
        }
    }
}
