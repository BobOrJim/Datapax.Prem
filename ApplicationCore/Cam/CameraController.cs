using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using System.Diagnostics;
using AForge.Video.DirectShow;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using Interfaces;

namespace ApplicationCore.Cam
{
    public class CameraController
    {
        public VideoCaptureDevice videoSource { get; set; }  //F12 på denna rackare visar på fler möjligheter som jag bör utnytja.
        FilterInfoCollection videoDevices;
        public string pathFolderWork { get; set; }
        public string pathFolderKeep { get; set; }
        private int videoDevicesID { get; set; }
        private string pictureFileNamePrefix { get; set; }
        public PictureController pictureController;
        public Boolean FirstTimeStarted = false;

        public CameraController(IDataAccess iDataAccess, int _videoDevicesID, string _pictureFileNamePrefix)
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

        public void RunCamera(bool _startRun)
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
        } //Start stop från UI

        private void NewFrameEventMethod(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = eventArgs.Frame;
            pictureController.SaveBitmapToDBAndToDisk(bitmap, pictureFileNamePrefix, pathFolderWork, pathFolderKeep);
        }

    }
}

