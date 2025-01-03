namespace Peo.Payroll.Domain.Model
{
    public record PayPeriod(DateOnly StartDate, DateOnly EndDate, DateTime PayDate, DateRange PayRange, PayrollType PayrollType)
    { }
}
