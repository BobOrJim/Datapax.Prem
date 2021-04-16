using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;

namespace ApplicationCore.Cam
{
    public class Cam1KeepTableController
    {
        public IDataAccessGeneralTablesNEW iDataAccessGeneralTables;

        public Cam1KeepTableController(IDataAccessGeneralTablesNEW _iDataAccessGeneralTables)
        {
            iDataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run(string timeBeforeDeviationTextBox, string timeAfterDeviationTextBox)
        {
            List<PictureSampleModel> deviationPictureSamplesFromOdd;
            List<PictureSampleModel> deviationPictureSamplesFromEven;
            Int64 _latestDeviationTime_unixTime = getUnixTimeOfLatestDeviation();

            deviationPictureSamplesFromOdd = lookForDeviationDataInTable(TableNames.Cam1OddTable.ToString(), _latestDeviationTime_unixTime, timeBeforeDeviationTextBox, timeAfterDeviationTextBox);
            MoveThesePictureFilesToKeepFolder(deviationPictureSamplesFromOdd);
            //här gäller det att vara vaksam på pass by value eller pass by ref
            MoveThesePostsToKeep(deviationPictureSamplesFromOdd);

            deviationPictureSamplesFromEven = lookForDeviationDataInTable(TableNames.Cam1EvenTable.ToString(), _latestDeviationTime_unixTime, timeBeforeDeviationTextBox, timeAfterDeviationTextBox);
            MoveThesePictureFilesToKeepFolder(deviationPictureSamplesFromEven);
            MoveThesePostsToKeep(deviationPictureSamplesFromEven);
        }

        public void MoveThesePictureFilesToKeepFolder(List<PictureSampleModel> deviationPictureSamples)
        {
            foreach(PictureSampleModel item in deviationPictureSamples)
            {
                string sourceFilePath = item.FilePathCurrent_TEXT + item.FileNameCurrent_TEXT + item.FileEndingCurrent_TEXT;
                System.Diagnostics.Debug.WriteLine($"In Cam1KeepTableController:MoveThesePictureFilesToKeepFolder är sourceFilePath: {sourceFilePath}");
                string destinationFilePath = item.FilePathKeep_TEXT + item.FileNameKeep_TEXT + item.FileEndingKeep_TEXT;
                System.Diagnostics.Debug.WriteLine($"In Cam1KeepTableController:MoveThesePictureFilesToKeepFolder är destinationFilePath: {destinationFilePath}");

                item.FilePathCurrent_TEXT = item.FilePathKeep_TEXT;
                item.FileNameCurrent_TEXT = item.FileNameKeep_TEXT;
                item.FileEndingCurrent_TEXT = item.FileEndingKeep_TEXT;
                
                System.IO.File.Move(sourceFilePath, destinationFilePath);
            }
        }
        public void MoveThesePostsToKeep(List<PictureSampleModel> deviationPictureSamples)
        {
            iDataAccessGeneralTables.GeneralTable_insertPictureObject(TableNames.Cam1KeepTable.ToString(), deviationPictureSamples);
        }

        public List<PictureSampleModel> lookForDeviationDataInTable(string tableName, Int64 _latestDeviationTime_unixTime, string TimeBeforeDeviationTextBox, string TimeAfterDeviationTextBox)
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
                System.Diagnostics.Debug.WriteLine($"In Cam1KeepTableController:lookForDataToMoveToKeepTable: Exception:  {e}");
            }
            Int64 _unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            if (_unixTimeMilliseconds < _latestDeviationTime_unixTime + 1000 * Convert.ToInt64(TimeAfterDeviationTextBox))
            {
                return iDataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(tableName, _fromtime, _toTime);
            }
            return new List<PictureSampleModel>();
        }

        public Int64 getUnixTimeOfLatestDeviation()
        {
            List<IOSampleModel> _result = new List<IOSampleModel>();
            _result = iDataAccessGeneralTables.GeneralTable_getAllPostsInTable(TableNames.IODeviationTable.ToString());
            Int64 _latestDeviationTime_unixTime = 0;
            foreach (IOSampleModel item in _result)
            {
                _latestDeviationTime_unixTime = Math.Max(_latestDeviationTime_unixTime, item.Timestamp_unix_BIGINT);
            }
            //System.Diagnostics.Debug.WriteLine($"In IOKeepTableController:Run() antal hämtade deviations är: {_result.Count()}");
            return _latestDeviationTime_unixTime;
        }
    }
}
