namespace FootballManager.Models
{
    public class ProfessionalManager : Manager
    {
        private const double InitialRanking = 60.0;

        public ProfessionalManager(string name)
            : base(name, InitialRanking)
        {
            
        }

        public override void RankingUpdate(double updateValue)
        {
            this.Ranking += updateValue*1.5;
        }
    }
}
