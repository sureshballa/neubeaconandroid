using System;
using System.Collections.Generic;

namespace NeuBeacons.Core {
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public static class BeaconManager {

		static BeaconManager ()
		{
		}
		
		public static Beacon GetBeacon(String id)
		{
			return BeaconRepositoryADO.GetBeacon(id);
		}
		
		public static IList<Beacon> GetBeacons ()
		{
			return new List<Beacon>(BeaconRepositoryADO.GetBeacons());
		}

		public static void SaveBeacon (Beacon item)
		{
			BeaconRepositoryADO.SaveBeacon(item);
		}
		
		public static String DeleteBeacon(String id)
		{
			return BeaconRepositoryADO.DeleteBeacon(id);
		}
	}
}