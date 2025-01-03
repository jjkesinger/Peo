namespace Peo.Payroll.Domain.Model
{
    public record DateRange(DateTime StartDate, DateTime EndDate)
    {
        public decimal GetAnnualFrequency()
        {
            var days = (int)Math.Round((EndDate - StartDate).TotalDays, 0);
            bool isLeapYear = DateTime.IsLeapYear(StartDate.Year);

            return days switch
            {
                7 => 52,
                14 => 26,
                15 => 24,
                28 => 12,
                29 when isLeapYear => 12,
                30 or 31 => 12,
                _ => (isLeapYear ? 366m / days : 365m / days)
            };
        }

        public bool Intersects(DateRange dateRange)
        {
            return StartDate <= dateRange.EndDate && dateRange.StartDate <= EndDate;
        }
    }
}
