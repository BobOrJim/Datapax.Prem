using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;

namespace ApplicationCore.Cam
{
    public class Cam1EvenTableController
    {
        private IDataAccessGeneralTablesNEW dataAccessGeneralTables;
        private List<PictureSampleModel> pictureSamples;

        public Cam1EvenTableController(IDataAccessGeneralTablesNEW _iDataAccessGeneralTables)
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
                pictureSamples = dataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(TableNames.Cam1OddTable.ToString(), 0, Int64.MaxValue);
                //System.Diagnostics.Debug.WriteLine($"Antal objekt i Odd är: " + pictureSamples.Count());
                dataAccessGeneralTables.GeneralTable_insertPictureObject(TableNames.Cam1ThrowTable.ToString(), pictureSamples);

            }
        }
    }
}