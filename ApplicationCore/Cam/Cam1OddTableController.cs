using System;
using System.Collections.Generic;
using Interfaces;
using Models;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.Cam
{
    public class Cam1OddTableController
    {
        private IDataAccess iDataAccess;
        private List<PictureSampleModel> pictureSamples;

        public Cam1OddTableController(IDataAccess _iDataAccess)
        {
            iDataAccess = _iDataAccess;
        }

        public void Run()
        {
            try
            {
                int unixTimeSeconds = (int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
                int unixTimeMinutes = unixTimeSeconds / 60;
                int unixTimeSecondsMod60 = unixTimeSeconds % 60;
                if (unixTimeMinutes % 2 == 1 && unixTimeSecondsMod60 == 50) //Vid sekund 50, skall en db spolas.
                {
                    pictureSamples = iDataAccess.PictureTable_cutPostsBetweenInTable(GlobalReadOnlyStrings.Cam1EvenTable, 0, Int64.MaxValue);
                    //System.Diagnostics.Debug.WriteLine($"Antal objekt i Even är: " + pictureSamples.Count());
                    iDataAccess.GeneralTable_insertPictureObject(GlobalReadOnlyStrings.Cam1ThrowTable, pictureSamples);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam1OddTableController : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam1OddTableController : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}
