using System;

namespace ExplyMe.Modules.Core.Helpers
{
    public static class MoneyFormatter
    {
        public static string Format(long amount)
        {
            return amount > 0 ? string.Format("{0:#.00}", Convert.ToDecimal(amount) / 100) : "0,00";
        }
    }
}
