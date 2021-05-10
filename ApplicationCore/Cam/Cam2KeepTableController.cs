using System;
using System.Collections.Generic;
using Interfaces;
using Models;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.Cam
{
    public class Cam2KeepTableController
    {
        private IDataAccess iDataAccessGeneralTables;

        public Cam2KeepTableController(IDataAccess _iDataAccessGeneralTables)
        {
            iDataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run(string timeBeforeDeviationTextBox, string timeAfterDeviationTextBox)
        {
            try
            {
                List<PictureSampleModel> deviationPictureSamplesFromOdd;
                List<PictureSampleModel> deviationPictureSamplesFromEven;
                Int64 _latestDeviationTime_unixTime = getUnixTimeOfLatestDeviation();

                deviationPictureSamplesFromOdd = lookForDeviationDataInTable(GlobalReadOnlyStrings.Cam2OddTable, _latestDeviationTime_unixTime, timeBeforeDeviationTextBox, timeAfterDeviationTextBox);
                MoveThesePictureFilesToKeepFolder(deviationPictureSamplesFromOdd);
                //här gäller det att vara vaksam på pass by value eller pass by ref
                MoveThesePostsToKeep(deviationPictureSamplesFromOdd);

                deviationPictureSamplesFromEven = lookForDeviationDataInTable(GlobalReadOnlyStrings.Cam2EvenTable, _latestDeviationTime_unixTime, timeBeforeDeviationTextBox, timeAfterDeviationTextBox);
                MoveThesePictureFilesToKeepFolder(deviationPictureSamplesFromEven);
                MoveThesePostsToKeep(deviationPictureSamplesFromEven);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam2KeepTableController : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam2KeepTableController : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void MoveThesePictureFilesToKeepFolder(List<PictureSampleModel> deviationPictureSamples)
        {
            try
            {
                foreach (PictureSampleModel item in deviationPictureSamples)
                {
                    string sourceFilePath = item.FilePathCurrent_TEXT + item.FileNameCurrent_TEXT + item.FileEndingCurrent_TEXT;
                    System.Diagnostics.Debug.WriteLine($"In Cam2KeepTableController:MoveThesePictureFilesToKeepFolder är sourceFilePath: {sourceFilePath}");
                    string destinationFilePath = item.FilePathKeep_TEXT + item.FileNameKeep_TEXT + item.FileEndingKeep_TEXT;
                    System.Diagnostics.Debug.WriteLine($"In Cam2KeepTableController:MoveThesePictureFilesToKeepFolder är destinationFilePath: {destinationFilePath}");

                    item.FilePathCurrent_TEXT = item.FilePathKeep_TEXT;
                    item.FileNameCurrent_TEXT = item.FileNameKeep_TEXT;
                    item.FileEndingCurrent_TEXT = item.FileEndingKeep_TEXT;

                    System.IO.File.Move(sourceFilePath, destinationFilePath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam2KeepTableController : MoveThesePictureFilesToKeepFolder: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam2KeepTableController : MoveThesePictureFilesToKeepFolder: ex.StackTrace = " + ex.StackTrace);
            }
        }
        public void MoveThesePostsToKeep(List<PictureSampleModel> deviationPictureSamples)
        {
            try
            {
                iDataAccessGeneralTables.GeneralTable_insertPictureObject(GlobalReadOnlyStrings.Cam2KeepTable, deviationPictureSamples);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam2KeepTableController : MoveThesePostsToKeep: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam2KeepTableController : MoveThesePostsToKeep: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public List<PictureSampleModel> lookForDeviationDataInTable(string tableName, Int64 _latestDeviationTime_unixTime, string TimeBeforeDeviationTextBox, string TimeAfterDeviationTextBox)
        {
            try
            {
                Int64 _fromtime = 0;
                Int64 _toTime = 0;
                try
                {
                    _fromtime = _latestDeviationTime_unixTime - 1000 * Convert.ToInt64(TimeBeforeDeviationTextBox);
                    _toTime = _latestDeviationTime_unixTime + 1000 * Convert.ToInt64(TimeAfterDeviationTextBox);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"In Cam2KeepTableController:lookForDataToMoveToKeepTable: Exception:  {e}");
                }
                Int64 _unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                if (_unixTimeMilliseconds < _latestDeviationTime_unixTime + 1000 * Convert.ToInt64(TimeAfterDeviationTextBox))
                {
                    return iDataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(tableName, _fromtime, _toTime);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam2KeepTableController : lookForDeviationDataInTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam2KeepTableController : lookForDeviationDataInTable: ex.StackTrace = " + ex.StackTrace);
            }
            return new List<PictureSampleModel>();
        }

        public Int64 getUnixTimeOfLatestDeviation()
        {
            Int64 _latestDeviationTime_unixTime = 0;

            try
            {
                List<IOSampleModel2> _result = new List<IOSampleModel2>();
                _result = iDataAccessGeneralTables.GeneralTable_getAllPostsInTable(GlobalReadOnlyStrings.IODeviationTable);

                foreach (IOSampleModel2 item in _result)
                {
                    _latestDeviationTime_unixTime = Math.Max(_latestDeviationTime_unixTime, item.Timestamp_unix_BIGINT);
                }
                //System.Diagnostics.Debug.WriteLine($"In IOKeepTableController:Run() antal hämtade deviations är: {_result.Count()}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam2KeepTableController : getUnixTimeOfLatestDeviation: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam2KeepTableController : getUnixTimeOfLatestDeviation: ex.StackTrace = " + ex.StackTrace);
            }
            return _latestDeviationTime_unixTime;
        }
    }
}
