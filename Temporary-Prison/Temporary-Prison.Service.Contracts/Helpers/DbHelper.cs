using System;
using System.Collections.Generic;
using System.Data;

namespace Temporary_Prison.Service.Contracts.Helpers
{
    public static class SqlDbTypeHelper
    {
        private static Dictionary<Type, SqlDbType> sqlTypeMap;

        static SqlDbTypeHelper()
        {
            sqlTypeMap = new Dictionary<Type, SqlDbType>()
            {
                { typeof(int), SqlDbType.Int },
                { typeof(long), SqlDbType.BigInt },
                { typeof(char[]), SqlDbType.NVarChar},
                { typeof(string), SqlDbType.NVarChar},
                { typeof(short), SqlDbType.SmallInt},
                { typeof(bool), SqlDbType.Bit},
                { typeof(DateTime), SqlDbType.DateTime},
                { typeof(decimal), SqlDbType.Money},
                { typeof(double), SqlDbType.Float},
                { typeof(TimeSpan), SqlDbType.Time }
                //....
            };
        }
        public static SqlDbType GetDbType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (sqlTypeMap.TryGetValue(type, out SqlDbType sqlDbType))
            {
                return sqlDbType;
            }
            throw new ArgumentException($"{type.FullName} is not support type");
        }
    }
}
