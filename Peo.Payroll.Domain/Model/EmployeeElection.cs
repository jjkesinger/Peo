namespace Peo.Payroll.Domain.Model
{
    public record EmployeeElection(string Description, ElectionType ElectionType, DateTime StartDate, DateTime? EndDate)
    {
        public bool IsActive(DateRange payPeriod)
        {
            return new DateRange(StartDate, EndDate ?? DateTime.MaxValue).Intersects(payPeriod);
        }
    }
}
