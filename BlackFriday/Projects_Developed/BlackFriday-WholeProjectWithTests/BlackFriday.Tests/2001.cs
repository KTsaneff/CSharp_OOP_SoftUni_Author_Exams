using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using BlackFriday;
using BlackFriday.Models;
using NUnit.Framework;
using static NUnit.Framework.Internal.OSPlatform;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_2001
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void Validate_ApplicationReport_OrderingsRequired()
    {
        StringBuilder sb = new StringBuilder();

        var controller = CreateObjectInstance(GetType("Controller"));

        var argsArray1 = new object[]
        {
            new object [] { "CommonUser", "user@applicationmail.bg", false },
            new object [] { "CommonUser", "admin@applicationmail.bg", false },
            new object [] { "AdminUser1", "admin_one@applicationmail.bg", true },
            new object [] { "OtherCommonUser", "user@applicationmail.bg", false },
            new object [] { "AdminUser2", "admin_two@applicationmail.bg", true },
            new object [] { "AdminUser3", "admin_three@applicationmail.bg", true },
            new object [] { "john_doe", "john.doe@gmail.com", false },
            new object [] { "MikeRoss", "mike.ross@hotmail.com", false },
            new object [] { "janeSVG", "jennifer_savage@mail.de", false },
        };

        foreach (object[] args in argsArray1)
        {
            var tempResult = InvokeMethod(controller, "RegisterUser", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var argsArray2 = new object[]
        {
            new object [] { "Item", "Laptop", "AdminUser1", 1200.00 },
            new object [] { "Service", "Warranty", "CommonUser", 200.00 },
            new object [] { "Item", "Laptop", "AdminUser1", 1500.00 },
            new object [] { "Furniture", "Desk", "AdminUser2", 350.00 },
            new object [] { "Item", "Phone", "UnknownUser", 900.00 },
            new object [] { "Service", "SoftwareInstallation", "AdminUser2", 100.00 },
            new object [] { "Item", "Monitor", "AdminUser1", 300.00 },
            new object [] { "Item", "GamingMouse", "AdminUser2", 80.00 },
            new object [] { "Item", "ExternalHardDrive", "AdminUser1", 120.00 },
            new object [] { "Service", "AntivirusSubscrition", "AdminUser2", 50.00 },
            new object [] { "Service", "CloudStorage", "AdminUser1", 10.00 },
            new object [] { "Service", "DeviceSetup", "AdminUser2", 40.00 },
        };

        foreach (object[] args in argsArray2)
        {
            var tempResult = InvokeMethod(controller, "AddProduct", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var argsArray3 = new object[]
        {
            new object [] { "Laptop", "AdminUser1", 1300.00 },
            new object [] { "Tablet", "AdminUser1", 500.00 },
            new object [] { "Laptop", "john_doe", 1250.00 },
            new object [] { "Monitor", "UnknownUser", 350.00 },
            new object [] { "CloudStorage", "AdminUser2", 15.00 },
        };

        foreach (object[] args in argsArray3)
        {
            var tempResult = InvokeMethod(controller, "UpdateProductPrice", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var argsArray4 = new object[]
        {
            new object [] { "john_doe", "Laptop", true },
            new object [] { "MikeRoss", "Monitor", false },
            new object [] { "AdminUser1", "GamingMouse", false },
            new object [] { "janeSVG", "SmartWatch", true },
            new object [] { "john_doe", "Laptop", false },
            new object [] { "UnknownUser", "DeviceSetup", true },
            new object [] { "MikeRoss", "CloudStorage", true },
            new object [] { "john_doe", "GamingMouse", true },
            new object [] { "MikeRoss", "CloudStorage", true },
            new object [] { "john_doe", "SoftwareInstallation", true },
            new object [] { "MikeRoss", "AntivirusSubscrition", true },
            new object [] { "john_doe", "ExternalHardDrive", true },
        };

        foreach (object[] args in argsArray4)
        {
            var tempResult = InvokeMethod(controller, "PurchaseProduct", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var argsArray5 = new object[]
        {
            new object [] { "janeSVG" },
            new object [] { "AdminUser1" },
        };

        foreach (object[] args in argsArray5)
        {
            var tempResult = InvokeMethod(controller, "RefreshSalesList", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var argsArray6 = new object[]
        {
            new object [] { "janeSVG", "CloudStorage", true },
        };

        foreach (object[] args in argsArray6)
        {
            var tempResult = InvokeMethod(controller, "PurchaseProduct", args);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        sb.AppendLine(InvokeMethod(controller, "ApplicationReport", null)?.ToString()?.TrimEnd());

        var actualResult = sb.ToString().TrimEnd();

        var expectedResult = "Client CommonUser is successfully registered.\r\nCommonUser is already registered.\r\nAdmin AdminUser1 is successfully registered with data access.\r\nuser@applicationmail.bg is already used by another user.\r\nAdmin AdminUser2 is successfully registered with data access.\r\nThe number of application administrators is limited.\r\nClient john_doe is successfully registered.\r\nClient MikeRoss is successfully registered.\r\nClient janeSVG is successfully registered.\r\nItem: Laptop is added in the application. Price: 1200.00\r\nCommonUser has no data access.\r\nLaptop already exists in the application.\r\nFurniture is not a valid type for the application.\r\nUnknownUser has no data access.\r\nService: SoftwareInstallation is added in the application. Price: 100.00\r\nItem: Monitor is added in the application. Price: 300.00\r\nItem: GamingMouse is added in the application. Price: 80.00\r\nItem: ExternalHardDrive is added in the application. Price: 120.00\r\nService: AntivirusSubscrition is added in the application. Price: 50.00\r\nService: CloudStorage is added in the application. Price: 10.00\r\nService: DeviceSetup is added in the application. Price: 40.00\r\nLaptop -> Price is updated: 1200.00 -> 1300.00\r\nTablet does not exist in the application.\r\njohn_doe has no data access.\r\nUnknownUser has no data access.\r\nCloudStorage -> Price is updated: 10.00 -> 15.00\r\njohn_doe purchased Laptop. Price: 910.00\r\nMikeRoss purchased Monitor. Price: 300.00\r\nAdminUser1 has no authorization for this functionality.\r\nSmartWatch does not exist in the application.\r\nLaptop is out of stock.\r\nUnknownUser has no authorization for this functionality.\r\nMikeRoss purchased CloudStorage. Price: 12.00\r\njohn_doe purchased GamingMouse. Price: 56.00\r\nCloudStorage is out of stock.\r\njohn_doe purchased SoftwareInstallation. Price: 80.00\r\nMikeRoss purchased AntivirusSubscrition. Price: 40.00\r\njohn_doe purchased ExternalHardDrive. Price: 84.00\r\njaneSVG has no data access.\r\n7 products are listed again.\r\njaneSVG purchased CloudStorage. Price: 12.00\r\nApplication administration:\r\nAdminUser1 - Status: Admin, Contact Info: hidden\r\nAdminUser2 - Status: Admin, Contact Info: hidden\r\nClients:\r\nCommonUser - Status: Client, Contact Info: user@applicationmail.bg\r\njaneSVG - Status: Client, Contact Info: jennifer_savage@mail.de\r\n-Black Friday Purchases: 1\r\n--CloudStorage\r\njohn_doe - Status: Client, Contact Info: john.doe@gmail.com\r\n-Black Friday Purchases: 4\r\n--Laptop\r\n--GamingMouse\r\n--SoftwareInstallation\r\n--ExternalHardDrive\r\nMikeRoss - Status: Client, Contact Info: mike.ross@hotmail.com\r\n-Black Friday Purchases: 2\r\n--CloudStorage\r\n--AntivirusSubscrition";

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