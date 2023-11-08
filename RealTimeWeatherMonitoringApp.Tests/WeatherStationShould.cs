using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using RealTimeWeatherMonitoringApp.BotsManagement;
using RealTimeWeatherMonitoringApp.WeatherManagement;


namespace RealTimeWeatherMonitoringApp.Tests
{
    public class WeatherStationShould
    {
        private readonly IFixture _fixture;
        private readonly WeatherStation _sut;

        public WeatherStationShould()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _sut = _fixture.Create<WeatherStation>();
        }
        [Fact]
        public void AddSubscriber_ShouldAddSubscriberToList()
        {
            var mockSubscriber1 = new Mock<IWeatherDataSubscriber>();
            var mockSubscriber2 = new Mock<IWeatherDataSubscriber>();
            _sut.AddSubscriber(mockSubscriber1.Object);
            _sut.AddSubscriber(mockSubscriber2.Object);
            Assert.Contains(mockSubscriber1.Object, _sut.GetSubscribers());
            Assert.Contains(mockSubscriber2.Object, _sut.GetSubscribers());
        }

        [Fact]
        public void RemoveSubscriber_ShouldRemoveSubscriberFromList()
        {
            var mockSubscriber = new Mock<IWeatherDataSubscriber>();
            _sut.RemoveSubscriber(mockSubscriber.Object);
            Assert.DoesNotContain(mockSubscriber.Object, _sut.GetSubscribers());
        }

        [Fact]
        public void SetWeatherStationState_ShouldUpdateStateAndNotifySubscribers()
        {
            var temperature = _fixture.Create<float>();
            var humidity = _fixture.Create<float>();
            var mockSubscriber = new Mock<IWeatherDataSubscriber>();
            _sut.AddSubscriber(mockSubscriber.Object);
            _sut.SetWeatherStationState(temperature, humidity);
            mockSubscriber.Verify(subscriber => subscriber.Update(temperature, humidity), Times.Once);
        }
    }
}
