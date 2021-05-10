using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infrastructure;
using Models;
using GlobalStringsReadOnly;
using System.Diagnostics;

namespace Presentation
{
    public partial class Dashboard : Form
    {
        public int delaySimulator_ms { get; set; } = 0;

        public Boolean runCamera1 { get; set; }
        public Boolean runCamera2 { get; set; }

        public Boolean _flushCamera1Folder { get; set; }
        MyStartup myStartup;
        public DataAccess dataAccess;

        public Dashboard(MyStartup _myStartup)
        {
            try
            {
                InitializeComponent();
                myStartup = _myStartup;
                myStartup.GetDashboardReference(this);
                dataAccess = new DataAccess();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Dashboard : Dashboard: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Dashboard : Dashboard: ex.StackTrace = " + ex.StackTrace);
            }
        }

        //Task on/off Buttons
        private void StartSimulator_Click(object sender, EventArgs e)
        {
            myStartup.StartSimulator = !myStartup.StartSimulator;
            StartSimulator.BackColor = myStartup.StartSimulator ? Color.Green : SystemColors.Control;
        }
        private void StartWorker_Click(object sender, EventArgs e)
        {
            myStartup.StartWorker = !myStartup.StartWorker;
            StartWorker.BackColor = myStartup.StartWorker ? Color.Green : SystemColors.Control;
        }

        #region Database buttons
        //FactoryTable Buttons
        private async void FactoryTable_create_Click(object sender, EventArgs e)
        {
            DisableFactorybuttons();
            await Task.Run(() => dataAccess.GeneralTable_createIOTemplateTable(GlobalReadOnlyStrings.FactoryTable));
            EnableFactoryButtons();
        }
        private async void FactoryTable_delete_Click(object sender, EventArgs e)
        {
            DisableFactorybuttons();
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.FactoryTable));
            EnableFactoryButtons();
        }
        private async void FactoryTable_Insert_Click(object sender, EventArgs e)
        {
            DisableFactorybuttons();
            await Task.Run(() => DataAccessFactoryTable.FactoryTable_insert());
            EnableFactoryButtons();
        }
        private async void FactoryTable_flush_Click(object sender, EventArgs e)
        {
            DisableFactorybuttons();
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.FactoryTable));
                //TableNames.FactoryTable.ToString()));
            EnableFactoryButtons();
        }
        private void EnableFactoryButtons()
        {
            FactoryTable_create.Enabled = true;
            FactoryTable_delete.Enabled = true;
            FactoryTable_Insert.Enabled = true;
            FactoryTable_flush.Enabled = true;
        }
        private void DisableFactorybuttons()
        {
            FactoryTable_create.Enabled = false;
            FactoryTable_delete.Enabled = false;
            FactoryTable_Insert.Enabled = false;
            FactoryTable_flush.Enabled = false;
        }
        private async void IOOddTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createIOTemplateTable(GlobalReadOnlyStrings.IOOddTable));
        }
        private async void IOOddTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.IOOddTable));
        }
        private async void IOOddTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.IOOddTable));
        }
        private async void IOEvenTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createIOTemplateTable(GlobalReadOnlyStrings.IOEvenTable));
        }
        private async void IOEvenTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.IOEvenTable));
        }
        private async void IOEvenTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.IOEvenTable));
        }
        private async void IOKeepTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createIOTemplateTable(GlobalReadOnlyStrings.IOKeepTable));
        }
        private async void IOKeepTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.IOKeepTable));
        }
        private async void IOKeepTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.IOKeepTable));
        }
        private async void IODeviationTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createIOTemplateTable(GlobalReadOnlyStrings.IODeviationTable));
        }
        private async void IODeviationTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.IODeviationTable));
        }
        private async void IODeviationTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.IODeviationTable));
        }
        private async void IODeviationTable_Insert_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Trycker på  IODeviationTable_Insert_Click");
                List<IOSampleModel2> _samples = new List<IOSampleModel2>();
                IOSampleModel2 _sample = new IOSampleModel2();
                _sample.DeviationID_TEXT = DeviationTextBox.Text;
                DeviationTextBox.Text = "";
                _samples.Add(_sample);
                System.Diagnostics.Debug.WriteLine($"Trycker på  IODeviationTable_Insert_Click");
                await Task.Run(() => dataAccess.GeneralTable_insertIOObject(GlobalReadOnlyStrings.IODeviationTable, _samples));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in Dashboard : IODeviationTable_Insert_Click: ex.Message = " + ex.Message);
                Debug.WriteLine($"Exception in Dashboard : IODeviationTable_Insert_Click: ex.StackTrace = " + ex.StackTrace);
            }
        }



        private async void Cam1Odd_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam1OddTable));
        }
        private async void Cam1Odd_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam1OddTable));
        }
        private async void Cam1Odd_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam1OddTable));
        }
        private async void Cam1EvenTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam1EvenTable));
        }
        private async void Cam1EvenTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam1EvenTable));
        }
        private async void Cam1EvenTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam1EvenTable));
        }
        private async void Cam1KeepTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam1KeepTable));
        }
        private async void Cam1KeepTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam1KeepTable));
        }
        private async void Cam1KeepTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam1KeepTable));
        }
        private async void Cam1ThrowTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam1ThrowTable));
        }
        private async void Cam1ThrowTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam1ThrowTable));
        }
        private async void Cam1ThrowTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam1ThrowTable));
        }

        private async void Cam2OddTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam2OddTable));
        }
        private async void Cam2OddTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam2OddTable));
        }
        private async void Cam2OddTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam2OddTable));
        }
        private async void Cam2EvenTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam2EvenTable));
        }
        private async void Cam2EvenTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam2EvenTable));
        }
        private async void Cam2EvenTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam2EvenTable));
        }

        private async void Cam2KeepTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam2KeepTable));
        }
        private async void Cam2KeepTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam2KeepTable));
        }
        private async void Cam2KeepTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam2KeepTable));
        }

        private async void Cam2ThrowTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_createPictureTemplateTable(GlobalReadOnlyStrings.Cam2ThrowTable));
        }
        private async void Cam2ThrowTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_delete(GlobalReadOnlyStrings.Cam2ThrowTable));
        }
        private async void Cam2ThrowTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataAccess.GeneralTable_flush(GlobalReadOnlyStrings.Cam2ThrowTable));
        }




        #endregion

        private void TestAreaButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Trycker på stora testknappen");
            //Have nothing else to test right now :)
        }

        private void StartCam1_Click(object sender, EventArgs e)
        {
            runCamera1 = !runCamera1;
            StartCam1.BackColor = runCamera1 ? Color.Green : SystemColors.Control;
        }

        private void StartCam2_Click(object sender, EventArgs e)
        {
            runCamera2 = !runCamera2;
            StartCam2.BackColor = runCamera2 ? Color.Green : SystemColors.Control;
        }


    }
}




