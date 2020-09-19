using HaloStatFinder.Data.Models;
using System.Threading.Tasks;

namespace HaloStatFinder.Data.Interfaces
{
	public interface IHaloStatService
	{
		Task<Halo2StatModel> GetHalo2StatsFromBungie(string gamerTag);
		Task<Halo3StatModel> GetHalo3StatsFromBungie(string gamerTag);
	}
}