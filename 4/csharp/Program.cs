using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var count1 = 0;
            var count2 = 0;
            var set1 = new HashSet<int>();
            var set2 = new HashSet<int>();
            for (int i = 124075; i <= 580769; i++)
            {
                if (IsMatch(i))
                {
                    set1.Add(i);
                    count1++;
                }

                if (IsMatch2(i))
                {
                    set2.Add(i);
                    count2++;
                }
            }

            Console.WriteLine(count1);
            Console.WriteLine(count2);

            set1.ExceptWith(set2);
            Console.WriteLine(string.Join(",", set1));
        }

        static bool IsMatch(int i)
        {
            var doubleDigit = false;
            var ascending = true;
            var digits = GetDigits(i);
            for (int j = 0; j < digits.Length - 1; j++)
            {
                if (digits[j] == digits[j + 1])
                {
                    doubleDigit = true;
                }
                if (digits[j + 1] < digits[j])
                {
                    ascending = false;
                }
            }
            if (doubleDigit && ascending)
            {
                return true;
            }
            return false;
        }

        static int[] GetDigits(int i)
        {
            return i.ToString().Select(x => int.Parse(x.ToString())).ToArray();
        }

        static bool IsMatch2(int i)
        {
            // map from digit to digit count
            var digitCountMap = new Dictionary<int, int>();
            var doubleDigit = false;
            var ascending = true;
            var digits = GetDigits(i);
            for (int j = 0; j < digits.Length; j++)
            {
                var digit = digits[j];

                // don't need to worry about adjacency because ascending 
                // constraint implies that repeated digits are adjacent
                if (digitCountMap.ContainsKey(digit))
                {
                    digitCountMap[digit]++;
                }
                else
                {
                    digitCountMap[digit] = 1;
                }

                if (j != digits.Length - 1 // skip last comparison
                && digits[j + 1] < digits[j])
                {
                    ascending = false;
                }
            }

            doubleDigit = digitCountMap.Values.Contains(2);
            if (doubleDigit && ascending)
            {
                return true;
            }
            return false;
        }
    }
}
