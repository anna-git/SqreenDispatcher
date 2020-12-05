using Microsoft.Data.Sqlite;
using SqreenDispatcher.Services.Model;
using System;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Targets
{
    public class DatabaseTarget : ITarget
    {
        private readonly DbOptions _options;

        public DatabaseTarget(DbOptions options) => _options = options;
        public async void Notify(SqreenMessage[] messages)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = _options.ConnectionString
            };
            SQLitePCL.Batteries.Init();
            using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            foreach (var message in messages)
            {
                var insertCmd = connection.CreateCommand();

                insertCmd.CommandText = "INSERT INTO Alerts (MessageId, EventUrl, MessageType) VALUES(@MessageId, @EventUrl, @MessageType)";
                insertCmd.Parameters.Add(new SqliteParameter("@MessageId", message.Id??string.Empty));
                insertCmd.Parameters.Add(new SqliteParameter("@MessageType", message.Type ?? string.Empty));
                insertCmd.Parameters.Add(new SqliteParameter("@EventUrl", message.Message.EventUrl ?? string.Empty));
                await insertCmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
    }
}
