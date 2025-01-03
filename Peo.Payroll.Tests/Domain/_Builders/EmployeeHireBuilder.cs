using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class EmployeeHireBuilder
    {
        private DateTime? _hireDate;
        private DateTime? _termDate;

        internal EmployeeHireBuilder WithStartDate(DateTime startDate)
        {
            _hireDate = startDate;
            return this;
        }

        internal EmployeeHireBuilder WithTermDate(DateTime termDate)
        {
            _termDate = termDate;
            return this;
        }

        internal EmployeeHireBuilder WithDefault()
        {
            WithStartDate(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return this;
        }

        internal EmployeeHire Build()
        {
            if (_hireDate == null)
            {
                throw new InvalidOperationException();
            }

            return new EmployeeHire(_hireDate.Value, _termDate);
        }
    }
}
