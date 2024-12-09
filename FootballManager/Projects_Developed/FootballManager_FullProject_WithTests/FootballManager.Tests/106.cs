using System;
using System.Linq;
using System.Reflection;
using FootballManager;
using NUnit.Framework;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

[TestFixture]
public class Tests_106
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateBaseClassesAreAbstract()
    {
        var typesToAssert = new[]
        {
            "Manager"
        };

        foreach (var typeName in typesToAssert)
        {
            Assert.That(IsAbstract(typeName));
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == name);

        return type;
    }

    private static bool IsAbstract(string name)
    {
        var type = GetType(name);
        return type != null && type.IsAbstract;
    }
}