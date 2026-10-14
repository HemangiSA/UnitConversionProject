
# Unit Conversion API

A RESTful API built with ASP.NET Core and C# to convert between length, temperature, and weight units.

## Features

- Length conversions: meters, feet, kilometers, miles
- Temperature conversions: Celsius, Fahrenheit, Kelvin
- Weight conversions: kilograms, grams, pounds
- Strategy Pattern using `IUnitConverter`
- Dependency injection for conversion strategies
- Centralized exception-handling middleware
- Swagger/OpenAPI documentation
- Automated unit tests using xUnit

## Technologies

- C#
- ASP.NET Core
- .NET 10
- REST API
- Swagger / OpenAPI
- xUnit

## API Endpoint

`POST /api/Conversion`

### Request example

```json
{
  "value": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit"
}
```

### Successful response

```json
{
  "value": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit",
  "convertedValue": 212
}
```

## Run the application

```bash
dotnet restore
dotnet build
dotnet run
```

Open `/swagger` using the application URL to explore and test the endpoint.

## Run automated tests

```bash
dotnet test UnitConversionApi.Tests/UnitConversionApi.Tests.csproj
```

## Design

- **Controller:** Handles HTTP requests and responses.
- **Conversion service:** Selects the appropriate conversion strategy.
- **Converters:** Implement length, temperature, and weight conversion logic.
- **Exception middleware:** Handles application errors centrally.
- **Dependency injection:** Registers the conversion service and converters.

## Validation and error handling

Invalid or unsupported conversions return HTTP 400 Bad Request. Unexpected application errors return HTTP 500 Internal Server Error.

## Future enhancements

- Add volume, speed, and area conversions.
- Add more unit tests and integration tests.
- Improve validation and API documentation.
