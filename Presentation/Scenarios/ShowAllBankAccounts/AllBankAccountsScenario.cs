using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowAllBankAccounts;

internal class AllBankAccountsScenario : IScenario
{
    private readonly IUserService _userService;

    public AllBankAccountsScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Show All Bank Accounts";

    public void Run()
    {
        IEnumerable<BankAccount> bankAccounts = _userService.GetAllBankAccounts();

        SelectionPrompt<BankAccount> selector = new SelectionPrompt<BankAccount>()
            .Title("Select bankAccount")
            .AddChoices(bankAccounts)
            .UseConverter(x => x.UserName);

        BankAccount bankAccount = AnsiConsole.Prompt(selector);

        AnsiConsole.WriteLine($"You selected {bankAccount.Balance}");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}