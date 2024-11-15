using BCP.Infrastructure;
using BCP.Application;
using Serilog;
using BCP.Api;
using ZymLabs.NSwag.FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(opt =>
	{
		var tokenKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentNullException("Token key not found");
		opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
		{
			ValidateIssuer = false,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
			ValidateIssuerSigningKey = true,
			ValidateAudience = false,
		};
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
