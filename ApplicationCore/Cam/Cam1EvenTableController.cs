using System;
using System.Collections.Generic;
using Models;
using Interfaces;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.Cam
{
    public class Cam1EvenTableController
    {
        private IDataAccess dataAccessGeneralTables;
        private List<PictureSampleModel> pictureSamples;

        public Cam1EvenTableController(IDataAccess _iDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _iDataAccessGeneralTables;
        }
        public void Run()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam1EvenTableController : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam1EvenTableController : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}