using Cobalt.CosmosConnector.Connection;
using Cobalt.CosmosConnector.Repository;
using ExampleImplementation.Models;
using ExampleImplementation.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICosmosConnector, CosmosConnector>();
builder.Services.AddTransient<ICosmosRepository<ExampleModel>, CosmosRepository<ExampleModel>>();
builder.Services.AddTransient<IExampleService, ExampleService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
