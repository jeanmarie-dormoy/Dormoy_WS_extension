using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    [ServiceContract]
    public interface IRequestT
    {
        [OperationContract]
        void updateTourismPlaceList(
            double west_lng, double south_lat,
            double east_lng, double north_lat);

        [OperationContract]
        List<Place> getPlaceList();

        [OperationContract]
        Place computeNearestPlace(Location velibDep, Boolean alternative);
    }
}
