﻿using Microsoft.Maps.MapControl.WPF;
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
            Dictionary<Place, Double> sortedDict; int t;
            if (placeList.Count == 0)
                return null;
            foreach (Place place in placeList)
            {
                dict[place] = GetDistance(
                    new Place(velibDep.location.Latitude, velibDep.location.Longitude, "velibDep"),
                    place);                    
            }

            sortedDict = dict.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);

            if (alternative)
            {
                t = 0;
                foreach (KeyValuePair<Place, Double> entry in sortedDict)
                {
                    t++;
                    if (t == 2)
                        return entry.Key;
                }
            }

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
            RequestY reqy = new RequestY();
            Place nextPlace = velibDep;
            Place oldPlace = null;
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
                    while (nextPlace != null)
                    {
                        placeList = placeList.Where(p => isPlaceInside(p, box)).ToList();
                        oldPlace = nextPlace;
                        res.Add(oldPlace.location);
                        nextPlace = computeNearestPlace(nextPlace, alternative);

                        if (nextPlace != null)
                        {
                            temp = reqy.getSegmentCoordinateList(
                                oldPlace.location, nextPlace.location, "cycling-regular");
                            foreach (Location loc in temp)
                                res.Insert(res.Count, loc);
                            temp.Clear();
                            box.east_lng = nextPlace.location.Longitude;
                            box.south_lat = nextPlace.location.Latitude;
                        }
                        
                    }
                /*  case:
                 *      A
                 *  B   
                 */
                } else if (velibArr.location.Latitude < velibDep.location.Latitude)
                {
                    while (nextPlace != null)
                    {
                        placeList = placeList.Where(p => isPlaceInside(p, box)).ToList();
                        oldPlace = nextPlace;
                        res.Add(oldPlace.location);
                        nextPlace = computeNearestPlace(nextPlace, alternative);

                        if (nextPlace != null)
                        {
                            temp = reqy.getSegmentCoordinateList(
                                oldPlace.location, nextPlace.location, "cycling-regular");
                            foreach (Location loc in temp)
                                res.Insert(res.Count, loc);
                            temp.Clear();
                            box.east_lng = nextPlace.location.Longitude;
                            box.north_lat = nextPlace.location.Latitude;
                        }

                    }
                }    
            } else if (velibArr.location.Longitude > velibDep.location.Longitude)
            {
                /*  case:
                *      B
                *  A   
                */
                if (velibArr.location.Latitude > velibDep.location.Latitude)
                {
                    while (nextPlace != null)
                    {
                        placeList = placeList.Where(p => isPlaceInside(p, box)).ToList();
                        oldPlace = nextPlace;
                        res.Add(oldPlace.location);
                        nextPlace = computeNearestPlace(nextPlace, alternative);
                        if (nextPlace != null)
                        {
                            temp = reqy.getSegmentCoordinateList(
                                oldPlace.location, nextPlace.location, "cycling-regular");
                            foreach (Location loc in temp)
                                res.Insert(res.Count, loc);
                            temp.Clear();
                            box.west_lng = nextPlace.location.Longitude;
                            box.south_lat = nextPlace.location.Latitude;
                        }
                    }
                }
                /*  case:
                *  A    
                *       B   
                */
                else if (velibArr.location.Latitude < velibDep.location.Latitude)
                {
                    while (nextPlace != null)
                    {
                        placeList = placeList.Where(p => isPlaceInside(p, box)).ToList();
                        oldPlace = nextPlace;
                        res.Add(oldPlace.location);
                        nextPlace = computeNearestPlace(nextPlace, alternative);
                        if (nextPlace != null)
                        {
                            temp = reqy.getSegmentCoordinateList(
                                oldPlace.location, nextPlace.location, "cycling-regular");
                            foreach (Location loc in temp)
                                res.Insert(res.Count, loc);
                            temp.Clear();
                            box.west_lng = nextPlace.location.Longitude;
                            box.north_lat = nextPlace.location.Latitude;
                        }
                    }
                }

            }

            if (oldPlace != null)
            {
                temp.Clear();
                temp = reqy.getSegmentCoordinateList(
                                    oldPlace.location, velibArr.location, "cycling-regular");
                foreach (Location loc in temp)
                    res.Insert(res.Count, loc);
            }

            return res;
        }

        public void updateTourismPlaceList()
        {
            WebRequest request;
            WebResponse response;
            Stream dataStream;
            StreamReader reader;
            string responseFromServer;
            string uri = $"https://places.ls.hereapi.com/places/v1/discover/around?apiKey={API_KEY}";
            uri += "&in=" + reformatData(box.west_lng) + ","
                + reformatData(box.south_lat) + ","
                + reformatData(box.east_lng) + ","
                + reformatData(box.north_lat);
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
