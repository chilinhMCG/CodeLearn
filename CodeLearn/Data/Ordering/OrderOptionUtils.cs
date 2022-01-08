using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data.Ordering
{
    public static class OrderByOptionUtils
    {
        public static string ToUrlQueryValue(this OrderOption option)
        {
            return option switch
            {
                OrderOption.Ascending => "Ascending",
                OrderOption.Descending => "Descending",
                _ => throw new ArgumentOutOfRangeException(nameof(option), "Provided option is invalid."),
            };
        }

        public static OrderOption ConvertFromUrlQueryParamValue(string value)
        {
            return value switch
            {
                "Ascending" => OrderOption.Ascending,
                "Descending" => OrderOption.Descending,
                _ => throw new ArgumentOutOfRangeException(nameof(value), "Provided query value is invalid."),
            };
        }
    }
}
