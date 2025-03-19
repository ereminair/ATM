using Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Registrations;

internal class RegistrationScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserManager _currentUserManager;

    public RegistrationScenarioProvider(IUserService userService, ICurrentUserManager currentUserManager)
    {
        _userService = userService;
        _currentUserManager = currentUserManager;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserManager.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new RegistrationScenario(_userService);
        return true;
    }
}