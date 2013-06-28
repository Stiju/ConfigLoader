using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConfigLoader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigLoader form = new ConfigLoader();
            if(form.Loaded)
                Application.Run(form);
        }
    }
}