using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using NeuBeacons.Core;
using NeuBeaconsAndroid;

namespace NeuBeaconsAndroid.Screens {
	/// <summary>
	/// View/edit a Task
	/// </summary>
	[Activity (Label = "BeaconDetailsScreen")]			
	public class BeaconDetailsScreen : Activity {
		Beacon task = new Beacon();
		Button cancelDeleteButton;
		EditText notesTextEdit;
		EditText nameTextEdit;
		Button saveButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			int taskID = Intent.GetIntExtra("TaskID", 0);
			if(taskID > 0) {
				task = BeaconManager.GetBeacon(taskID);
			}
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.BeaconDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
			notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
			saveButton = FindViewById<Button>(Resource.Id.SaveButton);
			
			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);
			
			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (task.ID == 0 ? "Cancel" : "Delete");
			
			nameTextEdit.Text = task.Name; 
			notesTextEdit.Text = task.Notes;

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		void Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			BeaconManager.SaveBeacon(task);
			Finish();
		}
		
		void CancelDelete()
		{
			if (task.ID != 0) {
				BeaconManager.DeleteBeacon(task.ID);
			}
			Finish();
		}
	}
}