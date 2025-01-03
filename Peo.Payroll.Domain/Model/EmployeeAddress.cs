namespace Peo.Payroll.Domain.Model
{
    public record EmployeeAddress(string Address1, string? Address2, string? Address3, string City, string State, string PostalCode, string Country)
    { }
}
