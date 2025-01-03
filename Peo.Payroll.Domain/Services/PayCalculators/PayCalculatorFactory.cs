using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.PayCalculators
{
    public interface ICalculatePayFactory
    {
        ICalculatePay GetPayCalculator(CalculatePayType payType);
    }

    public class PayCalculatorFactory : ICalculatePayFactory
    {
        public ICalculatePay GetPayCalculator(CalculatePayType payType)
        {
            return payType switch
            {
                CalculatePayType.Gross => new GrossPayCalculator(),
                CalculatePayType.Net => new NetPayCalculator(),
                CalculatePayType.Taxable => new TaxablePayCalculator(),
                CalculatePayType.Pto => new PtoCalculator(),
                CalculatePayType.YtdGross => new YtdGrossPayCalculator(),
                CalculatePayType.YtdNet => new YtdNetPayCalculator(),
                CalculatePayType.YtdTaxable => new YtdTaxablePayCalculator(),
                CalculatePayType.YtdPto => new YtdPtoCalculator(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
