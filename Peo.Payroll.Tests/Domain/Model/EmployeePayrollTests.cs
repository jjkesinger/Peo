using Peo.Payroll.Domain.Services.DeductionCalculators;
using Peo.Payroll.Domain.Services.Lookup;
using Peo.Payroll.Domain.Services.PayCalculators;
using Peo.Payroll.Domain.Services.WageCalculators;
using Peo.Payroll.Domain.Services.WithholdingCalculators;
using Peo.Payroll.Tests.Domain._Builders;

namespace Peo.Payroll.Tests.Domain.Model
{
    [TestClass]
    public class EmployeePayrollTests
    {
        [TestMethod, TestCategory("Integration")]
        public async Task CalculatePayments_Integration()
        {
            var payroll = new EmployeePayrollBuilder()
                .WithDefaultPayroll()
                .Build();

            var eelookup = new EmployeeElectionLookupService();
            var eclookup = new EmployeeContributionLookupService();
            var tlookup = new TaxLookupService();

            await payroll.CalculateAsync(new PayCalculatorFactory(),
                [
                    new HourlyWageCalculator(), 
                    new SalaryWageCalculator()
                ],
                [
                    new AddInsuranceDeductionCalculator(eelookup, eclookup),
                    new CfsaDeductionCalculator(eelookup, eclookup),
                    new EducationPlanDeductionCalculator(eelookup, eclookup),
                    new FsaDeductionCalculator(eelookup, eclookup),
                    new HsaDeductionCalculator(eelookup, eclookup),
                    new LifeInsuranceDeductionCalculator(eelookup, eclookup),
                    new LongTermDisabilityInsuranceDeductionCalculator (eelookup, eclookup),
                    new RetirementDeductionCalculator(eelookup, eclookup),
                    new ShortTermDisabilityInsuranceDeductionCalculator(eelookup, eclookup),
                    new MedicalDeductionCalculator(eelookup, eclookup),
                    new DentalVisionDeductionCalculator(eelookup, eclookup)
                ],
                [
                    new FederalTaxWithholdingCalculator(tlookup),
                    new MedicareTaxWithholdingCalculator(tlookup),
                    new SocialSecurityTaxWithholdingCalculator(tlookup)
                ]
            );

            Assert.AreEqual(4000m, payroll.GrossPay);
            Assert.AreEqual(3635m, payroll.TaxablePay);
            Assert.AreEqual(2304.70m, payroll.NetPay);
            Assert.AreEqual(0m, payroll.Pto);
        }
    }
}
