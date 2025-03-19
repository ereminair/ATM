using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Login;

internal class LoginScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Login";

    public void Run()
    {
        string userName = AnsiConsole.Ask<string>("UserName: ");

        string password = AnsiConsole.Ask<string>("Password: ");

        LoginResult result = _userService.Login(userName, password);

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.InvalidLogin => "Invalid login",
            LoginResult.InvalidPassword => "Invalid Password",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Console.Input.ReadKey(false);
    }
}