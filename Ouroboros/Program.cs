# region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
# endregion

namespace Ouroboros {
    public static class Program {
        public static const string APP_VERSION = "1.10";

        [STAThread]
        public static void Main() { // The main entry point for the application.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DatabaseUpdater());
        }
    }
}
