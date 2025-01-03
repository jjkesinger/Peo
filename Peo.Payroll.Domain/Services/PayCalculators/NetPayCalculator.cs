using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.PayCalculators
{
    public class NetPayCalculator : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            var netPay = payroll.GrossPay - 
                payroll.Withholdings.Sum(f => f.Amount) - 
                payroll.Deductions.Sum(f => f.Amount ?? 0m);

            return await Task.FromResult(netPay);
        }
    }
}
