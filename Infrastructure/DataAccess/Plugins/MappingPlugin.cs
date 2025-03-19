using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.OperationStatus;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.StateStatus;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Plugins;

internal class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<UserStatus>();
        builder.MapEnum<OperationType>();
    }
}