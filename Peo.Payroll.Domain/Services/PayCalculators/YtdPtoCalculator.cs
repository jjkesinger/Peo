using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.PayCalculators
{
    public class YtdPtoCalculator : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            return await Task.FromResult(payroll.Employee.EmployeeYtdHistory?.YtdPto ?? 0);
        }
    }
}
