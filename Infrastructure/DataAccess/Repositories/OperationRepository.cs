using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.OperationStatus;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

internal class OperationRepository : IOperationRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public OperationRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<Operation> GetAllOperation(string userName)
    {
        const string sql = """
                           select id, operation_type, amount
                           from operation
                           where user_name = @userName;
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userName", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        var operation = new List<Operation>();

        while (reader.Read())
        {
            OperationType type = reader.GetString(1) switch
            {
                "Deposit" => OperationType.Deposit,
                "Withdrawal" => OperationType.Withdrawal,
                _ => throw new ArgumentOutOfRangeException(nameof(userName)),
            };

            operation.Add(new Operation(
                reader.GetInt32(0),
                type,
                reader.GetDouble(2)));
        }

        return operation;
    }

    public void AddOperation(string userName, string operationType, double amount)
    {
        OperationType type = operationType switch
        {
            "Deposit" => OperationType.Deposit,
            "Withdrawal" => OperationType.Withdrawal,
            _ => throw new ArgumentOutOfRangeException(nameof(userName)),
        };
        const string sql = """
                           insert into operation(operation_type, amount, user_name)
                           values(:operation_type, :amount, :user_name)
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_name", userName)
            .AddParameter("operation_type", type.ToString())
            .AddParameter("amount", amount);

        command.ExecuteNonQuery();
    }
}