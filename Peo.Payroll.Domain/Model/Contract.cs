namespace Peo.Payroll.Domain.Model
{
    public record Contract(DateRange ContractDateRange)
    {
        public decimal GetDailyRate(ElectionType electionType)
        {
            return 0m;
        }
    }
}
