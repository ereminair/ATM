using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;

internal interface IUserRepository
{
    public User? FindUserByUserName(string userName);

    public void AddUser(string userName, string status, string password);

    public string? CheckAdminPassword(string userName);
}