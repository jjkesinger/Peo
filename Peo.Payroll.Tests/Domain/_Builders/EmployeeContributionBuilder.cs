using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class EmployeeContributionBuilder
    {
        private string _description = "";
        private ElectionType? _electionType;
        private DeductionTaxType? _deductionTaxType;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private decimal? _rate;
        private decimal? _amount;

        internal EmployeeContributionBuilder WithElectionType(ElectionType electionType)
        {
            _electionType = electionType;
            _description = electionType.ToString();
            return this;
        }

        internal EmployeeContributionBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        internal EmployeeContributionBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        internal EmployeeContributionBuilder WithDeductionTaxType(DeductionTaxType deduuctionTaxType)
        {
            _deductionTaxType = deduuctionTaxType;
            return this;
        }

        internal EmployeeContributionBuilder  WithRate(decimal rate)
        {
            _rate = rate;
            return this;
        }

        internal EmployeeContributionBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        internal EmployeeContributionBuilder WithDefault()
        {
            WithStartDate(DateTime.MinValue);
            WithEndDate(DateTime.MaxValue);
            WithDeductionTaxType(DeductionTaxType.PreTax);
            WithRate(.1m);

            return this;
        }

        internal EmployeeContribution Build()
        {
            if (_electionType == null || _deductionTaxType == null || (_rate == null && _amount == null))
                throw new InvalidOperationException();

            return new EmployeeContribution(_description, _deductionTaxType.Value, _electionType.Value, _startDate ?? DateTime.MinValue, _endDate ?? DateTime.MaxValue, _amount, _rate);
        }
    }
}
