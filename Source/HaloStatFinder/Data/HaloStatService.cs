using HaloStatFinder.Data.Interfaces;
using HaloStatFinder.Data.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HaloStatFinder.Data
{
	public class HaloStatService : IHaloStatService
	{
		public async Task<HaloStatModel> GetHalo2StatsFromBungie(string gamerTag)
		{
			HaloStatModel haloStatModel = new HaloStatModel();

			if (string.IsNullOrWhiteSpace(gamerTag)) return await Task.FromResult(haloStatModel);
			else
			{
				WebClient client = new WebClient();
				string downloadedString = client.DownloadString("https://halo.bungie.net/stats/playerstatshalo2.aspx?player=" + gamerTag);

				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(downloadedString);
				string whatUrLookingFor = doc.GetElementbyId("ctl00_mainContent_bnetpgl_identityStrip_div1").InnerHtml;

				HtmlNodeCollection childNodes = doc.DocumentNode.SelectNodes("//li");
				var result = childNodes.Single(x => x.InnerText.Contains("Total Games:") == true);

				List<string> cleanedResult = GetCleanAndSplitHalo2StatsFromBungie(result.InnerHtml);

				haloStatModel = ParseHalo2StatsFromBungie(cleanedResult);

				return await Task.FromResult(haloStatModel);
			}
		}

		/*
		 * Utility functions
		 */
		private List<string> GetCleanAndSplitHalo2StatsFromBungie(string htmlStats)
		{
			//return htmlStats.Replace("nbsp", "").Replace(";", "").Replace("|", "").Replace("&", "");

			string cleanedString = htmlStats.Replace("\r\n", "").Replace(" ", "");

			List<string> splitString = cleanedString.Split("&nbsp;&nbsp;|&nbsp;&nbsp;").ToList();

			return splitString;
		}

		private HaloStatModel ParseHalo2StatsFromBungie(List<string> htmlStats)
		{
			HaloStatModel haloStatModel = new HaloStatModel();

			foreach (string stat in htmlStats)
			{
				if (stat.Contains(Constants.Halo2Constants.TotalGames, StringComparison.OrdinalIgnoreCase))
				{
					haloStatModel.TotalGames = int.Parse(stat.Replace($"{Constants.Halo2Constants.TotalGames}:", ""));
				}
				else if (stat.Contains(Constants.Halo2Constants.LastPlayed, StringComparison.OrdinalIgnoreCase))
				{
					haloStatModel.LastPlayed = (stat.Replace($"{Constants.Halo2Constants.LastPlayed}:", ""));
				}
				else if (stat.Contains(Constants.Halo2Constants.TotalKills, StringComparison.OrdinalIgnoreCase))
				{
					haloStatModel.TotalKills = int.Parse(stat.Replace($"{Constants.Halo2Constants.TotalKills}:", ""));
				}
				else if (stat.Contains(Constants.Halo2Constants.TotalDeaths, StringComparison.OrdinalIgnoreCase))
				{
					haloStatModel.TotalDeaths = int.Parse(stat.Replace($"{Constants.Halo2Constants.TotalDeaths}:", ""));
				}
				else if (stat.Contains(Constants.Halo2Constants.TotalAssists, StringComparison.OrdinalIgnoreCase))
				{
					haloStatModel.TotalAssists = int.Parse(stat.Replace($"{Constants.Halo2Constants.TotalAssists}:", ""));
				}
			}

			return haloStatModel;
		}
	}
}
