using System;
using System.Collections.Generic;
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
            // var input = new List<string>{"R8,U5,L5,D3", "U7,R6,D4,L4"};
            //var input = new List<string>{"L5,U5", "U2,L6"};
            Console.WriteLine(input.AsJSON());

            var map = new Map(25000);

            map.DrawWire(input[0], true);
            map.DrawWire(input[1], false);
            var intersections = map.GetIntersections();
            Console.WriteLine(intersections.AsJSON());
            Console.WriteLine(GetMinManhattanDistance(intersections));
            Console.WriteLine(GetMinSignalDelay(intersections));
        }

        static int GetMinManhattanDistance(ISet<(int,int,int)> intersections){
            var minDist = int.MaxValue;
            foreach(var intersection in intersections){
                var manDist = Math.Abs(intersection.Item1) + Math.Abs(intersection.Item2);
                if(manDist < minDist){
                    minDist = manDist;
                }
            }

            return minDist;
        }

        static int GetMinSignalDelay(ISet<(int,int,int)> intersections){
            var minSteps = int.MaxValue;
            foreach(var intersection in intersections){
                if(intersection.Item3 < minSteps){
                    minSteps = intersection.Item3;
                }
            }

            return minSteps;
        }


    }
}
