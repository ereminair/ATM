using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;

internal interface ICurrentUserManager
{
    public User? User { get; }
}