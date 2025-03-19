using Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowAllBankAccounts;

internal class AllBankAccountsScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserManager _currentUserManager;

    public AllBankAccountsScenarioProvider(IUserService userService, ICurrentUserManager currentUserManager)
    {
        _userService = userService;
        _currentUserManager = currentUserManager;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserManager.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new AllBankAccountsScenario(_userService);
        return true;
    }
}