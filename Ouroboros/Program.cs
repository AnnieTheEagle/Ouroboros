# region Using Statements
using System;
using System.Windows.Forms;
# endregion

namespace Ouroboros {
    public static class Program {
        public const string APP_VERSION = "1.10";

        [STAThread]
        public static void Main() { // The main entry point for the application.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DatabaseUpdater());
        }
    }
}
