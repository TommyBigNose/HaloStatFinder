using HaloStatFinder.Data;
using HaloStatFinder.Data.Interfaces;
using HaloStatFinder.Data.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HaloStatFinder.Tests.Data
{
	public class HaloStatServiceTests
	{
		private IHaloStatService _sut;

		[SetUp]
		public void Setup()
		{
			_sut = new HaloStatService();
		}

		[TearDown]
		public void TearDown()
		{
		}

		[TestCase("SageOfChaos")]
		/**
		 * SageOfChaos : Total Halo 2 Games: Total Games: 5313 Last Played: 11/23/2009 4:10:18 PM Total Kills: 41131 Total Deaths: 28649 Total Assists: 9791
		 */
		public async Task GetHalo2StatsFromBungie_Goldenflow(string gamerTag)
		{

			// Arrange
			// Do nothing

			// Act
			Halo2StatModel result = await _sut.GetHalo2StatsFromBungie(gamerTag);

			// Assert
			Assert.IsTrue(result.TotalGames == 5313);
			Assert.IsTrue(result.LastPlayed.Equals("11/23/20094:10:18PM"));
			Assert.IsTrue(result.TotalKills == 41131);
			Assert.IsTrue(result.TotalDeaths == 28649);
			Assert.IsTrue(result.TotalAssists == 9791);
		}

		[TestCase("SageOfChaos")]
		/**
		 * SageOfChaos : Total Halo 3 Games: Total Games: 1720
		 */
		public async Task GetHalo3StatsFromBungie_Goldenflow(string gamerTag)
		{

			// Arrange
			// Do nothing

			// Act
			Halo3StatModel result = await _sut.GetHalo3StatsFromBungie(gamerTag);

			// Assert
			Assert.IsTrue(result.TotalGames == 1720);
			Assert.IsTrue(result.TotalExp == 1171);
			Assert.IsTrue(result.HighestSkill == 48);
			Assert.IsTrue(result.RankedKdRatio == float.Parse("1.37"));
			Assert.IsTrue(result.TotalRankedKills == 11681);
			Assert.IsTrue(result.TotalRankedDeaths == 8545);
			Assert.IsTrue(result.TotalRankedGames == 865);
			Assert.IsTrue(result.SocialKdRatio == float.Parse("1.70"));
			Assert.IsTrue(result.TotalSocialKills == 11811);
			Assert.IsTrue(result.TotalSocialDeaths == 6929);
			Assert.IsTrue(result.TotalSocialGames == 855);
		}


		[TestCase("SageOfChaos")]
		/**
		 * SageOfChaos : Total Halo Reach Competitive Games: 906
		 */
		public async Task GetHaloReachStatsFromBungie_Goldenflow(string gamerTag)
		{

			// Arrange
			// Do nothing

			// Act
			HaloReachStatModel result = await _sut.GetHaloReachStatsFromBungie(gamerTag);

			// Assert
			Assert.IsTrue(result.TotalGames == 906);
			Assert.IsTrue(result.TotalKills == 12501);
			Assert.IsTrue(result.TotalDeaths == 8656);
			Assert.IsTrue(result.TotalAssists == 1970);
			Assert.IsTrue(result.KillDeathRatio == float.Parse("1.44"));
			Assert.IsTrue(result.KillGameRatio == float.Parse("13.80"));
			Assert.IsTrue(result.DeathGameRatio == float.Parse("9.55"));
			Assert.IsTrue(result.KillHourRatio == float.Parse("99.43"));
			Assert.IsTrue(result.DeathHourRatio == float.Parse("68.85"));
			Assert.IsTrue(result.TotalMedals == 15872);
			Assert.IsTrue(result.MedalGameRatio == float.Parse("17.52"));
			Assert.IsTrue(result.MedalHourRatio == float.Parse("126.24"));
		}
	}
}
