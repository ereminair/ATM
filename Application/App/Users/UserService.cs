using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab5.Application.App.BankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.BankAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.StateStatus;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly CurrentUserManager _currentUserManager;
    private readonly IOperationRepository _operationRepository;
    private readonly CurrentBankAccount _currentBankAccount;
    private readonly IBankAccountRepository _bankAccountRepository;

    public UserService(
        IUserRepository userRepository,
        CurrentUserManager currentUserManager,
        IOperationRepository operationRepository,
        CurrentBankAccount currentBankAccount,
        IBankAccountRepository bankAccountRepository)
    {
        _userRepository = userRepository;
        _currentUserManager = currentUserManager;
        _operationRepository = operationRepository;
        _currentBankAccount = currentBankAccount;
        _bankAccountRepository = bankAccountRepository;
    }

    public LoginResult Login(string userName, string password)
    {
        User? user = _userRepository.FindUserByUserName(userName);
        if (user is null)
        {
            return new LoginResult.InvalidLogin();
        }

        if (user.Status is UserStatus.Admin && user.Password != _userRepository.CheckAdminPassword(userName))
        {
            return new LoginResult.InvalidPassword();
        }

        if (user.Password != password)
        {
            return new LoginResult.InvalidPassword();
        }

        _currentUserManager.User = user;
        _currentBankAccount.BankAccount = _bankAccountRepository.GetBankAccount(userName);
        return new LoginResult.Success();
    }

    public LogoutResult Logout()
    {
        if (_currentUserManager.User is null)
        {
            return new LogoutResult.Fail();
        }

        return new LogoutResult.Success();
    }

    public WithdrawalResult Withdrawal(double amount)
    {
        if (_currentUserManager.User is null || amount <= 0 || _currentBankAccount.BankAccount is null
            || amount > _currentBankAccount.BankAccount.Balance)
        {
            return new WithdrawalResult.ServiceError();
        }

        double newBalance = _currentBankAccount.BankAccount.Balance - amount;

        _bankAccountRepository.UpdateBankAccount(_currentUserManager.User, newBalance);
        _operationRepository.AddOperation(_currentUserManager.User.UserName, "Withdrawal", amount);
        _currentBankAccount.BankAccount.Balance = newBalance;

        return new WithdrawalResult.Success();
    }

    public DepositResult Deposit(double amount)
    {
        if (_currentUserManager.User is null || amount <= 0 || _currentBankAccount.BankAccount is null)
        {
            return new DepositResult.ServiceError();
        }

        double newBalance = _currentBankAccount.BankAccount.Balance + amount;

        _bankAccountRepository.UpdateBankAccount(_currentUserManager.User, newBalance);
        _operationRepository.AddOperation(_currentUserManager.User.UserName, "Deposit", amount);
        _currentBankAccount.BankAccount.Balance = newBalance;

        return new DepositResult.Success();
    }

    public IEnumerable<Operation> GetAllOperation()
    {
        if (_currentUserManager.User is null)
        {
            throw new InvalidOperationException("User not found");
        }

        return _operationRepository.GetAllOperation(_currentUserManager.User.UserName);
    }

    public IEnumerable<BankAccount> GetAllBankAccounts()
    {
        if (_currentUserManager.User is null)
        {
            throw new InvalidOperationException("User not found");
        }

        return _bankAccountRepository.GetAllBankAccounts(_currentUserManager.User.UserName);
    }

    public double GetBalance()
    {
        if (_currentBankAccount.BankAccount is null)
        {
            throw new InvalidOperationException("BankAccount not found");
        }

        return _currentBankAccount.BankAccount.Balance;
    }

    public RegistrationResult Registration(string userName, string password, string status)
    {
        User? user = _userRepository.FindUserByUserName(userName);

        if (user is not null)
        {
            return new RegistrationResult.AlreadyExists();
        }

        if (status == "admin" && password != _userRepository.CheckAdminPassword(userName))
        {
            return new RegistrationResult.Fail();
        }

        _userRepository.AddUser(userName, status, password);
        _bankAccountRepository.AddBankAccount(userName, 0);

        return new RegistrationResult.Success();
    }
}