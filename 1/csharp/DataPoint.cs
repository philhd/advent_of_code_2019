using CsvHelper.Configuration.Attributes;

public class DataPoint
{
    [Index(0)]
    public string Datum { get; set; }
}