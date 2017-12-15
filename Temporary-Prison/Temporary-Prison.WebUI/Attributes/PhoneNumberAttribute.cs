using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Temporary_Prison.Business.Attributes
{
    public sealed class PhoneNumberAttribute : DataTypeAttribute
    {
        private const string pattern = @"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$";

        private const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        private static Regex regex = GetRegex();

        private static Regex GetRegex()
        {
            TimeSpan timeout = TimeSpan.FromSeconds(3);
            return new Regex(pattern, options, timeout);
        }

        public PhoneNumberAttribute() :
            base(DataType.PhoneNumber)
        { }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string[] phoneNumbers = value as string[];

            if (phoneNumbers == null)
            {
                return false;
            }

            foreach (string number in phoneNumbers)
            {
                number.Replace("+", string.Empty).TrimEnd();
                if (!regex.IsMatch(number))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
