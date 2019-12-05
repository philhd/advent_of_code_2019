using System;
using System.Collections.Generic;

public class Wire
{
    private List<LineSegment> m_segments = new List<LineSegment>();
    public Wire(string instructions)
    {
        var steps = instructions.Split(",");
        var x = 0;
        var y = 0;

        foreach (var step in steps)
        {
            var direction = step.Substring(0, 1);
            var length = int.Parse(step.Substring(1, step.Length - 1));

            var start = new Point(x, y);
            Point end = null;
            switch (direction)
            {
                case "R":
                    x += length;
                    break;
                case "U":
                    y += length;
                    break;
                case "D":
                    y -= length;
                    break;
                case "L":
                    x -= length;
                    break;
                default:
                    throw new ArgumentException($"invalid direction {direction}");
            }

            end = new Point(x, y);
            m_segments.Add(new LineSegment(start, end));
        }
    }

    public IEnumerable<LineSegment> GetSegments()
    {
        return new List<LineSegment>(m_segments);
    }

    public int GetPathLength(Intersection intersection)
    {
        var length = 0;
        foreach (var segment in m_segments)
        {
            if (segment == intersection.Segment1 || segment == intersection.Segment2)
            {
                // get the absolute distance from the start of the segment to the intersection point
                length += Math.Abs(intersection.IntersectionPoint.X - segment.Start.X) + Math.Abs(intersection.IntersectionPoint.Y - segment.Start.Y);
                break;
            }

            length += segment.Length;
        }

        return length;
    }
}
