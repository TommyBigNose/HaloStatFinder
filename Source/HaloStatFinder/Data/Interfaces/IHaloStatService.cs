using HaloStatFinder.Data.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HaloStatFinder.Data.Interfaces
{
	public interface IHaloStatService
	{
		Task<HaloStatModel> GetHalo2StatsFromBungie(string gamerTag);
	}
}