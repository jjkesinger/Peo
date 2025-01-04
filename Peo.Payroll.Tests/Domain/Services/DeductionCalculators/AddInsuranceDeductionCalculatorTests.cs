using FakeItEasy;
using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.DeductionCalculators;
using Peo.Payroll.Domain.Services.Lookup;
using Peo.Payroll.Tests.Domain._Builders;

namespace Peo.Payroll.Tests.Domain.Services.DeductionCalculators;

[TestClass]
public class AddInsuranceDeductionCalculatorTests
{
    private readonly IEmployeeElectionLookupService _employeeElectionLookupService = A.Fake<IEmployeeElectionLookupService>();
    private readonly IEmployeeContributionLookupService _employeeContributionLookupService = A.Fake<IEmployeeContributionLookupService>();
    private readonly AddInsuranceDeductionCalculator Target;

    public AddInsuranceDeductionCalculatorTests()
    {
        Target = new AddInsuranceDeductionCalculator(_employeeElectionLookupService, _employeeContributionLookupService);
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeEnrolled_ShouldCalculateAmount()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        TestUtilities.SetPrivateProperty(payroll, nameof(EmployeePayroll.GrossPay), 4000m);

        A.CallTo(() => _employeeElectionLookupService
            .GetActiveElections(payroll, ElectionType.Add))
            .Returns([new EmployeeElectionBuilder().WithElectionType(ElectionType.Add).Build()]);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Add))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithRate(.2m)
                .WithElectionType(ElectionType.Add)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(800m, deductions.Sum(f => f.Amount));
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeNotEnrolled_ShouldCalculateZero()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        TestUtilities.SetPrivateProperty(payroll, nameof(EmployeePayroll.GrossPay), 4000m);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Add))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithRate(.2m)
                .WithElectionType(ElectionType.Add)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(0m, deductions.Sum(f => f.Amount));
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeEnrolledWith0Rate_ShouldCalculateZero()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        TestUtilities.SetPrivateProperty(payroll, nameof(EmployeePayroll.GrossPay), 4000m);

        A.CallTo(() => _employeeElectionLookupService
            .GetActiveElections(payroll, ElectionType.Add))
            .Returns([new EmployeeElectionBuilder().WithElectionType(ElectionType.Add).Build()]);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Add))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithRate(0m)
                .WithElectionType(ElectionType.Add)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(0m, deductions.Sum(f => f.Amount));
    }

    [TestMethod]
    public async Task CalculateAsync_GivenEmployeeEnrolledWithFixedAmount_ShouldCalculateAmount()
    {
        var payroll = new EmployeePayrollBuilder().WithDefaultPayroll().Build();
        TestUtilities.SetPrivateProperty(payroll, nameof(EmployeePayroll.GrossPay), 4000m);

        A.CallTo(() => _employeeElectionLookupService
            .GetActiveElections(payroll, ElectionType.Add))
            .Returns([new EmployeeElectionBuilder().WithElectionType(ElectionType.Add).Build()]);

        A.CallTo(() => _employeeContributionLookupService
            .GetEmployeeContributionsAsync(payroll, ElectionType.Add))
            .Returns([new EmployeeContributionBuilder()
                .WithDeductionTaxType(DeductionTaxType.PreTax)
                .WithAmount(10m)
                .WithElectionType(ElectionType.Add)
                .Build()]
            );

        var deductions = await Target.CalculateAsync(payroll);

        Assert.AreEqual(10m, deductions.Sum(f => f.Amount));
    }
}