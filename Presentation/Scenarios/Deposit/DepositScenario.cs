using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Deposit;

internal class DepositScenario : IScenario
{
    private readonly IUserService _userService;

    public DepositScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Deposit";

    public void Run()
    {
        double amount = AnsiConsole.Ask<double>("Amount: ");

        DepositResult result = _userService.Deposit(amount);

        string message = result switch
        {
            DepositResult.Success => "Successful deposit",
            DepositResult.InvalidAmount => "Invalid amount",
            DepositResult.ServiceError => "Service Error",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Console.Input.ReadKey(false);
    }
}