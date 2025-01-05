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
                            if (!contribution.Rate.HasValue && !contribution.Amount.HasValue)
                            {
                                throw new InvalidDataException($"Missing rate and amount for {contribution.Description} contribution.");
                            }

                            var amount = contribution.Amount ??
                                (contribution.DeductionTaxType == DeductionTaxType.PreTax ?
                                payroll.GrossPay * contribution.Rate!.Value :
                                payroll.NetPay * contribution.Rate!.Value);

                            return new Deduction(contribution.Description, contribution.DeductionTaxType, amount, contribution.Rate);
                        }).ToArray()
                )).SelectMany(a => a).ToArray();
        }
    }
}
