using System;
using System.Collections.Generic;
using System.Linq;
using CoreExtensions.Json;
using CoreExtensions.File;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var lines = "../input.txt".ReadFromFile();
            var linesList = lines.ToList();

            var numbers = linesList.First().Split(',').Select(x => int.Parse(x)).ToList();
            // Console.WriteLine(numbers.AsJSON());

            // TEST
            var testInput = "1,9,10,3,2,3,11,0,99,30,40,50";
            var testNumbers = testInput.Split(',').Select(x => int.Parse(x)).ToList();
            Run(numbers);
            Console.WriteLine(numbers.AsJSON());
        }

        static void Run(List<int> numbers){
            resetAlarm(numbers);
            compute(numbers);
        }

        // Once you have a working computer, the first step is to restore the gravity 
        // assist program (your puzzle input) to the "1202 program alarm" state it had 
        // just before the last computer caught fire. To do this, before running the program,
        //  replace position 1 with the value 12 and replace position 2 with the value 2.
        static void resetAlarm(List<int> numbers)
        {
            numbers[1] = 12;
            numbers[2] = 2;
        }

        static void compute(List<int> numbers){
            for(int i = 0;i<numbers.Count; i+=4){
                var opcode = numbers[i];
                if(opcode == 99){
                    return;
                }
                var op1 = numbers[numbers[i+1]];
                var op2 = numbers[numbers[i+2]];
                var resultIdx = numbers[i+3];
                switch(opcode){
                    case 1:
                        numbers[resultIdx] = op1 + op2;
                        break;
                    case 2:
                        numbers[resultIdx] = op1 * op2;
                        break;
                    default:
                        throw new ArgumentException("invalid op code");
                }
            }
        }
    }
}
