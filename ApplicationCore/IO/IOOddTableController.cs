using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataAccess;
using Models;
using Interfaces;

namespace ApplicationCore.IO
{
    public class IOOddTableController //Flyttar från Factory hit
    {
        private static int _unixTimeSeconds;
        private static int _unixTimeMinutes;
        private static int _unixTimeSecondsMod60;
        private static List<IOSampleModel> _samples = new List<IOSampleModel>();
        public IDataAccessGeneralTablesNEW iDataAccessGeneralTables;

        public IOOddTableController(IDataAccessGeneralTablesNEW _iDataAccessGeneralTables)
        {
            iDataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run()
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

        public void OddMinute() //Flyttar från FactoryTable till IOOddTable
        {
            _samples = iDataAccessGeneralTables.GeneralTable_cutAllPostsInTable(TableNames.FactoryTable.ToString());
            iDataAccessGeneralTables.GeneralTable_insertIOObject(TableNames.IOOddTable.ToString(), _samples);
        }

        public void OddMinuteAnd50Seconds() //Spolar IOEvenTable
        {
            iDataAccessGeneralTables.GeneralTable_flush(TableNames.IOEvenTable.ToString());
        }
    }
}



