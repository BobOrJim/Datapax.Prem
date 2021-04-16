﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using System.IO;
using System.Drawing.Imaging;

namespace ApplicationCore.Cam
{
    //asfasdf
    public class PictureController
    {
        private int unixTimeSeconds;
        private int unixTimeMinutes;
        private int unixTimeSecondsMod60;
        private List<PictureSampleModel> pictureSamples;
        public IDataAccess dataAccessGeneralTables;

        public PictureController(IDataAccess _iDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void SaveBitmapToDBAndToDisk(Bitmap bitmap, string pictureFileNamePrefix, string pathFolderWork, string pathFolderKeep)
        {

            #region //#region will create and init a pictureSampleModel

            PictureSampleModel pictureSampleModel = new PictureSampleModel()
            {
                PictureFileNamePrefix_TEXT = pictureFileNamePrefix, //Detta ger filnamn: pictureFileNamePrefix_TEXT + UnixTime + FileEnding_TEXT
                FilePathCurrent_TEXT = pathFolderWork,
                FileNameCurrent_TEXT = "",
                FileEndingCurrent_TEXT = ".jpeg",
                FilePathWork_TEXT = pathFolderWork,
                FileNameWork_TEXT = "",
                FileEndingWork_TEXT = ".jpeg",
                FilePathKeep_TEXT = pathFolderKeep,
                FileNameKeep_TEXT = "",
                FileEndingKeep_TEXT = ".jpeg"
            };
            pictureSampleModel.FileNameCurrent_TEXT = pictureSampleModel.PictureFileNamePrefix_TEXT + "_" +
                pictureSampleModel.Timestamp_unix_BIGINT.ToString();

            pictureSampleModel.FileNameWork_TEXT = pictureSampleModel.PictureFileNamePrefix_TEXT + "_" +
                pictureSampleModel.Timestamp_unix_BIGINT.ToString();

            pictureSampleModel.FileNameKeep_TEXT = pictureSampleModel.PictureFileNamePrefix_TEXT + "_" +
                pictureSampleModel.Timestamp_unix_BIGINT.ToString();
            #endregion
            
            updateLocalTimeParams(); //Ty jag vill att båda metoder nedan använder exakt samma tidsstämpel.
            SaveToDB(pictureSampleModel);
            SaveToDisk(bitmap, pictureSampleModel);
        }

        private void SaveToDB(PictureSampleModel pictureSampleModel)
        {
            pictureSamples = new List<PictureSampleModel> { pictureSampleModel };

            if (unixTimeMinutes % 2 == 0) 
            {
                dataAccessGeneralTables.GeneralTable_insertPictureObject(TableNames.Cam1EvenTable.ToString(), pictureSamples);
            }
            else
            {
                dataAccessGeneralTables.GeneralTable_insertPictureObject(TableNames.Cam1OddTable.ToString(), pictureSamples);
            }
        }

        private void SaveToDisk(Bitmap bitmap, PictureSampleModel pictureSampleModel)
        {
            try
            {
                if (Directory.Exists(pictureSampleModel.FilePathCurrent_TEXT))
                {
                    bitmap.Save(pictureSampleModel.FilePathCurrent_TEXT + pictureSampleModel.FileNameCurrent_TEXT +
                        pictureSampleModel.FileEndingCurrent_TEXT, ImageFormat.Jpeg);
                }
            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.WriteLine($"Exception in PictureController:SaveToDisk : " + e);
            }
        }

        private void updateLocalTimeParams()
        {
            unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
            unixTimeMinutes = unixTimeSeconds / 60;
            unixTimeSecondsMod60 = unixTimeSeconds % 60;
        }

    }
}




