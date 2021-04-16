using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalStringsReadOnly
{
	//When this system is commisioned at a real factory. IOTablesTemplateColumnNames will contain perhaps 1000 different IO
	//and thus we should probably write some kind of CSV file, and a separate program/parser. That will modify all *.sql storedprocedures automatic
	static public class GlobalReadOnlyStrings
    {
        public static readonly string pathCamera1WorkFolder = @"C:\Users\Jimmy\Desktop\Tests\V019\Presentation\Cam1WorkPictures\";
        public static readonly string pathCamera1KeepFolder = @"C:\Users\Jimmy\Desktop\Tests\V019\Presentation\Cam1KeepPictures\";
        public static readonly string IOTablesTemplateColumnNames  = " @ToTable_TEXT, @Timestamp_unix_BIGINT, @Datestamp_TEXT, @DeviationID_TEXT, @Hub2Hub_KKS123_SystemVolt_Erratic, " +
			"@Hub2Hub_KKS123_SystemVolt_Low, @Hub2Hub_KKS123_Retarder_LowCurrent, @Hub2Hub_KKS123_AuxPressure_Low, " +
			"@Panna_flisinmatning_skruv1_Motorskydd, @Panna_flisinmatning_skruv1_Sakerhetsbrytare, @Panna_flisinmatning_skruv1_Varvtalsvakt, @Panna_flisinmatning_skruv1_Nodstop, " +
			"@Panna_Fribord_flisinmating_pt1000, @Panna_Fribord_askutmating_pt1000, @Panna_Fribord_ForeBrannare_pt1000, @Panna_Fribord_EfterBrannare_pt1000, " +
			"@Karlatornet_Ventilation_Franluft_HogTemp, @Karlatornet_Ventilation_Franluft_LagTemp, @Karlatornet_Brandlarm_Hiss1_Aktivt, @Karlatornet_Brandlarm_Hiss2_Aktivt, " +
			"@Vestas_Verk12_Koppling_HogTemp, @Vestas_Verk12_Koppling_LagOljeNiva, @Vestas_Verk12_Koppling_TryckAvvikelse, @Vestas_Verk12_Vaderstation_WatchDog";



    }
}
