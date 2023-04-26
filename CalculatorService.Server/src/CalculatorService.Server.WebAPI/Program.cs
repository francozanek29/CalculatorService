using CalculatorService.Server.WebAPI.FilterAttributes;
using CalculatorService.Server.Bootstrapper;
using CalculatorService.Server.WebAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
  options.Filters.Add<LoadRequestContextActionFilterAttribute>();
  options.Filters.Add<ValidationFilterAttribute>();
  options.Filters.Add<HandledExceptionFilterAttribute>();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureFrameworkInfrastructure(typeof(ControllerMapperProfile).Assembly);
builder.WebHost.UseUrls("https://localhost:7241/");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }