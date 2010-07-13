Imports EVEC.Common
Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_DispatcherUnhandledException(ByVal sender As Object, ByVal e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        Try
            Dim objError As New [Error](ErrorTypes.systemError, e)

            ' Display the error
            ErrorHandler.displayError(objError)

            e.Handled = True
        Catch ex As Exception
            e.Handled = False
        End Try
    End Sub
End Class
