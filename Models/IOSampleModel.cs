using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class IOSampleModel
    {
        public string ToTable_TEXT { get; set; }
        public Int64 Timestamp_unix_BIGINT { get; set; } = 0;
        public string Datestamp_TEXT { get; } = "";
        public string DeviationID_TEXT { get; set; } = "";
        public bool Bit1 { get; } = false; //Dessa namn skall följa följande standard Lina_Maskin_Givar
        public bool Bit2 { get; } = false;
        public bool Bit3 { get; } = false;
        
        public IOSampleModel()
        {
            ToTable_TEXT = "SPARE";

            //Timestamp_unix_BIGINT
            //Typ info: BIGINT i ssms motsvarar en Int64 i C#
            var UnixTimeMilliseconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); // System.Int64 // 1607963957552
            Timestamp_unix_BIGINT = UnixTimeMilliseconds;

            //Datestamp_TEXT
            var UnixTimeSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(); // System.Int64  // 1607963957
            DateTime UnixTimeSecondsDateTime = DateTimeOffset.FromUnixTimeSeconds(UnixTimeSeconds).DateTime; //System.DateTime //2020-12-14 16:50:03
            var UnixTimeSecondsDateTimeString = UnixTimeSecondsDateTime.ToString(); // System.String // 2020-12-14 16:50:03
            Datestamp_TEXT = UnixTimeSecondsDateTimeString;

            //DeviationID_TEXT
            DeviationID_TEXT = "Not implemented yet";

            //Bit1 + Bit2 + Bit3
            Random fixRand = new Random((int)nanoTime()); //THIS DATA WILL COME FROM A FACTORY, THIS IS ONLY A SIMULATOR.
            Bit1 = fixRand.NextDouble() > 0.5;
            Bit2 = fixRand.NextDouble() > 0.5;
            Bit3 = fixRand.NextDouble() > 0.5;
        }

        private static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        } //Seed "generator" to random function
    }
}
