using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.PayCalculators
{
    public class GrossPayCalculator : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(payroll.Payments.Sum(f => f.Amount));
        }
    }
}
