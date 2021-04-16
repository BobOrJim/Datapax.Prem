using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataAccess;
using Models;
using Interfaces;
using GlobalStringsReadOnly;



namespace ApplicationCore.IO
{
    public class IOEvenTableController //Flyttar från Factory hit asdf
    {
        private static int _unixTimeSeconds;
        private static int _unixTimeMinutes;
        private static int _unixTimeSecondsMod60;
        private static List<IOSampleModel2> samples = new List<IOSampleModel2>();
        public IDataAccess dataAccessGeneralTables; //instans skall jag döpa om till litet i först....

        public IOEvenTableController(IDataAccess _IDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _IDataAccessGeneralTables;
        }

        public void Run()
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

        public List<IOSampleModel2> cutPostsInFactoryTable()
        {
            //List<IOSampleModel> test = new List<IOSampleModel>(); //Vid test av moq test.
            return dataAccessGeneralTables.GeneralTable_cutAllPostsInTable(GlobalReadOnlyStrings.FactoryTable);
        }
        public void InsertPostsInEvenTable(List<IOSampleModel2> _samples)
        {
            dataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOEvenTable, _samples);
        }

        public void MoveFromFactoryTableToEvenTable() //Flyttar från FactoryTable till IOOddTable
        {
            samples = dataAccessGeneralTables.GeneralTable_cutAllPostsInTable(GlobalReadOnlyStrings.FactoryTable);
            dataAccessGeneralTables.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IOEvenTable, samples);
        }

        public void FlushOddTable() //Spolar IOOddTable
        {
            dataAccessGeneralTables.GeneralTable_flush(GlobalReadOnlyStrings.IOOddTable);
        }
    }
}
