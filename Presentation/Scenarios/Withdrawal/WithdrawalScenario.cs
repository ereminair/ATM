using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Withdrawal;

internal class WithdrawalScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawalScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Withdrawal";

    public void Run()
    {
        double amount = AnsiConsole.Ask<double>("Amount: ");

        WithdrawalResult result = _userService.Withdrawal(amount);

        string message = result switch
        {
            WithdrawalResult.Success => "Successful Withdrawal",
            WithdrawalResult.InsufficientFunds => "Insufficient Funds",
            WithdrawalResult.ServiceError => "Service Error",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Console.Input.ReadKey(false);
    }
}