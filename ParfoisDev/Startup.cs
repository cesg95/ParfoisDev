namespace ParfoisDev
{
    using Data.Repository;
    using Data.Repository.Implementations;
    using Data.Repository.Interfaces;
    using Data.Services.Rules;

    using ApplicationImplementations = Application.Services.Implementations;
    using ApplicationInterfaces = Application.Services.Interfaces;
    using DataImplementations = Data.Services.Implementations;
    using DataInterfaces = Data.Services.Interfaces;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApiContext>();

            // Data.Repository
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            // Data.Services
            services.AddScoped<IRuleFactory, RuleFactory>();
            services.AddScoped<IRule, AprovadoRule>();
            services.AddScoped<IRule, ItemsAprovadosAMaiorRule>();
            services.AddScoped<IRule, ItemsAprovadosAMenorRule>();
            services.AddScoped<IRule, ValorAprovadoAMaiorRule>();
            services.AddScoped<IRule, ValorAprovadoAMenorRule>();
            services.AddScoped<IRule, ReprovadoRule>();
            services.AddScoped<DataInterfaces.IPedidoService, DataImplementations.PedidoService>();
            services.AddScoped<DataInterfaces.IStatusService, DataImplementations.StatusService>();

            // Application.Services
            services.AddScoped<ApplicationInterfaces.IPedidoService, ApplicationImplementations.PedidoService>();
            services.AddScoped<ApplicationInterfaces.IStatusService, ApplicationImplementations.StatusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                Importer.Use(endpoints);
            });
        }
    }
}
