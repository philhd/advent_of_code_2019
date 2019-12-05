using System;
using System.Linq;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            for(int i = 124075; i <= 580769; i++){
                var doubleDigit = false;
                var ascending = true;
                var digits = i.ToString().Select(x => int.Parse(x.ToString())).ToArray();
                for(int j=0; j < digits.Length-1;j++){
                    if(digits[j] == digits[j+1]){
                        doubleDigit = true;
                    }
                    if(digits[j+1] < digits[j]){
                        ascending = false;
                    }
                }
                if(doubleDigit && ascending){
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
