using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.Lookup
{
    public interface IEmployeeElectionLookupService
    {
        Task<EmployeeElection[]> GetActiveElections(EmployeePayroll payroll, ElectionType electionType);
    }

    public class EmployeeElectionLookupService : IEmployeeElectionLookupService
    {
        public async Task<EmployeeElection[]> GetActiveElections(EmployeePayroll payroll, ElectionType electionType)
        {
            var elections = payroll.Employee.EmployeeElections
                .Where(f => f.ElectionType == electionType && f.IsActive(payroll.Period.PayRange))
                .ToArray();

            return await Task.FromResult(elections);
        }
    }
}
