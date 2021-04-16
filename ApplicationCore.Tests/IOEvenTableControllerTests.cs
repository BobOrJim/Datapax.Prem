//using Autofac.Extras.Moq;
//using System;
//using Xunit;
//using Interfaces;
//using Models;
//using System.Collections.Generic;
//using Moq;

//namespace IOController.Tests
//{
//    public class IOEvenTableControllerTests
//    {

//        [Fact]
//        public void FlushOddTable_FlushOddTable_FlushOddTableIsCalled()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                //Detta interface blir en fake, d�r en metod ers�tts.
//                mock.Mock<IDataAccessGeneralTablesNEW>()
//                    .Setup(x => x.GeneralTable_flush(TableNames.IOOddTable.ToString()));

//                //Skapar instans, d�r fake interface ovan anv�nds
//                var ctrl = mock.Create<IOEvenTableController>();

//                ctrl.FlushOddTable(); //K�r metod FlushOddTable

//                //Kontrollerar att metod FlushOddTable, har anropat metod med inprameter: "GeneralTable_flush(TableNames.IOOddTable.ToString())"
//                mock.Mock<IDataAccessGeneralTablesNEW>()
//                    .Verify(x => x.GeneralTable_flush(TableNames.IOOddTable.ToString()), Times.Exactly(1));
//            }
//        }

//        [Fact]
//        public void cutPostsInFactoryTable_cutPostsInFactoryTable_ListIsReturnedWithouthModifications()
//        {
//            using (var mock = AutoMock.GetLoose())
//            {
//                //Setup av fake/mock metod till interface den finns i, vad den heter, och vilken signatur som skall ge vilken return
//                mock.Mock<IDataAccessGeneralTablesNEW>()
//                    .Setup(x => x.GeneralTable_cutAllPostsInTable(TableNames.FactoryTable.ToString())) //TableNames.FactoryTable.ToString()
//                    .Returns(GetSamplePosts());

//                //h�r skapar vi en testknass, som anv�nder fake grejen ovan, ist�llet f�r den riktiga.
//                var ctrl = mock.Create<IOEvenTableController>();
                
//                var actual = ctrl.cutPostsInFactoryTable(); //Notera att denna metod inte �r samma som ovan. Dvs jag jobbar med ett fult
//                //objekt men skillnaden att n�r exakt "GeneralTable_cutAllPostsInTable(TableNames.FactoryTable.ToString()" anropas s� ers�tts denna
//                //med min fake ovan. DVS, OM JAG I MIN RITKIGA KOD G�R OCH OCH KLADDAR P� cutPostsInFactoryTable METODEN S� DEN INTE G�R
//                //KORREKT ARNOP, eller inget anrop alls S� KOMMER DETTA FEL F�NGAS. (HAR TESTAT)
//                //KONKRET KONTROLLERAR JAG D� ATT ANROP GJ�RS, OCH ATT DET GJ�RS MED KORREKT STR�NG.
//                var expected = GetSamplePosts();

//                Assert.True(actual != null); //1: Kontrolelrar i produktionskoden att cutPostsInFactoryTable anropar GeneralTable_cutAllPostsInTable
//                //med korrekt inparams.
//                Assert.Equal(expected.Count, actual.Count); //2: Kontrollerar att cutPostsInFactoryTable returnerar det som 
//                //GeneralTable_cutAllPostsInTable returnerar. Dvs kontroll retur statement i cutPostsInFactoryTable �r korrekt.
//                for (int i = 0; i < expected.Count; i++)
//                {
//                    Assert.Equal(expected[i].DeviationID_TEXT, actual[i].DeviationID_TEXT); //3: Kan kontrollera att ingen funktion �ndrat p� urklippt
//                    //data innan den returneras.
//                }
//            }
//        }
        

//        private List<IOSampleModel> GetSamplePosts()
//        {
//            List<IOSampleModel> output = new List<IOSampleModel>
//            {
//                new IOSampleModel
//                {
//                    DeviationID_TEXT = "Test1"
//                },
//                new IOSampleModel
//                {
//                    DeviationID_TEXT = "Test2"
//                },
//                new IOSampleModel
//                {
//                    DeviationID_TEXT = "Test3"
//                }
//            };
//            return output;
//        }
//    }
//}
