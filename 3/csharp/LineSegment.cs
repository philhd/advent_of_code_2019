using System;

public class LineSegment
{
    public LineSegment(Point start, Point end)
    {
        Start = start;
        End = end;
        IsVertical = Start.X == End.X;
        MinX = Math.Min(Start.X, End.X);
        MinY = Math.Min(Start.Y, End.Y);
        MaxX = Math.Max(Start.X, End.X);
        MaxY = Math.Max(Start.Y, End.Y);
        Length = Math.Abs(End.X - Start.X) + Math.Abs(End.Y - Start.Y);
    }

    public Point Start { get; }
    public Point End { get; }

    public bool IsVertical { get; }

    public int MinX { get; }
    public int MaxX { get; }
    public int MinY { get; }
    public int MaxY { get; }
    public int Length { get; }

    public Intersection Intersects(LineSegment other)
    {
        // are they in the same orientation?
        if ((IsVertical && other.IsVertical) || (!IsVertical && !other.IsVertical))
        {
            // lines are parallel, no intersection
            return null;
        }

        // do they intersect?
        if (MinX > other.MaxX)
        {
            return null;
        }
        if (MaxX < other.MinX)
        {
            return null;
        }
        if (MinY > other.MaxY)
        {
            return null;
        }
        if (MaxY < other.MinY)
        {
            return null;
        }

        // take the x of the vertical segment
        var xCoord = IsVertical ? MinX : other.MinX;
        // take the y of the horizontal segment
        var yCoord = IsVertical ? other.MinY : MinY;
        return new Intersection
        {
            Segment1 = this,
            Segment2 = other,
            IntersectionPoint = new Point(xCoord, yCoord)
        };
    }
}