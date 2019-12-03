using System;
using System.Linq;
using CoreExtensions.Csv;
using CsvHelper.Configuration;

namespace csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var inputs = "../input.csv".ReadCsv<DataPoint>(new Configuration { HasHeaderRecord = false });
            Console.WriteLine(inputs.Select(x => GetFuel(double.Parse(x.Datum))).Sum());
            Console.WriteLine(inputs.Select(x => GetFuelIncludingFuel(double.Parse(x.Datum))).Sum());

            // TEST
            Console.WriteLine(GetFuelIncludingFuel(1969));
        }

        // Fuel required to launch a given module is based on its mass. Specifically, to find the fuel required for a module, take its mass, divide by three, round down, and subtract 2.
        static double GetFuel(double mass)
        {
            return Math.Floor(mass / 3.0) - 2.0;
        }

        static double GetFuelForFuel(double mass){
            var fuel = GetFuel(mass);
            // Console.WriteLine(fuel);
            if(fuel <= 0){
                return 0;
            }

            return fuel + GetFuelForFuel(fuel);
        }
        static double GetFuelIncludingFuel(double mass){
            var moduleFuel = GetFuel(mass);
            var fuelFuel = GetFuelForFuel(moduleFuel);
            return moduleFuel + fuelFuel;
        }
    }
}
