using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Logout;

internal class LogoutScenario : IScenario
{
    private readonly IUserService _userService;

    public LogoutScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Logout";

    public void Run()
    {
        LogoutResult result = _userService.Logout();

        string message = result switch
        {
            LogoutResult.Success => "Successful logout",
            LogoutResult.Fail => "Failed logout",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Console.Input.ReadKey(false);
    }
}