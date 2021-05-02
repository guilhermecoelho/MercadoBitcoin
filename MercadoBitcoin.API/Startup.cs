using AutoMapper;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MercadoBitcoin.API
{
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

            services.AddHttpClient<IHttpRequestHandler>();

            #region automapper
            MapperConfig mapperConfig = new MapperConfig();
            IMapper mapper = mapperConfig.MapperConfiguration();

            services.AddSingleton(mapper);
            #endregion

            #region ID
            services.AddTransient<ITickerService, TickerService>();
            services.AddTransient<IDaySummaryService, DaySummaryService>();
            services.AddTransient<ITradesService, TradesService>();
            services.AddTransient<IOrderbookService, OrderbookService>();
            services.AddTransient<IHttpRequestHandler, HttpRequestHandler>();
            #endregion

            #region swagger
            services.AddSwaggerGen();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "MercadoBitcoin API");
            });
        }
    }
}
