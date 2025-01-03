using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class EmployeeBuilder
    {
        private EmployeePay[] _payHistory = [];
        private EmployeeHire[] _hireHistory = [];
        private EmployeeAddress[] _addressHistory = [];
        private EmployeeElection[] _employeeElections = [];
        private EmployeeContribution[] _employeeContributions = [];
        private EmployeeYtdHistory? _ytdHistory;

        internal EmployeeBuilder WithEmployeePayHistory(EmployeePay[] payHistory)
        {
            _payHistory = payHistory;
            return this;
        }

        internal EmployeeBuilder WithEmployeeHireHistory(EmployeeHire[] hireHistory)
        {
            _hireHistory = hireHistory;
            return this;
        }

        internal EmployeeBuilder WithEmployeeAddressHistory(EmployeeAddress[] addressHistory)
        {
            _addressHistory = addressHistory;
            return this;
        }

        internal EmployeeBuilder WithEmployeeElections(EmployeeElection[] employeeElections)
        {
            _employeeElections = employeeElections;
            return this;
        }

        internal EmployeeBuilder WithEmployeeContributions(EmployeeContribution[] employeeContributions)
        {
            _employeeContributions = employeeContributions;
            return this;
        }

        internal EmployeeBuilder WithEmployeeYtdHistory(EmployeeYtdHistory ytdHistory)
        {
            _ytdHistory = ytdHistory;
            return this;
        }

        internal EmployeeBuilder WithDefault()
        {
            WithEmployeePayHistory([new EmployeePayBuilder().WithDefault().Build()]);
            WithEmployeeHireHistory([new EmployeeHireBuilder().WithDefault().Build()]);
            WithEmployeeElections([
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Add).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Dental).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Education).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Life).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Ltd).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Medical).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Retirement).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Std).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Hsa).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Cfsa).Build(),
                new EmployeeElectionBuilder().WithDefault().WithElectionType(ElectionType.Fsa).Build()
            ]);

            WithEmployeeContributions([
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Hsa).WithDeductionTaxType(DeductionTaxType.PreTax).WithAmount(100m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Retirement).WithDeductionTaxType(DeductionTaxType.PreTax).WithRate(.06m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Ltd).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(10m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Std).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(10m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Life).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(20m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Add).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(20m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Dental).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(25m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Medical).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(250m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Education).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(15m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Cfsa).WithDeductionTaxType(DeductionTaxType.PostTax).WithAmount(20m).Build(),
                new EmployeeContributionBuilder().WithElectionType(ElectionType.Fsa).WithDeductionTaxType(DeductionTaxType.PreTax).WithAmount(25m).Build()
            ]);
            return this;
        }

        internal Employee Build()
        {
            return new Employee(_payHistory, _hireHistory, _addressHistory, _employeeElections, _employeeContributions, _ytdHistory);
        }
    }
}
