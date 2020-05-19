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
    class RequestT : IRequestT
    {
        private static string API_KEY = "OiY75Ntz5fYsYZyroIju5x96Ezev4kY7f9KcKDTcZ8c";

        private string reformatData(double x)
        {
            String temp = x.ToString();
            return temp.Replace(",", ".");
        }

        public LocationCollection getTourismPlaceList(
            double west_lng, double south_lat,
            double east_lng, double north_lat)
        {
            WebRequest request;
            WebResponse response;
            Stream dataStream;
            StreamReader reader;
            string responseFromServer;
            string uri = $"https://places.ls.hereapi.com/places/v1/discover/around?api_key={API_KEY}";
            uri += "&in=" + reformatData(west_lng) + ","
                + reformatData(south_lat) + ","
                + reformatData(east_lng) + ","
                + reformatData(north_lat);
            request = WebRequest.Create(uri);
            response = request.GetResponse();

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();

            ResponseJSON answerJSON = JsonConvert.DeserializeObject<ResponseJSON>(responseFromServer);
            if (answerJSON == null)
                return null;
            return null;
        }
    }
}
