using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using BlackFriday;
using NUnit.Framework;
using static NUnit.Framework.Internal.OSPlatform;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_2126
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void Validate_ApplicationReport_NoOrderings_NoPurchases()
    {
        StringBuilder sb = new StringBuilder();

        var controller = CreateObjectInstance(GetType("Controller"));

        var argsArray1 = new object[]
        {
            new object [] { "TimBurton", "tim_bb@email.com", true },
            new object [] { "NonaPet", "nona_Pet@email.com", false },
        };

        foreach (object[] args in argsArray1)
        {
            var tempResult = InvokeMethod(controller, "RegisterUser", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var actualResult = InvokeMethod(controller, "ApplicationReport", null);

        var expectedResult = new StringBuilder();
        expectedResult.AppendLine("Application administration:");
        expectedResult.AppendLine($"TimBurton - Status: Admin, Contact Info: hidden");
        expectedResult.AppendLine($"Clients:");
        expectedResult.AppendLine($"NonaPet - Status: Client, Contact Info: nona_Pet@email.com");

        Assert.That(expectedResult.ToString().TrimEnd(), Is.EqualTo(actualResult));
    }

    private static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        try
        {
            var result = obj.GetType()
                .GetMethod(methodName)
                .Invoke(obj, parameters);

            return result;
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static object CreateObjectInstance(Type type, params object[] parameters)
    {
        try
        {
            var desiredConstructor = type.GetConstructors()
                .FirstOrDefault(x => x.GetParameters().Any());

            if (desiredConstructor == null)
            {
                return Activator.CreateInstance(type, parameters);
            }

            var instances = new List<object>();

            foreach (var parameterInfo in desiredConstructor.GetParameters())
            {
                var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                instances.Add(currentInstance);
            }

            return Activator.CreateInstance(type, instances.ToArray());
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name.Contains(name));

        return type;
    }
}