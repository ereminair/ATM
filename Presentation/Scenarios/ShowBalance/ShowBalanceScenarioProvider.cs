using Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowBalance;

internal class ShowBalanceScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserManager _currentUserManager;

    public ShowBalanceScenarioProvider(IUserService userService, ICurrentUserManager currentUserManager)
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

        scenario = new ShowBalanceScenario(_userService);
        return true;
    }
}