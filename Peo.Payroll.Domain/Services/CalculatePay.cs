using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services
{
    public interface ICalculatePay
    {
        Task<decimal> CalculateAsync(EmployeePayroll payroll);
    }
}
