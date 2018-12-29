using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Helpers
{
    public class DistanceHelper
    {
        #region Singleton
        private static DistanceHelper instance;

        public static DistanceHelper GetInstance()
        {
            if (instance == null)
            {
                return new DistanceHelper();
            }
            else
            {
                return instance;
            }
        }
        #endregion

        public async Task<double> GetDistance(double pToLongitude, double pToLatitude)
        {
            var position = await GetCurrentPosition();

            double fromLongitude=position.Longitude;
            double fromLatitude = position.Latitude;

            return GetDistanceFromLatLonInKm(fromLongitude,fromLatitude,pToLongitude,pToLatitude);
        }
        public static async Task<Position> GetCurrentPosition()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            Console.WriteLine(output);

            return position;
        }

        double GetDistanceFromLatLonInKm(double lat1,
                                         double lon1,
                                         double lat2,
                                         double lon2)
        {
            var R = 6371d; // Radius of the earth in km
            var dLat = Deg2Rad(lat2 - lat1);  // deg2rad below
            var dLon = Deg2Rad(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
              Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
              Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
            var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            var d = R * c; // Distance in km
            return d;
        }

        double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180d);
        }
    }
}
