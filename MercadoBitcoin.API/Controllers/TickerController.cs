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
    public class TickerController : ControllerBase
    {
        private readonly ITickerService _tickerService;
        private readonly IMapper _mapper;

        public TickerController(ITickerService tickerService, IMapper mapper)
        {
            _tickerService = tickerService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Get(TickerGetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var serviceRequest = _mapper.Map<service.TickerGetRequest>(request);

            var tickerServiceResp = await _tickerService.Get(serviceRequest);

            var resp = _mapper.Map<IEnumerable<Ticker>>(tickerServiceResp);

            return Ok(resp);
        }
    }
}
