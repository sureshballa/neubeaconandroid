using System;
using System.Collections.Generic;

namespace Tasky.Core {
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public static class BeaconManager {

		static BeaconManager ()
		{
		}
		
		public static Beacon GetBeacon(int id)
		{
			return BeaconRepositoryADO.GetBeacon(id);
		}
		
		public static IList<Beacon> GetBeacons ()
		{
			return new List<Beacon>(BeaconRepositoryADO.GetBeacons());
		}
		
		public static int SaveBeacon (Beacon item)
		{
			return BeaconRepositoryADO.SaveBeacon(item);
		}
		
		public static int DeleteBeacon(int id)
		{
			return BeaconRepositoryADO.DeleteBeacon(id);
		}
	}
}