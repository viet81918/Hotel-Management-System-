using System;
using System.Windows;

namespace NguyenAnhVietWPF
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Create a new instance of the application class
            var application = new Application();

            // Create an instance of the main window
            var mainWindow = new LoginWindow(); // Change this to your main window class

            // Set the main window of the application
            application.MainWindow = mainWindow;

            // Show the main window
            mainWindow.Show();

            // Run the application
            application.Run();
        }
    }
}
