using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Deposit;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Login;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Logout;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Registrations;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowAllBankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowAllOperation;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowBalance;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Withdrawal;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, RegistrationScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawalScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AllOperationScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AllBankAccountsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();

        return collection;
    }
}