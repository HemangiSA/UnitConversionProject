
namespace UnitConversionApi.Converters;

public class WeightConverter : IUnitConverter
{
    private static readonly Dictionary<string, double>
        KilogramsPerUnit = new(StringComparer.OrdinalIgnoreCase)
        {
            ["kilograms"] = 1.0,
            ["grams"] = 0.001,
            ["pounds"] = 0.45359237
        };

    public bool Supports(string fromUnit, string toUnit)
    {
        return KilogramsPerUnit.ContainsKey(fromUnit.Trim()) &&
               KilogramsPerUnit.ContainsKey(toUnit.Trim());
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
                "Unsupported weight conversion.");
        }

        if (value < 0)
        {
            throw new ArgumentException(
                "Weight cannot be negative.");
        }

        double kilograms = value * KilogramsPerUnit[fromUnit];

        return kilograms / KilogramsPerUnit[toUnit];
    }
}
