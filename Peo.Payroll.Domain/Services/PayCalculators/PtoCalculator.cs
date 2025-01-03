using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.PayCalculators
{
    public class PtoCalculator : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            var totalHours = payroll.Timesheet?.TimePeriods
                .Where(f => f.PayType == PayType.PTO)
                .Sum(f => f.TotalHours) 
                ?? 0;
            
            return await Task.FromResult(totalHours);
        }
    }
}
