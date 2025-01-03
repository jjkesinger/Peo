namespace Peo.Payroll.Domain.Model
{
    public enum WithholdingType
    {
        Federal,
        SocialSecurity, //FICA
        Medicare,
        CourtOrdered,
        Garnishment,
        Other
    }
}
