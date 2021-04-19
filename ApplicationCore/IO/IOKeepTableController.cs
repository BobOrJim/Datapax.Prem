
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using GlobalStringsReadOnly;

namespace ApplicationCore.IO
{
    public class IOKeepTableController
    {
        public IDataAccess idataAccessGeneralTables;
        public IOKeepTableController(IDataAccess _IDataAccessGeneralTables)
        {
            idataAccessGeneralTables = _IDataAccessGeneralTables;
        }

        public void Run(string TimeBeforeDeviationTextBox, string TimeAfterDeviationTextBox)
        {
            Int64 _unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            //List<IOSampleModel> _samples = new List<IOSampleModel>();
            Int64 _latestDeviationTime_unixTime = getUnixTimeOfLatestDeviation();
            //if (_unixTimeMilliseconds < (_latestDeviationTime_unixTime + 5000))
            //{
            lookForDataToMoveToKeepTable(_latestDeviationTime_unixTime, TimeBeforeDeviationTextBox, TimeAfterDeviationTextBox);
            //}
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
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"In WorkerTask: Exception:  {e}");
            }
            Int64 _unixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            if (_unixTimeMilliseconds < _latestDeviationTime_unixTime + 1000* Convert.ToInt64(TimeAfterDeviationTextBox))
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

        public Int64 getUnixTimeOfLatestDeviation()
        {
            List<IOSampleModel2> _result = new List<IOSampleModel2>();
            _result = idataAccessGeneralTables.GeneralTable_getAllPostsInTable(GlobalReadOnlyStrings.IODeviationTable);
            Int64 _latestDeviationTime_unixTime = 0;
            foreach (IOSampleModel2 item in _result)
            {
                _latestDeviationTime_unixTime = Math.Max(_latestDeviationTime_unixTime, item.Timestamp_unix_BIGINT);
            }
            //System.Diagnostics.Debug.WriteLine($"In IOKeepTableController:Run() antal hämtade deviations är: {_result.Count()}");
            return _latestDeviationTime_unixTime;
        }
    }
}




