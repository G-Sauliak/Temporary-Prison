namespace Temporary_Prison.Business.Enums
{
    public enum UserCreateStatus
    {
        Success = 0,

        DuplicateUserName = 1, //username already exists

        DuplicateEmail = 2, //email already exists

        InvalidEmail = 3, //new email was not accepted 
    }
}
