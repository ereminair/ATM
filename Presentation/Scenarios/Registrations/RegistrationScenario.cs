using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Registrations;

internal class RegistrationScenario : IScenario
{
    private readonly IUserService _userService;

    public RegistrationScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Registration";

    public void Run()
    {
        string userName = AnsiConsole.Ask<string>("UserName: ");

        string password = AnsiConsole.Ask<string>("Password: ");

        string status = AnsiConsole.Ask<string>("Status: ");

        RegistrationResult result = _userService.Registration(userName, password, status);

        string message = result switch
        {
            RegistrationResult.Success => "Success registration",
            RegistrationResult.Fail => "Fail registration",
            RegistrationResult.AlreadyExists => "User already exists",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Console.Input.ReadKey(false);
    }
}