namespace Peo.Payroll.Domain.Model
{
    public enum PayType
    {
        Salary,
        Holiday,
        PTO,
        Berevement
    }

    public enum CalculatePayType
    {
        Gross,
        Net,
        Taxable,
        Pto,
        YtdGross,
        YtdNet,
        YtdTaxable,
        YtdPto,
    }
}
