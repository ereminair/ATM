namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;

internal abstract record WithdrawalResult
{
    private WithdrawalResult() { }

    internal sealed record Success : WithdrawalResult;

    internal abstract record InsufficientFunds : WithdrawalResult;

    internal sealed record ServiceError : WithdrawalResult;
}