using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ServiceY;
using ServiceX;
using ServiceTourism;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;
using System.Linq;


namespace WindowsFormsClient
{
    public partial class Form1 : Form
    {
        private class Welcome
        {
            public string getURIOfSegmentResult
            {
                get;
                set;
            }
        }
        RequestY req;
        RequestT reqTourism;
        List<VelibStation> stations;
        Location nearestStationDepartLocation, nearestStationArriveeLocation;
        VelibStation nearestStationDepart, nearestStationArrivee;
        Location posDepart, posArrivee;
        MainWindow mainWin;
        LocationCollection locationList;
        //double west_lng, east_lng, north_lat, south_lat;

        public Form1()
        {
            InitializeComponent();
            AllowTransparency = true;
            textBoxDebug.Visible = false;
            labelAide.Visible = false;

            req = new RequestY();
            reqTourism = new RequestT();
            mainWin = MainWindow.getInstance();
            bingMapElement.Child = mainWin;
        
            foreach (Contract c in req.getContracts())
                comboboxVille.Items.Add(c.name);
            comboBoxChoiceTourism.Items.Add("OUI");
            comboBoxChoiceTourism.Items.Add("NON");
            checkBoxAlternativeRoute.Visible = false;        

            // Various things to test:
            /* comboboxVille.SelectedIndex = 0;
            textBoxAdresseArrivee.Text = "2 rue nansen";
            textBoxAdresseDepart.Text = "5 rue jussieu"; */

            // testREST();  //if you want to test some REST GET/POST methods
            // testPOST();

            // req.refreshStationList("rouen");
            // textBoxAdresseDepart.Text = "place du vieux marché";
            // textBoxAdresseArrivee.Text = "2 rue de lessard";

            //2 rue de nansen
            //5 rue jussieu //ROUEN
        }
        private void buttonCalcul_Click(object sender, EventArgs e)
        {
            string adresseDepart = textBoxAdresseDepart.Text;
            string adresseArrivee = textBoxAdresseArrivee.Text;
            posDepart = req.geocodingAddress(adresseDepart);
            posArrivee = req.geocodingAddress(adresseArrivee);
            mainWin.bingMap.Children.Clear();
            
            if (posDepart != null && posArrivee != null)
            {
                nearestStationDepart = req.computeNearestStation(posDepart);
                nearestStationArrivee = req.computeNearestStation(posArrivee);
                if (nearestStationDepart != null && nearestStationArrivee != null)
                {
                    labelInfos.Text = "OK:\tAdresse(s) de départ et d'arrivée localisées\n";
                    nearestStationDepartLocation = new Location(
                        nearestStationDepart.position.lat, nearestStationDepart.position.lng);
                    nearestStationArriveeLocation = new Location(
                        nearestStationArrivee.position.lat, nearestStationArrivee.position.lng);

                    locationList = req.getSegmentCoordinateList(
                        posDepart,
                        nearestStationDepartLocation,
                        "foot-walking");
                    mainWin.BuildSegment(locationList, Colors.Blue);

                    locationList = req.getSegmentCoordinateList(
                        nearestStationArriveeLocation, posArrivee,
                        "foot-walking");
                    mainWin.BuildSegment(locationList, Colors.Blue);

                    mainWin.BuildPushPin(new LocationCollection() {
                        posDepart, nearestStationDepartLocation, nearestStationArriveeLocation, posArrivee
                    }, "grey");
                    mainWin.CenterMap(posDepart, posArrivee);

                    if (checkBoxAlternativeRoute.Visible)
                    {
                        buildPolygon();
                    } else
                    {
                        locationList = req.getSegmentCoordinateList(
                        nearestStationDepartLocation, nearestStationArriveeLocation,
                        "cycling-regular");
                        mainWin.BuildSegment(locationList, Colors.Chartreuse);
                    }                               
                } else
                {
                    labelInfos.Text = "ERROR: lors du calcul d'itinéraire :(\n";
                }       
            } else {
                labelInfos.Text = "ERROR:\tAdresse(s) non reconnue\n";
                if (posDepart == null)
                {
                     labelInfos.Text += adresseDepart + "\n";
                } 
                if (posArrivee == null)
                {
                    labelInfos.Text += adresseArrivee + "\n";
                }
            }
        }

        private void comboBoxChoiceTourisme_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxAlternativeRoute.Checked = false;
            if (comboBoxChoiceTourism.SelectedItem.ToString().Equals("OUI"))
                checkBoxAlternativeRoute.Visible = true;
            else if (comboBoxChoiceTourism.SelectedItem.ToString().Equals("NON"))
                checkBoxAlternativeRoute.Visible = false;
        }

