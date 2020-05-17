using System;
using System.Collections.Generic;
using ServiceX;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServiceY
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IRequestY
    {
        [OperationContract]
        [WebGet(UriTemplate = "contracts",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Contract> getContracts();

        [OperationContract]
        [WebGet(UriTemplate = "refreshStationList/{city}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void refreshStationList(String city);

        [OperationContract]
        [WebGet(UriTemplate = "stations",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<VelibStation> getAllStations();

        [OperationContract]
        [WebGet(UriTemplate = "getStation/{station}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        String GetStation(String station);

        [OperationContract]
        [WebGet(UriTemplate = "geocoding/{location}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Position geocodingAddress(String location);

        [OperationContract]
        [WebInvoke(
            UriTemplate = "computeNearestStation",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        VelibStation computeNearestStation(Position pos);

        [OperationContract]
        [WebGet(UriTemplate = "uriOfSegment?dep_lat={dep_lat}&dep_lng={dep_lng}&arr_lat={arr_lat}&arr_lng={arr_lng}&transporation_way={transportation_way}",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getURIOfSegment(
            double dep_lat, double dep_lng,
            double arr_lat, double arr_lng,
            string transportation_way);
    }
}