
using UnitConversionApi.Converters;
using UnitConversionApi.Models;

namespace UnitConversionApi.Services;

public class ConversionService : IConversionService
{
    private readonly IEnumerable<IUnitConverter> _converters;

    public ConversionService(
        IEnumerable<IUnitConverter> converters)
    {
        _converters = converters;
    }

    public ConversionResponse Convert(
        ConversionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!request.Value.HasValue ||
            string.IsNullOrWhiteSpace(request.FromUnit) ||
            string.IsNullOrWhiteSpace(request.ToUnit))
        {
            throw new ArgumentException(
                "Value, FromUnit, and ToUnit are required.");
        }

        double value = request.Value.Value;

        if (!double.IsFinite(value))
        {
            throw new ArgumentException(
                "Value must be a finite number.");
        }

        string fromUnit = request.FromUnit.Trim();
        string toUnit = request.ToUnit.Trim();

        IUnitConverter? converter = _converters.FirstOrDefault(
            c => c.Supports(fromUnit, toUnit));

        if (converter is null)
        {
            throw new ArgumentException(
                $"Conversion from '{fromUnit}' to '{toUnit}' is not supported.");
        }

        double convertedValue = converter.Convert(
            value,
            fromUnit,
            toUnit);

        if (!double.IsFinite(convertedValue))
        {
            throw new ArgumentException(
                "The converted value is outside the supported range.");
        }

        return new ConversionResponse
        {
            Value = value,
            FromUnit = fromUnit,
            ToUnit = toUnit,
            ConvertedValue = Math.Round(convertedValue, 6)
        };
    }
}
