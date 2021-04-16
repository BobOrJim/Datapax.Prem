using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using ApplicationCore.IO;
using Infrastructure;
using Interfaces;
using ApplicationCore.Cam;
using GlobalStringsReadOnly;
using Interfaces;

namespace Presentation
{
    public class MyStartup
    {
        public Boolean StartSimulator { get; set; }
        public Boolean StartWorker { get; set; }
        private static Dashboard dashboard;
        
        public readonly DataAccess dataAccess;
        private readonly IOOddTableController iOOddTableController;
        private readonly IOEvenTableController iOEvenTableController;
        private readonly IOKeepTableController iOKeepTableController;
        private readonly CameraController cam1;
        private Cam1OddTableController cam1OddTableController;
        private Cam1EvenTableController cam1EvenTableController;
        private Cam1KeepTableController cam1KeepTableController;
        private Cam1GarbageCollector cam1GarbageCollector;

        public MyStartup()
        {
            dataAccess = new DataAccess();
            iOOddTableController = new IOOddTableController(dataAccess);   //Denna tas emot som interface. DIP
            iOEvenTableController = new IOEvenTableController(dataAccess); //Denna tas emot som interface DIP
            iOKeepTableController = new IOKeepTableController(dataAccess); //Denna tas emot som interface DIP

            cam1OddTableController = new Cam1OddTableController(dataAccess);
            cam1EvenTableController = new Cam1EvenTableController(dataAccess);
            cam1KeepTableController = new Cam1KeepTableController(dataAccess);
            cam1GarbageCollector = new Cam1GarbageCollector(dataAccess);
            cam1 = new CameraController(dataAccess) //Denna tas emot som interface DIP
            {
                pathFolderWork = GlobalReadOnlyStrings.pathCamera1WorkFolder,
                pathFolderKeep = GlobalReadOnlyStrings.pathCamera1KeepFolder,
                pictureFileNamePrefix = "Camera1",
                videoDevicesID = 0
            };
        }

        public void GetDashboardReference(Dashboard _dashboard) => dashboard = _dashboard;
        public async Task SimulatorTask() //Simulerar en Kep-Server (Vanlien en fabrik/produktionslina) och fyller factory tabell
        {
            var simulatorTaskLoops = 0;
            while (true)
            {
                if (StartSimulator)
                {
                    DataAccessFactoryTable.FactoryTable_insert();
                    dashboard.Invoke((MethodInvoker)delegate
                    {
                        dashboard.simulatorTaskLoops.Text = simulatorTaskLoops.ToString(); // To access the UI thread synchronous
                    });
                    simulatorTaskLoops++;
                }
                await Task.Delay(30);
            }
        }
        public async Task WorkerTask()
        {
            string timeBeforeDeviation = "0";
            string timeAfterDeviation = "0";

            int x = 10;
            int y = x;
            x = 20;

            System.Diagnostics.Debug.WriteLine($"X är   = {x}");
            System.Diagnostics.Debug.WriteLine($"Y är  = {y}");

            while (true)
            { 
                if (StartWorker) //ev skall denna start skrivas till disk, så enhet klarar strömavbrott.
                {
                    timeBeforeDeviation = dashboard.TimeBeforeDeviationTextBox.Text;
                    timeAfterDeviation = dashboard.TimeAfterDeviationTextBox.Text;

                    UpdateSomeUIData();

                    //210416
                    iOOddTableController.Run(); //Data collected during an Odd minute
                    iOEvenTableController.Run(); //Data collected during an Even minute
                    iOKeepTableController.Run(timeBeforeDeviation, timeAfterDeviation);

                    cam1.RunCamera(dashboard.runCamera1);
                    cam1OddTableController.Run();
                    cam1EvenTableController.Run();
                    cam1KeepTableController.Run(timeBeforeDeviation, timeAfterDeviation);
                    
                    cam1GarbageCollector.Run();



                    //cam1.FlushPictureFolder(dashboard._flushCamera1Folder);

                    //PictureController.Cam1TryToTakeNewPicture.Run(_dashboard);
                    //PictureController.Cam1OddTableController.Run();
                    //PictureController.Cam1EvenTableController.Run();
                    //PictureController.Cam1KeepTableController.Run(_dashboard.TimeBeforeDeviationTextBox.Text, _dashboard.TimeAfterDeviationTextBox.Text);
                    //PictureController.Cam1GarbageCollector.Run();

                    //Cam1TryToTakeNewPicture
                    //Cam1OddTableController
                    //Cam1EvenTableController
                    //Cam1KeepTableController
                    //Cam1GarbageCollector
                }


                await Task.Delay(100);
            }
        }






