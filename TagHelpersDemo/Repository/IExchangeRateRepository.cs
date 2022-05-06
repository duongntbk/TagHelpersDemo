using System.Collections.Generic;
using System.Threading.Tasks;

namespace TagHelpersDemo.Repository
{
    public interface IExchangeRateRepository
    {
        Task<RateDto> RetrieveAsync(Currency source, Currency target);

        Task<IEnumerable<RateDto>> RetrieveAllAsync(Currency target);
    }
}
