namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;

internal record BankAccount
{
    public string UserName { get; init; }

    public double Balance { get; set; }

    public BankAccount(string userName, double balance)
    {
        UserName = userName;
        Balance = balance;
    }
}
