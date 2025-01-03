using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.Lookup;

namespace Peo.Payroll.Domain.Services.DeductionCalculators
{
    public abstract class BaseDeductionCalculator(
        IEmployeeElectionLookupService employeeElectionLookupService,
        IEmployeeContributionLookupService employeeContributionLookupService)
    {
        protected async Task<Deduction[]> CalculateEmployeeDeductionAsync(EmployeePayroll payroll, ElectionType electionType)
        {
            return (await (await employeeElectionLookupService.GetActiveElections(payroll, electionType))
            .SelectAsync(async election =>
                    (await employeeContributionLookupService.GetEmployeeContributionsAsync(payroll, election.ElectionType))
                        .Select(contribution =>
                        {
                            if (contribution.Rate.HasValue && !contribution.Amount.HasValue)
                            {
                                var amount = contribution.DeductionTaxType == DeductionTaxType.PreTax ?
                                    payroll.GrossPay * contribution.Rate.Value :
                                    payroll.NetPay * contribution.Rate.Value;

                                return new Deduction(contribution.Description, contribution.DeductionTaxType, amount, contribution.Rate);
                            }

                            if (contribution.Amount.HasValue)
                            {
                                return new Deduction(contribution.Description, contribution.DeductionTaxType, contribution.Amount.Value, contribution.Rate);
                            }

                            throw new InvalidDataException($"Missing rate and amount for {election.Description} contribution.");
                        }).ToArray()
                )).SelectMany(a => a).ToArray();
        }

        protected static decimal CalculatePayrollAmount(EmployeePayroll payroll, ElectionType electionType)
        {
            decimal total = 0;

            foreach (var contract in payroll.Company.Contracts)
            {
                foreach (var period in payroll.Employee.EmployeeHireHistory)
                {
                    var effectiveStartDate = Max(period.StartDate, contract.ContractDateRange.StartDate, payroll.Period.PayRange.StartDate);
                    var effectiveEndDate = Min(period.TerminationDate ?? contract.ContractDateRange.EndDate, contract.ContractDateRange.EndDate, payroll.Period.PayRange.EndDate);

                    if (effectiveStartDate <= effectiveEndDate)
                    {
                        var effectiveDays = (effectiveEndDate - effectiveStartDate).Days + 1;

                        total += contract.GetDailyRate(electionType) * effectiveDays;
                    }
                }
            }

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private static DateTime Max(DateTime date1, DateTime date2, DateTime date3)
        {
            return new DateTime(Math.Max(date1.Ticks, Math.Max(date2.Ticks, date3.Ticks)), DateTimeKind.Utc);
        }

        private static DateTime Min(DateTime date1, DateTime date2, DateTime date3)
        {
            return new DateTime(Math.Min(date1.Ticks, Math.Min(date2.Ticks, date3.Ticks)), DateTimeKind.Utc);
        }
    }
}
