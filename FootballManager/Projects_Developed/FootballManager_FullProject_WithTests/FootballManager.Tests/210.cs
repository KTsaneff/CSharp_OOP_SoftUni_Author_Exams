using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FootballManager;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_210
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void Validate_MatchBetween_TeamHasAlreadySignedWithAnotherManager()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var methodArgs = new object[] { "Liverpool" };
        InvokeMethod(controller, "JoinChampionship", methodArgs);
        var methodArgs2 = new object[] { "Liverpool", "ProfessionalManager", "Jurgen Klopp" };
        InvokeMethod(controller, "SignManager", methodArgs2);

        var methodArgs3 = new object[] { "Manchester City" };
        InvokeMethod(controller, "JoinChampionship", methodArgs3);
        var methodArgs4 = new object[] { "Manchester City", "Professional Manager", "Pep Guardiola" };
        InvokeMethod(controller, "SignManager", methodArgs4);

        var actualResult = InvokeMethod(controller, "MatchBetween", new object[] { "Liverpool", "Manchester United" });

        var expectedResult = "This match does not meet the regulation rules of the Championship.";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
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