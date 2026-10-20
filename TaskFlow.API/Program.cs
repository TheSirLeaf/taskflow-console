using System.Text.Json.Serialization;
using TaskFlow.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Persistência em arquivo JSON (sem dependência de banco de dados).
var dataFile = builder.Configuration["TaskFlow:DataFile"] ?? "tarefas.json";
builder.Services.AddSingleton(new TarefaRepository(dataFile));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
