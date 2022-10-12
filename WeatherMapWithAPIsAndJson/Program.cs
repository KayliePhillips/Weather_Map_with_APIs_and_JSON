using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using static System.Net.WebRequestMethods;


var client = new HttpClient();

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

string API_key = config.GetConnectionString("WeatherAPIKey");

//var city_name = "Oklahoma City";

Console.WriteLine($"********** Welcome to the Weather App **********");
Console.WriteLine();
Console.WriteLine($"What city would you like to know the current temperature?");
var city_name= Console.ReadLine();
Console.WriteLine();
Console.WriteLine($"--------------------------------------------------");

var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid={API_key}&units=imperial";

var weatherResponse = client.GetStringAsync(weatherURL).Result;

var weatherParse = JObject.Parse(weatherResponse);

var tempF = weatherParse["main"]["temp"];
var feelsLike = weatherParse["main"]["feels_like"];
var description = weatherParse["weather"][0]["description"];

Console.WriteLine($"Temperature: {tempF} degrees fahrenheit.");
Console.WriteLine($"Feels like: {feelsLike} degrees fahrenheit.");
Console.WriteLine($"Description: {description}");
Console.WriteLine($"--------------------------------------------------");
Console.WriteLine();
Console.WriteLine();
Console.WriteLine($"Would you like to convert the temperature to celsius?  Yes or No");
var convertAnswer = Console.ReadLine().ToLower();

double tempFD = Convert.ToDouble(tempF);

if (convertAnswer == "yes")
{

    double tempC = (tempFD - 32) * 5 / 9;
    tempC = Math.Round(tempC, 2);
    Console.WriteLine($"Temperature in celsius: {tempC}");
}
else
{
    Console.WriteLine("Have a great day!");
}

