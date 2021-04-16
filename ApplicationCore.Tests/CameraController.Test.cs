//using System;
//using System.IO;
//using System.Threading;
//using Xunit;
//using ApplicationCore.Cam;


//OBS: jAG SKALL separera xUnit och Moq i olika filer.



//namespace Presentation.Tests
//{
//    public class CameraControllerTest
//    {

//        [Fact]
//        public void FlushPictureFolder_FlushPictureFolder_PictureFolderIsEmpty()
//        {

//            // Arrange
//            CameraController camera1 = new CameraController()
//            {
//                pathFolder = GlobalReadOnlyStrings.pathCamera1Folder,
//                pictureFileNamePrefix = "Camera1",
//                videoDevicesID = 0
//            };

//            // Act
//            camera1.FlushPictureFolder(true);

//            // Assert
//            Assert.Empty(Directory.GetFiles(camera1.pathFolder, "*", SearchOption.AllDirectories));
//        }
        
//        [Theory]
//        [InlineData(true, true)]
//        [InlineData(false, false)]
//        //[InlineData(false, true)]

//        public void RunCamera_StartCamera_CammeraIsRunning(Boolean startCamera, bool expected)
//        {
//            // Arrange
//            CameraController camera1 = new CameraController()
//            {
//                pathFolder = GlobalReadOnlyStrings.pathCamera1Folder,
//                pictureFileNamePrefix = "Camera1",
//                videoDevicesID = 0
//            };

//            // Act
//            camera1.RunCamera(startCamera);

//            // Assert
//            bool actual = camera1.videoSource.IsRunning;
//            //Thread.Sleep(500);

//            Assert.Equal(expected, actual);
//        }
//    }
//}
