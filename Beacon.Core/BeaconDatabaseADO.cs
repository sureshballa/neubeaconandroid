using System;
using System.Linq;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace NeuBeacons.Core
{
	/// <summary>
	/// TaskDatabase uses ADO.NET to create the [Items] table and create,read,update,delete data
	/// </summary>
	public class BeaconDatabase 
	{
		public string path;
		private HttpWebRequest httpReq;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		public BeaconDatabase (string url) 
		{
			this.path = url;
			httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			Console.WriteLine("Url is " + this.path);
		}

		public IEnumerable<Beacon> GetItems ()
		{
			var result = Task.Run<IEnumerable<Beacon>> (async () => {
				httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (this.path));
				var response = await httpReq.GetResponseAsync();
				List<Beacon> beacons = new List<Beacon>();
				using(StreamReader stream = new StreamReader(response.GetResponseStream()))
				{
					var resultString = stream.ReadToEnd();
					var jsonObject =JsonObject.Parse(resultString);

					foreach(var beacon in (JsonArray)jsonObject)
					{
						var obj = beacon as JsonObject;
						if(obj.ContainsKey("title") && obj.ContainsKey("description"))
						{
							beacons.Add(new Beacon() { Name = obj["title"] != null? obj["title"].ToString(): "", 
								Notes = obj["description"] != null? obj["description"].ToString(): "" });
						}
					}
				}

				return beacons;
			}).Result;

			return result;
		}

		public Beacon GetItem (int id) 
		{
			var t = new Beacon ();

			return t;
		}

		public int SaveItem (Beacon item) 
		{
			var result = Task.Run<int> (async () => {
				httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (this.path));
				httpReq.Method = "POST";
				httpReq.ContentType = "application/json";
				string postData = "{\"_id\": \"ibeacon2UUID\", \"Title\": \"" + item.Name + "\", \"Description\": \"beacons1description\"}";
				byte[] byteArray = Encoding.UTF8.GetBytes (postData);
				httpReq.ContentLength = byteArray.Length;
				Stream dataStream = httpReq.GetRequestStream ();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();
				var response = await httpReq.GetResponseAsync();
				var resultString = "0";
				using(StreamReader stream = new StreamReader(response.GetResponseStream()))
				{
					resultString = stream.ReadToEnd();
				}

				return int.Parse(resultString);

			}).Result;


			return result;
		}

		public int DeleteItem(int id) 
		{
			return 0;
		}
	}
}