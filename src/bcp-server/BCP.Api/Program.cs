using BCP.Infrastructure;
using BCP.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Serilog.Log.Logger = BCP.Application.Serilog.SerilogConfiguration.CreateSerilogLogger();

builder.Host.UseSerilog(Serilog.Log.Logger);

builder.Services.AddControllers();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
