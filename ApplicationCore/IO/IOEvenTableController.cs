using System;
using System.Collections.Generic;
using Models;
using Interfaces;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.IO
{
    public class IOEvenTableController //Flyttar från Factory hit
    {
        private static int _unixTimeSeconds;
        private static int _unixTimeMinutes;
        private static int _unixTimeSecondsMod60;
        private static List<IOSampleModel2> samples = new List<IOSampleModel2>();
        private IDataAccess dataAccessGeneralTables; 

        public IOEvenTableController(IDataAccess _IDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _IDataAccessGeneralTables;
        }

        public void Run()
        {
            try
            {
                _unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
                _unixTimeMinutes = _unixTimeSeconds / 60;
                _unixTimeSecondsMod60 = _unixTimeSeconds % 60;
                if (_unixTimeMinutes % 2 == 0) //Vid jamn minut
                {
                    samples = cutPostsInFactoryTable();
                    InsertPostsInEvenTable(samples);
                    //MoveFromFactoryTableToEvenTable();
                }
                if (_unixTimeMinutes % 2 == 0 && _unixTimeSecondsMod60 == 50) //Vid jämn minut OCH sekund 50
                {
                    FlushOddTable();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOEvenTableController : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOEvenTableController : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public List<IOSampleModel2> cutPostsInFactoryTable()
        {
            try
            {
                //List<IOSampleModel> test = new List<IOSampleModel>(); //Vid test av moq test.
                return dataAccessGeneralTables.GeneralTable_cutAllPostsInTable(GlobalReadOnlyStrings.FactoryTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOEvenTableController : cutPostsInFactoryTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOEvenTableController : cutPostsInFactoryTable: ex.StackTrace = " + ex.StackTrace);
            }
            return new List<IOSampleModel2>();
        }
        public void InsertPostsInEvenTable(List<IOSampleModel2> _samples)
        {
            try
            {
                dataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOEvenTable, _samples);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOEvenTableController : InsertPostsInEvenTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOEvenTableController : InsertPostsInEvenTable: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void MoveFromFactoryTableToEvenTable() //Flyttar från FactoryTable till IOOddTable
        {
            try
            {
                samples = dataAccessGeneralTables.GeneralTable_cutAllPostsInTable(GlobalReadOnlyStrings.FactoryTable);
                dataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOEvenTable, samples);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOEvenTableController : MoveFromFactoryTableToEvenTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOEvenTableController : MoveFromFactoryTableToEvenTable: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void FlushOddTable() //Spolar IOOddTable
        {
            try
            {
                dataAccessGeneralTables.GeneralTable_flush(GlobalReadOnlyStrings.IOOddTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IOEvenTableController : FlushOddTable: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in IOEvenTableController : FlushOddTable: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}
