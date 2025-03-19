namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;

internal abstract record DepositResult
{
    private DepositResult() { }

    internal sealed record Success : DepositResult;

    internal abstract record InvalidAmount : DepositResult;

    internal sealed record ServiceError : DepositResult;
}