using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    static public class Program
    {
        [STAThread]
        static void Main()
        {
            MyStartup taskManager = new MyStartup();
            //InterfaceAndInherit myPlayAround = new InterfaceAndInherit();
            //myPlayAround.RunStore();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            taskManager.SimulatorTask();
            taskManager.WorkerTask();
            Application.Run(new Dashboard(taskManager));
        }
    }
}
