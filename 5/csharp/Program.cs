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
            var computer = new IntercodeComputer(numbers);

            computer.Compute(1);
        }
    }
}
