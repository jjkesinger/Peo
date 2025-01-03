using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services
{
    public interface ICalculateWithholding
    {
        Task<Withholding[]> CalculateAsync(EmployeePayroll payroll);
    }
}
