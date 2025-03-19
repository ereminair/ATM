using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.App.BankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.App.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IOperationRepository, OperationRepository>();

        collection.AddScoped<CurrentUserManager>();
        collection.AddScoped<ICurrentUserManager>(
            p => p.GetRequiredService<CurrentUserManager>());
        collection.AddScoped<CurrentBankAccount>();
        collection.AddScoped<ICurrentBankAccount>(
            p => p.GetRequiredService<CurrentBankAccount>());

        return collection;
    }
}