using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using NeuBeacons.Core;
using NeuBeaconsAndroid;

namespace NeuBeaconsAndroid.Screens {
	/// <summary>
	/// View/edit a Task
	/// </summary>
	[Activity (Label = "BeaconDetailsScreen")]			
	public class BeaconDetailsScreen : Activity {
		Beacon beacon = new Beacon(true);
		Button cancelDeleteButton;
		EditText notesTextEdit;
		EditText nameTextEdit;
		Button saveButton;
		Guid currentGenerateGuid;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			String beaconID = Intent.GetStringExtra("BeaconID");
			if (!String.IsNullOrWhiteSpace(beaconID)) {
				beacon = BeaconManager.GetBeacon(beaconID);
			} else {
				currentGenerateGuid = Guid.NewGuid ();
				beacon.ID = currentGenerateGuid.ToString ();
			}
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.BeaconDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
			notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
			saveButton = FindViewById<Button>(Resource.Id.SaveButton);
			
			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);
			
			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (currentGenerateGuid != null ? "Cancel" : "Delete");
			
			nameTextEdit.Text = beacon.Name; 
			notesTextEdit.Text = beacon.Notes;

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		void Save()
		{
			beacon.Name = nameTextEdit.Text;
			beacon.Notes = notesTextEdit.Text;
			BeaconManager.SaveBeacon(beacon);
			Finish();
		}
		
		void CancelDelete()
		{
			if (currentGenerateGuid == null) {
				BeaconManager.DeleteBeacon(beacon.ID);
			}
			Finish();
		}
	}
}