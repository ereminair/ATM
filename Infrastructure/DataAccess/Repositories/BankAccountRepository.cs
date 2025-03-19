using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.OperationStatus;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

internal class BankAccountRepository : IBankAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public BankAccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<BankAccount> GetAllBankAccounts(string userName)
    {
        const string sql = """
                           select user_name, balance
                           from bank_account
                           where user_name = @userName;
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userName", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        var bankAccounts = new List<BankAccount>();

        while (reader.Read())
        {
            bankAccounts.Add(new BankAccount(
                userName,
                reader.GetDouble(1)));
        }

        return bankAccounts;
    }

    public IEnumerable<Operation> GetAllOperations(string userName)
    {
        const string sql = """
                           select id, operaton_type, amount
                           from operation
                           where user_name = @userName;
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userName", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        var operation = new List<Operation>();

        while (reader.ReadAsync() != null)
        {
            OperationType type = reader.GetString(1) switch
            {
                "Deposit" => OperationType.Deposit,
                "Withdrawal" => OperationType.Withdrawal,
                _ => throw new ArgumentOutOfRangeException(nameof(userName)),
            };

            operation.Add(new Operation(
                reader.GetInt16(0),
                type,
                reader.GetDouble(1)));
        }

        return operation;
    }

    public BankAccount? GetBankAccount(string userName)
    {
        const string sql = """
                               SELECT 
                                   user_name, 
                                   balance 
                               FROM bank_account 
                               WHERE user_name = @user_name;
                           """;

        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("user_name", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new BankAccount(
            reader.GetString(reader.GetOrdinal("user_name")),
            reader.GetDouble(reader.GetOrdinal("balance")));
    }

    public BankAccount AddBankAccount(string userName, double balance)
    {
        const string sql = """
                           insert into bank_account
                           values(:user_name, 0)
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_name", userName)
            .AddParameter("balance", balance);

        command.ExecuteNonQuery();

        return new BankAccount(userName, balance);
    }

    public void DeleteBankAccount(User user)
    {
        const string sql = """
                           delete from bank_account
                           where customer = customer
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("customer", user);

        command.ExecuteNonQuery();
    }

    public void UpdateBankAccount(User user, double balance)
    {
        const string sql = """
                           update bank_account
                           set balance = :balance
                           where user_name = user_name
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_name", user.UserName)
            .AddParameter("balance", balance);

        command.ExecuteNonQuery();
    }
}