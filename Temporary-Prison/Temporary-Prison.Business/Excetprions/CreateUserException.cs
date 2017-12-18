using System;
using Temporary_Prison.Business.Enums;

namespace Temporary_Prison.Business.Exceptions
{
    public class CreateUserException : Exception
    {
        public CreateUserException(UserCreateStatus statusCode)
                    : base(GetMessageFromStatusCode(statusCode))
        {
            _StatusCode = statusCode;
        }

        public CreateUserException(String message)
            : base(message) { }

        public CreateUserException() { }

        public CreateUserException(String message, Exception innerException)
            : base(message, innerException) { }


        public UserCreateStatus _StatusCode { get; private set; }


        private static string GetMessageFromStatusCode(UserCreateStatus statusCode)
        {
            switch (statusCode)
            {

                case UserCreateStatus.InvalidEmail:
                    return "he e-mail address provided is invalid. Please check the value and try again.";

                case UserCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case UserCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists.Please enter a different e - mail address";

            }

            return "User Manager Error";
        }
    }
}
