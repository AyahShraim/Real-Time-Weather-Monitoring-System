
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using Moq;
using RealTimeWeatherMonitoringApp.BotsManagement;
using RealTimeWeatherMonitoringApp.Utilities;

namespace RealTimeWeatherMonitoringApp.Tests.BotsTests
{
    public class SunBotShould
    {
        private readonly IFixture _fixture;
        private readonly SunBot _sut;
        public SunBotShould()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _sut = _fixture.Build<SunBot>()
                            .With(bot => bot.ActivatedMessage, "Sun Bot Activated")
                            .With(bot => bot.TemperatureThreshold, 70.0f)
                            .Create();
        }

        [Theory]
        [InlineAutoData(90.0f, true)]
        [InlineAutoData(70.0f, false)]
        [InlineAutoData(60.0f, false)]
        public void CheckIfActivated_ShouldReturnExpected(float currentTemp, bool expected)
        {
            _sut.CurrentTemperature = currentTemp;
            bool isActivated = _sut.IsActivate();
            Assert.Equal(expected, isActivated);
        }

        [Theory]
        [AutoData]
        public void Update_ShouldUpdateCurrentTemperature(float currentTemp)
        {
            _sut.Update(currentTemp, _fixture.Create<float>());
            Assert.Equal(currentTemp, _sut.CurrentTemperature);
        }

        [Theory]
        [InlineAutoData(80.0f, true)]
        [InlineAutoData(60.0f, false)]
        public void PerformAction_ShouldCallOutPutWriterWhenActivated(float currentTemp, bool expected)
        {
            _sut.CurrentTemperature = currentTemp;
            var outputWriterMock = _fixture.Freeze<Mock<IOutputWriter>>();
            _sut._outputWriter = outputWriterMock.Object;
            _sut.PerformAction();
            outputWriterMock.Verify(output => output.WriteLine(It.IsAny<string>()), expected ? Times.AtLeastOnce : Times.Never);
        }
    }
}

