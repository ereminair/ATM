using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;

internal interface IUserService
{
    public LoginResult Login(string userName, string password);

    public WithdrawalResult Withdrawal(double amount);

    public DepositResult Deposit(double amount);

    public LogoutResult Logout();

    public IEnumerable<Operation> GetAllOperation();

    public IEnumerable<BankAccount> GetAllBankAccounts();

    public double GetBalance();

    public RegistrationResult Registration(string userName, string password, string status);
}