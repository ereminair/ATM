namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;

internal abstract record LogoutResult
{
    private LogoutResult() { }

    internal record Success : LogoutResult;

    internal record Fail : LogoutResult;
}