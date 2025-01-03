using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.WageCalculators
{
    public class HourlyWageCalculator : ICalculatePayments
    {
        public async Task<Payment[]> CalculateAsync(EmployeePayroll payroll)
        {
            var payments = new List<Payment>();

            if (payroll.Timesheet?.TimePeriods == null || payroll.Timesheet.TimePeriods.Length == 0)
                return [.. payments];

            var earliestStart = payroll.Timesheet.TimePeriods.Min(tp => tp.StartTime);
            var latestEnd = payroll.Timesheet.TimePeriods.Max(tp => tp.EndTime);

            var payHistory = payroll.Employee.EmployeePayHistory
                .Where(f => f.EffectiveDate <= latestEnd && (!f.StopDate.HasValue || f.StopDate >= earliestStart))
                .OrderBy(f => f.EffectiveDate)
                .ThenBy(f => f.UpdateDate)
                .ToArray();

            foreach (var timePeriod in payroll.Timesheet.TimePeriods)
            {
                var startTime = timePeriod.StartTime;
                var endTime = timePeriod.EndTime;

                for (int i = 0; i < payHistory.Length; i++)
                {
                    var currentPay = payHistory[i];
                    if (currentPay.StopDate == null || currentPay.StopDate >= startTime)
                    {
                        var nextPay = i + 1 < payHistory.Length ? payHistory[i + 1] : null;
                        var currentEndTime = nextPay != null && nextPay.EffectiveDate <= endTime ? nextPay.EffectiveDate : endTime;
                        if (currentPay.WageType == WageType.Hourly)
                        {
                            AddPayment(startTime, currentEndTime, currentPay.Amount, payments);
                            startTime = currentEndTime;
                        }
                    }
                }
            }

            return await Task.FromResult(payments.ToArray());
        }

        private static void AddPayment(DateTime startDate, DateTime endDate, decimal amount, List<Payment> payments)
        {
            var total = (decimal)(endDate - startDate).TotalHours * amount;
            payments.Add(new Payment(Math.Round(total, 2, MidpointRounding.AwayFromZero)));
        }
    }

}
