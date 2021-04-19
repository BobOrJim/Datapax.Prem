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


namespace Presentation
{
    public class MyStartup
    {
        public Boolean StartSimulator { get; set; }
        public Boolean StartWorker { get; set; }
        private static Dashboard dashboard;
        
        public readonly DataAccess dataAccess;
        private readonly CameraController Camera1;

        private readonly IOOddTableController iOOddTableController;
        private readonly IOEvenTableController iOEvenTableController;
        private readonly IOKeepTableController iOKeepTableController;
        private Cam1OddTableController cam1OddTableController;
        private Cam1EvenTableController cam1EvenTableController;
        private Cam1KeepTableController cam1KeepTableController;
        private Cam1GarbageCollector cam1GarbageCollector;

        public MyStartup()
        {
            dataAccess = new DataAccess();
            //DIP av dataAcces till varje TableController
            iOOddTableController = new IOOddTableController(dataAccess);
            iOEvenTableController = new IOEvenTableController(dataAccess);
            iOKeepTableController = new IOKeepTableController(dataAccess);
            cam1OddTableController = new Cam1OddTableController(dataAccess);
            cam1EvenTableController = new Cam1EvenTableController(dataAccess);
            cam1KeepTableController = new Cam1KeepTableController(dataAccess);
            cam1GarbageCollector = new Cam1GarbageCollector(dataAccess);
            
            //init Camera with dataAccess, folder paths, DB prefix and HW Id.
            Camera1 = new CameraController(dataAccess)
            {
                pathFolderWork = GlobalReadOnlyStrings.pathCamera1WorkFolder,
                pathFolderKeep = GlobalReadOnlyStrings.pathCamera1KeepFolder,
                pictureFileNamePrefix = "Camera1",
                videoDevicesID = 0
            };
        }

        //myStartup får ref till dashboard här. dashbord får ref till myStartup i Program.cs
        public void GetDashboardReference(Dashboard _dashboard) => dashboard = _dashboard;

        //Simulerar en Kep-Server (i.e fabrik, produktionslina, arbetsmaskin etc) och fyller factory tabell
        //Inaktiveras vid driftsättning
        public async Task SimulatorTask() 
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
            while (true)
            { 
                if (StartWorker) //should be writen to disk, so unit restarts after power failiure.
                {
                    //Start several controllers to various functions.
                    UpdateSomeUIData();

                    iOOddTableController.Run(); //Data collected during an Odd minute
                    iOEvenTableController.Run(); //Data collected during an Even minute
                    iOKeepTableController.Run(dashboard.TimeBeforeDeviationTextBox.Text, dashboard.TimeAfterDeviationTextBox.Text);

                    Camera1.RunCamera(dashboard.runCamera1);
                    cam1OddTableController.Run();
                    cam1EvenTableController.Run();
                    cam1KeepTableController.Run(dashboard.TimeBeforeDeviationTextBox.Text, dashboard.TimeAfterDeviationTextBox.Text);
                    
                    cam1GarbageCollector.Run();



                    //cam1.FlushPictureFolder(dashboard._flushCamera1Folder);

                    //PictureController.Cam1TryToTakeNewPicture.Run(_dashboard);
                    //PictureController.Cam1OddTableController.Run();
                    //PictureController.Cam1EvenTableController.Run();
                    //PictureController.Cam1KeepTableController.Run(_dashboard.TimeBeforeDeviationTextBox.Text, _dashboard.TimeAfterDeviationTextBox.Text);
                    //PictureController.Cam1GarbageCollector.Run();


                }
                await Task.Delay(100);
            }
        }



        //Tablesize, picture-count and some other data are sent fron the worker task to the dashboard task using a delegate.
        public void UpdateSomeUIData()
        {
            dashboard.Invoke((MethodInvoker)delegate
            {
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
                dashboard.PicturesInWorkFolder.Text = Directory.GetFiles(Camera1.pathFolderWork, "*", SearchOption.AllDirectories).Length.ToString();
                dashboard.PicturesInKeepFolder.Text = Directory.GetFiles(Camera1.pathFolderKeep, "*", SearchOption.AllDirectories).Length.ToString();






                //System.Diagnostics.Debug.WriteLine($"Altal filer är  = {fCount}");
                //GeneralTable.GeneralTable_cutPastePostsbetween(TableNames.FactoryTable.ToString(), TableNames.IOKeepTable.ToString(), 10000, 10000);


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

