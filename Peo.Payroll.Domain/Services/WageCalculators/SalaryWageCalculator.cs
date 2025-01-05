using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.WageCalculators
{
    public class SalaryWageCalculator : ICalculatePayments
    {
        public async Task<Payment[]> CalculateAsync(EmployeePayroll payroll)
        {
            var payments = new List<Payment>();

            var startTime = payroll.Period.PayRange.StartDate;
            var endTime = payroll.Period.PayRange.EndDate;

            var payHistory = payroll.Employee.EmployeePayHistory
                .Where(f => f.EffectiveDate <= endTime && (!f.StopDate.HasValue || f.StopDate >= startTime))
                .OrderBy(f => f.EffectiveDate)
                .ThenBy(f => f.UpdateDate)
                .ToArray();

            for (int i = 0; i < payHistory.Length; i++)
            {
                var currentPay = payHistory[i];
                if (currentPay.StopDate == null || currentPay.StopDate >= startTime)
                {
                    var nextPay = i + 1 < payHistory.Length ? payHistory[i + 1] : null;
                    var currentEndTime = nextPay != null && nextPay.EffectiveDate <= endTime ? nextPay.EffectiveDate : endTime;
                    if (currentPay.WageType == WageType.Salary)
                    {
                        var fraction = new decimal((currentEndTime - startTime).TotalDays / (endTime - startTime).TotalDays);
                        var frequency = payroll.Period.PayRange.GetAnnualFrequency();
                        var total = (currentPay.Amount / frequency) * fraction;

                        payments.Add(new Payment(Math.Round(total, 2, MidpointRounding.AwayFromZero)));
                        startTime = currentEndTime;
                    }
                }
            }

            return await Task.FromResult(payments.ToArray());
        }
    }

}
