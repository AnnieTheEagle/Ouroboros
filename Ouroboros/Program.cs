# region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
# endregion

namespace Ouroboros {
    static class Program {
        [STAThread]
        static void Main() { // The main entry point for the application.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DatabaseUpdater());
        }
    }
}
