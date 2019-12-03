using System;
using System.Linq;
using CoreExtensions.File;
using CoreExtensions.Json;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "../input.txt".ReadFromFile().ToList();
            Console.WriteLine(input.AsJSON());
        }
    }
}
