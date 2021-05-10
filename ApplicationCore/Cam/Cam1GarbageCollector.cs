using System;
using System.Collections.Generic;
using System.IO;
using Interfaces;
using Models;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace ApplicationCore.Cam
{
    public class Cam1GarbageCollector
    {

        private IDataAccess dataAccessGeneralTables;
        private List<PictureSampleModel> pictureSamples;

        public Cam1GarbageCollector(IDataAccess _iDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run()
        {
            try
            {
                pictureSamples = dataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(GlobalReadOnlyStrings.Cam1ThrowTable, 0, Int64.MaxValue);
                RemovePicturesFromDisk(pictureSamples);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam1GarbageCollector : Run: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam1GarbageCollector : Run: ex.StackTrace = " + ex.StackTrace);
            }
        }

        public void RemovePicturesFromDisk(List<PictureSampleModel> pictureSamples) 
        {
            try
            {
                foreach (PictureSampleModel item in pictureSamples)
                {
                    //System.IO.DirectoryInfo di = new DirectoryInfo(item.FilePathCurrent_TEXT);
                    string fileToRemove = item.FilePathCurrent_TEXT + item.FileNameCurrent_TEXT + item.FileEndingCurrent_TEXT;
                    //System.Diagnostics.Debug.WriteLine($"File we will try to remove: {fileToRemove}");
                    if (File.Exists(fileToRemove))
                    {
                        //System.Diagnostics.Debug.WriteLine($"File found, lets delete it");
                        File.Delete(fileToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Cam1GarbageCollector : RemovePicturesFromDisk: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Cam1GarbageCollector : RemovePicturesFromDisk: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}
