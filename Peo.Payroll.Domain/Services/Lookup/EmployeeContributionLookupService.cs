using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.Lookup
{
    public interface IEmployeeContributionLookupService
    {
        Task<EmployeeContribution[]> GetEmployeeContributionsAsync(EmployeePayroll payroll, ElectionType electionType);
    }

    public class EmployeeContributionLookupService : IEmployeeContributionLookupService
    {
        public async Task<EmployeeContribution[]> GetEmployeeContributionsAsync(EmployeePayroll payroll, ElectionType electionType)
        {
            var contributions = payroll.Employee.EmployeeContributions
                .Where(f => f.ElectionType == electionType && f.IsActive(payroll.Period.PayRange))
                .ToArray();

            return await Task.FromResult(contributions);
        }
    }
}
