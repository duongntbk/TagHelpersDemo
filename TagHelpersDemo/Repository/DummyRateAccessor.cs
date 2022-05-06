using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelpersDemo.Repository
{
    public class DummyRateAccessor : IExchangeRateRepository
    {
        private readonly Dictionary<Currency, decimal> _db;

        public DummyRateAccessor() =>
            _db = new Dictionary<Currency, decimal>
            {
                { Currency.USD, 1 },
                { Currency.JPY, 0.0077m},
                { Currency.VND, 0.000044m},
                { Currency.EUR, 1.05m},
                { Currency.CNY, 0.15m}
            };

        public async Task<RateDto> RetrieveAsync(Currency source, Currency target)
        {
            await Task.Delay(50); // simulate delay of an API call

            return new RateDto
            {
                SourceName = source.ToString(),
                TargetName = target.ToString(),
                Rate = _db[source] / _db[target]
            };
        }

        public async Task<IEnumerable<RateDto>> RetrieveAllAsync(Currency target)
        {
            await Task.Delay(50); // simulate delay of an API call

            return from kvp in _db
                   let source = kvp.Key
                   let sourceToTarget = kvp.Value / _db[target]
                   where source != target
                   select new RateDto
                   {
                       SourceName = source.ToString(),
                       TargetName = target.ToString(),
                       Rate = sourceToTarget
                   };
        }
    }
}
