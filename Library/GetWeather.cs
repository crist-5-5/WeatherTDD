using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GetWeather
    {
        public static string searchURL = "https://www.metaweather.com/api/location/search/?query=";
        public static string locationURL = "https://www.metaweather.com/api/location/";

        public static string FetchWebsite(string input)
        {
            string ret = "";
            using (var client = new WebClient())
            using (var stream = client.OpenRead(input))
            using (var textReader = new StreamReader(stream, Encoding.UTF8, true))
            {
                ret = textReader.ReadToEnd();
            }
            return ret;
        }

        public static List<Search> Search(string input)
        {
            if (input == "")
                return new List<Search>();
            string content = FetchWebsite(searchURL + input);
            return JsonConvert.DeserializeObject<List<Search> >(content);
        }

        public static RootobjectLocation GetLocation(int woeid)
        {
            string content = "";
            try
            {
                content = FetchWebsite(locationURL + woeid.ToString());
            }
            catch (System.Net.WebException e)
            {
                if (e.Message.Contains("404"))
                    return new RootobjectLocation();
                throw new System.ArgumentException("Unhandled connection error", "original");
            }

            return JsonConvert.DeserializeObject<RootobjectLocation>(content);
        }
    }
}

public class RootobjectLocation
{
    public Consolidated_Weather[] consolidated_weather { get; set; }
    public DateTime time { get; set; }
    public DateTime sun_rise { get; set; }
    public DateTime sun_set { get; set; }
    public string timezone_name { get; set; }
    public Parent parent { get; set; }
    public Source[] sources { get; set; }
    public string title { get; set; }
    public string location_type { get; set; }
    public int woeid { get; set; }
    public string latt_long { get; set; }
    public string timezone { get; set; }
}

public class Parent
{
    public string title { get; set; }
    public string location_type { get; set; }
    public int woeid { get; set; }
    public string latt_long { get; set; }
}

public class Consolidated_Weather
{
    public long id { get; set; }
    public string weather_state_name { get; set; }
    public string weather_state_abbr { get; set; }
    public string wind_direction_compass { get; set; }
    public DateTime created { get; set; }
    public string applicable_date { get; set; }
    public float min_temp { get; set; }
    public float max_temp { get; set; }
    public float the_temp { get; set; }
    public float wind_speed { get; set; }
    public float wind_direction { get; set; }
    public float air_pressure { get; set; }
    public int humidity { get; set; }
    public float visibility { get; set; }
    public int predictability { get; set; }
}

public class Source
{
    public string title { get; set; }
    public string slug { get; set; }
    public string url { get; set; }
    public int crawl_rate { get; set; }
}


public class RootobjectSearch
{
    public Search[] Property1 { get; set; }
}

public class Search
{
    public string title { get; set; }
    public string location_type { get; set; }
    public int woeid { get; set; }
    public string latt_long { get; set; }
}
