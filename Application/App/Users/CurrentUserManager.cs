using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.App.Users;

internal class CurrentUserManager : ICurrentUserManager
{
    public User? User { get; set; }
}