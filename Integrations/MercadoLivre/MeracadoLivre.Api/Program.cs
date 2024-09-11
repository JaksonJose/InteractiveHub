using MercadoLivre.Core.Bac;
using MercadoLivre.Core.Broker;
using MercadoLivre.Core.Interface.IBac;
using MercadoLivre.Core.Interface.IRepository;
using MercadoLivre.Core.Interface.IServices;
using MercadoLivre.Core.Services;
using MercadoLivre.Core.Utility;
using MercadoLivre.Data.Repository;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

string defaultConnectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
SD.LeadHubDbConnection = configuration.GetConnectionString("LeadHubDbConnection") ?? string.Empty;

// Add services to the container.
builder.Services.AddTransient<IDbConnection>(config =>
    new NpgsqlConnection(defaultConnectionString));

builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ Broker
builder.Services.AddScoped<IMessageBroker, MessageBroker>();

// Services DI
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Services BAC
builder.Services.AddScoped<IMessageBac, MessageBac>();
builder.Services.AddScoped<IQuestionBac, QuestionBac>();
builder.Services.AddScoped<IMercadoLivreConfigBac, MercadoLivreConfigBac>();

// Services Repository
builder.Services.AddScoped<IMercadoLivreRepository, MercadoLivreConfigRepository>();

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
