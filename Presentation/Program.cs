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
            MyStartup myStartup = new MyStartup();
            //InterfaceAndInherit myPlayAround = new InterfaceAndInherit();
            //myPlayAround.RunStore();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myStartup.SimulatorTask();
            myStartup.WorkerTask();
            Application.Run(new Dashboard(myStartup));
        }
    }
}
