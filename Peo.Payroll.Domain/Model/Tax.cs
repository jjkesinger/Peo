namespace Peo.Payroll.Domain.Model
{
    public record Tax(string Description, TaxType TaxType, decimal Rate)
    {
    }
}