        //Denna metod riskerar att bli ett par sidor, jag skulle kunna lägga den i en ny class, men då måste
        //_dashboard referensen skickas med. Jag prioriterar att hålla nere antal referenser till dashboard
        //och lägger därför updateSomeUIData metoden nedan.
        public void UpdateSomeUIData()
        {
            
            dashboard.Invoke((MethodInvoker)delegate
            {  // To access the UI thread 
                //Thread.Sleep(500);

                dashboard.UIClockString.Text = DateTime.Now.ToString("HH:mm:ss"); ;
                var UnixTimeSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                dashboard.UIClockUnix.Text = UnixTimeSeconds.ToString();
                dashboard.FactoryTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.FactoryTable).ToString();
                dashboard.IOOddTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.IOOddTable).ToString();
                dashboard.IOEvenTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.IOEvenTable).ToString();
                dashboard.IOKeepTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.IOKeepTable).ToString();
                dashboard.DeviationTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.IODeviationTable).ToString();
                dashboard.Cam1OddTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.Cam1OddTable).ToString();
                dashboard.Cam1EvenTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.Cam1EvenTable).ToString();
                dashboard.Cam1KeepTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.Cam1KeepTable).ToString();
                dashboard.Cam1ThrowTableRows.Text = dataAccess.GeneralTable_getNrOfRows(GlobalReadOnlyStrings.Cam1ThrowTable).ToString();
                dashboard.PicturesInWorkFolder.Text = Directory.GetFiles(cam1.pathFolderWork, "*", SearchOption.AllDirectories).Length.ToString();
                dashboard.PicturesInKeepFolder.Text = Directory.GetFiles(cam1.pathFolderKeep, "*", SearchOption.AllDirectories).Length.ToString();






                //            System.Diagnostics.Debug.WriteLine($"Altal filer är  = {fCount}");
                //GeneralTable.GeneralTable_cutPastePostsbetween(TableNames.FactoryTable.ToString(), TableNames.IOKeepTable.ToString(), 10000, 10000);

                //_dashboard.flowLineToOdd.Visible = false;

                //_dashboard.Cam2OddTableRows.Text = GeneralTable.GeneralTable_getNrOfRows(TableNames.Cam2OddTable.ToString()).ToString();
                //_dashboard.Cam2EvenTableRows.Text = GeneralTable.GeneralTable_getNrOfRows(TableNames.Cam2EvenTable.ToString()).ToString();
                //_dashboard.Cam2KeepTableRows.Text = GeneralTable.GeneralTable_getNrOfRows(TableNames.Cam2KeepTable.ToString()).ToString();
                //_dashboard.Cam2ThrowTableRows.Text = GeneralTable.GeneralTable_getNrOfRows(TableNames.Cam2ThrowTable.ToString()).ToString();

                //System.Diagnostics.Debug.WriteLine($"updateSomeUIData StartSimulator = {StartSimulator}");
                //_dashboard.StartSimulator.BackColor = StartSimulator ? System.Drawing.Color.Green : System.Drawing.SystemColors.Control;


                /*if (StartSimulator) 
                {
                    _dashboard.StartSimulator.BackColor = System.Drawing.Color.Green;
                } 
                else
                {
                    _dashboard.StartSimulator.BackColor = System.Drawing.SystemColors.Control;
                }*/
                //System.Diagnostics.Debug.WriteLine($"updateSomeUIData StartWorker = {}");
            });
        }
    }
}

