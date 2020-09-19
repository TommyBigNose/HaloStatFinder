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
		}
	}
}
