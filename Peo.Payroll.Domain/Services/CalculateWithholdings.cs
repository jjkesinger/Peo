using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services
{
    public interface ICalculateWithholdings
    {
        Task<Withholding[]> CalculateAsync(EmployeePayroll payroll);
    }
}
