using System;
using Temporary_Prison.Business.Enums;

namespace Temporary_Prison.Business.Exceptions
{
    public class CreateOrUpdateUserException : Exception
    {
        public CreateOrUpdateUserException(CreateOrUpdateUserCodeStatus statusCode)
                    : base(GetMessageFromStatusCode(statusCode))
        {
            _StatusCode = statusCode;
        }

        public CreateOrUpdateUserCodeStatus _StatusCode { get; private set; }


        private static string GetMessageFromStatusCode(CreateOrUpdateUserCodeStatus statusCode)
        {
            switch (statusCode)
            {

                case CreateOrUpdateUserCodeStatus.InvalidEmail:
                    return "e-mail address provided is invalid.";

                case CreateOrUpdateUserCodeStatus.DuplicateUserName:
                    return "User name already exists.";

                case CreateOrUpdateUserCodeStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists.";

            }

            return "User Manager Error";
        }
    }
}
