using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TransportLines.GeocodeService;
using TransportLines.RouteService;

namespace TransportLines.View
{
    /// <summary>
    /// Interaction logic for SearchResult.xaml
    /// </summary>
    public partial class SearchResult : Window
    {
        string key = "AsZ4AF2YP5GKxkR54lIXGeVrqYs_wCMEnNS4vARiOllTAyj8JyFM0xiM20xDjivs";
        public SearchResult()
        {
            InitializeComponent();
            Search();
        }
        public void Search()
        {
            TransportVM transport = new TransportVM();
            List<string> all = transport.getallstrings();
            double[] ot = GeocodeAddress("София");
            double[] to = GeocodeAddress("Варна");
            OfferDetails.Content = CreateRoute(ot[0], ot[1], to[0], to[1]);


            // OfferDetails.Content = to[0] + " " + to[1];
        }
        public Double[] GeocodeAddress(string address)
        {

            double[] re;

            GeocodeRequest geocodeRequest = new GeocodeRequest();
            re = new double[2];
            // Set the credentials using a valid Bing Maps key
            geocodeRequest.Credentials = new Microsoft.Maps.MapControl.WPF.Credentials();
            geocodeRequest.Credentials.ApplicationId = key;

            // Set the full address query
            geocodeRequest.Query = address;

            // Set the options to only return high confidence results 
            ConfidenceFilter[] filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = GeocodeService.Confidence.High;

            // Add the filters to the options
            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeOptions.Filters = filters;
            geocodeRequest.Options = geocodeOptions;

            // Make the geocode request
            GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeResponse geocodeResponse = geocodeService.Geocode(geocodeRequest);

            if (geocodeResponse.Results.Length > 0)
            {

                re[0] = geocodeResponse.Results[0].Locations[0].Latitude;
                re[1] = geocodeResponse.Results[0].Locations[0].Longitude;
                return re;
            }
            else
            {
                re[0] = 0.0;
                re[1] = 0.0;
                return re;
            }
            return re;
        }


        private string CreateRoute(double ot0, double ot1, double to0, double to1)
        {
            string results = "";
            RouteRequest routeRequest = new RouteRequest();

            // Set the credentials using a valid Bing Maps key
            routeRequest.Credentials = new Microsoft.Maps.MapControl.WPF.Credentials();
            routeRequest.Credentials.ApplicationId = key;

            //Parse user data to create array of waypoints

            Waypoint[] waypoints = new Waypoint[2];




            waypoints[0] = new Waypoint();

            waypoints[0].Location = new RouteService.Location();

            waypoints[0].Location.Latitude = ot0;
            waypoints[0].Location.Longitude = ot1;
            waypoints[1] = new Waypoint();

            waypoints[1].Location = new RouteService.Location();

            waypoints[1].Location.Latitude = to0;
            waypoints[1].Location.Longitude = to1;




            routeRequest.Waypoints = waypoints;

            // Make the calculate route request
            RouteServiceClient routeService = new RouteServiceClient("BasicHttpBinding_IRouteService");
            RouteResponse routeResponse = routeService.CalculateRoute(routeRequest);
            MapPolyline mapPolyline = new MapPolyline();


            // mapPolyline. = routeResponse.Result.RoutePath;
            //  mapPolyline.StrokeColor = Colors.Black;
            //  mapPolyline.StrokeThickness = 3;
            //  mapPolyline.StrokeDashed = true;
            MapRoute.Children.Add(mapPolyline);
            // Iterate through each itinerary item to get the route directions
            StringBuilder directions = new StringBuilder("");

            if (routeResponse.Result.Legs.Length > 0)
            {
                int instructionCount = 0;
                int legCount = 0;

                foreach (RouteLeg leg in routeResponse.Result.Legs)
                {
                    legCount++;
                    directions.Append(string.Format("Leg #{0}\n", legCount));

                    foreach (ItineraryItem item in leg.Itinerary)
                    {
                        instructionCount++;
                        directions.Append(string.Format("{0}. {1}\n",
                            instructionCount, item.Text));
                    }
                }
                //Remove all Bing Maps tags around keywords.  
                //If you wanted to format the results, you could use the tags
                Regex regex = new Regex("<[/a-zA-Z:]*>",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);
                results = regex.Replace(directions.ToString(), string.Empty);
            }
            else
                results = "No Route found";

            return results;
        }
    }
}
