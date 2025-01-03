namespace Peo.Payroll.Domain.Model
{
    public class PayrollEvent
    {
        public IEnumerable<EmployeePayroll> EmployeePayrolls { get; set; } = [];
    }
}