        private void buildPolygon()
        {
            LocationCollection placeLocations, tourismItinerary;
            List<Place> placeList, visitedPlaces, nonVisitedPlaces;
            nonVisitedPlaces = new List<Place>();
            visitedPlaces = new List<Place>();
            placeLocations = new LocationCollection();
            double west_lng = Math.Min(nearestStationDepartLocation.Longitude, nearestStationArriveeLocation.Longitude);
            double east_lng = Math.Max(nearestStationDepartLocation.Longitude, nearestStationArriveeLocation.Longitude);
            double south_lat = Math.Min(nearestStationDepartLocation.Latitude, nearestStationArriveeLocation.Latitude);
            double north_lat = Math.Max(nearestStationDepartLocation.Latitude, nearestStationArriveeLocation.Latitude);

            // textBoxDebug.Visible = true;
            // textBoxDebug.Text = "west_lng=" + west_lng + "east_lng=" + east_lng + "south_lat=" + south_lat + "north_lat=" + north_lat;
            
            LocationCollection testPolygon = new LocationCollection() {
                new Location(south_lat, west_lng),
                new Location(south_lat, east_lng),
                new Location(north_lat, east_lng),
                new Location(north_lat, west_lng)
            };
            mainWin.BuildDebugPolygon(testPolygon);

            reqTourism.box = new Box(west_lng, south_lat, east_lng, north_lat);
            reqTourism.updateTourismPlaceList();
            placeList = reqTourism.getPlaceList();  
            placeList.ForEach(place => placeLocations.Add(new Location(place.location.Latitude, place.location.Longitude)));

            tourismItinerary = reqTourism.buildTourismItinerary(
                new Place(nearestStationDepartLocation, "velibDep"),
                new Place(nearestStationArriveeLocation, "velibArr"),
                checkBoxAlternativeRoute.Checked);       
            mainWin.BuildSegment(tourismItinerary, Colors.LightGreen);
           
            foreach (Location loc in tourismItinerary)
            {
                int index = placeList.FindIndex(
                    place =>
                    place.location.Longitude == loc.Longitude && place.location.Latitude == loc.Latitude);
                if (index != -1)
                    visitedPlaces.Add(placeList[index]);
            }
            nonVisitedPlaces = placeList.Where(p => !visitedPlaces.Contains(p)).ToList<Place>();
            var coucou = tourismItinerary;
            mainWin.BuildPushPin(nonVisitedPlaces, Colors.DarkRed, false);
            mainWin.BuildPushPin(visitedPlaces, Colors.Green, true);
        }

        private void comboboxVille_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainWin.bingMap.Children.Clear();
            labelInfos.Text = "Patientez svp...\n";

            textBoxAdresseDepart.Text = "";
            textBoxAdresseArrivee.Text = "";
            checkBoxAlternativeRoute.Checked = false;
            checkBoxAlternativeRoute.Visible = false;
            comboBoxChoiceTourism.ResetText();
            string selectedVille = comboboxVille.SelectedItem.ToString();

            req.refreshStationList(selectedVille);
            stations = req.getAllStations();
            labelInfos.Text = "Liste des stations bien récupérée.\n";
        }


        private void buttonAide_Click(object sender, EventArgs e)
        {
            if (!labelAide.Visible)
            {
                labelAide.Visible = true;
                labelAide.Text =
                    "1.  sélectionnez la ville   Si vous voulez un itinéraire Velib qui passe par des lieux de tourisme,"
                    + " cliquez oui dans la combobox\n"
                    + "2.  rentrez l'adresse de départ et celle de fin de l'itinéraire (doivent être dans la même ville)"
                    + " Cochez \"Alternative Route\" si vous voulez un itinéraire de tourisme alternatif\n"
                    + "3.  cliquez sur \"Calcul itinéraire\"\n"
                    + "4.  Si vous avez demandé un itinéraire velib Classique, les 3 segments pied - vélo - pied"
                    + " s'affichent. Dans le cas où vous avez demandé que l'itinéraire velib passe par des lieux de\n"
                    + "    tourisme, vous voyez en vert clair l'itinéraire de tourisme.\n"
                    + "5.  Si vous survolez un pin, vous pouvez voir son nom s'afficher\n";
            } else
            {
                labelAide.Visible = false;
                labelAide.Text = "";
            }
        }

        private VelibStation getStationWithName(string name)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                if (stations[i].name == name)
                    return stations[i];
            }
            return null;
        }

        private void testREST()
        {
            textBoxDebug.Visible = true;
            string _url = "http://localhost:8733/Design_Time_Addresses/ServiceY/Service1/JSONendpoint/uriOfSegment?"
                + "dep_lat=43%2E5714&dep_lng=1%2E4650&arr_lat=43%2E6073&arr_lng=1%2E4398&transporation_way=bike";
            WebRequest request;
            WebResponse response;
            Stream dataStream;
            StreamReader reader;
            request = WebRequest.Create(_url);
            response = request.GetResponse();

            dataStream = response.GetResponseStream();
            reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Welcome answerJSON = JsonConvert.DeserializeObject<Welcome>(responseFromServer);
            textBoxDebug.Text = answerJSON.getURIOfSegmentResult;
        }

        

        private void testPOST() //IF you run this method, add breakpoint at the end of this method
        {
            
            WebRequest request = WebRequest.Create(
                "http://localhost:8733/Design_Time_Addresses/ServiceY/Service1/JSONendpoint/refreshStationList/toulouse");
            WebResponse response = request.GetResponse();

            request = WebRequest.Create(
                "http://localhost:8733/Design_Time_Addresses/ServiceY/Service1/JSONendpoint/computeNearestStation");
            // { "lat":43.569183,"lng":1.466106}
            // string postData = "lat=" + Uri.EscapeDataString("43.569183");
            // postData += "&lng=" + Uri.EscapeDataString("1.466106");
            string postData = "{\"lat\":43.569183, \"lng\":1.466106}";
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
            VelibStation answerJSON = JsonConvert.DeserializeObject<VelibStation>(responseFromServer);
            
            labelInfos.Text = answerJSON.ToString(); //put breakpoint here if you want to check answerJSON response
        }
    }
}

//[[8.681495,49.41461],[8.686507,49.41943],[8.687872,49.420318]]
//locationList = req.getSegmentCoordinateList(8.681495, 49.41461, 8.687872, 49.420318, "");
/*
mainWin.BuildSegment(locationList);
mainWin.BuildPushPin(new LocationCollection {
    new Location(49.41461, 8.681495),
    new Location(49.420318, 8.687872)
}); */
//1.43922052825561,43.5677165058716,1.46445685202763,43.6074900232999
