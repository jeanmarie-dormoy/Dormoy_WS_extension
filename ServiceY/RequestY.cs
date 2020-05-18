using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ServiceX;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Microsoft.Maps.MapControl.WPF;
using System.Text;

namespace ServiceY
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
            ConcurrencyMode = ConcurrencyMode.Single)]
    public class RequestY : IRequestY
    {
        private static string contract;
        private Request reqX;
        private static string API_KEY = "5b3ce3597851110001cf62488f2206f7d24a4fbd967c3ba5f827961e";


        WebRequest request;
        WebResponse response;
        Stream dataStream;
        StreamReader reader;
        string responseFromServer;
        public List<VelibStation> stationList;
        public RequestY()
        {
            reqX = new Request();
        }
        public List<Contract> getContracts()
        {
            return reqX.getContracts();
        }

        public void refreshStationList(String city)
        {
            contract = city;
            reqX.refreshStationList(city);
            stationList = reqX.getAllStations().OfType<VelibStation>().ToList();
        }

        public List<VelibStation> getAllStations()
        {
            return reqX.getAllStations();
        }

        public Location geocodingAddress(String location)
        {
            RootJSON answerJSON;
            double[] bbox;
            Position position;
            string url, formattedLoc = "";
            string[] splittedLoc = location.Split(' ');
            for (int i = 0; i < splittedLoc.Length; i++)
            {
                if (i != splittedLoc.Length - 1)
                    formattedLoc = formattedLoc + splittedLoc[i] + "%20";
                else
                    formattedLoc = formattedLoc + splittedLoc[i];
            }
            url = $"https://api.openrouteservice.org/geocode/search/structured?api_key={API_KEY}"
                + "&address=" + formattedLoc + "&locality=" + contract;
            request = WebRequest.Create(url);
            response = request.GetResponse();

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();

            answerJSON = JsonConvert.DeserializeObject<RootJSON>(responseFromServer);
            if (answerJSON == null)
                return null;

            bbox = answerJSON.Bbox;
            if (Math.Abs(bbox[0] - bbox[2]) > 0.1 || Math.Abs(bbox[1] - bbox[3]) > 0.1)
                return null;
            position = new Position();
            position.lng = bbox[0];
            position.lat = bbox[1];

            return new Location(position.lat, position.lng);
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public VelibStation computeNearestStation(Location pos)
        {
            Dictionary<VelibStation, Double> dict = new Dictionary<VelibStation, double>();
            Dictionary<VelibStation, Double> sortedDict;
            foreach (VelibStation station in stationList)
            {
                dict[station] = GetDistance(
                    pos.Latitude,
                    pos.Longitude,
                    station.position.lat,
                    station.position.lng);
            }

            sortedDict = dict.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);

            foreach (KeyValuePair<VelibStation, Double> entry in sortedDict)
            {
                if (entry.Key.available_bikes > 0)
                    return entry.Key;
            }
            return null;
        }

        private string reformatData(double x)
        {
            String temp = x.ToString();
            return temp.Replace(",", ".");
        }

        public String getURIOfSegment(
            double dep_lat, double dep_lng,
            double arr_lat, double arr_lng,
            string transportation_way)
        {
            string uri_string = $"https://www.openstreetmap.org/directions?engine=fossgis_osrm_{transportation_way}&route=";
            uri_string += reformatData(dep_lat) + "%2C" + reformatData(dep_lng) + "%3B";
            uri_string += reformatData(arr_lat) + "%2C" + reformatData(arr_lng);
            return uri_string;
        }

        public LocationCollection getSegmentCoordinateList(
            Location dep,
            Location arr,
            string transportation_way)
        {
            //transportation_way = "cycling-regular";
            string uri_string = $"https://api.openrouteservice.org/v2/directions/{transportation_way}/geojson?api_key={API_KEY}";
            WebRequest request = WebRequest.Create(uri_string);
            /*
             {"coordinates":[[8.681495,49.41461],[8.686507,49.41943],[8.687872,49.420318]],"instructions":"false","maneuvers":"true"}
             */
            string postData = "{" +
                "\"coordinates\":["
                    + "[" + reformatData(dep.Longitude) + "," + reformatData(dep.Latitude) + "],"
                    + "[" + reformatData(arr.Longitude) + "," + reformatData(arr.Latitude) + "]"
                    + "]," 
                    + "\"instructions\":\"false\","
                    + "\"maneuvers\":\"true\""
                    +"}";
            byte[] data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            
            response = request.GetResponse();
            string responseFromServer = new StreamReader(response.GetResponseStream()).ReadToEnd();
            ResponseORS responseJSON = JsonConvert.DeserializeObject<ResponseORS>(responseFromServer);

            if (responseJSON == null || responseJSON.Features[0].Geometry.Coordinates == null
                || responseJSON.Features[0].Geometry.Coordinates.Length == 0)
                return null;
            
            double[][] arrayOfCoordinates = responseJSON.Features[0].Geometry.Coordinates;
            LocationCollection res = new LocationCollection();
            for (int i = 0; i < arrayOfCoordinates.Length; ++i)
            {
                if (arrayOfCoordinates[i] != null && arrayOfCoordinates[i].Length == 2)
                {
                    res.Add(new Location(arrayOfCoordinates[i][1], arrayOfCoordinates[i][0]));
                }
            }

            return res;
        }

        public String GetStation(String station)
        {
            return reqX.getStation(station);
        }
    }

    //call this map: https://maps.openrouteservice.org/directions?a=43.814491,7.753606,43.784523,7.495817,43.735205,7.414121&b=0&c=0&k1=en-US&k2=km
    // URI not compatible with IE11 C# webbrowser
}
