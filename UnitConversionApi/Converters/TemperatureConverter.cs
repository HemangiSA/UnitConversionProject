
namespace UnitConversionApi.Converters;

public class TemperatureConverter : IUnitConverter
{
    private static readonly string[] TemperatureUnits =
    [
        "celsius",
        "fahrenheit",
        "kelvin"
    ];

    public bool Supports(string fromUnit, string toUnit)
    {
        return TemperatureUnits.Contains(
                   fromUnit.Trim(),
                   StringComparer.OrdinalIgnoreCase)
               &&
               TemperatureUnits.Contains(
                   toUnit.Trim(),
                   StringComparer.OrdinalIgnoreCase);
    }

    public double Convert(
        double value,
        string fromUnit,
        string toUnit)
    {
        fromUnit = fromUnit.Trim().ToLowerInvariant();
        toUnit = toUnit.Trim().ToLowerInvariant();

        if (!Supports(fromUnit, toUnit))
        {
            throw new ArgumentException(
                "Unsupported temperature conversion.");
        }

        // Convert the input to Celsius first.
        double celsius = fromUnit switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32) * 5 / 9,
            "kelvin" => value - 273.15,
            _ => throw new ArgumentException("Invalid temperature unit.")
        };

        if (celsius < -273.15)
        {
            throw new ArgumentException(
                "Temperature cannot be below absolute zero.");
        }

        // Convert Celsius to the requested unit.
        return toUnit switch
        {
            "celsius" => celsius,
            "fahrenheit" => (celsius * 9 / 5) + 32,
            "kelvin" => celsius + 273.15,
            _ => throw new ArgumentException("Invalid temperature unit.")
        };
    }
}
