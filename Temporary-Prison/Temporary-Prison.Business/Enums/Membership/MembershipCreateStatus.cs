namespace Temporary_Prison.Business.Enums
{
    public enum CreateOrUpdateUserCodeStatus
    {
        DuplicateUserName = 1, //username already exists

        DuplicateEmail = 2, //email already exists

        InvalidEmail = 3, //new email was not accepted 
    }
}
