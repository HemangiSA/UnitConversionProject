
using Xunit;
using UnitConversionApi.Converters;
using UnitConversionApi.Models;
using UnitConversionApi.Services;

namespace UnitConversionApi.Tests;

public class ConversionServiceTests
{
    private readonly ConversionService _service;

    public ConversionServiceTests()
    {
        IUnitConverter[] converters =
        [
            new LengthConverter(),
            new TemperatureConverter(),
            new WeightConverter()
        ];

        _service = new ConversionService(converters);
    }

    [Fact]
    public void Convert_CelsiusToFahrenheit_Returns212()
    {
        var request = new ConversionRequest
        {
            Value = 100,
            FromUnit = "celsius",
            ToUnit = "fahrenheit"
        };

        var result = _service.Convert(request);

        Assert.Equal(212, result.ConvertedValue);
    }

    [Fact]
    public void Convert_MetersToCelsius_ThrowsArgumentException()
    {
        var request = new ConversionRequest
        {
            Value = 10,
            FromUnit = "meters",
            ToUnit = "celsius"
        };

        Assert.Throws<ArgumentException>(
            () => _service.Convert(request));
    }
}
