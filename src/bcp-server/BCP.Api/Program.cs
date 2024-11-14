using BCP.Infrastructure;
using BCP.Application;
using Serilog;
using BCP.Api;
using ZymLabs.NSwag.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Serilog.Log.Logger = BCP.Application.Serilog.SerilogConfiguration.CreateSerilogLogger();

builder.Host.UseSerilog(Serilog.Log.Logger);

builder.Services.AddControllers();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAPIServices(builder.Configuration);

builder.Services.AddScoped<FluentValidationSchemaProcessor>(provider =>
{
	var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
	var loggerFactory = provider.GetService<ILoggerFactory>();

	return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseOpenApi();
	//app.UseSwaggerUi3();

	app.UseSwaggerUi(settings =>
	{
		//settings.Path = "/api";
		//settings.DocumentPath = "/api/specification.json";
	});

}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
