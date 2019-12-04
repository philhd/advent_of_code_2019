using System;
using System.Collections.Generic;
using CoreExtensions.Json;

public class Map
{
    private Square[,] m_grid;
    private ISet<(int,int,int)> m_intersections;
    public Map(int size)
    {
        m_grid = new Square[size, size];
        m_intersections = new HashSet<(int,int,int)>();
    }

    public void DrawWire(string wire, bool isRed)
    {
        var instructions = wire.Split(",");
        var x = 0;
        var y = 0;
        short steps = 0;

        foreach (var step in instructions)
        {
            var direction = step.Substring(0, 1);
            var length = int.Parse(step.Substring(1, step.Length - 1));

            for (int i = 0; i < length; i++)
            {
                steps++;
                switch(direction){
                    case "R":
                        x++;
                        break;
                    case "U":
                        y++;
                        break;
                    case "D":
                        y--;
                        break;
                    case "L":
                        x--;
                        break;
                    default:
                        throw new ArgumentException($"invalid direction {direction}");
                }

                var xIdx = x.ToIdx();
                var yIdx = y.ToIdx();

                if(isRed && !m_grid[xIdx,yIdx].HasRed){
                    m_grid[xIdx,yIdx].HasRed = true;
                    m_grid[xIdx,yIdx].RedSteps = steps;
                } else if(!m_grid[xIdx,yIdx].HasBlue) {
                    m_grid[xIdx,yIdx].HasBlue = true;
                    m_grid[xIdx,yIdx].BlueSteps = steps;
                }

                if(m_grid[xIdx,yIdx].HasRed && m_grid[xIdx,yIdx].HasBlue){
                    m_intersections.Add((x,y,(m_grid[xIdx,yIdx].RedSteps + m_grid[xIdx,yIdx].BlueSteps)));
                }
            }
        }
    }

    public ISet<(int,int,int)> GetIntersections(){
        return new HashSet<(int,int,int)>(m_intersections);
    }
}