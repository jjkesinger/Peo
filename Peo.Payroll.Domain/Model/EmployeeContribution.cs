namespace Peo.Payroll.Domain.Model
{
    public record EmployeeContribution(string Description, 
        DeductionTaxType DeductionTaxType, 
        ElectionType ElectionType, 
        DateTime StartDate,
        DateTime? EndDate,
        decimal? Amount = null, 
        decimal? Rate = null)
    {
        public bool IsActive(DateRange payPeriod)
        {
            return new DateRange(StartDate, EndDate ?? DateTime.MaxValue).Intersects(payPeriod);
        }
    }
}
