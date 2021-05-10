using System;
using System.Collections.Generic;
using Models;
using Interfaces;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.IO
{
    public class IOKeepTableController
    {
        private IDataAccess idataAccessGeneralTables;
        public IOKeepTableController(IDataAccess _IDataAccessGeneralTables)
        {
            idataAccessGeneralTables = _IDataAccessGeneralTables;
        }

        public void Run(string TimeBeforeDeviationTextBox, string TimeAfterDeviationTextBox)
        {
            try
            {
                Int64 _unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                Int64 _latestDeviationTime_unixTime = getUnixTimeOfLatestDeviation();
                lookForDataToMoveToKeepTable(_latestDeviationTime_unixTime, TimeBeforeDeviationTextBox, TimeAfterDeviationTextBox);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOKeepTableController : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOKeepTableController : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void lookForDataToMoveToKeepTable(Int64 _latestDeviationTime_unixTime, string TimeBeforeDeviationTextBox, string TimeAfterDeviationTextBox)
        {
            List<IOSampleModel2> _samples = new List<IOSampleModel2>();
            Int64 _fromtime = 0;
            Int64 _toTime = 0;
            try
            {
                _fromtime = _latestDeviationTime_unixTime - 1000* Convert.ToInt64(TimeBeforeDeviationTextBox);
                _toTime = _latestDeviationTime_unixTime + 1000 * Convert.ToInt64(TimeAfterDeviationTextBox);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOKeepTableController : lookForDataToMoveToKeepTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOKeepTableController : lookForDataToMoveToKeepTable: ex.StackTrace = " + ex.StackTrace);
            }
            try
            {
                Int64 _unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                if (_unixTimeMilliseconds < _latestDeviationTime_unixTime + 1000 * Convert.ToInt64(TimeAfterDeviationTextBox))
                {
                    //Console.WriteLine($"  _unixTimeMilliseconds : {_unixTimeMilliseconds}");
                    //Console.WriteLine($"  _latestDeviationTime_unixTime  : {_latestDeviationTime_unixTime}");
                    //Console.WriteLine($"  Convert.ToInt64(TimeBeforeDeviationTextBox) : {Convert.ToInt64(TimeBeforeDeviationTextBox)}");
                    _samples = idataAccessGeneralTables.GeneralTable_cutPostsBetweenInTable(GlobalReadOnlyStrings.IOOddTable, _fromtime, _toTime);
                    idataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOKeepTable, _samples);
                    _samples = idataAccessGeneralTables.GeneralTable_cutPostsBetweenInTable(GlobalReadOnlyStrings.IOEvenTable, _fromtime, _toTime);
                    idataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOKeepTable, _samples);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOKeepTableController : lookForDataToMoveToKeepTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOKeepTableController : lookForDataToMoveToKeepTable: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public Int64 getUnixTimeOfLatestDeviation()
        {
            Int64 _latestDeviationTime_unixTime = 0;

            try
            {
                List<IOSampleModel2> _result = new List<IOSampleModel2>();
                _result = idataAccessGeneralTables.GeneralTable_getAllPostsInTable(GlobalReadOnlyStrings.IODeviationTable);
                foreach (IOSampleModel2 item in _result)
                {
                    _latestDeviationTime_unixTime = Math.Max(_latestDeviationTime_unixTime, item.Timestamp_unix_BIGINT);
                }
                //System.Diagnostics.Debug.WriteLine($"In IOKeepTableController:Run() antal hämtade deviations är: {_result.Count()}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOKeepTableController : getUnixTimeOfLatestDeviation: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOKeepTableController : getUnixTimeOfLatestDeviation: ex.StackTrace = " + ex.StackTrace);
            }
            return _latestDeviationTime_unixTime;
        }
    }
}




