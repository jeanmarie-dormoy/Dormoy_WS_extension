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
using System.Timers;

namespace ServiceX
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
            ConcurrencyMode = ConcurrencyMode.Single)]
    public class Request : IRequest
    {
        private int countRefresh = 0;
        private static Timer timer;
        private static string API_KEY = "6640c674c48152feb1dbda25bcc299ff5f09d8c8";
        private static string contract;
        private string url;


        public List<VelibStation> stationList;
        public List<Contract> contractList;

        WebRequest request;
        WebResponse response;
        Stream dataStream;
        StreamReader reader;
        string responseFromServer;

        public int getCountRefresh() { return countRefresh; }
        public Request()
        {
            timer = new Timer(60000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
            string urlContracts = $"https://api.jcdecaux.com/vls/v3/contracts?apiKey={API_KEY}";
            request = WebRequest.Create(urlContracts);
            response = request.GetResponse();

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);

            responseFromServer = reader.ReadToEnd();

            contractList = JsonConvert.DeserializeObject<List<Contract>>(responseFromServer);

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (contract != null && contract.Length > 0)
            {
                this.refreshStationList(contract);
            }
        }

        public void refreshStationList(string ville)
        {
            countRefresh += 1;
            contract = ville.ToUpper();

            url = $"https://api.jcdecaux.com/vls/v1/stations?contract={contract}&apiKey={API_KEY}";
            request = WebRequest.Create(url);
            response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);

            responseFromServer = reader.ReadToEnd();

            stationList = JsonConvert.DeserializeObject<List<VelibStation>>(responseFromServer);
            return;
        }

        public List<Contract> getContracts()
        {
            var res = new List<Contract>();
            foreach (Contract c in contractList)
            {
                if (c.country_code == "FR")
                {
                    c.name = c.name.ToUpper();
                    res.Add(c);
                }
            }
            return res;
        }



        public String getStation(String keyword)
        {
            return stationList.Find(s => s.name.Contains(keyword.ToUpper())).ToString();
        }
        public List<VelibStation> getAllStations()
        {
            return stationList;
        }
        public void closeConnection()
        {
            reader.Close();
            response.Close();
        }
    }
}
