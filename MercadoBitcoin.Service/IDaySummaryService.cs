using MercadoBitcoin.Service.Entities;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public interface IDaySummaryService
    {
        Task<DaySummary> Get(DaySummaryGetRequest request);
    }
}