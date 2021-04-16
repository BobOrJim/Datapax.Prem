using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;

namespace ApplicationCore.Cam
{
    public class Cam1OddTableController
    {
        public IDataAccess dataAccessGeneralTables;
        private List<PictureSampleModel> pictureSamples;

        public Cam1OddTableController(IDataAccess _iDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run()
        {
            int unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
            int unixTimeMinutes = unixTimeSeconds / 60;
            int unixTimeSecondsMod60 = unixTimeSeconds % 60;
            if (unixTimeMinutes % 2 == 1 && unixTimeSecondsMod60 == 50) //Vid sekund 50, skall en db spolas.
            {
                pictureSamples = dataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(TableNames.Cam1EvenTable.ToString(), 0, Int64.MaxValue);
                //System.Diagnostics.Debug.WriteLine($"Antal objekt i Even är: " + pictureSamples.Count());
                dataAccessGeneralTables.GeneralTable_insertPictureObject(TableNames.Cam1ThrowTable.ToString(), pictureSamples);
            }
        }
    }
}
