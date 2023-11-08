using AutoFixture.Xunit2;
using RealTimeWeatherMonitoringApp.Utilities.ResultHnadler;

namespace RealTimeWeatherMonitoringApp.Tests
{
    public class OperationResultShould
    {

        [Theory]
        [AutoData]
        public void CreateSuccessMessageOperationResult(string msg)
        {
            var result = OperationResult.SuccessResult(msg);
            Assert.True(result.IsSuccess); 
            Assert.Equal(msg, result.Message);     
        }

        [Theory]
        [AutoData]
        public void CreateFailureMessageOperationResult(string msg)
        {
            var result = OperationResult.FailureResult(msg);
            Assert.False(result.IsSuccess);
            Assert.Equal(msg, result.Message);
        }

        [Theory]
        [AutoData]
        public void CreateSuccessDataOperationResult(string msg, object data)
        {
            var result = OperationResult.SuccessDataMessage(msg, data);
            Assert.True(result.IsSuccess);
            Assert.Equal(msg, result.Message);
            Assert.Equal(data, result.Data);
        }

        [Theory]
        [AutoData]
        public void CreateFailureDataOperationResult(string msg, object data)
        {
            var result = OperationResult.FailureDataMessage(msg, data);
            Assert.False(result.IsSuccess);
            Assert.Equal(msg, result.Message);
            Assert.Equal(data, result.Data);
        }
    }
}
