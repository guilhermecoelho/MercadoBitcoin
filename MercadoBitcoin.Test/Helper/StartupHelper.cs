using AutoMapper;
using MercadoBitcoin.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoBitcoin.Test.Helper
{
    public class StartupHelper
    {
        private IConfiguration _configuration;

        public StartupHelper()
        {
          
        }

        public ServiceCollection BuildService()
        {
            _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var services = new ServiceCollection();
            MapperConfig mapperConfig = new MapperConfig();
            IMapper mapper = mapperConfig.MapperConfiguration();

            services.AddSingleton(_configuration);
            services.AddSingleton(mapper);

            return services;
        }
    }
}
