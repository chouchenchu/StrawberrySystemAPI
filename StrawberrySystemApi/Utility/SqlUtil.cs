using System;

namespace StrawberrySystemApi.Utility
{
    public static class SqlUtil
    {
        public static string FormatDBString(this Enum val)
            => $@"N'{val.ToString()}'";
        public static string FormatDBString(this string val)
        {
            if (val == null)
                return "NULL";
            else
                return $"N'{val.Replace("'", "''")}'";
        }
        public static string FormatDBBBit(this bool val)
            => val ? "1" : "0";

        public static string FormatDBDateTime(this DateTime? val)
        {
            if (val == null)
                return "NULL";
            else
                return $"'{val.Value.ToString("yyyy/MM/dd HH:mm:ss")}'";
        }

        public static string FormatDBDateTime(this DateTime val)
                => $"'{val.ToString("yyyy/MM/dd HH:mm:ss")}'";

        public static DateTime? NullableDatetime(this object val)
        {
            if (val != DBNull.Value)
                return Convert.ToDateTime(val);
            else
                return null;
        }

        public static string NullableString(this object val)
            => Convert.ToString(val);

    }
}
