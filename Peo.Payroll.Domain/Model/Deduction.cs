namespace Peo.Payroll.Domain.Model
{
    public record Deduction(string Description, DeductionTaxType DeductionTaxType, decimal? Amount = null, decimal ? Rate = null)
    {
    }
}
