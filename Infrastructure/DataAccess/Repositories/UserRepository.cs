using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.StateStatus;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FindUserByUserName(string userName)
    {
        const string sql = """
                           select user_id, user_name, user_role, password
                           from users
                           where user_name = :userName;
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userName", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        UserStatus user_role = reader.GetString(2) switch
        {
            "customer" => UserStatus.Customer,
            "admin" => UserStatus.Admin,
            _ => throw new ArgumentOutOfRangeException(nameof(userName)),
        };
        return new User(
            reader.GetInt16(0),
            reader.GetString(1),
            user_role,
            reader.GetString(3));
    }

    public void UpdateUser(int id, string password, string status)
    {
        UserStatus user_role = status switch
        {
            "customer" => UserStatus.Customer,
            "admin" => UserStatus.Admin,
            _ => throw new ArgumentOutOfRangeException(nameof(id)),
        };

        const string sql = """
                           update users
                           set password = :password
                           where user_id = :id
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", id)
            .AddParameter("password", password)
            .AddParameter("user_role", user_role);

        command.ExecuteNonQuery();
    }

    public void DeleteUser(int id)
    {
        const string sql = """
                           delete from users
                           where user_id = :id
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", id);

        command.ExecuteNonQuery();
    }

    public void AddUser(string userName, string status, string password)
    {
        string userRole = status switch
        {
            "customer" => "customer",
            "admin" => "admin",
            _ => throw new ArgumentOutOfRangeException(nameof(userName)),
        };

        const string sql = """
                           insert into users (user_name, user_role, password)
                           values(:user_name, :user_role, :password)
                           """;

        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_name", userName)
            .AddParameter("password", password)
            .AddParameter("user_role", userRole);

        command.ExecuteNonQuery();
    }

    public string? CheckAdminPassword(string userName)
    {
        const string sql = """
                           select password
                           from users
                           where user_name = @userName
                           """;
        using NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userName", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return reader.GetString(0);
    }
}