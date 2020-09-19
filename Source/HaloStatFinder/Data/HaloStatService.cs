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
		#region Halo2
		public async Task<Halo2StatModel> GetHalo2StatsFromBungie(string gamerTag)
		{
			Halo2StatModel haloStatModel = new Halo2StatModel();

			if (string.IsNullOrWhiteSpace(gamerTag)) return await Task.FromResult(haloStatModel);
			else
			{
				WebClient client = new WebClient();
				string downloadedString = client.DownloadString(Constants.Halo2Constants.UrlBase + gamerTag);

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

		private List<string> GetCleanAndSplitHalo2StatsFromBungie(string htmlStats)
		{
			//return htmlStats.Replace("nbsp", "").Replace(";", "").Replace("|", "").Replace("&", "");

			string cleanedString = htmlStats.Replace("\r\n", "").Replace(" ", "");

			List<string> splitString = cleanedString.Split("&nbsp;&nbsp;|&nbsp;&nbsp;").ToList();

			return splitString;
		}

		private Halo2StatModel ParseHalo2StatsFromBungie(List<string> htmlStats)
		{
			Halo2StatModel haloStatModel = new Halo2StatModel();

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
		#endregion

		#region Halo3
		public async Task<Halo3StatModel> GetHalo3StatsFromBungie(string gamerTag)
		{
			Halo3StatModel haloStatModel = new Halo3StatModel();

			if (string.IsNullOrWhiteSpace(gamerTag)) return await Task.FromResult(haloStatModel);
			else
			{
				WebClient client = new WebClient();
				string downloadedString = client.DownloadString(Constants.Halo3Constants.UrlBase + gamerTag);

				HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(downloadedString);

				// TODO: This currently gets the right divs, but it also grabs others that I don't need.
				// Need to collect the ones that I need and then parse what I need out of them
				foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[contains(@class, 'halo3')]"))
				{
					string value = node.InnerText;
					Console.WriteLine(value);
				}

				return await Task.FromResult(haloStatModel);
			}
		}
		#endregion
	}
}
