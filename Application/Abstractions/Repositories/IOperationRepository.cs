using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstractions.Repositories;

internal interface IOperationRepository
{
    public IEnumerable<Operation> GetAllOperation(string userName);

    public void AddOperation(string userName, string operationType, double amount);
}