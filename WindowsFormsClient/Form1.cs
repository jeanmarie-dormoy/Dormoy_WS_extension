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
        double west_lng, east_lng, north_lat, south_lat;

        public Form1()
        {
            InitializeComponent();
            textBoxDebug.Visible = false;
            labelAide.Visible = false;
            req = new RequestY();
            reqTourism = new RequestT();
            List<Contract> cList = req.getContracts();
            foreach (Contract c in cList)
                comboboxVille.Items.Add(c.name);
            comboBoxChoiceTourism.Items.Add("OUI");
            comboBoxChoiceTourism.Items.Add("NON");
            checkBoxAlternativeRoute.Visible = false;
            mainWin = MainWindow.getInstance();
            bingMapElement.Child = mainWin;
            this.AllowTransparency = true;

            locationList = new LocationCollection
            {
                new Location(43.569781, 1.467381),
                new Location(43.607265, 1.439456)
            };

            comboboxVille.SelectedIndex = 1;
            textBoxAdresseDepart.Text = "135 avenue de rangueil";
            textBoxAdresseArrivee.Text = "5 place du peyrou";
            buttonCalcul_Click(null, null);
            buildPolygon();

            // testREST();  //if you want to test some REST GET/POST methods
            // testPOST();

            // req.refreshStationList("rouen");
            // textBoxAdresseDepart.Text = "place du vieux marché";
            // textBoxAdresseArrivee.Text = "2 rue de lessard";
        }

        

        private void buttonCalcul_Click(object sender, EventArgs e)
        {
            string adresseDepart = textBoxAdresseDepart.Text;
            string adresseArrivee = textBoxAdresseArrivee.Text;
            posDepart = req.geocodingAddress(adresseDepart);
            posArrivee = req.geocodingAddress(adresseArrivee);
            
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
                    mainWin.BuildSegment(locationList);
                    
                    locationList = req.getSegmentCoordinateList(
                        nearestStationDepartLocation, nearestStationArriveeLocation, 
                        "cycling-regular");
                    mainWin.BuildSegment(locationList);

                    locationList = req.getSegmentCoordinateList(
                        nearestStationArriveeLocation, posArrivee,
                        "foot-walking");
                    mainWin.BuildSegment(locationList);

                    mainWin.BuildPushPin(new LocationCollection() {
                        posDepart, nearestStationDepartLocation, nearestStationArriveeLocation, posArrivee
                    }, "grey");
                    mainWin.CenterMap(posDepart, posArrivee);                        
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
            //1.43922052825561,43.5677165058716,1.46445685202763,43.6074900232999
            west_lng = Math.Min(nearestStationDepartLocation.Longitude, nearestStationArriveeLocation.Longitude);
            east_lng = Math.Max(nearestStationDepartLocation.Longitude, nearestStationArriveeLocation.Longitude);
            south_lat = Math.Min(nearestStationDepartLocation.Latitude, nearestStationArriveeLocation.Latitude);
            north_lat = Math.Max(nearestStationDepartLocation.Latitude, nearestStationArriveeLocation.Latitude);

            textBoxDebug.Visible = true;
            textBoxDebug.Text = "west_lng=" + west_lng + "east_lng=" + east_lng + "south_lat=" + south_lat + "north_lat=" + north_lat;
            LocationCollection testPolygon = new LocationCollection() {
                new Location(south_lat, west_lng),
                new Location(south_lat, east_lng),
                new Location(north_lat, east_lng),
                new Location(north_lat, west_lng)
            };
            mainWin.BuildDebugPolygon(testPolygon);
            reqTourism.updateTourismPlaceList(west_lng, south_lat, east_lng, north_lat);
            List<Place> placeList = reqTourism.getPlaceList();

            LocationCollection placeLocations = new LocationCollection();
            placeList.ForEach(place => placeLocations.Add(new Location(place.location.Latitude, place.location.Longitude)));
            mainWin.BuildPushPin(placeList, "green");

        }

        private void comboboxVille_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            labelInfos.Text = "Patientez svp...\n";
            string selectedVille = comboboxVille.SelectedItem.ToString();

            req.refreshStationList(selectedVille);
            stations = req.getAllStations();
            labelInfos.Text = "Liste des stations bien récupérée.\n";
        }


        private void buttonAide_Click(object sender, EventArgs e)
        {
            labelAide.Visible = true;
            labelAide.Text =
                "1.  sélectionnez la ville\n"
                + "2.  rentrez le départ et la fin de l'itinéraire (doivent être dans la même ville)\n"
                + "3.  cliquez sur calculez itinéraire\n"
                + "4.  l'application affiche par défaut le premier segment pédestre de l'itinéraire\n"
                + "    vous pouvez cliquez sur les boutons correspondant pour visualiser chacun des\n"
                + "    segments de l'itinéraire";
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
