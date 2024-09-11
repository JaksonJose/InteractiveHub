using Npgsql;
using System.Data;
using whatsapp.Core.Bac;
using whatsapp.Core.Broker;
using whatsapp.Core.Interfaces.IBac;
using whatsapp.Core.Interfaces.IServices;
using whatsapp.Core.Services;
using Whatsapp.Api.Services;
using Whatsapp.Core.Bac;
using Whatsapp.Core.Interfaces.IBac;
using Whatsapp.Core.Interfaces.IRepository;
using Whatsapp.Core.Interfaces.IServices;
using Whatsapp.Core.Models.Send;
using Whatsapp.Core.Services;
using Whatsapp.Core.Utility;
using Whatsapp.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

string connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

string? sendTemplateUrl = configuration["WhatsappUrls:SendTemplate"];
SD.LeadsManagerAPIBase = configuration["ServiceUrls:LeadsManagerApi"];
SD.WhatsappAPIBase = configuration["WhatsappUrls:SendReceiveMessage"];

// Add services to the container.

// Configuration for dapper and postgres.
builder.Services.AddTransient<IDbConnection>(config =>
    new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHttpClient(nameof(SendMessagePayLoad), u => u.BaseAddress = new Uri(sendTemplateUrl!));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ Broker
builder.Services.AddScoped<IMessageBroker, MessageBroker>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IWhatsappService, WhatsappService>();

builder.Services.AddScoped<IWhatsappConfigBac, WhatsappConfigBac>();
builder.Services.AddScoped<IWhatsappConfigRepository, WhatsappConfigRepository>();

builder.Services.AddScoped<IWhatsappSendMessageBac, WhatsappSendMessaBac>();

builder.Services.AddControllers();
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
