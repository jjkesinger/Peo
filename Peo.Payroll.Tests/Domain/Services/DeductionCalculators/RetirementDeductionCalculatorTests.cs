using FakeItEasy;
using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.DeductionCalculators;
using Peo.Payroll.Domain.Services.Lookup;
using Peo.Payroll.Tests.Domain._Builders;
using Peo.Payroll.Tests.Domain.Mocks;

namespace Peo.Payroll.Tests.Domain.Services.DeductionCalculators;

[TestClass]
public class RetirementDeductionCalculatorTests
{
    private readonly IEmployeeElectionLookupService _employeeElectionLookupService = A.Fake<IEmployeeElectionLookupService>();
    private readonly IEmployeeContributionLookupService _employeeContributionLookupService = A.Fake<IEmployeeContributionLookupService>();
    private readonly RetirementDeductionCalculator Target;

    public RetirementDeductionCalculatorTests()
    {
        Target = new RetirementDeductionCalculator(_employeeElectionLookupService, _employeeContributionLookupService);
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeEnrolledInRetirement_ShouldCalculateAmount()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        await payroll.CalculatePay(new PayCalculatorFactoryMock());

        A.CallTo(() => _employeeElectionLookupService
            .GetActiveElections(payroll, ElectionType.Retirement))
            .Returns([new EmployeeElectionBuilder().WithElectionType(ElectionType.Retirement).Build()]);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Retirement))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithRate(.2m)
                .WithElectionType(ElectionType.Retirement)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(800m, deductions.Sum(f => f.Amount));
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeNotEnrolledInRetirement_ShouldCalculateZero()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        await payroll.CalculatePay(new PayCalculatorFactoryMock());

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Retirement))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithRate(.2m)
                .WithElectionType(ElectionType.Retirement)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(0m, deductions.Sum(f => f.Amount));
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeEnrolledInRetirementWith0Rate_ShouldCalculateZero()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        await payroll.CalculatePay(new PayCalculatorFactoryMock());

        A.CallTo(() => _employeeElectionLookupService
            .GetActiveElections(payroll, ElectionType.Retirement))
            .Returns([new EmployeeElectionBuilder().WithElectionType(ElectionType.Retirement).Build()]);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Retirement))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithRate(0m)
                .WithElectionType(ElectionType.Retirement)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(0m, deductions.Sum(f => f.Amount));
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeEnrolledInRetirementWithFixedAmount_ShouldCalculateAmount()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        await payroll.CalculatePay(new PayCalculatorFactoryMock());

        A.CallTo(() => _employeeElectionLookupService
            .GetActiveElections(payroll, ElectionType.Retirement))
            .Returns([new EmployeeElectionBuilder().WithElectionType(ElectionType.Retirement).Build()]);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Retirement))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithAmount(10m)
                .WithElectionType(ElectionType.Retirement)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(10m, deductions.Sum(f => f.Amount));
    }
}
