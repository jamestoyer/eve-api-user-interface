Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Windows.Forms

Namespace Database
    Public Class Connection
        ''' <summary>
        ''' The private connection field to the database
        ''' </summary>
        Private objConnection As SqlCeConnection

        ''' <summary>
        ''' The connection to the database
        ''' </summary>
        Public ReadOnly Property dbConnection() As SqlCeConnection
            Get
                Return objConnection
            End Get
        End Property

        ''' <summary>
        ''' Opens a connection to the database
        ''' </summary>
        ''' <returns>The success of the connection in boolean form</returns>
        Public Function Open(ByVal dataFileName As String) As Boolean
            ' The database location
            Dim strLocation As String
            Dim objSettings As Settings
            Dim frmLocate As frmLocateDatabase

            ' Get the settings
            objSettings = Settings.NewInstance

            ' Check to see if the data files are present
            For Each DataFileStatus In objSettings.DataFileNames
                If DataFileStatus.FileName = dataFileName AndAlso Not DataFileStatus.Found Then
                    MessageBox.Show("The data files appear to be missing or corrupt. Either retry or reinstall EVE Command", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Next

            ' Save the settings
            objSettings.Save()

            ' Create the location from the settings file
            strLocation = String.Format("{0}{1}Databases{1}{2}", Settings.DataDir, Path.DirectorySeparatorChar, dataFileName)

ConnectToDatabase:
            Try
                ' Create a new instance of the connection
                objConnection = New SqlCeConnection

                ' Build the string
                objConnection.ConnectionString = "Data Source=" & strLocation

                ' Open the conneciton
                objConnection.Open()
            Catch ex As Exception
                ' Inform the user of the error
                If (MessageBox.Show("The connection to the database failed. Would you like to change the location and try again?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) <> DialogResult.Yes) Then Return False

                ' Open the location form
                frmLocate = New frmLocateDatabase()
                strLocation = frmLocate.Prompt(strLocation)
                If strLocation = "" Then Return False

                ' Attempt to connect
                GoTo ConnectToDatabase
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Closes the connection to the database
        ''' </summary>
        Public Sub Close()
            objConnection.Close()
            objConnection.Dispose()
        End Sub
    End Class
End Namespace
