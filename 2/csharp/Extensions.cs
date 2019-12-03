using System.Collections.Generic;
using System.IO;

public static class Extensions
{
    public static IEnumerable<string> ReadFromFile(this string path)
    {
        using (StreamReader inputFile = new StreamReader(path))
        {
            while (true)
            {
                var line = inputFile.ReadLine();
                if (line == null)
                {
                    break;
                }
                yield return line;
            }
        }
    }
}