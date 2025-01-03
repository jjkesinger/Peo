using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class ContractBuilder
    {
        private DateTime? _startDate;
        private DateTime? _endDate;

        internal ContractBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        internal ContractBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        internal ContractBuilder WithDefault()
        {
            WithStartDate(new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Utc));
            WithEndDate(new DateTime(2024, 12, 21, 23, 59, 59, 999, DateTimeKind.Utc));

            return this;
        }

        internal Contract Build()
        {
            if (_startDate == null || _endDate == null)
            {
                throw new InvalidOperationException();
            }

            return new Contract(new DateRange(_startDate.Value, _endDate.Value));
        }
    }
}
