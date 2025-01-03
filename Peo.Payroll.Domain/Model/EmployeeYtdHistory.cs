namespace Peo.Payroll.Domain.Model
{
    public record EmployeeYtdHistory(decimal YtdGrossIncome, 
        decimal YtdNetIncome, 
        decimal YtdTaxableIncome, 
        decimal YtdPto)
    {
    }
}
