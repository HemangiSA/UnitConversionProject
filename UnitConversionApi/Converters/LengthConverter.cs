
using UnitConversionApi.Converters;

namespace UnitConversionApi.Converters;

public class LengthConverter : IUnitConverter
{
    private static readonly Dictionary<string, double>
        MetersPerUnit = new(StringComparer.OrdinalIgnoreCase)
        {
            ["meters"] = 1.0,
            ["feet"] = 0.3048,
            ["kilometers"] = 1000.0,
            ["miles"] = 1609.344
        };

    public bool Supports(string fromUnit, string toUnit)
    {
        return MetersPerUnit.ContainsKey(fromUnit.Trim()) &&
               MetersPerUnit.ContainsKey(toUnit.Trim());
    }

    public double Convert(
        double value,
        string fromUnit,
        string toUnit)
    {
        fromUnit = fromUnit.Trim();
        toUnit = toUnit.Trim();

        if (!Supports(fromUnit, toUnit))
        {
            throw new ArgumentException(
                "Unsupported length conversion.");
        }

        double meters = value * MetersPerUnit[fromUnit];

        return meters / MetersPerUnit[toUnit];
    }
}
