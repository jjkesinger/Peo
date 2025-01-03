namespace Peo.Payroll.Domain.Model
{
    public record TimePeriod(DateTime StartTime, DateTime EndTime, PayType PayType)
    {
        public decimal TotalHours => new((EndTime - StartTime).TotalHours);
    }
}
