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

        public void BuildSegment(LocationCollection locationList)
        {

            int i = 1;
            foreach (var location in locationList)
            {
                Pushpin pushpin = new Pushpin()
                {
                    
                    Content = i.ToString(),
                    Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, (byte)i))
                };
                MapLayer.SetPosition(pushpin, location);
                _instance.bingMap.Children.Add(pushpin);
                i++;
            }

            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new SolidColorBrush(Colors.Blue);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            polyline.Locations = locationList;
            _instance.bingMap.Children.Add(polyline);
        }
    }
}
