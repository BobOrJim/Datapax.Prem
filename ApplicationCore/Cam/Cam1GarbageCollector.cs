using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;

namespace ApplicationCore.Cam
{
    public class Cam1GarbageCollector
    {

        public IDataAccess dataAccessGeneralTables;
        private List<PictureSampleModel> pictureSamples;

        public Cam1GarbageCollector(IDataAccess _iDataAccessGeneralTables)
        {
            dataAccessGeneralTables = _iDataAccessGeneralTables;
        }

        public void Run()
        {
            pictureSamples = dataAccessGeneralTables.PictureTable_cutPostsBetweenInTable(TableNames.Cam1ThrowTable.ToString(), 0, Int64.MaxValue);
            RemovePicturesFromDisk(pictureSamples);
        }

        public void RemovePicturesFromDisk(List<PictureSampleModel> pictureSamples) //Skall egenteligen aldrig användas, då den inte spolar DB också.
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
    }
}
