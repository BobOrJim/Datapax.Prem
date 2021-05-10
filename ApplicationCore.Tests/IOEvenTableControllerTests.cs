using Autofac.Extras.Moq;
using System;
using Xunit;
using Interfaces;
using Models;
using System.Collections.Generic;
using Moq;
using Infrastructure;
using ApplicationCore.IO;
using GlobalStringsReadOnly;

//Kl�mer p� Moq tester

namespace IOController.Tests
{
    public class IOEvenTableControllerTests
    {
        //MethodName_StateOrActionInTest_ExpectedBehavior
        [Fact]
        public void FlushOddTable_CallFlushOddTable_FlushOddTableIsCalled()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Detta fake interface, d�r en metod ers�tts.
                mock.Mock<IDataAccess>().Setup(x => x.GeneralTable_flush(GlobalReadOnlyStrings.IOOddTable));

                //Skapar instans, d�r fake interface anv�nds
                IOEvenTableController ctrl = mock.Create<IOEvenTableController>();

                ctrl.FlushOddTable(); //K�r metod FlushOddTable

                //Kontrollerar att metod FlushOddTable har anropats exakt en g�ng.
                mock.Mock<IDataAccess>() .Verify(x => x.GeneralTable_flush(GlobalReadOnlyStrings.IOOddTable), Times.Exactly(1));
            }
        }

        //MethodName_StateOrActionInTest_ExpectedBehavior
        [Fact]
        public void cutPostsInFactoryTable_cutPostsInFactoryTable_ListIsReturnedWithouthModifications()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //ARRANGE
                //"Tilldelar" GeneralTable_cutAllPostsInTable metoden Moq data.
                mock.Mock<IDataAccess>()
                    .Setup(x => x.GeneralTable_cutAllPostsInTable(GlobalReadOnlyStrings.FactoryTable))
                    .Returns(GetSamplePosts());

                //Skapar ett Mock objekt, som nu f�r Mock data "via" sin GeneralTable_cutAllPostsInTable metod fr�n ovan.
                IOEvenTableController ctrl = mock.Create<IOEvenTableController>();


                //ACT
                List<IOSampleModel2> actual = ctrl.cutPostsInFactoryTable();
                List<IOSampleModel2> expected = GetSamplePosts();



                //ASSERT
                //1: Kontrolelrar att cutPostsInFactoryTable anropar GeneralTable_cutAllPostsInTable med korrekt inparams.
                Assert.True(actual != null);
                //2: Kontrol av retur statement. I.E cutPostsInFactoryTable returnerar samma antal element som GeneralTable_cutAllPostsInTable.
                Assert.Equal(expected.Count, actual.Count);
                //3: Kan kontrollerar att ingen funktion �ndrat p� anropad/urklippt data innan den returneras.
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].DeviationID_TEXT, actual[i].DeviationID_TEXT); 
                }
            }
        }


        private List<IOSampleModel2> GetSamplePosts()
        {
            List<IOSampleModel2> output = new List<IOSampleModel2>
            {
                new IOSampleModel2
                {
                    DeviationID_TEXT = "Test1"
                },
                new IOSampleModel2
                {
                    DeviationID_TEXT = "Test2"
                },
                new IOSampleModel2
                {
                    DeviationID_TEXT = "Test3"
                }
            };
            return output;
        }
    }
}
