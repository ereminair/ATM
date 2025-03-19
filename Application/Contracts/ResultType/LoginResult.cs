namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;

internal abstract record LoginResult
{
    private LoginResult() { }

    internal sealed record Success : LoginResult;

    internal sealed record InvalidPassword : LoginResult;

    internal sealed record InvalidLogin : LoginResult;
}