using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.WageCalculators;
using Peo.Payroll.Tests.Domain._Builders;

namespace Peo.Payroll.Tests.Domain.Services.WageCalculators;

[TestClass]
public class HourlyWageCalculatorTests
{
    private readonly HourlyWageCalculator Target = new();

    [TestMethod]
    public async Task CalculateAsync_GivenHourlyEmployee_CalculatesTotalWages()
    {
        var payroll = new EmployeePayrollBuilder()
            .WithCompany(new CompanyBuilder().Build())
            .WithEmployee(new EmployeeBuilder()
                .WithEmployeePayHistory([
                    new EmployeePayBuilder()
                        .WithEffectiveDate(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local))
                        .WithUpdateDate(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local))
                        .WithPaymentAmount(50m)
                        .WithWageType(WageType.Hourly)
                        .Build()
                    ])
                .WithEmployeeHireHistory([
                    new EmployeeHireBuilder()
                        .WithStartDate(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local))
                        .Build()
                    ])
                .Build())
            .WithPayPeriod(new PayPeriodBuilder()
                .WithStartDateTime(new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local))
                .WithEndDateTime(new DateTime(2024, 12, 21, 23, 59, 59, 999, DateTimeKind.Local))
                .WithPayDateTime(new DateTime(2024, 12, 27, 0, 0, 0, DateTimeKind.Local))
                .WithPayrollType(PayrollType.Regular)
                .Build())
            .WithTimeSheet(new TimeSheetBuilder()
                .WithTimePeriods([
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 8, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 9, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 9, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 10, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 10, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 11, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 11, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 12, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 12, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.PTO)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 15, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 15, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 16, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 16, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.PTO)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 17, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 17, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 18, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 18, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.Salary)
                        .Build(),
                    new TimePeriodBuilder()
                        .WithStartDateTime(new DateTime(2024, 12, 19, 0, 0, 0, DateTimeKind.Local))
                        .WithEndDateTime(new DateTime(2024, 12, 19, 8, 0, 0, DateTimeKind.Local))
                        .WithPayType(PayType.PTO)
                        .Build(),
                    ])
                .Build())
            .Build();

        var val = await Target.CalculateAsync(payroll);

        Assert.AreEqual(4000m, val.Sum(f => f.Amount));
    }
}
