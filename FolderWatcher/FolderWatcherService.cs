using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace FolderWatcher
{
    public partial class FolderWatcherService : ServiceBase
    {
        public FolderWatcherService()
        {
            InitializeComponent();
        }

        public const string TestServiceName = "TestFolderWatcher";
        private FileSystemWatcher watcher = null;
       
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            // EventLog.CreateEventSource("Test FileWatcher Service", "Application");
            // EventLog.WriteEntry("Test FileWatcher Service", "Test Event");

            string scriptPath = File.ReadAllText(@"C:\Users\mike.littlefield\source\repos\FolderWatcher\FolderWatcher\test.ps1");

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(scriptPath);
                PowerShellInstance.Invoke();
            }
        }

        protected override void OnStart(string[] args)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Filter = "*.txt";
            watcher.Path = @"C:\Test";

            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Created += new FileSystemEventHandler(OnCreated);

            watcher.EnableRaisingEvents = true;
        }

        protected override void OnStop()
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
        }
    }
}
