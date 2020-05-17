using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceX;

namespace ConsoleClientTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Request req = new Request();
            List<Contract> cList = req.getContracts();

            foreach (Contract c in cList)
                Console.WriteLine(c.name);
            Console.Write("Choose a city:\t");
            string city = Console.ReadLine();

            req.refreshStationList(city);
            List<VelibStation> stations = req.getAllStations();
            foreach (VelibStation v in stations)
                Console.WriteLine(v.name);


            Console.Write("Choose a station:\t");
            string stationName = Console.ReadLine();

            while (stationName != "exit")
            {
                string stationInfos = req.getStation(stationName);
                Console.WriteLine(stationInfos);
                Console.WriteLine("coutRefresh=\t" + req.getCountRefresh());
                Console.Write("Choose a station:\t");
                stationName = Console.ReadLine();
            }



            req.closeConnection();

            Console.ReadLine();


            /*
                MaJ dynamiquement toutes les x minutes pour avoir une liste de stations a jour
                select ville, ensuite select station, ensuite get les infos de la station
             */
        }
    }
}
