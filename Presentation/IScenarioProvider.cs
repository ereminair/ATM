using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

internal interface IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}