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

            Console.WriteLine(input.AsJSON());

            // map from node to parent
            var tree = new Dictionary<string,string>();

            foreach(var link in input){
                var nodes = link.Split(')');
                var child = nodes[1];
                var parent = nodes[0];

                // lazy initialize nodes to point to themself
                if(!tree.ContainsKey(parent)){
                    tree[parent] = parent;
                }
                if(!tree.ContainsKey(child)){
                    tree[child] = child;
                }

                // add link from child to parent
                tree[child] = parent;
            }

            var edgesCount = 0;

            // for each node, count the edges to the root
            foreach(var node in tree.Keys){
                // Console.WriteLine($"for {node}");
                var currentNode = node;
                while(tree[currentNode] != currentNode){
                    // Console.WriteLine($"adding edge for {currentNode}->{tree[currentNode]}");
                    edgesCount++;
                    currentNode = tree[currentNode];
                }
            }

            Console.WriteLine(edgesCount);
        }
    }
}
