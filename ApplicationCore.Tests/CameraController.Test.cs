using System;
using System.IO;
using System.Threading;
using Xunit;
using ApplicationCore.Cam;
using GlobalStringsReadOnly;
using Infrastructure;
//https://xunit.net/docs/comparisons


//Klämer på xUnit

namespace Presentation.Tests
{
    public class CameraControllerTest
    {
        private readonly DataAccess dataAccess;


        [Fact]
        public void TrueIsTrue()
        {
            Assert.True(true);
        }

        //MethodName_StateOrActionInTest_ExpectedBehavior
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void RunCamera_StartingCamera_CammeraIsRunning(Boolean startCamera, bool expected)
        {
            //Arrange
            CameraController camera1 = new CameraController(dataAccess, 0, "Camera1"); //Note, dataacces is not used here, i.e its ok that it is null.

            // Act
            camera1.RunCamera(startCamera);

            // Assert
            bool actual = camera1.videoSource.IsRunning;
            Assert.Equal(expected, actual);
        }
    }
}






