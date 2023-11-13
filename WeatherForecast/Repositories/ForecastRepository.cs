using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Config;
using WeatherForecast.OpenWeatherMapModels;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using WeatherForecast.OpenWeatherMapModels;
using WeatherForecast.Repositories;
using Constants = WeatherForecast.Config.Constants;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        [HttpGet]
        WeatherResponse IForecastRepository.GetForecast(string city)
        {
            string IDOWeather = Constants.OPEN_WEATHER_APPID;
            // Connection String
            var client = new RestClient($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&APPID={IDOWeather}");
            
            var request = new RestRequest($"",Method.Get);
            RestResponse response = client.Execute(request);
            

            if (response.IsSuccessful)
            {
                // Deserialize the string content into JToken object
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                // Deserialize the JToken object into our WeatherResponse Class
                return content.ToObject<WeatherResponse>();

            }

            return null;
        }
    }
}