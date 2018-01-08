using System;

namespace Temporary_Prison.Service.Contracts.Helpers
{
    public static class ValueTypeHelper
    {
        public static Type GetType(Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return Nullable.GetUnderlyingType(type);
            }
            else
            {
                return type;
            }
        }
    }
}
