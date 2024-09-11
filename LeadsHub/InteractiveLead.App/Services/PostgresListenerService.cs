
using Npgsql;
using System.Text.Json;
using InteractiveLead.Core.Models;
using InteractiveLead.Core.NotifyServices;

namespace InteractiveLead.App.Services
{
    /// <summary>
    /// Listen/notify resource of postgresql
    /// It will be fired when a event heppens in sepecific entity
    /// </summary>
    public class PostgresListenerService : BackgroundService
    {
        private readonly NpgsqlConnection _connection;
        
        private readonly MessageNotifyService _messageNotify = new();

        public PostgresListenerService(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _connection.OpenAsync(stoppingToken);

            _connection.Notification += (o, e) =>
            {
                Lead? lead = JsonSerializer.Deserialize<Lead>(e.Payload);

                if (lead != null)
                {
                    _messageNotify.NotifyNewLeadReceived(lead);
                }
            };

            using (var cmd = new NpgsqlCommand("LISTEN lead_channel", _connection))
            {
                await cmd.ExecuteNonQueryAsync(stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await _connection.WaitAsync(stoppingToken);
            }
        }

        public override void Dispose()
        {
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
