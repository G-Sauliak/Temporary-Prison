using System;
using Temporary_Prison.Business.Enums;

namespace Temporary_Prison.Business.Exceptions
{
    public class CreateOrUpdateUserException : Exception
    {
        public CreateOrUpdateUserException(UserCreateStatus statusCode)
                    : base(GetMessageFromStatusCode(statusCode))
        {
            _StatusCode = statusCode;
        }

        public UserCreateStatus _StatusCode { get; private set; }


        private static string GetMessageFromStatusCode(UserCreateStatus statusCode)
        {
            switch (statusCode)
            {

                case UserCreateStatus.InvalidEmail:
                    return "he e-mail address provided is invalid.";

                case UserCreateStatus.DuplicateUserName:
                    return "User name already exists.";

                case UserCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists.";

            }

            return "User Manager Error";
        }
    }
}
