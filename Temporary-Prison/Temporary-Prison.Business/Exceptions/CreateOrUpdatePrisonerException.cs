using System;
using Temporary_Prison.Business.Enums;

namespace Temporary_Prison.Business.Exceptions
{
    public class CreateOrUpdatePrisonerException : Exception
    {
        public CreateOrUpdatePrisonerException(CreateOrUpdatePrisonerCodeStatus statusCode)
                    : base(GetMessageFromStatusCode(statusCode))
        {
            _StatusCode = statusCode;
        }

        public CreateOrUpdatePrisonerCodeStatus _StatusCode { get; private set; }


        private static string GetMessageFromStatusCode(CreateOrUpdatePrisonerCodeStatus statusCode)
        {
            switch (statusCode)
            {

                case CreateOrUpdatePrisonerCodeStatus.MoreThanСurrentYear:
                    return "year of birth more than current year";

                case CreateOrUpdatePrisonerCodeStatus.SmallAge:
                    return "age is less than allowable";

            }

            return "User Manager Error";
        }
    }
}
