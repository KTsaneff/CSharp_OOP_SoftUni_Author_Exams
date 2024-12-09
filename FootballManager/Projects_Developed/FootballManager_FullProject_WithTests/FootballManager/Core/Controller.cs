using FootballManager.Core.Contracts;
using FootballManager.Models;
using FootballManager.Models.Contracts;
using FootballManager.Repositories;
using FootballManager.Repositories.Contracts;
using FootballManager.Utilities.Messages;
using System.Text;

namespace FootballManager.Core
{
    public class Controller : IController
    {
        private IRepository<ITeam> championship = new TeamRepository();

        public string JoinChampionship(string teamName)
        {
            if(championship.Models.Count == championship.Capacity)
            {
                return OutputMessages.ChampionshipFull;
            }

            if(championship.Exists(teamName))
            {
                return string.Format(OutputMessages.TeamWithSameNameExisting, teamName);
            }

            ITeam team = new Team(teamName);

            championship.Add(team);

            return string.Format(OutputMessages.TeamSuccessfullyJoined, teamName);
        }
        public string SignManager(string teamName, string managerTypeName, string managerName)
        {
            if(!championship.Exists(teamName))
            {
                return string.Format(OutputMessages.TeamDoesNotTakePart, teamName);
            }

            if(managerTypeName != nameof(AmateurManager) && 
                managerTypeName != nameof(SeniorManager) && 
                managerTypeName != nameof(ProfessionalManager))
            {
                return string.Format(OutputMessages.ManagerTypeNotPresented, managerTypeName);
            }

            ITeam team = championship.Get(teamName);
            if(team.TeamManager != null)
            {
                return string.Format(OutputMessages.TeamSignedWithAnotherManager, teamName, team.TeamManager.Name);
            }

            if(championship.Models.Any(x => x.TeamManager != null && x.TeamManager.Name == managerName))
            {
                return string.Format(OutputMessages.ManagerAssignedToAnotherTeam, managerName);
            }

            IManager manager;
            if(managerTypeName == nameof(AmateurManager))
            {
                manager = new AmateurManager(managerName);
            }
            else if(managerTypeName == nameof(SeniorManager))
            {
                manager = new SeniorManager(managerName);
            }
            else
            {
                manager = new ProfessionalManager(managerName);
            }

            ITeam teamToContract = championship.Get(teamName);

            teamToContract.SignWith(manager);

            return string.Format(OutputMessages.TeamSuccessfullySignedWithManager, managerName, teamName);
        }

        public string MatchBetween(string teamOneName, string teamTwoName)
        {
            if(!championship.Exists(teamOneName) || !championship.Exists(teamTwoName))
            {
                return OutputMessages.OneOfTheTeamDoesNotExist;
            }

            ITeam teamOne = championship.Get(teamOneName);
            ITeam teamTwo = championship.Get(teamTwoName);

            if(teamOne.PresentCondition > teamTwo.PresentCondition)
            {
                teamOne.GainPoints(3);
                if(teamOne.TeamManager != null)
                {
                    teamOne.TeamManager.RankingUpdate(5);
                }
                if(teamTwo.TeamManager != null)
                {
                    teamTwo.TeamManager.RankingUpdate(-5);
                }

                return string.Format(OutputMessages.TeamWinsMatch, teamOneName, teamTwoName);
            }
            else if(teamOne.PresentCondition < teamTwo.PresentCondition)
            {
                teamTwo.GainPoints(3);
                if(teamTwo.TeamManager != null)
                {
                    teamTwo.TeamManager.RankingUpdate(5);
                }
                if(teamOne.TeamManager != null)
                {
                    teamOne.TeamManager.RankingUpdate(-5);
                }

                return string.Format(OutputMessages.TeamWinsMatch, teamTwoName, teamOneName);
            }
            else
            {
                teamOne.GainPoints(1);
                teamTwo.GainPoints(1);

                return string.Format(OutputMessages.MatchIsDraw, teamOneName, teamTwoName);
            }
        }

        public string PromoteTeam(string droppingTeamName, string promotingTeamName, string managerTypeName, string managerName)
        {
            if (!championship.Exists(droppingTeamName))
            {
                return string.Format(OutputMessages.DroppingTeamDoesNotExist, droppingTeamName);
            }

            if(championship.Exists(promotingTeamName))
            {
                return string.Format(OutputMessages.TeamWithSameNameExisting, promotingTeamName);
            }

            ITeam team = new Team(promotingTeamName);

            if(managerTypeName == nameof(AmateurManager) ||
                managerTypeName == nameof(SeniorManager) ||
                managerTypeName == nameof(ProfessionalManager) || !championship.Models.Where(t => t.TeamManager != null).Any(t => t.TeamManager.Name == managerName))
            {
                IManager manager;
                
                if(managerTypeName == nameof(AmateurManager))
                {
                    manager = new AmateurManager(managerName);
                }
                else if(managerTypeName == nameof(SeniorManager))
                {
                    manager = new SeniorManager(managerName);
                }
                else
                {
                    manager = new ProfessionalManager(managerName);
                }

                team.SignWith(manager);
            }

            foreach (var teamToReset in championship.Models)
            {
                teamToReset.ResetPoints();
            }

            championship.Remove(droppingTeamName);
            championship.Add(team);

            return string.Format(OutputMessages.TeamHasBeenPromoted, promotingTeamName);
        }

        public string ChampionshipRankings()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***Ranking Table***");

            int counter = 1;

            foreach (var team in championship.Models.OrderByDescending(x => x.ChampionshipPoints).ThenByDescending(x => x.PresentCondition))
            {
                sb.AppendLine($"{counter}. {team}/{team.TeamManager}");
                counter++;
            }

            return sb.ToString().TrimEnd();
        }
    }
}
