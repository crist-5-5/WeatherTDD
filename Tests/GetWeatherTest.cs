using Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class GetWeatherTest
    {
        [Test]
        public void OkTest()
        {
            Assert.Pass();
        }

        [Test]
        public void CheckConnection()
        {
            //Arrange
            string input = "https://www.metaweather.com/api/location/search/?query=Bucharest";
            string content = "[{\"title\":\"Bucharest\",\"location_type\":\"City\",\"woeid\":868274,\"latt_long\":\"44.434200,26.102970\"}]";
            //Act
            string output = GetWeather.FetchWebsite(input).Replace(" ", "");
            //Assert
            if (!content.Equals(output))
                Assert.Warn("API structure may have changed!");
        }

        [Test]
        public void CheckDeserialization()
        {
            //Arange
            string input = "Bucharest";
            //Act
            Search searchResult = GetWeather.Search(input)[0];
            RootobjectLocation location = GetWeather.GetLocation(searchResult.woeid);
            //Assert
            Assert.AreEqual("Romania", location.parent.title);
            Assert.AreEqual("Bucharest", location.title);
        }

        [Test]
        public void SearchCity()
        {
            //Arange
            string input = "yo";
            //Act
            List<Search> searchResult = GetWeather.Search(input);
            //Assert
            Assert.AreEqual("Yokohama", searchResult[2].title);
            Assert.AreEqual("York", searchResult[6].title);
            Assert.AreEqual(7, searchResult.Count);
        }

        [Test]
        public void emptySearch()
        {
            //Arange
            string input = "";
            //Act
            List<Search> searchResult = GetWeather.Search(input);
            //Assert
        }

        [Test]
        public void gibberishSearch()
        {
            //Arange
            string input = "aslrgo23q590q3ngbg";
            //Act
            List<Search> searchResult = GetWeather.Search(input);
            //Assert
        }

        [Test]
        public void UrlInjectionSearch()
        {
            //Arange
            string input = "buc/ro";
            //Act
            List<Search> searchResult = GetWeather.Search(input);
            //Assert
        }

        [Test]
        public void invalidWoeid()
        {
            //Arange
            string input = "Bucharest";
            //Act
            RootobjectLocation location = GetWeather.GetLocation(99999999);
            //Assert
        }
    }
}
