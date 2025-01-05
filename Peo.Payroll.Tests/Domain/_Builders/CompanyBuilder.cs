using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Tests.Domain._Builders
{
    internal class CompanyBuilder
    {
        private string? _companyName;

        internal CompanyBuilder WithCompanyName(string companyName)
        {
            _companyName = companyName;
            return this;
        }

        internal CompanyBuilder WithDefault()
        {
            WithCompanyName("Test Company");
            return this;
        }

        internal Company Build()
        {
            if (_companyName == null)
                throw new InvalidOperationException();

            return new Company(_companyName);
        }
    }
}
