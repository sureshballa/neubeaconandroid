using System;

namespace Tasky.Core {
	/// <summary>
	/// Beacon business object
	/// </summary>
	public class Beacon {
		public Beacon ()
		{
		}

        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }	// TODO: add this field to the user-interface
	}
}