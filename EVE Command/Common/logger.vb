Imports System.IO
Imports System.Reflection
Public Class logger

    Private _logLocation As String
    Private _logFileName As String = String.Empty

    Public Sub New(ByVal logType As logName)
        Select Case logType
            Case logName.generalLog
                _logFileName = "generalLog.txt"
            Case logName.errorLog
                _logFileName = "errorLog.txt"
        End Select

        ' Get the directory of the application
        GetDirectoryOfApplication()
    End Sub

    Private Sub GetDirectoryOfApplication()
        Dim asm As Assembly = Assembly.GetExecutingAssembly()
        _logLocation = Path.Combine(Path.GetDirectoryName(asm.Location), "logs")
    End Sub

    Public Sub writeToLog(ByVal message As String)

        Try
            ' Check the file directory exists and if not create it
            logDirectoryExists()

            ' Add the new message to the document
            Dim logFile As StreamWriter = New StreamWriter(Path.Combine(_logLocation, _logFileName), True)
            logFile.WriteLine("[{0}] - {1}", Now, message)
            logFile.Flush()

            ' Close and dispose
            logFile.Close()
            logFile.Dispose()
        Catch ex As Exception
            MessageBox.Show(My.Resources.Resources.LogErrorMessage, My.Resources.Resources.LogErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub logDirectoryExists()
        Dim directoryExists As Boolean = Directory.Exists(_logLocation)

        ' Create the directory id it doesn't exist
        If Not directoryExists Then Directory.CreateDirectory(_logLocation)
    End Sub

End Class

Public Enum logName
    generalLog
    errorLog
End Enum