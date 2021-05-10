using System;
using System.Collections.Generic;
using Models;
using Interfaces;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.IO
{
    public class IOOddTableController //Flyttar från Factory hit
    {
        private static int _unixTimeSeconds;
        private static int _unixTimeMinutes;
        private static int _unixTimeSecondsMod60;
        private static List<IOSampleModel2> _samples = new List<IOSampleModel2>();
        private IDataAccess iDataAccessGeneralTables;

        public IOOddTableController(IDataAccess _iDataAccessGeneralTables)
        {
            iDataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run()
        {
            try
            {
                _unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
                _unixTimeMinutes = _unixTimeSeconds / 60;
                _unixTimeSecondsMod60 = _unixTimeSeconds % 60;
                if (_unixTimeMinutes % 2 == 1) //Vid udda minut
                {
                    OddMinute();
                }
                if (_unixTimeMinutes % 2 == 1 && _unixTimeSecondsMod60 == 50) //Vid udda minut OCH sekund 50
                {
                    OddMinuteAnd50Seconds();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOOddTableController : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOOddTableController : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void OddMinute() //Flyttar från FactoryTable till IOOddTable
        {
            try
            {
                _samples = iDataAccessGeneralTables.GeneralTable_cutAllPostsInTable(GlobalReadOnlyStrings.FactoryTable);
                iDataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOOddTable, _samples);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOOddTableController : OddMinute: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOOddTableController : OddMinute: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void OddMinuteAnd50Seconds() //Spolar IOEvenTable
        {
            try
            {
                iDataAccessGeneralTables.GeneralTable_flush(GlobalReadOnlyStrings.IOEvenTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOOddTableController : OddMinuteAnd50Seconds: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOOddTableController : OddMinuteAnd50Seconds: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}



