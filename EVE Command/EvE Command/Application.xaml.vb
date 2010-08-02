Class Application

    Private Sub Application_DispatcherUnhandledException(ByVal sender As Object, ByVal e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        Dim errorLog As logger = New logger(logName.errorLog)

        ' Let the user know there is an error
        MessageBox.Show("Oops an unhandled error has occured. It's ok though, the error has been logged", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error)

        ' Log the error
        errorLog.writeToLog(e.Exception.Message)
        e.Handled = True
    End Sub

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Startup(ByVal sender As Object, ByVal e As System.Windows.StartupEventArgs) Handles Me.Startup
#If DEBUG Then
        Dim generalLog As logger = New logger(logName.errorLog)
        generalLog.writeToLog("Program starting up")
#End If
    End Sub
End Class
