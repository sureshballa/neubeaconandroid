using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuBeacons.Core {
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public static class BeaconManager {

		static BeaconManager ()
		{
		}
		
		public async static Task<Beacon> GetBeaconAsync(String id)
		{
			return await BeaconRepositoryADO.GetBeaconAsync(id);
		}
		
		public async static Task<IList<Beacon>> GetBeaconsAsync ()
		{
			return await BeaconRepositoryADO.GetBeaconsAsync();
		}

		public async static Task SaveBeaconAsync (Beacon item)
		{
			await BeaconRepositoryADO.SaveBeaconAsync(item);
		}
		
		public async static Task DeleteBeaconAsync(String id)
		{
			await BeaconRepositoryADO.DeleteBeaconAsync(id);
		}
	}
}