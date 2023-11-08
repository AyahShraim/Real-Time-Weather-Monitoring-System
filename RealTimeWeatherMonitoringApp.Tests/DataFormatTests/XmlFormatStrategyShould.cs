using RealTimeWeatherMonitoringApp.DataFormatManagement;

namespace RealTimeWeatherMonitoringApp.Tests.DataFormatTests
{
    public class XmlFormatStrategyShould
    {
        private readonly XMLFormatStrategy _sut;

        public XmlFormatStrategyShould()
        {
            _sut = new XMLFormatStrategy();
        }

        [Theory]
        [InlineData("<key>", false)]
        [InlineData("<root><key>value</key></root>", true)]
        public void ValidateXmlInputFormat_ShouldReturnExpected(string inputData, bool expectedResult)
        {
            bool isValidXml = _sut.ValidateFormat(inputData);
            Assert.Equal(isValidXml, expectedResult);
        }

        [Theory]
        [InlineData("<WeatherData><Location>Sample Location</Location><Temperature>25.0</Temperature><Humidity>humidity</Humidity></WeatherData>", false)]
        [InlineData("<WeatherData><Location>Sample Location</Location><Temperature>25.0</Temperature><Humidity>50.0</Humidity></WeatherData>", true)]
        public void ExtractDataFromXml_ReturnExpected(string weatherData, bool expectedResult)
        {
            var result = _sut.ExtractData(weatherData);
            Assert.Equal(expectedResult, result.IsSuccess);

        }
    }
}