using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using ServiceY;
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
        public Place(Location loc, string title)
        {
            location = loc;
            this.title = title;
        }
    }

    public class Box
    {
        public double west_lng, south_lat, east_lng, north_lat;
        public Box(double west_lng, double south_lat, double east_lng, double north_lat)
        {
            this.west_lng = west_lng;
            this.south_lat = south_lat;
            this.east_lng = east_lng;
            this.north_lat = north_lat;
        }
    }
    public class RequestT : IRequestT
    {
        private List<Place> placeList = new List<Place>();
        private static string API_KEY = "OiY75Ntz5fYsYZyroIju5x96Ezev4kY7f9KcKDTcZ8c";
        public Box box;

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

        private double GetDistance(Place a, Place b)
        {
            return Math.Sqrt(
                Math.Pow(a.location.Longitude - b.location.Longitude, 2)
                + Math.Pow(a.location.Latitude - b.location.Latitude, 2));
        }

        public Place computeNearestPlace(Place velibDep, Boolean alternative)
        {
            Dictionary<Place, Double> dict = new Dictionary<Place, double>();
            Dictionary<Place, Double> sortedDict;
            foreach (Place place in placeList)
            {
                dict[place] = GetDistance(
                    new Place(velibDep.location.Latitude, velibDep.location.Longitude, "velibDep"),
                    place);                    
            }

            sortedDict = dict.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);

            foreach (KeyValuePair<Place, Double> entry in sortedDict)
            {
                //if (entry.Key.available_bikes > 0)
                    return entry.Key;
            }
            return null;
        }

        private Boolean isPlaceInside(Place place, Box box)
        {
            return
                place.location.Longitude > box.west_lng && place.location.Longitude < box.east_lng
                && place.location.Latitude > box.south_lat && place.location.Latitude < box.north_lat;
        }

        public LocationCollection buildTourismItinerary(Place velibDep, Place velibArr, Boolean alternative)
        {
            //List<Place> res = new List<Place>();
            RequestY reqy = new RequestY();
            Place nextPlace = velibDep;
            Place oldPlace;
            LocationCollection res = new LocationCollection(), temp = new LocationCollection();
            res.Add(velibDep.location);
            
            

            if (velibArr.location.Longitude < velibDep.location.Longitude)
            {
                /*  case:
                 *  B
                 *      A
                 */
                if (velibArr.location.Latitude > velibDep.location.Latitude)
                {
                    
                    placeList = placeList.Where(p => isPlaceInside(p, box)).ToList();
                    oldPlace = nextPlace;
                    nextPlace = computeNearestPlace(nextPlace, alternative);
                    temp = reqy.getSegmentCoordinateList(
                        oldPlace.location, nextPlace.location, "cycling-regular");
                    foreach (Location loc in temp)
                        res.Insert(res.Count, loc);
                    temp.Clear();

                    box.east_lng = nextPlace.location.Longitude;
                    box.south_lat = nextPlace.location.Latitude;
                }
            }


            return res;
        }

        public void updateTourismPlaceList(
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
