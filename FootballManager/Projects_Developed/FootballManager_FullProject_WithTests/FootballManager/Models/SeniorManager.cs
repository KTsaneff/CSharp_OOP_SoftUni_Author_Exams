namespace FootballManager.Models
{
    public class SeniorManager : Manager
    {
        private const double InitialRanking = 30.0;

        public SeniorManager(string name)
            : base(name, InitialRanking)
        {
            
        }

        public override void RankingUpdate(double updateValue)
        {
            this.Ranking += updateValue;
        }
    }
}
