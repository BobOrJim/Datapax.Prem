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
//                //Detta interface blir en fake, där en metod ersätts.
//                mock.Mock<IDataAccessGeneralTablesNEW>()
//                    .Setup(x => x.GeneralTable_flush(TableNames.IOOddTable.ToString()));

//                //Skapar instans, där fake interface ovan används
//                var ctrl = mock.Create<IOEvenTableController>();

//                ctrl.FlushOddTable(); //Kör metod FlushOddTable

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

//                //här skapar vi en testknass, som använder fake grejen ovan, istället för den riktiga.
//                var ctrl = mock.Create<IOEvenTableController>();
                
//                var actual = ctrl.cutPostsInFactoryTable(); //Notera att denna metod inte är samma som ovan. Dvs jag jobbar med ett fult
//                //objekt men skillnaden att när exakt "GeneralTable_cutAllPostsInTable(TableNames.FactoryTable.ToString()" anropas så ersätts denna
//                //med min fake ovan. DVS, OM JAG I MIN RITKIGA KOD GÅR OCH OCH KLADDAR PÅ cutPostsInFactoryTable METODEN SÅ DEN INTE GÖR
//                //KORREKT ARNOP, eller inget anrop alls SÅ KOMMER DETTA FEL FÅNGAS. (HAR TESTAT)
//                //KONKRET KONTROLLERAR JAG DÅ ATT ANROP GJÖRS, OCH ATT DET GJÖRS MED KORREKT STRÄNG.
//                var expected = GetSamplePosts();

//                Assert.True(actual != null); //1: Kontrolelrar i produktionskoden att cutPostsInFactoryTable anropar GeneralTable_cutAllPostsInTable
//                //med korrekt inparams.
//                Assert.Equal(expected.Count, actual.Count); //2: Kontrollerar att cutPostsInFactoryTable returnerar det som 
//                //GeneralTable_cutAllPostsInTable returnerar. Dvs kontroll retur statement i cutPostsInFactoryTable är korrekt.
//                for (int i = 0; i < expected.Count; i++)
//                {
//                    Assert.Equal(expected[i].DeviationID_TEXT, actual[i].DeviationID_TEXT); //3: Kan kontrollera att ingen funktion ändrat på urklippt
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
