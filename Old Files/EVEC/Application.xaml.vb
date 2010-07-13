Imports EVEC.Data
Imports System.IO
Class Application
    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Exit(ByVal sender As Object, ByVal e As System.Windows.ExitEventArgs) Handles Me.Exit
        ' Get the data directory
        Dim strDataDir As String
        strDataDir = Settings.DataDir

        ' Create the cache sub directory path
        Dim strCachePath As String
        strCachePath = Path.Combine(strDataDir, "Cache")

        ' Delete the temp folder
        If Directory.Exists(Path.Combine(strCachePath, "Temp")) = True Then
            My.Computer.FileSystem.DeleteDirectory(Path.Combine(strCachePath, "Temp"), FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
    End Sub
End Class
