using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class EmployeePayrollBuilder
    {
        private Company? _company;
        private Employee? _employee;
        private PayPeriod? _payPeriod;
        private TimeSheet? _timeSheet;

        internal EmployeePayrollBuilder WithCompany(Company company)
        {
            _company = company;
            return this;
        }

        internal EmployeePayrollBuilder WithEmployee(Employee employee)
        {
            _employee = employee;
            return this;
        }

        internal EmployeePayrollBuilder WithPayPeriod(PayPeriod payPeriod)
        {
            _payPeriod = payPeriod;
            return this;
        }

        internal EmployeePayrollBuilder WithTimeSheet(TimeSheet timesheet)
        {
            _timeSheet = timesheet;
            return this;
        }

        internal EmployeePayrollBuilder WithDefaultPayroll()
        {
            WithCompany(new CompanyBuilder().WithDefault().Build());
            WithEmployee(new EmployeeBuilder().WithDefault().Build());
            WithPayPeriod(new PayPeriodBuilder().WithDefault().Build());

            return this;
        }

        internal EmployeePayroll Build()
        {
            if (_company == null || _employee == null || _payPeriod == null)
            {
                throw new InvalidOperationException();
            }

            return new EmployeePayroll(_company, _employee, _payPeriod, _timeSheet);
        }
    }
}
