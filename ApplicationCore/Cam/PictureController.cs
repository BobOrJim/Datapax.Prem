using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using System.IO;
using System.Drawing.Imaging;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.Cam
{

    public class PictureController
    {
        private int unixTimeSeconds;
        private int unixTimeMinutes;
        private int unixTimeSecondsMod60;
        private List<PictureSampleModel> pictureSamples;
        private IDataAccess dataAccessGeneralTables;
        private string CamEvenTable;
        private string CamOddTable;
        public PictureController(IDataAccess _iDataAccessGeneralTables, string _pictureFileNamePrefix)
        {
            try
            {
                dataAccessGeneralTables = _iDataAccessGeneralTables;
                if (_pictureFileNamePrefix == "Camera1")
                {
                    CamEvenTable = GlobalReadOnlyStrings.Cam1EvenTable;
                    CamOddTable = GlobalReadOnlyStrings.Cam1OddTable;
                }
                if (_pictureFileNamePrefix == "Camera2")
                {
                    CamEvenTable = GlobalReadOnlyStrings.Cam2EvenTable;
                    CamOddTable = GlobalReadOnlyStrings.Cam2OddTable;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PictureController : PictureController: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in PictureController : PictureController: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void SaveBitmapToDBAndToDisk(Bitmap bitmap, string pictureFileNamePrefix, string pathFolderWork, string pathFolderKeep)
        {

            try
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PictureController : SaveBitmapToDBAndToDisk: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in PictureController : SaveBitmapToDBAndToDisk: ex.StackTrace = " + ex.StackTrace);
            }
        }

        private void SaveToDB(PictureSampleModel pictureSampleModel)
        {
            try
            {
                pictureSamples = new List<PictureSampleModel> { pictureSampleModel };

                if (unixTimeMinutes % 2 == 0)
                {
                    dataAccessGeneralTables.GeneralTable_insertPictureObject(CamEvenTable, pictureSamples);
                }
                else
                {
                    dataAccessGeneralTables.GeneralTable_insertPictureObject(CamOddTable, pictureSamples);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PictureController : SaveToDB: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in PictureController : SaveToDB: ex.StackTrace = " + ex.StackTrace);
            }
        }

        private void SaveToDisk(Bitmap bitmap, PictureSampleModel pictureSampleModel)
        {
            try
            {
                if (Directory.Exists(pictureSampleModel.FilePathCurrent_TEXT))
                {
                    bitmap.Save(pictureSampleModel.FilePathCurrent_TEXT + pictureSampleModel.FileNameCurrent_TEXT + pictureSampleModel.FileEndingCurrent_TEXT, ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PictureController : SaveToDisk: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in PictureController : SaveToDisk: ex.StackTrace = " + ex.StackTrace);
            }
        }

        private void updateLocalTimeParams()
        {
            try
            {
                unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
                unixTimeMinutes = unixTimeSeconds / 60;
                unixTimeSecondsMod60 = unixTimeSeconds % 60;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PictureController : updateLocalTimeParams: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in PictureController : updateLocalTimeParams: ex.StackTrace = " + ex.StackTrace);
            }
        }

    }
}




