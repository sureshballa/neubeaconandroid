using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Json;
using System.Net;

namespace NeuBeacons.Core {
	public class BeaconRepositoryADO {

		protected static string URL = "http://neuibeaconsservice.herokuapp.com/ibeacons";

		public async static Task<Beacon> GetBeaconAsync(String id)
		{
			id = id.Replace("\"", "");

			var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (BeaconRepositoryADO.URL + "/" + id));
			var response = await httpReq.GetResponseAsync();

			Beacon beacon;

			using(StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				var resultString = stream.ReadToEnd();
				var serverFormat = new { @_id = "", title = "", description = "" };
				var serverObject = JsonConvert.DeserializeAnonymousType(resultString, serverFormat);
				beacon = new Beacon(false);
				beacon.ID = serverObject._id;
				beacon.Name = serverObject.title;
				beacon.Notes = serverObject.description;
			}

			return beacon;
		}

		public async static Task<IList<Beacon>> GetBeaconsAsync ()
		{
			var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (BeaconRepositoryADO.URL));
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
						beacons.Add(new Beacon(false) { Name = obj["title"] != null? obj["title"].ToString(): "", 
							Notes = obj["description"] != null? obj["description"].ToString(): "",
							ID = obj["_id"] != null ? obj["_id"].ToString(): ""});
					}
				}
			}

			return beacons;
		}

		public async static Task SaveBeaconAsync(Beacon item)
		{
			var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (BeaconRepositoryADO.URL));
			if(item.IsNew)
			{
				httpReq.Method = "POST";
			}
			else
			{
				httpReq.Method = "PUT";
			}
			httpReq.ContentType = "application/json";
			string postData = JsonConvert.SerializeObject (new { @_id = item.ID, title = item.Name, description = item.Notes });
			byte[] byteArray = Encoding.UTF8.GetBytes (postData);
			httpReq.ContentLength = byteArray.Length;
			Stream dataStream = httpReq.GetRequestStream ();
			dataStream.Write (byteArray, 0, byteArray.Length);
			dataStream.Close ();
			var response = await httpReq.GetResponseAsync();
			var resultString = "0";
			using (StreamReader stream = new StreamReader (response.GetResponseStream ())) 
			{
				resultString = stream.ReadToEnd ();
			}
		}

		public static Task DeleteBeaconAsync(String id)
		{
			return null;
		}
	}
}

