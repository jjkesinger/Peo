using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.WageCalculators
{
    public class SalaryWageCalculator : ICalculatePayments
    {
        public async Task<Payment[]> CalculateAsync(EmployeePayroll payroll)
        {
            var payments = new List<Payment>();
            
            var start = payroll.Period.PayRange.StartDate;
            var end = payroll.Period.PayRange.EndDate;

            foreach (var pay in payroll.Employee.EmployeePayHistory)
            {
                if (pay.WageType == WageType.Salary)
                {
                    start = start < pay.EffectiveDate ? pay.EffectiveDate : start;
                    end = (pay.StopDate.HasValue && end > pay.StopDate) ? pay.StopDate.Value : end;

                    var fraction = new decimal((end - start).TotalSeconds / (payroll.Period.PayRange.EndDate- payroll.Period.PayRange.StartDate).TotalSeconds);
                    var frequency = payroll.Period.PayRange.GetAnnualFrequency();
                    var total = (pay.Amount / frequency) * fraction;
                    
                    payments.Add(new Payment(Math.Round(total, 2, MidpointRounding.AwayFromZero)));
                }
            }

            return await Task.FromResult(payments.ToArray());
        }
    }
}
