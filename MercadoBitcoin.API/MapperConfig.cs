using AutoMapper;
using service = MercadoBitcoin.Service.Entities;
using api = MercadoBitcoin.API.Entities;
using MercadoBitcoin.Domain;

namespace MercadoBitcoin.API
{
    public class MapperConfig
    {
        public IMapper MapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<service.Ticker, api.Ticker>().ForMember(dest => dest.Date, m => m.MapFrom(source => new Utils().ConverterTimestampToDatetime(source.Date)));
                cfg.CreateMap<api.TickerGetRequest, service.TickerGetRequest>();

                cfg.CreateMap<service.Orderbook, api.Orderbook>();
                cfg.CreateMap<api.OrderbookGetRequest, service.OrderbookGetRequest>();

                cfg.CreateMap<service.Trades, api.Trades>().ForMember(dest => dest.Date, m => m.MapFrom(source => new Utils().ConverterTimestampToDatetime(source.Date)));
                cfg.CreateMap<api.TradesGetRequest, service.TradesGetRequest>();
                cfg.CreateMap<api.TradesGetByTidRequest, service.TradesGetRequest>();

                cfg.CreateMap<api.DaySummaryGetRequest, service.DaySummaryGetRequest>();
                cfg.CreateMap<service.DaySummary, api.DaySummary>().ForMember(dest => dest.AvgPrice, m => m.MapFrom(source => source.Avg_price));

            }
           );

            return config.CreateMapper();
        }
    }
}
