using System.Diagnostics.Metrics;

namespace Service.Helpers;

public class SKUHelper
{
    private static long _counter = 1;
    public static string GenerateSKU()
    {
       return _counter++.ToString().PadLeft(12, '0');
    }
}
