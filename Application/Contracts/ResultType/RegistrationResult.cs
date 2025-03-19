namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.ResultType;

internal abstract record RegistrationResult
{
    private RegistrationResult() { }

    internal record Success : RegistrationResult;

    internal record AlreadyExists : RegistrationResult;

    internal record Fail : RegistrationResult;
}