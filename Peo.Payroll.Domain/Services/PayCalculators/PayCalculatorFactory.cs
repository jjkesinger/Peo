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
            if (payType == CalculatePayType.Gross)
            {
                return new GrossPayCalculator();
            }

            if (payType == CalculatePayType.Net)
            {
                return new NetPayCalculator();
            }

            if (payType == CalculatePayType.Taxable)
            {
                return new TaxablePayCalculator();
            }

            if (payType == CalculatePayType.Pto)
            {
                return new PtoCalculator();
            }

            if (payType == CalculatePayType.YtdGross)
            {
                return new YtdGrossPayCalculator();
            }

            if (payType == CalculatePayType.YtdNet)
            {
                return new YtdNetPayCalculator();
            }

            if (payType == CalculatePayType.YtdTaxable)
            {
                return new YtdTaxablePayCalculator();
            }

            if (payType == CalculatePayType.YtdPto)
            {
                return new YtdPtoCalculator();
            }

            throw new NotImplementedException();
        }
    }
}
