using Peo.Payroll.Domain.Services.WageCalculators;
using Peo.Payroll.Tests.Domain._Builders;

namespace Peo.Payroll.Tests.Domain.Services.WageCalculators;

[TestClass]
public class SalaryWageCalculatorTests
{
    private readonly SalaryWageCalculator Target = new();

    [TestMethod]
    public async Task CalculateAsync_GivenSalaryEmployee_ShouldCalculateTotalWages()
    {
        var payroll = new EmployeePayrollBuilder()
            .WithDefaultPayroll()
            .Build();

        var payments = await Target.CalculateAsync(payroll);

        Assert.AreEqual(4000m, payments.Sum(f => f.Amount));
    }
}
