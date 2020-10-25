namespace HaloStatFinder.Data.Models
{
	public class HaloReachStatModel
	{
		public int TotalGames { get; set; }
		public string TotalPlaytime { get; set; }
		public int TotalKills { get; set; }
		public int TotalDeaths { get; set; }
		public int TotalAssists { get; set; }
		public float KillDeathRatio { get; set; }
		public float KillGameRatio { get; set; }
		public float DeathGameRatio { get; set; }
		public float KillHourRatio { get; set; }
		public float DeathHourRatio { get; set; }
		public float TotalMedals { get; set; }
		public float MedalGameRatio { get; set; }
		public float MedalHourRatio { get; set; }
	}
}
