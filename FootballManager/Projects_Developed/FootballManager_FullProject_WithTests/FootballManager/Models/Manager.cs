using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;

namespace FootballManager.Models
{
    public abstract class Manager : IManager
    {
        private string name;
        private double ranking;

        protected Manager(string name, double ranking)
        {
            Name = name;
            Ranking = ranking;
        }

        public string Name 
        { 
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ManagerNameNull);
                }
                name = value;
            }
        }

        public double Ranking { get => ranking; protected set => ranking = value; }

        public abstract void RankingUpdate(double updateValue);
        public override string ToString()
        {
            return $"{this.Name} - {this.GetType().Name} (Ranking: {this.Ranking:F2})";
        }
    }
}
