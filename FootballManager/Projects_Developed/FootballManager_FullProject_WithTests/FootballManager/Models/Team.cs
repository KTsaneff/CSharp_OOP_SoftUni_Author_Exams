using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;

namespace FootballManager.Models
{
    public class Team : ITeam
    {
        private string name;
        private int championshipPoints;
        private IManager teamManager;

        public Team(string name)
        {
            Name = name;
            ChampionshipPoints = 0;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }

        public int ChampionshipPoints { get => championshipPoints; private set => championshipPoints = value; }

        public IManager TeamManager { get => teamManager; private set => teamManager = value; }

        public int PresentCondition => GetPresentCondition();


        public void GainPoints(int points)
        {
            this.championshipPoints += points;
        }

        public void ResetPoints()
        {
            this.championshipPoints = 0;
        }

        public void SignWith(IManager manager)
        {
            this.teamManager = manager;
        }

        public override string ToString()
        {
            return $"Team: {this.Name} Points: {this.ChampionshipPoints}";
        }
        private int GetPresentCondition()
        {
            if (this.TeamManager == null)
            {
                return 0;
            }
            else if (ChampionshipPoints == 0)
            {
                return (int)Math.Floor(this.TeamManager.Ranking);
            }
            else
            {
                return (int)Math.Floor(this.ChampionshipPoints * this.TeamManager.Ranking);
            }
        }
    }
}
