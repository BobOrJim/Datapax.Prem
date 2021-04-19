using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class IOSampleModel2
    {
        public string ToTable_TEXT { get; set; }
        public Int64 Timestamp_unix_BIGINT { get; set; } = 0;
        public string Datestamp_TEXT { get; } = "";
        public string DeviationID_TEXT { get; set; } = "";

        public bool Hub2Hub_KKS123_SystemVolt_Erratic { get; } = false;
        public bool Hub2Hub_KKS123_SystemVolt_Low { get; } = false;
        public bool Hub2Hub_KKS123_Retarder_LowCurrent { get; } = false;
        public bool Hub2Hub_KKS123_AuxPressure_Low { get; } = false;

        public bool Panna_flisinmatning_skruv1_Motorskydd { get; } = false;
        public bool Panna_flisinmatning_skruv1_Sakerhetsbrytare { get; } = false;
        public bool Panna_flisinmatning_skruv1_Varvtalsvakt { get; } = false;
        public bool Panna_flisinmatning_skruv1_Nodstop { get; } = false;

        public int Panna_Fribord_flisinmating_pt1000 { get; } = 0;
        public int Panna_Fribord_askutmating_pt1000 { get; } = 0;
        public int Panna_Fribord_ForeBrannare_pt1000 { get; } = 0;
        public int Panna_Fribord_EfterBrannare_pt1000 { get; } = 0;

        public bool Karlatornet_Ventilation_Franluft_HogTemp { get; } = false;
        public bool Karlatornet_Ventilation_Franluft_LagTemp { get; } = false;
        public bool Karlatornet_Brandlarm_Hiss1_Aktivt { get; } = false;
        public bool Karlatornet_Brandlarm_Hiss2_Aktivt { get; } = false;

        public bool Vestas_Verk12_Koppling_HogTemp { get; } = false;
        public bool Vestas_Verk12_Koppling_LagOljeNiva { get; } = false;
        public bool Vestas_Verk12_Koppling_TryckAvvikelse { get; } = false;
        public bool Vestas_Verk12_Vaderstation_WatchDog { get; } = false;



        public IOSampleModel2()
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

            Random fixRand = new Random((int)nanoTime()); //THIS DATA WILL COME FROM A FACTORY, THIS IS ONLY A SIMULATOR.

            Hub2Hub_KKS123_SystemVolt_Erratic = fixRand.NextDouble() > 0.5;
            Hub2Hub_KKS123_SystemVolt_Low = fixRand.NextDouble() > 0.5;
            Hub2Hub_KKS123_Retarder_LowCurrent = fixRand.NextDouble() > 0.5;
            Hub2Hub_KKS123_AuxPressure_Low = fixRand.NextDouble() > 0.5;

            Panna_flisinmatning_skruv1_Motorskydd = fixRand.NextDouble() > 0.5;
            Panna_flisinmatning_skruv1_Sakerhetsbrytare = fixRand.NextDouble() > 0.5;
            Panna_flisinmatning_skruv1_Varvtalsvakt = fixRand.NextDouble() > 0.5;
            Panna_flisinmatning_skruv1_Nodstop = fixRand.NextDouble() > 0.5;

            Panna_Fribord_flisinmating_pt1000 = fixRand.Next(600, 630);
            Panna_Fribord_askutmating_pt1000 = fixRand.Next(600, 630);
            Panna_Fribord_ForeBrannare_pt1000 = fixRand.Next(600, 630);
            Panna_Fribord_EfterBrannare_pt1000 = fixRand.Next(600, 630);

            Karlatornet_Ventilation_Franluft_HogTemp = fixRand.NextDouble() > 0.5;
            Karlatornet_Ventilation_Franluft_LagTemp = fixRand.NextDouble() > 0.5;
            Karlatornet_Brandlarm_Hiss1_Aktivt = fixRand.NextDouble() > 0.5;
            Karlatornet_Brandlarm_Hiss2_Aktivt = fixRand.NextDouble() > 0.5;

            Vestas_Verk12_Koppling_HogTemp = fixRand.NextDouble() > 0.5;
            Vestas_Verk12_Koppling_LagOljeNiva = fixRand.NextDouble() > 0.5;
            Vestas_Verk12_Koppling_TryckAvvikelse = fixRand.NextDouble() > 0.5;
            Vestas_Verk12_Vaderstation_WatchDog = fixRand.NextDouble() > 0.5;

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
