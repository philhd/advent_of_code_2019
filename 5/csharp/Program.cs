using System;
using System.Collections.Generic;
using System.Linq;
using CoreExtensions.Json;
using CoreExtensions.File;
using System.Diagnostics;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = "../input.txt".ReadFromFile();
            var linesList = lines.ToList();
            var numbers = linesList.First().Split(',').Select(x => int.Parse(x)).ToList();
            // var numbers = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9".Split(',').Select(x => int.Parse(x)).ToList();
            var computer = new IntercodeComputer(numbers);

            computer.Compute(5);
        }
    }
}
