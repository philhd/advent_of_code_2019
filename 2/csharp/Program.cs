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
            var goalOutput = 19690720;

            for (int noun = 0; noun < numbers.Count - 1; noun++)
            {
                for (int verb = 0; verb < numbers.Count - 1; verb++)
                {
                    computer.Reset();
                    computer.SetInputs(noun, verb);
                    computer.Compute();
                    var compOutput = computer.GetOutput();
                    if(compOutput == goalOutput){
                        Console.WriteLine($"noun={noun} verb={verb} answer={100 * noun + verb}");
                        return;
                    }
                    Console.WriteLine(compOutput);
                }
            }
        }
    }
}
