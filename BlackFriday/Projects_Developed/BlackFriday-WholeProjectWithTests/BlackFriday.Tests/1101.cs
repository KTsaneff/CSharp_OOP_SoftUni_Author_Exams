using System;
using System.Linq;
using System.Reflection;
using BlackFriday;
using NUnit.Framework;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

[TestFixture]
public class Tests_1101
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateIProductAndDerivedExist()
    {
        var typesToAssert = new[]
        {
            "IProduct",
            "Product",
            "Item",
            "Service"
        };

        foreach (var typeName in typesToAssert)
        {
            Assert.That(GetType(typeName), Is.Not.Null, $"{typeName} type doesn't exist!");
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == name);

        return type;
    }
}