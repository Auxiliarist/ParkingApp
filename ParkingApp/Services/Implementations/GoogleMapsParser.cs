using ParkingApp.Models;
using ParkingApp.Services.Interfaces;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApp.Services.Implementations
{
    public class GoogleMapsParser : IGoogleMapsParser
    {
        public GoogleMapsParser()
        {
        }

        public string BuildDirectionsUrl(Position start, Position end)
        {
            // Origin of route
            string str_origin = "origin=" + start.Latitude + "," + start.Longitude;

            // Destination of route
            string str_dest = "destination=" + end.Latitude + "," + end.Longitude;

            // Api Key
            string key = $"key={Constants.ApiKeys.GOOGLE_MAPS_ANDROID_API_KEY}";

            // Building the parameters to the web service
            string parameters = str_origin + "&" + str_dest + "&" + key;

            // Output format
            string output = "json";

            // Building the url to the web service
            string url = "https://maps.googleapis.com/maps/api/directions/" + output + "?" + parameters;

            return url;
        }

        public string BuildStaticMapUrl(Position start, double radius)
        {
            // Origin of route
            string str_center = "center=" + start?.Latitude + "," + start?.Longitude;

            // Zoom level
            string zoom = "zoom=15";

            // Size of the map
            string size = "size=600x300";

            // Building the parameters to the web service
            string parameters = str_center + "&" + zoom + "&" + size;

            string url = "https://maps.googleapis.com/maps/api/staticmap?" + parameters;

            string url2 = "https://maps.googleapis.com/maps/api/staticmap?center=40.714728,-73.998672&zoom=19&size=600x300";

            return url2;
        }

        public async Task<string> DownloadUrlAsync(string url)
        {
            string data;

            using (WebClient client = new WebClient())
            {
                data = await client.DownloadStringTaskAsync(url);
            }

            return data;
        }

        public async Task<List<List<Dictionary<string, string>>>> ParsePolylinesAsync(string json)
        {
            JsonObject jsonObject;
            List<List<Dictionary<string, string>>> routes = null;

            await Task.Run(() =>
            {
                try
                {
                    jsonObject = JsonObject.Parse(json);

                    // Start parsing data
                    routes = Parse(jsonObject);
                }
                catch (Exception)
                {
                }
            });

            return routes;
        }

        /// <summary>
        ///    Method to decode polyline points Courtesy : http://jeffreysambells.com/2010/05/27/decoding-polylines-from-google-maps-direction-api-with-java 
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        private List<Position> DecodePoly(string encoded)
        {
            List<Position> poly = new List<Position>();

            int index = 0, lat = 0, lng = 0;
            int len = encoded.Length;

            while (index < len)
            {
                int b, shift = 0, result = 0;
                do
                {
                    b = encoded.ElementAt(index++) - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lat += dlat;

                shift = 0;
                result = 0;
                do
                {
                    b = encoded.ElementAt(index++) - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);
                int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lng += dlng;

                Position coords = new Position(lat / 1E5, lng / 1E5);
                poly.Add(coords);
            }

            return poly;
        }

        private List<List<Dictionary<string, string>>> Parse(JsonObject jObject)
        {
            List<List<Dictionary<string, string>>> routes = new List<List<Dictionary<string, string>>>();
            JsonArrayObjects jRoutes = null;
            JsonArrayObjects jLegs = null;
            JsonArrayObjects jSteps = null;

            //try
            //{
            //    var pls = jObject.ArrayObjects("routes");

            // //Traversing all routes for(int i = 0; i < pls.Count; i++) { var plss =
            // pls[i].ArrayObjects("legs"); var path = new List<Dictionary<string, string>>();

            // //Traversing all legs for(int j = 0; j < plss.Count; j++) { } }

            //}
            //catch (Exception e)
            //{
            //}

            try
            {
                jRoutes = jObject.ArrayObjects("routes");

                //Traversing all routes
                foreach (var leg in jRoutes)
                {
                    jLegs = leg.ArrayObjects("legs");
                    var path = new List<Dictionary<string, string>>();

                    //Traversing all legs
                    foreach (var step in jLegs)
                    {
                        jSteps = step.ArrayObjects("steps");

                        //Traversing all steps
                        foreach (var point in jSteps)
                        {
                            string polyline = "";

                            var poly = point.Get<JsonObject>("polyline");
                            polyline = poly.Get("points");

                            List<Position> list = DecodePoly(polyline);

                            //Traversing all points
                            foreach (var position in list)
                            {
                                Dictionary<string, string> hm = new Dictionary<string, string>
                                {
                                    {"lat", Convert.ToString(position.Latitude) },
                                    {"lng", Convert.ToString(position.Longitude) },
                                };
                                path.Add(hm);
                            }
                        }
                        routes.Add(path);
                    }
                }
            }
            catch (Exception)
            {
            }

            return routes;
        }
    }
}
