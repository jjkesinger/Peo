using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services
{
    public interface ICalculatePayments
    {
        public Task<Payment[]> CalculateAsync(EmployeePayroll payroll);
    }
}
