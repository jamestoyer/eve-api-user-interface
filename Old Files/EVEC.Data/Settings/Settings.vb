Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Xml.Serialization
'TODO: Create methods to save and restore a settings file manually
'TODO: Consider automating the backup process
''' <summary>
''' Settings file for the whole project
''' </summary>
Public Class Settings
    Private Shared objThreadLock As Object = New Object

    Sub New()

    End Sub

    Private Sub New(ByVal Version As String)
        AppVersion = Version
    End Sub

#Region "App Version Information"
    Private strAppVersion As String

    ''' <summary>
    ''' The application version associated with the settings file
    ''' </summary>
    Public Property AppVersion() As String
        Get
            Return strAppVersion
        End Get
        Set(ByVal value As String)
            SyncLock objThreadLock
                strAppVersion = value
            End SyncLock
        End Set
    End Property

    ''' <summary>
    ''' Checks the current application version against the version in the settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckAppVersion()
        Dim strCurrentVersion As String

        ' Get the current version
        strCurrentVersion = Assembly.GetExecutingAssembly.GetName.Version.ToString

        ' Compare versions and inform the user to backup if different
        If strCurrentVersion <> AppVersion Then
            BackupPrompt()
        End If

        ' Update the app version in the settings
        AppVersion = strCurrentVersion
    End Sub

    ''' <summary>
    ''' Prompts the user to backup the settings file
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BackupPrompt()
        Dim objBackupSettings As DialogResult
        Dim objSaveFileDialog As New SaveFileDialog

        ' Tell the user to backup the settings file
        objBackupSettings = MessageBox.Show("EVE Command has been updated. Would you like to backup the settings before proceeding (recommended)?", "EVE Command Updated", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        ' Act according the result of the dialog
        If objBackupSettings = DialogResult.Yes Then
            With objSaveFileDialog
                'TODO: Fix this so that the settings file backup ends up with the rest of the settings
                .Title = "Backup"
                .Filter = "Settings Backup Files (*.bak) | *.bak"
                .FileName = String.Format("EVE_Command_Settings_{0}.xml.bak", AppVersion)
                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                objBackupSettings = .ShowDialog()

                ' Check to see if the user pressed ok to save
                If objBackupSettings = DialogResult.OK Then
                    File.Copy(SettingsFileName, .FileName, True)
                End If
            End With

        End If
    End Sub
#End Region

#Region "Data File Checking"
    Private Shared strDataFileNames As New List(Of DataFile)

    Public ReadOnly Property DataFileNames() As List(Of DataFile)
        Get
            Return strDataFileNames
        End Get
    End Property

    Public Shared Function FindDatafile(ByVal dataFile As String) As DataFile
        Dim strLocation As String

        ' Create the locations to find
        strLocation = String.Format("{0}{1}Databases{1}{2}", Settings.DataDir, Path.DirectorySeparatorChar, dataFile)

        ' Check to see if the file is there
        If File.Exists(strLocation) <> True Then
            ' The file obviously isn't where it should be, or it's a new install
            Dim strOriginal As String

            ' Create the location of the original install directory
            strOriginal = String.Format("{0}Resources{1}{2}", AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar, dataFile)

            ' Check to see if the file actually was installed originally. If not either myself or the installer fucked up
            If File.Exists(strOriginal) <> True Then
                ' Return a fail
                Return New DataFile(dataFile, False)
            End If

            ' Try to copy the file
            Try
                File.Copy(strOriginal, strLocation)
            Catch generatedExceptionName As Exception
                ' Return a fail
                Return New DataFile(dataFile, False)
            End Try
        End If
        ' Return a success
        Return New DataFile(dataFile, True)
    End Function

    Public Sub CheckForDataFiles()
        strDataFileNames.Clear()
        strDataFileNames.Add(FindDatafile("Userdata.sdf"))
        strDataFileNames.Add(FindDatafile("EveData.sdf"))
    End Sub
#End Region

#Region "Settings File Management"
#Region "Properties and Fields"
    ''' <summary>
    ''' Field to hold data directory. Value is initialised by the first read of DataDir property
    ''' </summary>
    Private Shared strDataDir As String

    ''' <summary>
    ''' Field to hold the name of the settings file in use. Value initialised by the first read of DataDir property.
    ''' </summary>
    Private Shared strSettingsFileName As String

    ''' <summary>
    ''' Variable to hold the settings
    ''' </summary>
    Private Shared objInstance As Settings = Nothing

    ''' <summary>
    ''' The directory for all user specific data
    ''' </summary>
    Public Shared ReadOnly Property DataDir() As String
        Get
            SyncLock objThreadLock
                If strDataDir = Nothing Then
                    ' Create the settings file name
                    strSettingsFileName = Path.DirectorySeparatorChar & "settings.xml"
#If DEBUG Then
                    strSettingsFileName = Path.DirectorySeparatorChar & "settings-debug.xml"
#End If

                    ' Get the data directory
                    strDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EVE Command")

                    ' Check to see if the directory exists and if not create it
                    If Directory.Exists(strDataDir) <> True Then
                        Directory.CreateDirectory(strDataDir)
                    End If

                    ' Create the cache sub directory path
                    Dim strCachePath As String
                    strCachePath = Path.Combine(strDataDir, "Cache")

                    ' Check to see if the cache sub directories exist
                    If Directory.Exists(strCachePath) <> True Then
                        Directory.CreateDirectory(strCachePath)
                    End If
                    If Directory.Exists(Path.Combine(strCachePath, "Images")) <> True Then
                        Directory.CreateDirectory(Path.Combine(strCachePath, "Images"))
                    End If
                    If Directory.Exists(Path.Combine(strCachePath, "Temp")) <> True Then
                        Directory.CreateDirectory(Path.Combine(strCachePath, "Temp"))
                    End If

                    ' Check to see if the database sub directory exist
                    If Directory.Exists(Path.Combine(strDataDir, "Databases")) <> True Then
                        Directory.CreateDirectory(Path.Combine(strDataDir, "Databases"))
                    End If
                End If
                Return strDataDir
            End SyncLock
            Return strDataDir
        End Get
    End Property

    ''' <summary>
    ''' The file name of the settings file
    ''' </summary>
    Public Shared ReadOnly Property SettingsFileName() As String
        Get
            Return DataDir & strSettingsFileName
        End Get
    End Property
#End Region

#Region "Public Methods"
    ''' <summary>
    ''' Method to return the settings file
    ''' </summary>
    ''' <returns>The settings file</returns>
    Public Shared Function NewInstance() As Settings
        ' Lock the code and get a new instance
        SyncLock objThreadLock
            If objInstance Is Nothing Then
                objInstance = Load()
            End If

            ' Return the settings
            Return objInstance
        End SyncLock
    End Function

    ''' <summary>
    ''' Saves settings 
    ''' </summary>
    Public Sub Save()
        ' Save the file
        SyncLock objThreadLock
            ' Create a new file variable for the file stream
            Dim objSaveSettings As FileStream

            ' Create an xml serialiser variable
            Dim xmlSaveXML As XmlSerializer

            ' Create a new filestream
            objSaveSettings = New FileStream(SettingsFileName, FileMode.Create, FileAccess.Write)

            ' Serialise and save the settings
            xmlSaveXML = New XmlSerializer(GetType(Settings))
            xmlSaveXML.Serialize(objSaveSettings, Settings.NewInstance)

            ' Dispose of the variables
            objSaveSettings.Close()
        End SyncLock
    End Sub
#End Region

#Region "Private Methods"
    'TODO: Fix this summary thing
    ''' <summary>
    ''' Loads the settings from the file
    ''' </summary>
    ''' <remarks>
    ''' If the settings file is 0 length or fails to load, then look for a backup
    ''' copy and ask if that is to be used. Once a settings file is loaded, a backup is taken as a 'last good settings file'
    ''' </remarks>
    ''' <returns>A Settings object loaded from file</returns>
    Private Shared Function Load() As Settings
        Dim objSettings As Settings = Nothing
        Dim strBackupFileName As String

        ' Construct the backup file name
        strBackupFileName = SettingsFileName & ".bak"

        ' Check to see if a settings or backup file exists
        If File.Exists(SettingsFileName) = True Then
            ' Try loading from the file
            Try
                objSettings = LoadFile(SettingsFileName)
            Catch ex As Exception
            End Try

            ' Backup the settings
            If objSettings IsNot Nothing Then
                Try
                    File.Copy(SettingsFileName, strBackupFileName, True)
                Catch ex As Exception
                End Try
            End If
        End If

        If File.Exists(strBackupFileName) = True AndAlso objSettings Is Nothing Then
            Dim objBackupFileInfo As New FileInfo(strBackupFileName)
            Dim strFileDate As String
            Dim objRestoreResult As DialogResult

            ' Get the file info for the backup
            strFileDate = (objBackupFileInfo.LastWriteTime.ToLocalTime().ToShortDateString() & " at ") + objBackupFileInfo.LastWriteTime.ToLocalTime().ToShortTimeString()

            ' Inform the user the settings are fucked
            objRestoreResult = MessageBox.Show(String.Format("The settings file is either missing or corrupt. The last backup was made on {0}. Would you like to use this backup file?", strFileDate), "Settings Corrupt", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If objRestoreResult = DialogResult.Yes Then
                ' Try to load from backup file
                Try
                    objSettings = LoadFile(strBackupFileName)
                Catch ex As Exception
                End Try

                ' If the settings loaded OK, copy to the main settings file, then copy back to stamp date
                If objSettings IsNot Nothing Then
                    Try
                        File.Copy(strBackupFileName, SettingsFileName, True)
                        File.Copy(SettingsFileName, strBackupFileName, True)
                    Catch ex As Exception
                    End Try
                Else
                    ' Notify user the restore failed
                    MessageBox.Show("Unable to load the backup file. A new settings file will be created", "Settings Corrupt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End If

        ' If no settings have been found, create a new file
        If objSettings Is Nothing Then
            ' Create new settings file
            objSettings = New Settings(Assembly.GetExecutingAssembly.GetName.Version.ToString())
        End If

        ' Check the settings version and for datafiles
        objSettings.CheckAppVersion()
        objSettings.CheckForDataFiles()

        Return objSettings
    End Function

    ''' <summary>
    ''' Loads the settings from a specified file path
    ''' </summary>
    ''' <param name="fileName">The fully qualified filename for the settings file to be loaded</param>
    ''' <returns>The Settings object loaded</returns>
    Private Shared Function LoadFile(ByVal fileName As String) As Settings
        ' Create a new file variable for the file stream
        Dim objOpenSettings As FileStream

        ' Create an xml serialiser variable
        Dim xmlLoadedXML As XmlSerializer

        ' Istantiate the variables
        objOpenSettings = New FileStream(fileName, FileMode.Open, FileAccess.Read)
        xmlLoadedXML = New XmlSerializer(GetType(Settings))

        ' Get the settings
        LoadFile = DirectCast(xmlLoadedXML.Deserialize(objOpenSettings), Settings)

        ' Dispose of the variables
        objOpenSettings.Close()

        Return LoadFile
    End Function
#End Region


#End Region
End Class




