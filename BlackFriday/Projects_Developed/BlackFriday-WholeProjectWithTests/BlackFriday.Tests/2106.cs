using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlackFriday;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_2106
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void Validate_RegisterUser_EmailAlreadyUserByAnotherUser()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var methodArgs1 = new object[] { "TimBurton", "tim_bb@email.com", false };
        InvokeMethod(controller, "RegisterUser", methodArgs1);

        var methodArgs2 = new object[] { "RegSec", "tim_bb@email.com", false };
         var actualResult = InvokeMethod(controller, "RegisterUser", methodArgs2);

        var expectedresult = $"tim_bb@email.com is already used by another user.";
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