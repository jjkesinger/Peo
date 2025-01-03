using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.Lookup;

namespace Peo.Payroll.Domain.Services.WithholdingCalculators
{
    public class SocialSecurityTaxWithholdingCalculator(ITaxLookupService taxLookupService) : ICalculateWithholdings
    {
        public async Task<Withholding[]> CalculateAsync(EmployeePayroll payroll)
        {
            return (await taxLookupService.GetApplicableEmployeeTaxes(payroll, TaxType.SocialSecurity))
                .Select(tax => new Withholding(tax.Description, WithholdingType.SocialSecurity, tax.Rate * payroll.GrossPay))
                .ToArray();
        }
    }
}
