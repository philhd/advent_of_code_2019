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
            // var input = "../testInput.txt".ReadFromFile().ToList();
            // var input = "../testInput2.txt".ReadFromFile().ToList();
            
            Console.WriteLine(input.AsJSON());

            // map from node to parent
            var tree = new Dictionary<string, string>();

            foreach (var link in input)
            {
                var nodes = link.Split(')');
                var child = nodes[1];
                var parent = nodes[0];

                // lazy initialize nodes to point to themself
                if (!tree.ContainsKey(parent))
                {
                    tree[parent] = parent;
                }
                if (!tree.ContainsKey(child))
                {
                    tree[child] = child;
                }

                // add link from child to parent
                tree[child] = parent;
            }

            var edgesCount = 0;

            // for each node, count the edges to the root
            foreach (var node in tree.Keys)
            {
                // Console.WriteLine($"for {node}");
                var currentNode = node;
                while (tree[currentNode] != currentNode)
                {
                    // Console.WriteLine($"adding edge for {currentNode}->{tree[currentNode]}");
                    edgesCount++;
                    currentNode = tree[currentNode];
                }
            }

            Console.WriteLine(edgesCount);

            // Find paths to root
            var path1 = GetPath(tree, "SAN", "COM");
            var path2 = GetPath(tree, "YOU", "COM");
            Console.WriteLine(path1.AsJSON());
            Console.WriteLine(path2.AsJSON());

            // Find common root
            var commonRoot = GetCommonRoot(tree, path1, path2);

            // Find distance from each to the common root
            var dist1 = GetPath(tree, "SAN", commonRoot);
            var dist2 = GetPath(tree, "YOU", commonRoot);

            // subtract 1 for each because it's the orbitee not the orbiter
            // subtract another to get the path length to get edge count from node count
            Console.WriteLine(dist1.Count - 2 + dist2.Count - 2);
        }

        static string GetCommonRoot(Dictionary<string, string> tree, List<string> path1, List<string> path2)
        {
            // trace back from main root to find common root
            path1.Reverse();
            path2.Reverse();
            for (int i = 0; i < Math.Min(path1.Count, path2.Count); i++)
            {
                if (path1[i] != path2[i])
                {
                    return path1[i - 1];
                }
            }
            throw new InvalidOperationException("no common root found");
        }

        static List<string> GetPath(Dictionary<string, string> tree, string start, string end)
        {
            var nodes = new List<string>();
            var currentNode = start;
            while (true)
            {
                // Console.WriteLine($"adding edge for {currentNode}->{tree[currentNode]}");
                nodes.Add(currentNode);

                // if we reach the root before finding the end, there is no path
                if (tree[currentNode] == currentNode)
                {
                    throw new InvalidOperationException("no path found");
                }

                // exit condition
                if (tree[currentNode] == end)
                {
                    nodes.Add(tree[currentNode]);
                    break;
                }
                currentNode = tree[currentNode];
            }

            return nodes;
        }
    }
}
