namespace Temporary_Prison.Business.Enums
{
    public enum CreateOrUpdatePrisonerCodeStatus
    {
        MoreThanСurrentYear = 1, //if age is greater than the current year

        SmallAge = 2, //if age less than 10 years
    }
}
