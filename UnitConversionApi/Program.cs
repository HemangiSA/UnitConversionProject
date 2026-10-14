
using UnitConversionApi.Services;
using UnitConversionApi.Middleware;
using UnitConversionApi.Converters;

var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers
builder.Services.AddControllers();

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register conversion strategies
builder.Services.AddScoped<IUnitConverter, LengthConverter>();
builder.Services.AddScoped<IUnitConverter, TemperatureConverter>();
builder.Services.AddScoped<IUnitConverter, WeightConverter>();

// Register the main conversion service
builder.Services.AddScoped<IConversionService, ConversionService>();

// Register authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Centralized exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

app.Run();
