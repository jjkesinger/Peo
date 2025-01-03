using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class CompanyBuilder
    {
        private Contract[] _contracts = [];

        internal CompanyBuilder WithContracts(Contract[] contracts)
        {
            _contracts = contracts;
            return this;
        }

        internal CompanyBuilder WithDefault()
        {
            WithContracts([new ContractBuilder().WithDefault().Build()]);
            return this;
        }

        internal Company Build()
        {
            return new Company(_contracts);
        }
    }
}
