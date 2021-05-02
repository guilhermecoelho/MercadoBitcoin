using AutoMapper;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using service = MercadoBitcoin.Service.Entities;

namespace MercadoBitcoin.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaySummaryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDaySummaryService _daySummaryService;

        public DaySummaryController(IMapper mapper, IDaySummaryService daySummaryService)
        {
            _mapper = mapper;
            _daySummaryService = daySummaryService;
        }

        [HttpPost]
        public async Task<ActionResult> Get(DaySummaryGetRequest request)
        {
            var requestService = _mapper.Map<service.DaySummaryGetRequest>(request);
            var respService = await _daySummaryService.Get(requestService);

            var resp = _mapper.Map<DaySummary>(respService);

            return Ok(resp);
        }
    }
}
