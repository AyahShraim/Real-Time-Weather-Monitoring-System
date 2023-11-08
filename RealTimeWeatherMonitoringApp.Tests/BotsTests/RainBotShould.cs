using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using RealTimeWeatherMonitoringApp.BotsManagement;
using RealTimeWeatherMonitoringApp.Utilities;

namespace RealTimeWeatherMonitoringApp.Tests.BotsTests
{
    public class RainBotShould
    {
        private readonly IFixture _fixture;
        private readonly RainBot _sut;
        public RainBotShould()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _sut = _fixture.Build<RainBot>()
                            .With(bot => bot.ActivatedMessage, "Rain Bot Activated")
                            .With(bot => bot.HumidityThreshold, 50.0f)
                            .Create();
        }

        [Theory]
        [InlineAutoData(70.0f, true)]
        [InlineAutoData(50.0f, false)]
        [InlineAutoData(40.0f, false)]
        public void CheckIfActivated_ShouldReturnExpected(float currentHumidity, bool expected)
        {
            _sut.CurrentHumidity = currentHumidity; 
            bool isActivated  = _sut.IsActivate();
            Assert.Equal(expected,isActivated);
        }

        [Theory]
        [AutoData]
        public void Update_ShouldUpdateCurrentHumidity(float currentHumidity)
        {
            _sut.Update(_fixture.Create<float>(), currentHumidity);  
            Assert.Equal(currentHumidity, _sut.CurrentHumidity);
        }

        [Theory]
        [InlineAutoData(70.0f, true)]
        [InlineAutoData(40.0f, false)]
        public void PerformAction_ShouldCallOutPutWriterWhenActivated(float currentHumidity, bool expected )
        {
            _sut.CurrentHumidity = currentHumidity;
            var outputWriterMock = _fixture.Freeze<Mock<IOutputWriter>>();
            _sut._outputWriter = outputWriterMock.Object;
            _sut.PerformAction();
            outputWriterMock.Verify(output => output.WriteLine(It.IsAny<string>()), expected?Times.AtLeastOnce: Times.Never);
        }
    }
}
