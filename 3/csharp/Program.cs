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
            // var input = new List<string> { "R8,U5,L5,D3", "U7,R6,D4,L4" };
            // var input = new List<string>{"L5,U5", "U2,L6"};
            Console.WriteLine(input.AsJSON());
            // RunScenario1(input);
            RunScenario2(input);
        }


        static void RunScenario1(List<string> input)
        {
            var map = new Map(25000);
            map.DrawWire(input[0], true);
            map.DrawWire(input[1], false);
            var intersections = map.GetIntersections();
            Console.WriteLine(intersections.AsJSON());
            Console.WriteLine(GetMinManhattanDistance(intersections));
            Console.WriteLine(GetMinSignalDelay(intersections));
        }

        static void RunScenario2(List<string> input)
        {
            var segmentMap = new SegmentMap();
            segmentMap.DrawWire(input[0]);
            segmentMap.DrawWire(input[1]);
            var intersections = segmentMap.GetIntersections();
            Console.WriteLine(intersections.AsJSON());
            Console.WriteLine(GetMinManhattanDistance(intersections));
            Console.WriteLine(GetMinSignalDelay(intersections, segmentMap.Wires));
        }

        static int GetMinManhattanDistance(ISet<(int, int, int)> intersections)
        {
            var minDist = int.MaxValue;
            foreach (var intersection in intersections)
            {
                var manDist = Math.Abs(intersection.Item1) + Math.Abs(intersection.Item2);
                if (manDist < minDist)
                {
                    minDist = manDist;
                }
            }

            return minDist;
        }

        static int GetMinManhattanDistance(IEnumerable<Intersection> intersections)
        {
            var minDist = int.MaxValue;
            foreach (var intersection in intersections)
            {
                var manDist = Math.Abs(intersection.IntersectionPoint.X) + Math.Abs(intersection.IntersectionPoint.Y);
                if (manDist < minDist)
                {
                    minDist = manDist;
                }
            }

            return minDist;
        }

        static int GetMinSignalDelay(ISet<(int, int, int)> intersections)
        {
            var minSteps = int.MaxValue;
            foreach (var intersection in intersections)
            {
                if (intersection.Item3 < minSteps)
                {
                    minSteps = intersection.Item3;
                }
            }

            return minSteps;
        }

        static int GetMinSignalDelay(IEnumerable<Intersection> intersections, List<Wire> wires)
        {
            var minDelay = int.MaxValue;
            foreach (var intersection in intersections)
            {
                var pathLength = wires.Sum(x => x.GetPathLength(intersection));
                if(pathLength < minDelay){
                    minDelay = pathLength;
                }
            }

            return minDelay;
        }
    }
}
