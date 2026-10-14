using System.ComponentModel.DataAnnotations;

namespace UnitConversionApi.Models;

public class ConversionRequest
{
    [Required]
    public double? Value { get; set; }

    [Required]
    public string? FromUnit { get; set; }

    [Required]
    public string? ToUnit { get; set; }
}