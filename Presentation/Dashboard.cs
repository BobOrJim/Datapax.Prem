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

namespace Presentation
{
    public partial class Dashboard : Form
    {
        public int delaySimulator_ms { get; set; } = 0;

        public Boolean runCamera1 { get; set; }
        public Boolean _flushCamera1Folder { get; set; }
        MyStartup taskManager;
        public DataAccessGeneralTablesNEW _dataAccessGeneralTablesNEW;

        public Dashboard(MyStartup _taskManager)
        {
            InitializeComponent();
            taskManager = _taskManager;
            taskManager.GetDashboardReference(this);
            _dataAccessGeneralTablesNEW = new DataAccessGeneralTablesNEW();
            //TaskManager.GetDashboardReference(this);
        }

        //Task on/off Buttons
        private void StartSimulator_Click(object sender, EventArgs e)
        {
            taskManager.StartSimulator = !taskManager.StartSimulator;
            StartSimulator.BackColor = taskManager.StartSimulator ? Color.Green : SystemColors.Control;
        }
        private void StartWorker_Click(object sender, EventArgs e)
        {
            taskManager.StartWorker = !taskManager.StartWorker;
            StartWorker.BackColor = taskManager.StartWorker ? Color.Green : SystemColors.Control;
        }

        #region Database buttons
        //FactoryTable Buttons
        private async void FactoryTable_create_Click(object sender, EventArgs e)
        {
            DisableFactorybuttons();
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createIOTemplateTable(TableNames.FactoryTable.ToString()));
            EnableFactoryButtons();
        }
        private async void FactoryTable_delete_Click(object sender, EventArgs e)
        {
            DisableFactorybuttons();
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.FactoryTable.ToString()));
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
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.FactoryTable.ToString()));
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
        //IOOddTable Buttons
        private async void IOOddTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createIOTemplateTable(TableNames.IOOddTable.ToString()));
        }
        private async void IOOddTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.IOOddTable.ToString()));
        }
        private async void IOOddTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.IOOddTable.ToString()));
        }
        //IOEvenTable Buttons
        private async void IOEvenTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createIOTemplateTable(TableNames.IOEvenTable.ToString()));
        }
        private async void IOEvenTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.IOEvenTable.ToString()));
        }
        private async void IOEvenTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.IOEvenTable.ToString()));
        }
        //IOKeepTable Buttons
        private async void IOKeepTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createIOTemplateTable(TableNames.IOKeepTable.ToString()));
        }
        private async void IOKeepTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.IOKeepTable.ToString()));
        }
        private async void IOKeepTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.IOKeepTable.ToString()));
        }
        //DeviationTable Buttons
        private async void IODeviationTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createIOTemplateTable(TableNames.IODeviationTable.ToString()));
        }
        private async void IODeviationTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.IODeviationTable.ToString()));
        }
        private async void IODeviationTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.IODeviationTable.ToString()));
        }
        private async void IODeviationTable_Insert_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Trycker på  IODeviationTable_Insert_Click");
            List<IOSampleModel> _samples = new List<IOSampleModel>();
            IOSampleModel _sample = new IOSampleModel();
            _sample.DeviationID_TEXT = DeviationTextBox.Text;
            DeviationTextBox.Text = "";
            _samples.Add(_sample);
            System.Diagnostics.Debug.WriteLine($"Trycker på  IODeviationTable_Insert_Click");
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_insertIOObject(TableNames.IODeviationTable.ToString(), _samples));
        }
        //Cam1OddTable Buttons
        private async void Cam1Odd_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createPictureTemplateTable(TableNames.Cam1OddTable.ToString()));
        }
        private async void Cam1Odd_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.Cam1OddTable.ToString()));
        }
        private async void Cam1Odd_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.Cam1OddTable.ToString()));
        }
        //Cam1EvenTable Buttons
        private async void Cam1EvenTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createPictureTemplateTable(TableNames.Cam1EvenTable.ToString()));
        }
        private async void Cam1EvenTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.Cam1EvenTable.ToString()));
        }
        private async void Cam1EvenTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.Cam1EvenTable.ToString()));
        }
        //Cam1KeepTable Buttons
        private async void Cam1KeepTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createPictureTemplateTable(TableNames.Cam1KeepTable.ToString()));
        }
        private async void Cam1KeepTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.Cam1KeepTable.ToString()));
        }
        private async void Cam1KeepTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.Cam1KeepTable.ToString()));
        }
        //Cam1ThrowTable Buttons
        private async void Cam1ThrowTable_Create_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_createPictureTemplateTable(TableNames.Cam1ThrowTable.ToString()));
        }
        private async void Cam1ThrowTable_Delete_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_delete(TableNames.Cam1ThrowTable.ToString()));
        }
        private async void Cam1ThrowTable_FlushRows_Click(object sender, EventArgs e)
        {
            await Task.Run(() => _dataAccessGeneralTablesNEW.GeneralTable_flush(TableNames.Cam1ThrowTable.ToString()));
        }
        #endregion

        private void TestAreaButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Trycker på stora testknappen");

            //cam1DashboardController.needSnapshot = !cam1DashboardController.needSnapshot;
            /*
            List<PictureSample> _samples = new List<PictureSample>();
            PictureSample _sample = new PictureSample();
            _samples.Add(_sample);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1OddTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1EvenTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1EvenTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1KeepTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1KeepTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1ThrowTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1ThrowTable.ToString(), _samples);
            GeneralTable.GeneralTable_insertPictureObject(TableNames.Cam1ThrowTable.ToString(), _samples);
            _samples = GeneralTable.PictureTable_cutPostsBetweenInTable(TableNames.Cam1OddTable.ToString(), 0, 1000000000000000);
            System.Diagnostics.Debug.WriteLine($"ANTAL RADER ÄR: {_samples.Count()}");
            */
        }

        private void StartCam1_Click(object sender, EventArgs e)
        {
            runCamera1 = !runCamera1;
            StartCam1.BackColor = runCamera1 ? Color.Green : SystemColors.Control;
        }

    }
}




