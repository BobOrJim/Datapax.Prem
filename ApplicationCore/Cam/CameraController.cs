using System;
using AForge.Video;
using System.Diagnostics;
using AForge.Video.DirectShow;
using System.Drawing;
using Interfaces;

namespace ApplicationCore.Cam
{
    public class CameraController
    {
        public VideoCaptureDevice videoSource { get; set; }  //Denna erbjuder mycket trevligt
        private FilterInfoCollection videoDevices;
        public string pathFolderWork { get; set; }
        public string pathFolderKeep { get; set; }
        private int videoDevicesID { get; set; }
        private string pictureFileNamePrefix { get; set; }
        private PictureController pictureController;
        private Boolean FirstTimeStarted = false;

        public CameraController(IDataAccess iDataAccess, int _videoDevicesID, string _pictureFileNamePrefix)
        {
            try
            {
                videoDevicesID = _videoDevicesID;
                pictureFileNamePrefix = _pictureFileNamePrefix;
                pictureController = new PictureController(iDataAccess, pictureFileNamePrefix);
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    Debug.WriteLine("InKonstruktor videoDevices.Count == 0 ");
                }
                Debug.WriteLine($"InKonstruktor I am : {pictureFileNamePrefix} and my videoDevicesID is {videoDevicesID}");
                Debug.WriteLine($"InKonstruktor videoDevices.Count: {videoDevices.Count} ");
                Debug.WriteLine($"InKonstruktor videoDevices[0].Name: {videoDevices[0].Name}   videoDevices[1].Name: {videoDevices[1].Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in CameraController : CameraController: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in CameraController : CameraController: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void RunCamera(bool _startRun)
        {
            try
            {
                if (_startRun)
                {
                    if (FirstTimeStarted == false)
                    {
                        videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                        videoSource = new VideoCaptureDevice(videoDevices[videoDevicesID].MonikerString);
                        videoSource.NewFrame += new NewFrameEventHandler(NewFrameEventMethod); //Detta event triggar denna funk, när HW fångat en ny frame.
                    }
                    FirstTimeStarted = true;

                    if (videoDevices.Count <= 1)
                    {
                        Debug.WriteLine(" Only one camera is found ");
                    }
                    //Debug.WriteLine($" I am : {pictureFileNamePrefix} and my videoDevicesID is {videoDevicesID}");
                    //Debug.WriteLine($" videoDevices.Count: {videoDevices.Count} ");
                    //Debug.WriteLine($" videoDevices[0].Name: {videoDevices[0].Name}   videoDevices[1].Name: {videoDevices[1].Name}");
                    videoSource.Start();
                }
                else
                {
                    if (videoSource != null && videoSource.IsRunning)
                    {
                        //Debug.WriteLine($" Sending Stop command to camera {pictureFileNamePrefix} with ID {videoDevicesID} ");
                        videoSource.SignalToStop();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in CameraController : RunCamera: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in CameraController : RunCamera: ex.StackTrace = " + ex.StackTrace);
            }
        } //Start stop från UI

        private void NewFrameEventMethod(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = eventArgs.Frame;
                pictureController.SaveBitmapToDBAndToDisk(bitmap, pictureFileNamePrefix, pathFolderWork, pathFolderKeep);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in CameraController : NewFrameEventMethod: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in CameraController : NewFrameEventMethod: ex.StackTrace = " + ex.StackTrace);
            }
        }

    }
}

