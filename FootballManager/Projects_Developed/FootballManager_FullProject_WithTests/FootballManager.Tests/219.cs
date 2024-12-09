using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FootballManager;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_219
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void Validate_ChampionshipRankings_MethodWorksProperly()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        StringBuilder sb = new StringBuilder();

        var argumentsArray1 = new object[]
        {
            new object[] { "ManchesterUnited" },
            new object[] { "ManchesterCity" },
            new object[] { "Liverpool" },
            new object[] { "Tottenham" },
            new object[] { "Arsenal" },
            new object[] { "Arsenal" },
            new object[] { "AstonVilla" },
            new object[] { "Brentford" },
            new object[] { "Brighton" },
            new object[] { "Chelsea" },
            new object[] { "Everton" },
            new object[] { "LeichesterCity" }
        };

        foreach (object[] arguments in argumentsArray1)
        {
            InvokeMethod(controller, "JoinChampionship", arguments);
        }

        var argumentsArray2 = new object[]
        {
            new object[] { "Liverpool", "ProfessionalManager", "JurgenKlopp" },
            new object[] { "ManchesterUnited", "ProfessionalManager", "ErikTenHag" },
            new object[] { "ManchesterCity", "ProfessionalManager", "JosepGuardiola" },
            new object[] { "Tottenham", "LicensedManager", "MassimilianoAllegri" },
            new object[] { "Arsenal", "SeniorManager", "ArsenVenger" },
            new object[] { "AstonVilla", "AmateurManager", "ErikTenHag" },
            new object[] { "Brentford", "SeniorManager", "ThomasFrank" },
            new object[] { "Brighton", "AmateurManager", "FabianHurzeler" },
            new object[] { "Chelsea", "ProfessionalManager", "EnzoMaresna" },
            new object[] { "Everton", "SeniorManager", "SeanDyche" },
            new object[] { "LeichesterCity", "ProfessionalManager", "SteveCooper" },
            new object[] { "QueensParkRangers", "AmatuerManager", "JamieVardy" },
            new object[] { "Arsenal", "ProfessionalManager", "JoseMourinho" }
        };

        foreach (object[] arguments in argumentsArray2)
        {
            InvokeMethod(controller, "SignManager", arguments);
        }

        var argumentsArray3 = new object[]
        {
            new object[] { "Arsenal", "AstonVilla" },
            new object[] { "Chelsea", "Brighton" },
            new object[] { "Everton", "ManchesterUnited" },
            new object[] { "ManchesterCity", "Brentford" },
            new object[] { "Liverpool", "Arsenal" },
            new object[] { "QueensParkRangers", "LeichesterCity" },
            new object[] { "LeichesterCity", "Tottenham" },
            new object[] { "ManchesterUnited", "Everton" },
            new object[] { "ManchesterCity", "ManchesterUnited" },
            new object[] { "Brighton", "Brentford" },
            new object[] { "AstonVilla", "Chelsea" },
            new object[] { "Arsenal", "Tottenham" }
        };

        foreach (object[] arguments in argumentsArray3)
        {
            InvokeMethod(controller, "MatchBetween", arguments);
        }

        var argumentsArray4 = new object[]
        {
            new object[] { "Tottenham", "ProfessionalManager", "JoseMourinho" },
            new object[] { "AstonVilla", "ProfessionalManager", "XabiAlonso" }
        };

        foreach (object[] arguments in argumentsArray4)
        {
            InvokeMethod(controller, "SignManager", arguments);
        }

        var argumentsArray5 = new object[]
        {
            new object[] { "Liverpool", "Chelsea" },
            new object[] { "ManchesterUnited", "Liverpool" },
            new object[] { "QueensParkRangers", "Liverpool" },
            new object[] { "Tottenham", "Chelsea" },
            new object[] { "Liverpool", "Everton" },
            new object[] { "ManchesterCity", "Tottenham" },
            new object[] { "Tottenham", "ManchesterCity" },
            new object[] { "AstonVilla", "Brentford" },
            new object[] { "Tottenham", "AstonVilla" },
            new object[] { "Brighton", "Brentford" },
            new object[] { "Everton", "Brighton" },
        };

        foreach (object[] arguments in argumentsArray5)
        {
            InvokeMethod(controller, "MatchBetween", arguments);
        }

        var argumentsArray6 = new object[]
        {
            new object[] { "LeichesterCity", "Redding", "ProfessionalManager", "GiovanniTrapatoni" },
            new object[] { "Everton", "Redding", "ProfessionalManager", "GiovanniTrapattoni" }
        };

        foreach (object[] arguments in argumentsArray6)
        {
            InvokeMethod(controller, "PromoteTeam", arguments);
        }

        var expectedResult = "***Ranking Table***\r\n1. Team: ManchesterUnited Points: 0/ErikTenHag - ProfessionalManager (Ranking: 90.00)\r\n2. Team: Chelsea Points: 0/EnzoMaresna - ProfessionalManager (Ranking: 90.00)\r\n3. Team: ManchesterCity Points: 0/JosepGuardiola - ProfessionalManager (Ranking: 75.00)\r\n4. Team: AstonVilla Points: 0/XabiAlonso - ProfessionalManager (Ranking: 60.00)\r\n5. Team: Redding Points: 0/GiovanniTrapattoni - ProfessionalManager (Ranking: 60.00)\r\n6. Team: Liverpool Points: 0/JurgenKlopp - ProfessionalManager (Ranking: 45.00)\r\n7. Team: Arsenal Points: 0/ArsenVenger - SeniorManager (Ranking: 45.00)\r\n8. Team: Brentford Points: 0/ThomasFrank - SeniorManager (Ranking: 40.00)\r\n9. Team: Tottenham Points: 0/JoseMourinho - ProfessionalManager (Ranking: 30.00)\r\n10. Team: Brighton Points: 0/FabianHurzeler - AmateurManager (Ranking: 0.00)";

        var actualResult = InvokeMethod(controller, "ChampionshipRankings", null);

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