using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.WageCalculators
{
    public class HourlyWageCalculator : ICalculatePayments
    {
        public async Task<Payment[]> CalculateAsync(EmployeePayroll payroll)
        {
            var payments = new List<Payment>();

            if (payroll.Timesheet == null || payroll.Timesheet.TimePeriods.Length == 0)
                return [.. payments];

            var timePeriods = payroll.Timesheet.TimePeriods;
            foreach (var timePeriod in timePeriods)
            {
                var pay = payroll.Employee.EmployeePayHistory
                    .OrderByDescending(f => f.EffectiveDate)
                    .ThenByDescending(f => f.UpdateDate)
                    .First(f => f.EffectiveDate <= timePeriod.StartTime);

                var newPay = payroll.Employee.EmployeePayHistory
                    .OrderByDescending(f => f.EffectiveDate)
                    .ThenByDescending(f => f.UpdateDate)
                    .First(f => f.EffectiveDate <= timePeriod.EndTime);

                if (pay.WageType == WageType.Hourly)
                {
                    if (pay.Equals(newPay))
                    {
                        AddPayment(timePeriod.StartTime, timePeriod.EndTime, pay.Amount, payments);
                    }
                    else
                    {
                        AddPayment(timePeriod.StartTime, newPay.EffectiveDate, pay.Amount, payments);

                        if (newPay.WageType == WageType.Hourly)
                        {
                            AddPayment(newPay.EffectiveDate, timePeriod.EndTime, pay.Amount, payments);
                        }
                    }
                }
            }

            return await Task.FromResult(payments.ToArray());
        }

        private static void AddPayment(DateTime startDate, DateTime endDate, decimal amount, List<Payment> payments)
        {
            var total = new decimal((endDate - startDate).TotalHours) * amount;
            payments.Add(new Payment(Math.Round(total, 2, MidpointRounding.AwayFromZero)));
        }
    }
}
