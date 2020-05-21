using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ServiceTourism
{
    [ServiceContract]
    public interface IRequestT
    {
        [OperationContract]
        void updateTourismPlaceList();

        [OperationContract]
        List<Place> getPlaceList();

        [OperationContract]
        Place computeNearestPlace(Place velibDep, Boolean alternative);

        [OperationContract]
        LocationCollection buildTourismItinerary(Place velibDep, Place velibArr, Boolean alternative); 
    }

    [DataContract]
    public class Place
    {
        [DataMember]
        public Location location;
        [DataMember]
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

        public bool equals(Place other)
        {
            return this.location.Longitude == other.location.Longitude
                && this.location.Latitude == other.location.Latitude
                && this.title.Equals(other.title);
        }
    }
}
