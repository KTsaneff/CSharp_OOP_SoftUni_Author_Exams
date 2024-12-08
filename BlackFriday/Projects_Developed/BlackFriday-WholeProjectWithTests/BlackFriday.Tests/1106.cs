using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlackFriday;
using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;
using NUnit.Framework;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

[TestFixture]
public class Tests_1106
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;


    [Test]
    public void ValidateUserProperties()
    {
        var type = GetType("User");

        var properties = new[]
        {
            new Property(typeof(string), "UserName", new string[]{ "Private"}),
            new Property(typeof(bool), "HasDataAccess", new string[]{ "Private", "Family"}),
            new Property(typeof(string), "Email", new string[] { "Private" })
        };

        ValidateProperties(type, properties);
    }
    private class Property
    {
        public Property(Type type, string name, string[] modifiers)
        {
            this.Type = type;
            this.Name = name;
            this.Modifiers = modifiers;
        }

        public Type Type { get; }

        public string Name { get; }

        public IEnumerable<string> Modifiers { get; }
    }

    private static void ValidateProperties(Type type, IEnumerable<Property> properties)
    {
        foreach (var expectedProperty in properties)
        {
            var expectedType = expectedProperty.Type;
            var propertyName = expectedProperty.Name;
            var validModifiers = expectedProperty.Modifiers;

            var actualProperty = type.GetProperty(propertyName);
            Assert.That(actualProperty, Is.Not.Null, $"{type}.{propertyName} does not exist!");

            var propertySetResult = GetAccessModifier(actualProperty);
            Assert.That(validModifiers.Contains(propertySetResult), $"Set method doesn't have correct access modifier!");

            var actualType = actualProperty.PropertyType;

            

            Assert.That(actualType, Is.EqualTo(expectedType), $"{type}.{propertyName} has the wrong type!");
        }
    }

    private static string GetAccessModifier(PropertyInfo actualProperty)
    {
        if (actualProperty.SetMethod == null)
            return null;

        if (actualProperty.SetMethod.IsPrivate)
            return "Private";

        if (actualProperty.SetMethod.IsPublic)
            return "Public";

        if (actualProperty.SetMethod.IsFamily)
            return "Family";

        return null;
    }

    private static Type GetCollectionType(Type modelType)
    {
        var collectionType = typeof(ICollection<>).MakeGenericType(modelType);
        return collectionType;
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == name);

        return type;
    }
}