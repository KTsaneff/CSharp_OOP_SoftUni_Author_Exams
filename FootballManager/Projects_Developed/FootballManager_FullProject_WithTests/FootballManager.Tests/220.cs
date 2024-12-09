using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FootballManager;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_220
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void Validate_TestExample_FullExampleOuput()
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
            var tempResult = InvokeMethod(controller, "JoinChampionship", arguments);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
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
            var tempResult = InvokeMethod(controller, "SignManager", arguments);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
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
            var tempResult = InvokeMethod(controller, "MatchBetween", arguments);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var argumentsArray4 = new object[]
        {
            new object[] { "Tottenham", "ProfessionalManager", "JoseMourinho" },
            new object[] { "AstonVilla", "ProfessionalManager", "XabiAlonso" }
        };

        foreach (object[] arguments in argumentsArray4)
        {
            var tempResult = InvokeMethod(controller, "SignManager", arguments);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
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
            var tempResult = InvokeMethod(controller, "MatchBetween", arguments);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var rankings = InvokeMethod(controller, "ChampionshipRankings", null);
        sb.AppendLine(rankings.ToString());

        var argumentsArray6 = new object[]
        {
            new object[] { "LeichesterCity", "Redding", "ProfessionalManager", "GiovanniTrapatoni" },
            new object[] { "Everton", "Redding", "ProfessionalManager", "GiovanniTrapattoni" }
        };

        foreach (object[] arguments in argumentsArray6)
        {
            var tempResult = InvokeMethod(controller, "PromoteTeam", arguments);
            sb.AppendLine(tempResult?.ToString()?.TrimEnd());
        }

        var rankings2 = InvokeMethod(controller, "ChampionshipRankings", null);
        sb.AppendLine(rankings2.ToString());

        var expectedResult = "ManchesterUnited has successfully joined the Championship.\r\nManchesterCity has successfully joined the Championship.\r\nLiverpool has successfully joined the Championship.\r\nTottenham has successfully joined the Championship.\r\nArsenal has successfully joined the Championship.\r\nArsenal has already joined the Championship.\r\nAstonVilla has successfully joined the Championship.\r\nBrentford has successfully joined the Championship.\r\nBrighton has successfully joined the Championship.\r\nChelsea has successfully joined the Championship.\r\nEverton has successfully joined the Championship.\r\nChampionship is full!\r\nManager JurgenKlopp is assigned to team Liverpool.\r\nManager ErikTenHag is assigned to team ManchesterUnited.\r\nManager JosepGuardiola is assigned to team ManchesterCity.\r\nLicensedManager is an invalid manager type for the application.\r\nManager ArsenVenger is assigned to team Arsenal.\r\nManager ErikTenHag is already assigned to another team.\r\nManager ThomasFrank is assigned to team Brentford.\r\nManager FabianHurzeler is assigned to team Brighton.\r\nManager EnzoMaresna is assigned to team Chelsea.\r\nManager SeanDyche is assigned to team Everton.\r\nTeam LeichesterCity does not take part in the Championship.\r\nTeam QueensParkRangers does not take part in the Championship.\r\nTeam Arsenal has already signed a contract with ArsenVenger.\r\nTeam Arsenal wins the match against AstonVilla.\r\nTeam Chelsea wins the match against Brighton.\r\nTeam ManchesterUnited wins the match against Everton.\r\nTeam ManchesterCity wins the match against Brentford.\r\nTeam Arsenal wins the match against Liverpool.\r\nThis match does not meet the regulation rules of the Championship.\r\nThis match does not meet the regulation rules of the Championship.\r\nTeam ManchesterUnited wins the match against Everton.\r\nTeam ManchesterUnited wins the match against ManchesterCity.\r\nTeam Brentford wins the match against Brighton.\r\nTeam Chelsea wins the match against AstonVilla.\r\nTeam Arsenal wins the match against Tottenham.\r\nManager JoseMourinho is assigned to team Tottenham.\r\nManager XabiAlonso is assigned to team AstonVilla.\r\nTeam Chelsea wins the match against Liverpool.\r\nTeam ManchesterUnited wins the match against Liverpool.\r\nThis match does not meet the regulation rules of the Championship.\r\nTeam Chelsea wins the match against Tottenham.\r\nTeam Liverpool wins the match against Everton.\r\nTeam ManchesterCity wins the match against Tottenham.\r\nTeam ManchesterCity wins the match against Tottenham.\r\nTeam Brentford wins the match against AstonVilla.\r\nTeam AstonVilla wins the match against Tottenham.\r\nTeam Brentford wins the match against Brighton.\r\nTeam Everton wins the match against Brighton.\r\n***Ranking Table***\r\n1. Team: ManchesterUnited Points: 12/ErikTenHag - ProfessionalManager (Ranking: 90.00)\r\n2. Team: Chelsea Points: 12/EnzoMaresna - ProfessionalManager (Ranking: 90.00)\r\n3. Team: ManchesterCity Points: 9/JosepGuardiola - ProfessionalManager (Ranking: 75.00)\r\n4. Team: Arsenal Points: 9/ArsenVenger - SeniorManager (Ranking: 45.00)\r\n5. Team: Brentford Points: 9/ThomasFrank - SeniorManager (Ranking: 40.00)\r\n6. Team: AstonVilla Points: 3/XabiAlonso - ProfessionalManager (Ranking: 60.00)\r\n7. Team: Liverpool Points: 3/JurgenKlopp - ProfessionalManager (Ranking: 45.00)\r\n8. Team: Everton Points: 3/SeanDyche - SeniorManager (Ranking: 20.00)\r\n9. Team: Tottenham Points: 0/JoseMourinho - ProfessionalManager (Ranking: 30.00)\r\n10. Team: Brighton Points: 0/FabianHurzeler - AmateurManager (Ranking: 0.00)\r\nTeam LeichesterCity does not exist in the Championship.\r\nTeam Redding wins a promotion for the new season.\r\n***Ranking Table***\r\n1. Team: ManchesterUnited Points: 0/ErikTenHag - ProfessionalManager (Ranking: 90.00)\r\n2. Team: Chelsea Points: 0/EnzoMaresna - ProfessionalManager (Ranking: 90.00)\r\n3. Team: ManchesterCity Points: 0/JosepGuardiola - ProfessionalManager (Ranking: 75.00)\r\n4. Team: AstonVilla Points: 0/XabiAlonso - ProfessionalManager (Ranking: 60.00)\r\n5. Team: Redding Points: 0/GiovanniTrapattoni - ProfessionalManager (Ranking: 60.00)\r\n6. Team: Liverpool Points: 0/JurgenKlopp - ProfessionalManager (Ranking: 45.00)\r\n7. Team: Arsenal Points: 0/ArsenVenger - SeniorManager (Ranking: 45.00)\r\n8. Team: Brentford Points: 0/ThomasFrank - SeniorManager (Ranking: 40.00)\r\n9. Team: Tottenham Points: 0/JoseMourinho - ProfessionalManager (Ranking: 30.00)\r\n10. Team: Brighton Points: 0/FabianHurzeler - AmateurManager (Ranking: 0.00)";

        var actualResult = sb.ToString().TrimEnd();

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