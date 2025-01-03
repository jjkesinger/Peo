using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class TimePeriodBuilder
    {
        private PayType? _payType;
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;

        internal TimePeriodBuilder WithStartDateTime(DateTime startDate)
        {
            _startDateTime = startDate;
            return this;
        }

        internal TimePeriodBuilder WithEndDateTime(DateTime endDate)
        {
            _endDateTime = endDate;
            return this;
        }

        internal TimePeriodBuilder WithPayType(PayType payType)
        {
            _payType = payType;
            return this;
        }

        internal TimePeriod Build()
        {
            if (_endDateTime == null || _startDateTime == null || _payType == null)
            {
                throw new InvalidOperationException();
            }

            return new TimePeriod(_startDateTime.Value, _endDateTime.Value, _payType.Value);
        }
    }
}
