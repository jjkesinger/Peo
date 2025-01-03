namespace Peo.Payroll.Domain.Model
{
    public record Employee(
        EmployeePay[] EmployeePayHistory, 
        EmployeeHire[] EmployeeHireHistory, 
        EmployeeAddress[] Addresses,
        EmployeeElection[] EmployeeElections,
        EmployeeContribution[] EmployeeContributions,
        EmployeeYtdHistory? EmployeeYtdHistory = null)
    {

    }
}
