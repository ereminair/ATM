using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;

internal interface IBankAccountRepository
{
    public IEnumerable<BankAccount> GetAllBankAccounts(string userName);

    public IEnumerable<Operation> GetAllOperations(string userName);

    public BankAccount? GetBankAccount(string userName);

    public BankAccount AddBankAccount(string userName, double balance);

    public void UpdateBankAccount(User user, double balance);
}