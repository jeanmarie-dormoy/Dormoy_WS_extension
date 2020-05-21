using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using ServiceTourism;

namespace WindowsFormsClient
{
    
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        private static MainWindow _instance;
        public static MainWindow getInstance()
        {
            if (_instance == null)
            {
                _instance = new MainWindow();
                _instance.bingMap.Mode = new RoadMode();
            }
            return _instance;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void BuildPushPin(LocationCollection locationList, string color)
        {
            Color colorObj;
            switch (color)
            {
                case "green":
                    colorObj = Color.FromArgb(200, 20, 180, (byte) 0);
                    break;
                case "grey":
                default:
                    colorObj = Color.FromArgb(100, 100, 100, (byte) 0);
                    break;
            }
            int i = 1;
            foreach (var location in locationList)
            {
                colorObj.B = (byte) i;
                Pushpin pushpin = new Pushpin()
                {
                    Content = i.ToString(),
                    Background = new SolidColorBrush(colorObj)
                };
                MapLayer.SetPosition(pushpin, location);
                _instance.bingMap.Children.Add(pushpin);
                i++;
            }
        }

        public void BuildPushPin(List<Place> placeList, string color)
        {
            Color colorObj;
            switch (color)
            {
                case "green":
                    colorObj = Color.FromArgb(200, 20, 180, (byte)0);
                    break;
                case "grey":
                default:
                    colorObj = Color.FromArgb(100, 100, 100, (byte)0);
                    break;
            }
            int i = 1;
            foreach (var place in placeList)
            {
                colorObj.B = (byte)i;
                ToolTip infos = new ToolTip();
                infos.Content = place.title;
                infos.Width = 170;
                Pushpin pushpin = new Pushpin()
                {
                    //Content = place.title,
                    Background = new SolidColorBrush(colorObj),
                    ToolTip = infos
                };
                //pushpin.Template = (ControlTemplate) FindResource("PushPinTemplate");
                MapLayer.SetPosition(pushpin, place.location);
                _instance.bingMap.Children.Add(pushpin);
                i++;
            }
        }

        public void BuildDebugPolygon(LocationCollection locationList)
        {
            MapPolygon polygon = new MapPolygon();
            polygon.Fill = new SolidColorBrush(Colors.Blue);
            polygon.Stroke = new SolidColorBrush(Colors.Green);
            polygon.StrokeThickness = 2;
            polygon.Opacity = 0.1;
            polygon.Locations = locationList;
            _instance.bingMap.Children.Add(polygon);
        }

        public void CenterMap(Location begin, Location end)
        {
            Location middle = new Location(
                (begin.Latitude + end.Latitude) / 2,
                (begin.Longitude + end.Longitude) / 2);
            _instance.bingMap.Center = middle;
            _instance.bingMap.ZoomLevel = 15;
        }

        public void BuildSegment(LocationCollection locationList, Color color)
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new SolidColorBrush(color);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            polyline.Locations = locationList;
            _instance.bingMap.Children.Add(polyline);
        }

        public void BuildSegment(List<Place> placeList)
        {
            LocationCollection locationList = new LocationCollection();
            foreach (Place p in placeList)
                locationList.Add(p.location);

            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new SolidColorBrush(Colors.Aquamarine);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            polyline.Locations = locationList;
            _instance.bingMap.Children.Add(polyline);

        }
    }
}
