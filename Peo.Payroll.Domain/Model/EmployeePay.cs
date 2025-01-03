namespace Peo.Payroll.Domain.Model
{
    public record EmployeePay(DateTime EffectiveDate, DateTime UpdateDate, DateTime? StopDate, decimal Amount, WageType WageType)
    {
        
    }
}
