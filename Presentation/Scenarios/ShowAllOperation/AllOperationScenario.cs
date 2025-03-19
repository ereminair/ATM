using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.ShowAllOperation;

internal class AllOperationScenario : IScenario
{
    private readonly IUserService _userService;

    public AllOperationScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Show All Operation";

    public void Run()
    {
        IEnumerable<Operation> operations = _userService.GetAllOperation();

        IEnumerable<Operation> enumerable = operations.ToList();

        if (!enumerable.Any())
        {
            AnsiConsole.WriteLine("There are no operations available.");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        Table table = new Table()
            .AddColumn("ID")
            .AddColumn("Type")
            .AddColumn("Amount");

        foreach (Operation operation in enumerable)
        {
            table.AddRow(operation.Id.ToString(), operation.Type.ToString(), operation.Amount.ToString(string.Empty));
        }

        AnsiConsole.Write(table);

        AnsiConsole.Console.Input.ReadKey(false);
    }
}