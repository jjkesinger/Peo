using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.Lookup;

namespace Peo.Payroll.Domain.Services.WithholdingCalculators
{
    public class MedicareTaxWithholdingCalculator(ITaxLookupService taxLookupService) : ICalculateWithholdings
    {
        public async Task<Withholding[]> CalculateAsync(EmployeePayroll payroll)
        {
            return (await taxLookupService.GetApplicableEmployeeTaxes(payroll, TaxType.Medicare))
                .Select(tax => new Withholding(tax.Description, WithholdingType.Medicare, tax.Rate * payroll.GrossPay))
                .ToArray();
        }
    }
}
