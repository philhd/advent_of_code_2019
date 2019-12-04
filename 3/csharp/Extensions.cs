using System;

public static class Extensions
{
    public static int ToCoord(this int idx)
    {
        if (idx % 2 == 0)
        {
            return idx / 2;
        }

        return (int)Math.Ceiling(idx / 2.0) * -1;
    }

    public static int ToIdx(this int coord){
        if(coord >= 0){
            return 2 * coord;
        }

        return 2 * (coord * -1) - 1;
    }
}