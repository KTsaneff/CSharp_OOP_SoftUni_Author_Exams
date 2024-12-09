﻿using System;
using System.Linq;
using System.Reflection;
using FootballManager;
using NUnit.Framework;
using FootballManager;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

[TestFixture]
public class Tests_103
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateIRepositoryAndDerivedExist()
    {
        var typesToAssert = new[]
        {
            "IRepository`1",
            "TeamRepository"
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