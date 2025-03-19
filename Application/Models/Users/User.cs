using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.StateStatus;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

internal record User(int Id, string UserName, UserStatus Status, string Password);