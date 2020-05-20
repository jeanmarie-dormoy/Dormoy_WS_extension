using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
           ConcurrencyMode = ConcurrencyMode.Single)]

    public class Place
    {
        public Location location;
        public string title;
        public Place(double lat, double lng, string title)
        {
            location = new Location(lat, lng);
            this.title = title;
        }
    }
    public class RequestT : IRequestT
    {
        private List<Place> placeList = new List<Place>();
        private static string API_KEY = "OiY75Ntz5fYsYZyroIju5x96Ezev4kY7f9KcKDTcZ8c";

        public List<Place> getPlaceList() { return this.placeList; }
        private string reformatData(double x)
        {
            String temp = x.ToString();
            return temp.Replace(",", ".");
        }

        private bool keepIt(string value)
        {
            switch (value)
            {
                case "education-facility":
                case "service":
                case "hospital-health-care-facility":
                case "business-services":
                    return false;
                default:
                    return true;
            }
        }

        public void getTourismPlaceList(
            double west_lng, double south_lat,
            double east_lng, double north_lat)
        {
            WebRequest request;
            WebResponse response;
            Stream dataStream;
            StreamReader reader;
            string responseFromServer;
            string uri = $"https://places.ls.hereapi.com/places/v1/discover/around?apiKey={API_KEY}";
            uri += "&in=" + reformatData(west_lng) + ","
                + reformatData(south_lat) + ","
                + reformatData(east_lng) + ","
                + reformatData(north_lat);
            request = WebRequest.Create(uri);
            response = request.GetResponse();

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();

            ResponseJSON responseJSON = ResponseJSON.FromJson(responseFromServer);

            if (responseJSON == null || responseJSON.Results.Items == null
                || responseJSON.Results.Items.Length == 0)
                return;
            placeList.Clear();
            Item[] placesList = responseJSON.Results.Items;
            //List<Place> res = new List<Place>();
            for (int i = 0; i < placesList.Length; ++i)
            {
                Item item = placesList[i];
                if (item != null && keepIt(item.Category.Id))
                {
                    placeList.Add(new Place(item.Position[0], item.Position[1], item.Title));
                }
            }
        }
    }
}
