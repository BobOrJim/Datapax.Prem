using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using GlobalStringsReadOnly;

namespace ApplicationCore.Cam
{
    public class Cam2EvenTableController
    {
        private IDataAccess dataAccessGeneralTables;
        private List<PictureSampleModel> pictureSamples;

        public Cam2EvenTableController(IDataAccess _iDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _iDataAccessGeneralTables;
        }
        public void Run()
        {
            int unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
            int unixTimeMinutes = unixTimeSeconds / 60;
            int unixTimeSecondsMod60 = unixTimeSeconds % 60;
            if (unixTimeMinutes % 2 == 0 && unixTimeSecondsMod60 == 50) //Vid sekund 50, skall en db spolas.
            {
                pictureSamples = dataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(GlobalReadOnlyStrings.Cam1OddTable, 0, Int64.MaxValue);
                //System.Diagnostics.Debug.WriteLine($"Antal objekt i Odd är: " + pictureSamples.Count());
                dataAccessGeneralTables.GeneralTable_insertPictureObject(GlobalReadOnlyStrings.Cam1ThrowTable, pictureSamples);

            }
        }
    }
}