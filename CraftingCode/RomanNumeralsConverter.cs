using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingCode
{
    public class RomanNumeralsConverter
    {
        static RomanNumeralsConverter()
        {
            RomanNumerals = new Dictionary<int, string>()
            {
                {10, "X"},
                {5, "V" },
                {1, "I" }
            };
        }

        public static string Convert(int number)
        {
            var result = new StringBuilder();

            foreach (var romanMap in RomanNumerals)
            {
                while (number >= romanMap.Key)
                {
                    result.Append(romanMap.Value);
                    number -= romanMap.Key;
                }
            }

            return result.ToString();
        }

        private static readonly Dictionary<int, string> RomanNumerals;
    }
}
