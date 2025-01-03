using Peo.Payroll.Domain.Services;
using Peo.Payroll.Domain.Services.PayCalculators;
using System.Collections.Immutable;

namespace Peo.Payroll.Domain.Model
{
    public record EmployeePayroll(Company Company, Employee Employee, PayPeriod Period, TimeSheet? Timesheet = null)
    {
        public ImmutableArray<Withholding> Withholdings { get; private set; } = [];
        public ImmutableArray<Payment> Payments { get; private set; } = [];
        public ImmutableArray<Deduction> Deductions { get; private set; } = [];

        public decimal GrossPay { get; private set; } = decimal.Zero;
        public decimal TaxablePay { get; private set; } = decimal.Zero;
        public decimal NetPay { get; private set; } = decimal.Zero;
        public decimal Pto { get; private set; } = decimal.Zero;

        public decimal YtdGrossPay { get; private set; } = decimal.Zero;
        public decimal YtdTaxablePay { get; private set; } = decimal.Zero;
        public decimal YtdNetPay { get; private set; } = decimal.Zero;
        public decimal YtdPto { get; private set; } = decimal.Zero;

        public async Task CalculateAsync(
            ICalculatePayFactory payFactory,
            IEnumerable<ICalculatePayments> calculatePayments,
            IEnumerable<ICalculateDeductions> calculateDeductions,
            IEnumerable<ICalculateWithholdings> calculateWithholdings)
        {
            YtdGrossPay = await payFactory.GetPayCalculator(CalculatePayType.YtdGross).CalculateAsync(this).ConfigureAwait(false);
            YtdTaxablePay = await payFactory.GetPayCalculator(CalculatePayType.YtdTaxable).CalculateAsync(this).ConfigureAwait(false);
            YtdNetPay = await payFactory.GetPayCalculator(CalculatePayType.YtdNet).CalculateAsync(this).ConfigureAwait(false);
            YtdPto = await payFactory.GetPayCalculator(CalculatePayType.YtdPto).CalculateAsync(this).ConfigureAwait(false);

            await CalculatePaymentsAsync(calculatePayments).ConfigureAwait(false);
            GrossPay = await payFactory.GetPayCalculator(CalculatePayType.Gross).CalculateAsync(this).ConfigureAwait(false);

            await CalculateDeductionsAsync(calculateDeductions).ConfigureAwait(false);

            TaxablePay = await payFactory.GetPayCalculator(CalculatePayType.Taxable).CalculateAsync(this).ConfigureAwait(false);
            await CalculateWithholdingsAsync(calculateWithholdings).ConfigureAwait(false);

            NetPay = await payFactory.GetPayCalculator(CalculatePayType.Net).CalculateAsync(this).ConfigureAwait(false);

            Pto = await payFactory.GetPayCalculator(CalculatePayType.Pto).CalculateAsync(this).ConfigureAwait(false);

            YtdGrossPay += GrossPay;
            YtdTaxablePay += TaxablePay;
            YtdNetPay += NetPay;
            YtdPto += Pto;
        }

        private async Task CalculatePaymentsAsync(IEnumerable<ICalculatePayments> calculatePayments)
        {
            Payments = (await Task.WhenAll(calculatePayments.Select(async f => await f.CalculateAsync(this).ConfigureAwait(false)))).SelectMany(f => f).ToImmutableArray();
        }

        private async Task CalculateDeductionsAsync(IEnumerable<ICalculateDeductions> calculateDeductions)
        {
            Deductions = (await Task.WhenAll(calculateDeductions.Select(async f => await f.CalculateAsync(this).ConfigureAwait(false)))).SelectMany(f => f).ToImmutableArray();
        }

        private async Task CalculateWithholdingsAsync(IEnumerable<ICalculateWithholdings> calculateWithholdings)
        {
            Withholdings = (await Task.WhenAll(calculateWithholdings.Select(async f => await f.CalculateAsync(this).ConfigureAwait(false)))).SelectMany(f => f).ToImmutableArray();
        }
    }
}
