using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlackFriday;
using BlackFriday.Models.Contracts;
using NUnit.Framework;

[TestFixture]
public class Tests_1112
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateProductRepositoryMethods()
    {
        var type = GetType("ProductRepository");

        var methods = new[]
        {
            new Method(typeof(void), "AddNew", typeof(IProduct)),
            new Method(typeof(IProduct), "GetByName", typeof(string)),
            new Method(typeof(bool), "Exists", typeof(string)),
        };

        ValidateMethods(type, methods);
    }
    private class Method
    {
        public Method(Type returnType, string name, params Type[] parameterTypes)
        {
            this.ReturnType = returnType;
            this.Name = name;
            this.ParameterTypes = parameterTypes;
        }

        public Type ReturnType { get; }

        public string Name { get; }

        public Type[] ParameterTypes { get; }
    }
    private static void ValidateMethods(Type type, IEnumerable<Method> methods)
    {
        foreach (var expectedMethod in methods)
        {
            var expectedReturnType = expectedMethod.ReturnType;
            var expectedMethodName = expectedMethod.Name;
            var expectedParameters = expectedMethod.ParameterTypes;

            var actualMethod = type.GetMethod(expectedMethodName);
            Assert.That(actualMethod, Is.Not.Null, $"{type}.{expectedMethodName}() does not exist!");

            var actualReturnType = actualMethod.ReturnType;
            Assert.That(actualReturnType, Is.EqualTo(expectedReturnType),
                $"{type}.{expectedMethodName}() has the wrong return type!");

            var actualParameters = actualMethod.GetParameters();
            for (var i = 0; i < expectedParameters.Length; i++)
            {
                var expectedParameter = expectedParameters[i];
                var actualParameter = actualParameters[i];

                var expectedParameterType = expectedParameter;
                var actualParameterType = actualParameter.ParameterType;

                Assert.That(actualParameterType, Is.EqualTo(expectedParameterType),
                    $"{type}.{expectedMethodName}() parameter {i + 1} has incorrect type!");
            }
        }
    }

    private static Type GetCollectionType(Type modelType)
    {
        var collectionType = typeof(IReadOnlyCollection<>).MakeGenericType(modelType);
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