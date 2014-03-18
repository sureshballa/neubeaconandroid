using System;

namespace NeuBeacons.Core {
	/// <summary>
	/// Beacon business object
	/// </summary>
	public class Beacon {
		public Beacon (bool isNew)
		{
			this.IsNew = isNew;
		}

		public bool IsNew { get; private set; }

		public string ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }	// TODO: add this field to the user-interface
	}
}