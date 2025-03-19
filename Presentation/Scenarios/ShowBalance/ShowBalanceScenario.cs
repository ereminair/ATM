using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowBalance;

internal class ShowBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public ShowBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Get Balance";

    public void Run()
    {
        double balance = _userService.GetBalance();

        AnsiConsole.WriteLine($"You selected {balance}");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}