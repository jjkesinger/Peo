namespace Peo.Payroll.Domain.Model
{
    public record Withholding(string Description, WithholdingType WithholdingType, decimal Amount)
    {
    }
}
