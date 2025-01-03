using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.WageCalculators
{
    public class SalaryWageCalculator : ICalculatePayments
    {
        public async Task<Payment[]> CalculateAsync(EmployeePayroll payroll)
        {
            var payments = new List<Payment>();

            var originalStart = payroll.Period.PayRange.StartDate;
            var originalEnd = payroll.Period.PayRange.EndDate;

            foreach (var pay in payroll.Employee.EmployeePayHistory)
            {
                if (pay.WageType == WageType.Salary)
                {
                    var start = originalStart < pay.EffectiveDate ? pay.EffectiveDate : originalStart;
                    var end = (pay.StopDate.HasValue && originalEnd > pay.StopDate) ? pay.StopDate.Value : originalEnd;

                    var fraction = new decimal((end - start).TotalDays / (originalEnd - originalStart).TotalDays);
                    var frequency = payroll.Period.PayRange.GetAnnualFrequency();
                    var total = (pay.Amount / frequency) * fraction;

                    payments.Add(new Payment(Math.Round(total, 2, MidpointRounding.AwayFromZero)));
                }
            }

            return await Task.FromResult(payments.ToArray());
        }
    }

}
