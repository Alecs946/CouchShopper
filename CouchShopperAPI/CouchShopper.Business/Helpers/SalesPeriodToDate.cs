using CouchShopper.Data.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Helpers
{
    public static class SalesPeriodToDate
    {
        public static DateTime GetPeriod(this SalesPeriod period)
        {
            var date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            switch (period)
            {
                case SalesPeriod.Days7:
                    return date.AddDays(-7);
                case SalesPeriod.Days30:
                    return date.AddDays(-30);
                case SalesPeriod.Days90:
                    return date.AddDays(-90);
                default:
                    return date;
            }
        }
    }
}
