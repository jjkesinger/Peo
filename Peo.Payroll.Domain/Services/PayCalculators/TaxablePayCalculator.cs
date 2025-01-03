using Peo.Payroll.Domain.Model;

namespace Peo.Payroll.Domain.Services.PayCalculators
{
    public class TaxablePayCalculator : ICalculatePay
    {
        public async Task<decimal> CalculateAsync(EmployeePayroll payroll)
        {
            var taxablePay = payroll.GrossPay - 
                payroll.Deductions
                    .Where(f => f.DeductionTaxType == DeductionTaxType.PreTax)
                    .Sum(f => f.Amount ?? 0);

            return await Task.FromResult(taxablePay);
        }
    }
}
