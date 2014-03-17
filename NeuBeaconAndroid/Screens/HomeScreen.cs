using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using NeuBeacons.Core;
using NeuBeaconsAndroid;

namespace NeuBeaconsAndroid.Screens {
	/// <summary>
	/// Main ListView screen displays a list of tasks, plus an [Add] button
	/// </summary>
	[Activity (Label = "Beacons", MainLauncher = true, Icon="@drawable/icon")]			
	public class HomeScreen : Activity {
		Adapters.BeaconListAdapter beaconList;
		IList<Beacon> beacons;
		Button addBeaconButton;
		ListView beaconListView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set our layout to be the home screen
			SetContentView(Resource.Layout.HomeScreen);

			//Find our controls
			beaconListView = FindViewById<ListView> (Resource.Id.BeaconList);
			addBeaconButton = FindViewById<Button> (Resource.Id.AddButton);

			// wire up add task button handler
			if(addBeaconButton != null) {
				addBeaconButton.Click += (sender, e) => {
					StartActivity(typeof(BeaconDetailsScreen));
				};
			}
			
			// wire up task click handler
			if(beaconListView != null) {
				beaconListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var beaconDetails = new Intent (this, typeof (BeaconDetailsScreen));
					beaconDetails.PutExtra ("TaskID", beacons[e.Position].ID);
					StartActivity (beaconDetails);
				};
			}
		}
		
		protected override void OnResume ()
		{
			base.OnResume ();

			beacons = BeaconManager.GetBeacons();
			
			// create our adapter
			beaconList = new Adapters.BeaconListAdapter(this, beacons);

			//Hook up our adapter to our ListView
			beaconListView.Adapter = beaconList;
		}
	}
}