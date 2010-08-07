Class Application

    Private Sub Application_DispatcherUnhandledException(ByVal sender As Object, ByVal e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        Dim errorLog As logger = New logger(logName.errorLog)

        ' Let the user know there is an error
        MessageBox.Show(My.Resources.Resources.UnhandledExceptionMessage, My.Resources.Resources.UnhandledExceptionTitle, MessageBoxButton.OK, MessageBoxImage.Error)

        ' Log the error
        errorLog.writeToLog(e.Exception.Message)
        e.Handled = True
    End Sub

    Private Sub Application_Exit(ByVal sender As Object, ByVal e As System.Windows.ExitEventArgs) Handles Me.Exit
#If DEBUG Then
        Dim generalLog As logger = New logger(logName.generalLog)
        generalLog.writeToLog(My.Resources.Resources.ProgramShuttingDown)
#End If
    End Sub

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Startup(ByVal sender As Object, ByVal e As System.Windows.StartupEventArgs) Handles Me.Startup
#If DEBUG Then
        Dim generalLog As logger = New logger(logName.generalLog)
        generalLog.writeToLog(My.Resources.Resources.ProgramStartingUp)
#End If
    End Sub
End Class
