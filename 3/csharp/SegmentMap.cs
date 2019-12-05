using System.Collections.Generic;
using System.Linq;

public class SegmentMap
{
    public List<Wire> Wires { get; }

    public SegmentMap()
    {
        Wires = new List<Wire>();
    }

    public void DrawWire(string instructions)
    {
        Wires.Add(new Wire(instructions));
    }

    public IEnumerable<Intersection> GetIntersections()
    {
        List<Intersection> intersections = new List<Intersection>();
        var wire1 = Wires[0];
        var wire2 = Wires[1];
        foreach (var segment in wire1.GetSegments())
        {
            foreach (var segment2 in wire2.GetSegments())
            {
                var intersection = segment.Intersects(segment2);
                if (intersection != null)
                {
                    intersections.Add(intersection);
                }
            }
        }
        return intersections.Where(x => !(x.IntersectionPoint.X == 0 && x.IntersectionPoint.Y == 0));
    }
}