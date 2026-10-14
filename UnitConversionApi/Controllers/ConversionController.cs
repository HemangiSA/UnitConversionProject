
using Microsoft.AspNetCore.Mvc;
using UnitConversionApi.Models;
using UnitConversionApi.Services;

namespace UnitConversionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _conversionService;

    public ConversionController(
        IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpPost]
    public ActionResult<ConversionResponse> Convert(
        [FromBody] ConversionRequest request)
    {
        ConversionResponse response =
            _conversionService.Convert(request);

        return Ok(response);
    }
}
