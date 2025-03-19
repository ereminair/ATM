using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.OperationStatus;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

internal record Operation(int Id, OperationType Type, double Amount);
