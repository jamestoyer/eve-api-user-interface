Imports System.Data.EntityClient
Imports System.IO
Imports System.Windows.Forms

Namespace DEO
    Public Class ConnectionString
        Dim strConnectionString As String
        Shared strDatabaseName As String

        Public Shared Function Create(ByVal dataFileName As String) As String
            strDatabaseName = dataFileName
            Create = String.Format("{0};provider=System.Data.SqlServerCe.3.5;provider connection string=""{1}""", MetaData, DataSource)
        End Function

        Private Shared Function MetaData() As String
            Dim strNameOnly As String
            If InStr(strDatabaseName, ".sdf") Then
                strNameOnly = Replace(strDatabaseName, ".sdf", "")
            Else
                strNameOnly = strDatabaseName
            End If

            MetaData = "metadata="
            MetaData &= String.Format("res://*/DEO.{0}.csdl|", strNameOnly)
            MetaData &= String.Format("res://*/DEO.{0}.ssdl|", strNameOnly)
            MetaData &= String.Format("res://*/DEO.{0}.msl", strNameOnly)
        End Function

        Private Shared Function DataSource() As String
            If InStr(strDatabaseName, ".sdf") < 1 Then
                strDatabaseName &= ".sdf"
            End If

            ' Check to see if the data files are present
            For Each DataFileStatus In Settings.NewInstance.DataFileNames
                If DataFileStatus.FileName = strDatabaseName AndAlso Not DataFileStatus.Found Then
                    MessageBox.Show("The data files appear to be missing or corrupt. Either retry or reinstall EVE Command", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Next

            ' Create the location from the settings file
            DataSource = String.Format("Data Source={0}{1}Databases{1}{2}", Settings.DataDir, Path.DirectorySeparatorChar, strDatabaseName)
        End Function
    End Class
End Namespace
