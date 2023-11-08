using AutoFixture.Xunit2;
using Newtonsoft.Json;
using RealTimeWeatherMonitoringApp.DataFormatManagement;
using RealTimeWeatherMonitoringApp.WeatherManagement;

namespace RealTimeWeatherMonitoringApp.Tests.DataFormatTests
{
    public class JsonFormatStrategyShould
    {
        private readonly JsonFormatStrategy _sut;

        public JsonFormatStrategyShould()
        {
            _sut = new JsonFormatStrategy();
        }

        [Theory]
        [InlineData("<key>", false)]
        [InlineData("{ \"key\": \"value\" }", true)]
        public void ValidateJsonInputFormat_ShouldReturnExpected(string inputData, bool expectedResult)
        {
            bool isValidJson = _sut.ValidateFormat(inputData);
            Assert.Equal(isValidJson, expectedResult);
        }

        [Theory]
        [AutoData]
        public void ExtractDataFromJson_ReturnSuccess(WeatherData validWeatherData)
        {
            var validInput = JsonConvert.SerializeObject(validWeatherData);
            var result = _sut.ExtractData(validInput);
            Assert.True(result.IsSuccess);
            Assert.Equal("Weather data extracted!", result.Message);
        }

        [Theory]
        [AutoData]
        public void ExtractDataFromJson_ReturnFailure(string invalidInputData)
        {
            var result = _sut.ExtractData(invalidInputData);
            Assert.False(result.IsSuccess);
        }
    }
}