using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.App.BankAccounts;

internal class CurrentBankAccount : ICurrentBankAccount
{
    public BankAccount? BankAccount { get; set; }
}