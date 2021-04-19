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
        public VideoCaptureDevice videoSource { get; }  //F12 på denna rackare visar på fler möjligheter som jag bör utnytja.
        public string pathFolderWork { get; set; }
        public string pathFolderKeep { get; set; }
        public int videoDevicesID { get; set; }
        public string pictureFileNamePrefix { get; set; }
        public PictureController pictureController;

        public CameraController(IDataAccess _iDataAccessGeneralTables)
        {
            pictureController = new PictureController(_iDataAccessGeneralTables);
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[videoDevicesID].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(NewFrameEventMethod); //Detta event triggar denna funk, när HW fångat en ny frame.
        }

        public void RunCamera(bool _startRun)
        {
            if (_startRun)
            {
                videoSource.Start();
            }
            else
            {
                videoSource.SignalToStop();
            }
        } //Start stop från UI

        private void NewFrameEventMethod(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = eventArgs.Frame;
            pictureController.SaveBitmapToDBAndToDisk(bitmap, pictureFileNamePrefix, pathFolderWork, pathFolderKeep);
        }

    }
}

