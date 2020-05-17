using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ServiceX
{
    [ServiceContract]
    public interface IRequest
    {

        [OperationContract]
        int getCountRefresh();
        [OperationContract]
        List<Contract> getContracts();
        [OperationContract]
        void refreshStationList(string ville);
        [OperationContract]
        String getStation(String keyword);
        [OperationContract]
        List<VelibStation> getAllStations();
        [OperationContract]
        void closeConnection();
    }
}
