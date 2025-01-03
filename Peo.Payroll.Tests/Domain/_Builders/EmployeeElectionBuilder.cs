using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class EmployeeElectionBuilder
    {
        private string _description = "";
        private ElectionType? _electionType;
        private DateTime? _startDate;
        private DateTime? _endDate;

        internal EmployeeElectionBuilder WithElectionType(ElectionType electionType)
        {
            _electionType = electionType;
            _description = electionType.ToString();
            return this;
        }

        internal EmployeeElectionBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        internal EmployeeElectionBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        internal EmployeeElectionBuilder WithDefault()
        {
            WithStartDate(DateTime.MinValue);
            WithEndDate(DateTime.MaxValue);
            
            return this;
        }

        internal EmployeeElection Build()
        {
            if (_electionType == null)
                throw new InvalidOperationException();

            return new EmployeeElection(_description, _electionType.Value, _startDate ?? DateTime.MinValue, _endDate ?? DateTime.MaxValue);
        }
    }
}
