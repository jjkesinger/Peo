using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services;
using Peo.Payroll.Domain.Services.PayCalculators;

namespace Peo.Payroll.Tests.Domain.Mocks
{
    internal class PayCalculatorFactoryMock : ICalculatePayFactory
    {
        public ICalculatePay GetPayCalculator(CalculatePayType payType)
        {
            if (payType == CalculatePayType.Gross)
            {
                return new CalculateGrossPayMock();
            }

            if (payType == CalculatePayType.Net)
            {
                return new CalculateNetPayMock();
            }

            if (payType == CalculatePayType.Taxable)
            {
                return new CalculateTaxablePayMock();
            }

            if (payType == CalculatePayType.Pto)
            {
                return new CalculatePtoMock();
            }

            if (payType == CalculatePayType.YtdGross)
            {
                return new CalculateYtdGrossPayMock();
            }

            if (payType == CalculatePayType.YtdNet)
            {
                return new CalculateYtdNetPayMock();
            }

            if (payType == CalculatePayType.YtdTaxable)
            {
                return new CalculateYtdTaxablePayMock();
            }

            if (payType == CalculatePayType.YtdPto)
            {
                return new CalculateYtdPtoMock();
            }

            throw new NotImplementedException();
        }
    }

    internal class CalculateNetPayMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(2500m);
        }
    }

    internal class CalculateGrossPayMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(4000m);
        }
    }

    internal class CalculateTaxablePayMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(3000m);
        }
    }

    internal class CalculatePtoMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(0m);
        }
    }

    internal class CalculateYtdGrossPayMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(30000m);
        }
    }

    internal class CalculateYtdTaxablePayMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(20000m);
        }
    }

    internal class CalculateYtdNetPayMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(26000m);
        }
    }

    internal class CalculateYtdPtoMock : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(45m);
        }
    }
}
