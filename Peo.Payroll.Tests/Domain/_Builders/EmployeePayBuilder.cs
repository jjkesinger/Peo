using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class EmployeePayBuilder
    {
        private DateTime? _effectiveDate;
        private DateTime? _updateDate;
        private DateTime? _termDate;
        private decimal? _amount;
        private WageType? _wageType;

        internal EmployeePayBuilder WithEffectiveDate(DateTime effectiveDate)
        {
            _effectiveDate = effectiveDate;
            return this;
        }

        internal EmployeePayBuilder WithUpdateDate(DateTime updateDate)
        {
            _updateDate = updateDate;
            return this;
        }

        internal EmployeePayBuilder WithTermDate(DateTime termDate)
        {
            _termDate = termDate;
            return this;
        }

        internal EmployeePayBuilder WithPaymentAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        internal EmployeePayBuilder WithWageType(WageType wageType)
        {
            _wageType = wageType;
            return this;
        }

        internal EmployeePayBuilder WithDefault()
        {
            WithEffectiveDate(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            WithUpdateDate(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            WithPaymentAmount(104000m).WithWageType(WageType.Salary);

            return this;
        }

        internal EmployeePay Build()
        {
            if (_effectiveDate == null || _updateDate == null || _amount == null || _wageType == null)
            {
                throw new InvalidOperationException();
            }

            return new EmployeePay(_effectiveDate.Value, _updateDate.Value, _termDate, _amount.Value, _wageType.Value);
        }
    }
}
