# FolderWatcher
##Overview
FolderWatcher is a Windows service that watches a folder for the creation of new files with specific file extensions.  If a new file is created in the folder and it matches the file filter, a PowerShell script is run.  I am working on using XML serialization to supply variables to the FolderWatcherService class, but until then, the scriptPath field in the OnCreated method needs to be updated manually as well as the watcher.Filter and watcher.Path properties in the OnStart method.
