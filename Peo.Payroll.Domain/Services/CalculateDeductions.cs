using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services
{
    public interface ICalculateDeduction
    {
        Task<Deduction[]> CalculateAsync(EmployeePayroll payroll);
    }
}
