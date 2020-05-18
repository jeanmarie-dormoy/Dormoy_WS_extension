using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ServiceY;
using ServiceX;
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
        List<VelibStation> stations;
        VelibStation stationDepart, stationArrivee;

        private string uri_string;        
        private Boolean segment1, segment2, segment3;
        VelibStation nearestStationDepart, nearestStationArrivee;
        Position posDepart, posArrivee;
        MainWindow mainWin;
            
              
        public Form1()
        {
            InitializeComponent();
            textBoxDebug.Visible = false;
            labelAide.Visible = false;
            req = new RequestY();
            List<Contract> cList = req.getContracts();
            foreach (Contract c in cList)
                comboboxVille.Items.Add(c.name);
            mainWin = MainWindow.getInstance();
            bingMapElement.Child = mainWin;

            var locationList = new LocationCollection
            {
                new Location(43.569781, 1.467381),
                new Location(43.607265, 1.439456)
            };

            //[[8.681495,49.41461],[8.686507,49.41943],[8.687872,49.420318]]
            req.getSegmentCoordinateList(8.681495, 49.41461, 8.687872, 49.420318, "");
            mainWin.BuildSegment(locationList);
            

            

            // testREST();  //if you want to test some REST GET/POST methods
            // testPOST();


            // req.refreshStationList("rouen");
            // textBoxAdresseDepart.Text = "place du vieux marché";
            // textBoxAdresseArrivee.Text = "2 rue de lessard";
            
            /*
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            webBrowserMap.Visible = false; */
        }

        

        private void buttonCalcul_Click(object sender, EventArgs e)
        {
            string adresseDepart = textBoxAdresseDepart.Text,
                adresseArrivee = textBoxAdresseArrivee.Text;
            posDepart = req.geocodingAddress(adresseDepart);
            posArrivee = req.geocodingAddress(adresseArrivee);
            

            if (posDepart != null && posArrivee != null)
            {
                nearestStationDepart = req.computeNearestStation(posDepart);
                nearestStationArrivee = req.computeNearestStation(posArrivee);
                if (nearestStationDepart != null && nearestStationArrivee != null)
                {
                    /*
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    webBrowserMap.Visible = true;*/
                    segment1 = segment2 = segment3 = false;
                    segment1 = true;

                    uri_string = req.getURIOfSegment(
                        posDepart.lat, posDepart.lng,
                        nearestStationDepart.position.lat, nearestStationDepart.position.lng,
                        "foot");
                    labelInfos.Text = uri_string;
                    textBoxDebug.Text = uri_string;
                    /*
                    webBrowserMap.Url = new Uri(uri_string);
                    webBrowserMap.Update();

                    
                    webBrowserMap.Navigate(uri_string); */
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

        private void comboboxVille_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            labelInfos.Text = "Patientez svp...\n";
            string selectedVille = comboboxVille.SelectedItem.ToString();

            req.refreshStationList(selectedVille);
            stations = req.getAllStations();
            labelInfos.Text = "Liste des stations bien récupérée.\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (segment1 == false)
            {
                if (nearestStationArrivee != null && posArrivee != null)
                {
                    segment1 = segment2 = segment3 = false;
                    segment1 = true;

                    uri_string = req.getURIOfSegment(
                        posDepart.lat, posDepart.lng,
                        nearestStationDepart.position.lat, nearestStationDepart.position.lng,
                        "foot");
                    labelInfos.Text = uri_string;
                    textBoxDebug.Text = uri_string;
                    /*
                    webBrowserMap.Url = new Uri(uri_string);
                    webBrowserMap.Update(); */
                }
                else
                {
                    labelInfos.Text = "ERROR: lors du calcul d'itinéraire :(\n";
                }
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (segment2 == false)
            {
                if (nearestStationDepart != null && nearestStationArrivee != null)
                {
                    segment1 = segment2 = segment3 = false;
                    segment2 = true;

                    uri_string = req.getURIOfSegment(
                        nearestStationDepart.position.lat, nearestStationDepart.position.lng,
                        nearestStationArrivee.position.lat, nearestStationArrivee.position.lng,
                        "bike");
                    labelInfos.Text = uri_string;
                    textBoxDebug.Text = uri_string;
                    /*
                    webBrowserMap.Url = new Uri(uri_string);
                    webBrowserMap.Update(); */
                }
                else
                {
                    labelInfos.Text = "ERROR: lors du calcul d'itinéraire :(\n";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (segment3 == false)
            {
                if (nearestStationArrivee != null && posArrivee != null)
                {
                    segment1 = segment2 = segment3 = false;
                    segment3 = true;

                    uri_string = req.getURIOfSegment(
                        nearestStationArrivee.position.lat, nearestStationArrivee.position.lng,
                        posArrivee.lat, posArrivee.lng,
                        "foot");
                    labelInfos.Text = uri_string;
                    textBoxDebug.Text = uri_string;
                    /*
                    webBrowserMap.Url = new Uri(uri_string);
                    webBrowserMap.Update(); */
                }
                else
                {
                    labelInfos.Text = "ERROR: lors du calcul d'itinéraire :(\n";
                }
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
