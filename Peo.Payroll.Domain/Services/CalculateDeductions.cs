using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services
{
    public interface ICalculateDeductions
    {
        Task<Deduction[]> CalculateAsync(EmployeePayroll payroll);
    }
}
