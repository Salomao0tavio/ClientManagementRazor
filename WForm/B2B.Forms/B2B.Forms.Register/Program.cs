using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace B2B.Forms.Register
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicializa a configuração da aplicação
            ApplicationConfiguration.Initialize();

            // Configuração de serviços e obtenção do ServiceProvider
            var serviceProvider = ConfigureServices();

            // Obtém instâncias necessárias para passar para RegisterForm
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<IdentityDataContext>();
            var registerService = new RegisterService(userManager);

            // Inicia o formulário de registro
            Application.Run(new RegisterForm(registerService, context));
        }

        // Configuração de serviços da aplicação
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Adiciona logging
            services.AddLogging();

            // Configuração do banco de dados
            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer("Data Source=NOOTEBOOKOTAVIO;Initial Catalog=B2B;User Id=Otavio;Password=Otavio@pis123;TrustServerCertificate=True;encrypt=false")); // Substitua pela sua string de conexão

            // Configuração do Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDataContext>(); // Ajuste para o contexto correto
            return services.BuildServiceProvider();
        }
    }
}
