﻿using Peo.Payroll.Domain.Model;
using Peo.Payroll.Domain.Services.Lookup;

namespace Peo.Payroll.Domain.Services.DeductionCalculators
{
    public class MedicalDeductionCalculator(
        IEmployeeElectionLookupService employeeElectionLookupService,
        IEmployeeContributionLookupService employeeContributionLookupService) :
        BaseDeductionCalculator(employeeElectionLookupService, employeeContributionLookupService), ICalculateDeduction
    {
        public async Task<Deduction[]> CalculateAsync(EmployeePayroll payroll)
        {
            return await CalculateEmployeeDeductionAsync(payroll, ElectionType.Medical);
        }
    }
}
