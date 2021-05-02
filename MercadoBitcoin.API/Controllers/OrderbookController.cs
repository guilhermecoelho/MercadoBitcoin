using AutoMapper;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using service = MercadoBitcoin.Service.Entities;

namespace MercadoBitcoin.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderbookController : ControllerBase
    {
        private readonly IOrderbookService _orderbookService;
        private readonly IMapper _mapper;

        public OrderbookController(IOrderbookService orderbookService, IMapper mapper)
        {
            _orderbookService = orderbookService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Get(OrderbookGetRequest request)
        {
            var serviceRequest = _mapper.Map<service.OrderbookGetRequest>(request);

            var orderbookServiceResp = await _orderbookService.Get(serviceRequest);

            var resp = _mapper.Map<Orderbook>(orderbookServiceResp);

            return Ok(resp);
        }
    }
}
