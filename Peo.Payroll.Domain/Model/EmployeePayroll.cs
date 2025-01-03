using Peo.Payroll.Domain.Services;
using Peo.Payroll.Domain.Services.PayCalculators;

namespace Peo.Payroll.Domain.Model
{
    public record EmployeePayroll(Company Company, Employee Employee, PayPeriod Period, TimeSheet? Timesheet = null)
    {
        public Withholding[] Withholdings { get; private set; } = [];
        public Payment[] Payments { get; private set; } = [];
        public Deduction[] Deductions { get; private set; } = [];

        public decimal GrossPay { get; private set; } = decimal.Zero;
        public decimal TaxablePay { get; private set; } = decimal.Zero;
        public decimal NetPay { get; private set; } = decimal.Zero;
        public decimal Pto { get; private set; } = decimal.Zero;

        public decimal YtdGrossPay { get; private set; } = decimal.Zero;
        public decimal YtdTaxablePay { get; private set; } = decimal.Zero;
        public decimal YtdNetPay { get; private set; } = decimal.Zero;
        public decimal YtdPto { get; private set; } = decimal.Zero;

        public async Task Calculate(
            ICalculatePayFactory payFactory,
            IEnumerable<ICalculatePayments> calculatePayments,
            IEnumerable<ICalculateDeduction> calculateDeductions,
            IEnumerable<ICalculateWithholding> calculateWithholdings)
        {
            YtdGrossPay = await payFactory.GetPayCalculator(CalculatePayType.YtdGross).CalculateAsync(this);
            YtdTaxablePay = await payFactory.GetPayCalculator(CalculatePayType.YtdTaxable).CalculateAsync(this);
            YtdNetPay = await payFactory.GetPayCalculator(CalculatePayType.YtdNet).CalculateAsync(this);
            YtdPto = await payFactory.GetPayCalculator(CalculatePayType.YtdPto).CalculateAsync(this);

            await CalculatePayments(calculatePayments);
            GrossPay = await payFactory.GetPayCalculator(CalculatePayType.Gross).CalculateAsync(this);
            
            await CalculateDeductions(calculateDeductions);

            TaxablePay = await payFactory.GetPayCalculator(CalculatePayType.Taxable).CalculateAsync(this);
            await CalculateWithholdings(calculateWithholdings);
            
            NetPay = await payFactory.GetPayCalculator(CalculatePayType.Net).CalculateAsync(this);

            Pto = await payFactory.GetPayCalculator(CalculatePayType.Pto).CalculateAsync(this);

            YtdGrossPay += GrossPay;
            YtdTaxablePay += TaxablePay;
            YtdNetPay += NetPay;
            YtdPto += Pto;
        }

        private async Task CalculatePayments(IEnumerable<ICalculatePayments> calculatePayments)
        {
            Payments = (await calculatePayments.SelectAsync(async f => await f.CalculateAsync(this))).SelectMany(f => f).ToArray();
        }

        private async Task CalculateDeductions(IEnumerable<ICalculateDeduction> calculateDeductions)
        {
            Deductions = (await calculateDeductions.SelectAsync(async f => await f.CalculateAsync(this))).SelectMany(f => f).ToArray();
        }

        private async Task CalculateWithholdings(IEnumerable<ICalculateWithholding> calculateWithholdings)
        {
            Withholdings = (await calculateWithholdings.SelectAsync(async f => await f.CalculateAsync(this))).SelectMany(f => f).ToArray();
        }

        public async Task CalculatePay(ICalculatePayFactory payFactory)
        {
            await Calculate(payFactory, [], [], []);
        }
    }
}
