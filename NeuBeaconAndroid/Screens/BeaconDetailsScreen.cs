using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Threading.Tasks;
using NeuBeacons.Core;
using NeuBeaconsAndroid;

namespace NeuBeaconsAndroid.Screens {
	/// <summary>
	/// View/edit a Beacon
	/// </summary>
	[Activity (Label = "BeaconDetailsScreen")]			
	public class BeaconDetailsScreen : Activity {
		Beacon beacon = new Beacon(true);
		Button cancelDeleteButton;
		EditText notesTextEdit;
		EditText nameTextEdit;
		Button saveButton;
		Guid currentGenerateGuid;

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			String beaconID = Intent.GetStringExtra("BeaconID");
			if (!String.IsNullOrWhiteSpace(beaconID)) {
				beacon = await BeaconManager.GetBeaconAsync(beaconID);
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
			cancelDeleteButton.Click += async (sender, e) => { await CancelDelete(); };
			saveButton.Click += async (sender, e) => { await Save(); };
		}

		protected async Task Save()
		{
			beacon.Name = nameTextEdit.Text;
			beacon.Notes = notesTextEdit.Text;
			await BeaconManager.SaveBeaconAsync(beacon);
			Finish();
		}
		
		protected async Task CancelDelete()
		{
			if (currentGenerateGuid == null) {
				await BeaconManager.DeleteBeaconAsync(beacon.ID);
			}
			Finish();
		}
	}
}