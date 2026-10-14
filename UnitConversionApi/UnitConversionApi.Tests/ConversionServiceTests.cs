
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

    [Theory]
    [InlineData(1, "kilometers", "meters", 1000)]
    [InlineData(1, "miles", "feet", 5280)]
    [InlineData(1, "kilograms", "grams", 1000)]
    [InlineData(1, "kilograms", "pounds", 2.204622)]
    [InlineData(32, "fahrenheit", "celsius", 0)]
    [InlineData(0, "celsius", "kelvin", 273.15)]
    public void Convert_ValidConversion_ReturnsExpectedResult(
        double value,
        string fromUnit,
        string toUnit,
        double expected)
    {
        var request = new ConversionRequest
        {
            Value = value,
            FromUnit = fromUnit,
            ToUnit = toUnit
        };

        var result = _service.Convert(request);

        Assert.Equal(expected, result.ConvertedValue, 5);
    }

    [Theory]
    [InlineData(-1, "kilograms", "grams")]
    [InlineData(-300, "celsius", "fahrenheit")]
    [InlineData(10, "meters", "celsius")]
    public void Convert_InvalidConversion_ThrowsArgumentException(
        double value,
        string fromUnit,
        string toUnit)
    {
        var request = new ConversionRequest
        {
            Value = value,
            FromUnit = fromUnit,
            ToUnit = toUnit
        };

        Assert.Throws<ArgumentException>(
            () => _service.Convert(request));
    }
}
