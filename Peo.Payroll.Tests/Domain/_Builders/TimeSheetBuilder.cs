using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class TimeSheetBuilder
    {
        private TimePeriod[] _timePeriods = [];

        internal TimeSheetBuilder WithTimePeriods(TimePeriod[] timePeriods)
        {
            _timePeriods = timePeriods;
            return this;
        }

        internal TimeSheet Build()
        {
            return new TimeSheet(_timePeriods);
        }
    }
}
