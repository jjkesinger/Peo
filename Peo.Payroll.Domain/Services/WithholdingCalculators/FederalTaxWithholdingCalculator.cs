using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.Lookup;

namespace Peo.Payroll.Domain.Services.WithholdingCalculators
{
    public class FederalTaxWithholdingCalculator(ITaxLookupService taxLookupService) : ICalculateWithholding
    {
        public async Task<Withholding[]> CalculateAsync(EmployeePayroll payroll)
        {
            return (await taxLookupService.GetApplicableEmployeeTaxes(payroll, TaxType.Federal))
                .Select(tax => new Withholding(tax.Description, WithholdingType.Federal, tax.Rate * payroll.TaxablePay))
                .ToArray();
        }
    }
}
