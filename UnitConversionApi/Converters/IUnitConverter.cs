
namespace UnitConversionApi.Converters;

public interface IUnitConverter
{
    bool Supports(string fromUnit, string toUnit);

    double Convert(
        double value,
        string fromUnit,
        string toUnit);
}
