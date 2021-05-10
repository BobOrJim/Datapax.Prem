using System;
using System.Diagnostics;

namespace Models
{
    public class PictureSampleModel
    {
        public string ToTable_TEXT { get; set; } = "";
        public Int64 Timestamp_unix_BIGINT { get; set; } = 0;
        public string Datestamp_TEXT { get; } = "";
        public string DeviationID_TEXT { get; set; } = "";
        //Ovan är gemensam med IOSampleModel
        //Nedan ger flexibilitet map linux/docker/azure/browser och filer kommer behöva ändra sökvägar fler gånger. Dessutom ger det viss spårbarhet.
        public string PictureFileNamePrefix_TEXT { get; set; } = ""; //Detta ger filnamn: pictureFileNamePrefix_TEXT + UnixTime + FileEnding_TEXT
        public string FilePathCurrent_TEXT { get; set; } = "";
        public string FileNameCurrent_TEXT { get; set; } = "";
        public string FileEndingCurrent_TEXT { get; set; } = "";
        public string FilePathWork_TEXT { get; set; } = "";
        public string FileNameWork_TEXT { get; set; } = "";
        public string FileEndingWork_TEXT { get; set; } = "";
        public string FilePathKeep_TEXT { get; set; } = "";
        public string FileNameKeep_TEXT { get; set; } = "";
        public string FileEndingKeep_TEXT { get; set; } = "";
        public string FilePathSpare1_TEXT { get; set; } = "";
        public string FileNameSpare1_TEXT { get; set; } = "";
        public string FileEndingSpare1_TEXT { get; set; } = "";
        public string FilePathSpare2_TEXT { get; set; } = "";
        public string FileNameSpare2_TEXT { get; set; } = "";
        public string FileEndingSpare2_TEXT { get; set; } = "";
        public bool IsLabeledForGarbageCollector_BIT { get; set; } = false;
        public bool SpareBit_BIT { get; set; } = false;


        public PictureSampleModel()
        {
            try
            {
                ToTable_TEXT = "SPARE";

                //Timestamp_unix_BIGINT
                //BIGINT i SSMS motsvarar en Int64 i C#
                var UnixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); // System.Int64 // 1607963957552
                Timestamp_unix_BIGINT = UnixTimeMilliseconds;

                //Datestamp_TEXT
                var UnixTimeSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(); // System.Int64  // 1607963957
                DateTime UnixTimeSecondsDateTime = DateTimeOffset.FromUnixTimeSeconds(UnixTimeSeconds).DateTime; //System.DateTime //2020-12-14 16:50:03
                var UnixTimeSecondsDateTimeString = UnixTimeSecondsDateTime.ToString(); // System.String // 2020-12-14 16:50:03
                Datestamp_TEXT = UnixTimeSecondsDateTimeString;

                //DeviationID_TEXT
                DeviationID_TEXT = "Ready to be set by Deviation function";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PictureSampleModel : PictureSampleModel: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in PictureSampleModel : PictureSampleModel: ex.StackTrace = " + ex.StackTrace);
            }
        }
    }
}
