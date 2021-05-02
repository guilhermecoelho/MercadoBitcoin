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
    public class TradesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITradesService _tradesService;

        public TradesController(IMapper mapper, ITradesService tradesService)
        {
            _mapper = mapper;
            _tradesService = tradesService;
        }

        [HttpPost]
        public async Task<ActionResult> Get(TradesGetRequest request)
        {
            var serviceRequest = _mapper.Map<service.TradesGetRequest>(request);

            var tradesServiceResp = await _tradesService.Get(serviceRequest);

            var resp = _mapper.Map<IEnumerable<Trades>>(tradesServiceResp);

            return Ok(resp);
        }

        //[HttpPost]
        //public async Task<ActionResult> GetByTid(TradesGetByTidRequest request)
        //{
        //    var serviceRequest = _mapper.Map<service.TradesGetRequest>(request);

        //    var tradesServiceResp = await _tradesService.Get(serviceRequest);

        //    var resp = _mapper.Map<IEnumerable<Trades>>(tradesServiceResp);

        //    return Ok(resp);
        //}
    }
}
