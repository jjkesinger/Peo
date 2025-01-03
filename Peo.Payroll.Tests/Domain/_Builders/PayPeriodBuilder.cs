using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class PayPeriodBuilder
    {
        private DateTime? _payDate;
        private DateOnly? _startDate;
        private DateTime? _startDateTime;
        private DateOnly? _endDate;
        private DateTime? _endDateTime;
        private PayrollType? _payrollType;

        internal PayPeriodBuilder WithStartDateTime(DateTime startDate)
        {
            _startDateTime = startDate;
            _startDate = new DateOnly(startDate.Year, startDate.Month, startDate.Day);
            return this;
        }

        internal PayPeriodBuilder WithEndDateTime(DateTime endDate)
        {
            _endDateTime = endDate;
            _endDate = new DateOnly(endDate.Year, endDate.Month, endDate.Day);
            return this;
        }

        internal PayPeriodBuilder WithPayDateTime(DateTime payDate)
        {
            _payDate = payDate;
            return this;
        }

        internal PayPeriodBuilder WithPayrollType(PayrollType payrollType)
        {
            _payrollType = payrollType;
            return this;
        }

        internal PayPeriodBuilder WithDefault()
        {
            WithStartDateTime(new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Utc));
            WithEndDateTime(new DateTime(2024, 12, 21, 23, 59, 59, 999, DateTimeKind.Utc));
            WithPayDateTime(new DateTime(2024, 12, 27, 0, 0, 0, DateTimeKind.Utc));
            WithPayrollType(PayrollType.Regular);
            return this;
        }

        internal PayPeriod Build()
        {
            if (_startDate == null || _startDateTime == null || _endDate == null || _endDateTime == null || _payDate == null || _payrollType == null)
            {
                throw new InvalidOperationException();
            }

            return new PayPeriod(_startDate.Value, _endDate.Value, _payDate.Value, new DateRange(_startDateTime.Value, _endDateTime.Value), _payrollType.Value);
        }
    }
}
