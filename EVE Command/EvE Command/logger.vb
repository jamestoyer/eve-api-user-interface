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
        Dim asm As Assembly = Assembly.GetExecutingAssembly()
        Dim applicationLocation As String = Path.GetDirectoryName(asm.Location)
        _logLocation = Path.Combine(applicationLocation, "logs")
    End Sub

    Public Sub writeToLog(ByVal message As String)

        Try
            ' Check the file exists and if not create it
            logExists()

            ' Add the new message to the document
            Dim logFile As StreamWriter = New StreamWriter(Path.Combine(_logLocation, _logFileName))
            logFile.WriteLine(String.Format("[{0}] - {1}", Now, message))

            ' Close and dispose
            logFile.Close()
            logFile.Dispose()
        Catch ex As Exception
            MessageBox.Show("Unable to write to log. Please check that the application has permission to access its directory folder", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub logExists()
        Dim logLocation As String = Path.Combine(_logLocation, _logFileName)
        Dim fileExists As Boolean = File.Exists(logLocation)

        ' Check the log directory exists
        logDirectoryExits()

        ' Act acording to the file existing
        If fileExists Then Return

        ' The file doesn't exist to lets create it
        Dim logFile As FileStream = File.Create(logLocation)
        logFile.Close()
        logFile.Dispose()
    End Sub

    Private Sub logDirectoryExits()
        Dim directoryExists As Boolean = Directory.Exists(_logLocation)

        ' Create the directory id it doesn't exist
        If Not directoryExists Then Directory.CreateDirectory(_logLocation)
    End Sub

End Class

Public Enum logName
    generalLog
    errorLog
End Enum