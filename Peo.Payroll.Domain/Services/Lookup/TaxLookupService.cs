using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.Lookup
{
    public interface ITaxLookupService
    {
        Task<Tax[]> GetApplicableEmployeeTaxes(EmployeePayroll payroll, TaxType taxType);
    }

    public class TaxLookupService : ITaxLookupService
    {
        public async Task<Tax[]> GetApplicableEmployeeTaxes(EmployeePayroll payroll, TaxType taxType)
        {
            var taxes = new List<Tax>
            {
                new("Federal", TaxType.Federal, .18m),
                new("Medicare", TaxType.Medicare, .0145m),
                new("Social Security", TaxType.SocialSecurity, .062m)
            };

            return await Task.FromResult(taxes.Where(f=>f.TaxType == taxType).ToArray());
        }
    }
}
