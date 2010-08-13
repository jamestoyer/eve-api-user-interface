Imports System.IO
Imports System.Reflection
Imports System.Xml.Serialization
'TODO: Create methods to save and restore a settings file manually
'TODO: Consider automating the backup process
''' <summary>
''' Settings file for the whole project
''' </summary>
Public Class Settings
    Private Shared threadLock As Object = New Object

    Public Sub New()

    End Sub

    Private Sub New(ByVal Version As String)
        AppVersion = Version
    End Sub

#Region "Fields"
    Private Shared _dataDirectories As List(Of DataDirectory)

    ''' <summary>
    ''' Variable to hold the settings
    ''' </summary>
    Private Shared settingsInstance As Settings = Nothing

    Private Shared _settingsFileName As String
#End Region

#Region "Serialisable Properties"
    ''' <summary>
    ''' The application version associated with the settings file
    ''' </summary>
    <XmlElement("Version")>
    Public Property AppVersion() As String

    ''' <summary>
    ''' The List(of dataDirectories) which contain all the datafiles
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlArray("DataDirectories")>
    <XmlArrayItem("Directory")>
    Public ReadOnly Property dataDirectories As List(Of DataDirectory)
        Get
            Return _dataDirectories
        End Get
    End Property
#End Region

#Region "App Version Information"
    ''' <summary>
    ''' Checks the current application version against the version in the settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckAppVersion()
        ' Get the current version
        Dim currentVersion As String = Assembly.GetExecutingAssembly.GetName.Version.ToString

        ' Compare versions and inform the user to backup if different
        If Not IsNothing(AppVersion) AndAlso currentVersion <> AppVersion Then
            BackupPrompt()
        End If

        ' Update the app version in the settings
        AppVersion = currentVersion
    End Sub

    ''' <summary>
    ''' Prompts the user to backup the settings file
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BackupPrompt()
        Dim saveFileDialog = New Microsoft.Win32.SaveFileDialog
        Dim backupResult = New MessageBoxResult

        ' Tell the user to backup the settings file
        backupResult = MessageBox.Show(My.Resources.Resources.EvECommandUpdatedMsg, My.Resources.Resources.EvECommandUpdated, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes)

        ' Act according the result of the dialog
        If backupResult = MessageBoxResult.Yes Then
            With saveFileDialog
                .Title = "Backup"
                .Filter = "Settings Backup Files (*.bak) | *.bak"
                .FileName = String.Format("EvE_Command_Settings_{0}.xml.bak", AppVersion)
                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

                ' See if the user pressed save
                If .ShowDialog() Then
                    File.Copy(GetSettingsLocation(settingsFileName), .FileName, True)
                End If
            End With
        End If
    End Sub
#End Region

#Region "Data File Checking"
    ''' <summary>
    ''' Checks to see if the folder parsed in exists and creates where needs be 
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub initialiseDirectory(ByVal initDir As String)
        If Not Directory.Exists(initDir) Then
            Directory.CreateDirectory(initDir)
        End If
    End Sub

    ''' <summary>
    ''' Checks to see if the folder parsed in exists and creates where needs be 
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub initialiseDirectory(ByVal location As dirLocation)
        Dim initDir As String = getDataDir(location)

        initialiseDirectory(initDir)
    End Sub

    ''' <summary>
    ''' Checks through the directories enum to see if the datafiles are present
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckForDataFiles()
        ' Check each of the data directories in the dirLocation enum
        Dim directories As List(Of DataDirectory) = New List(Of DataDirectory)
        directories.Clear()
        directories.Add(FindDirectory(dirLocation.globalDir))
        'directories.Add(FindDirectory(dirLocation.userDir))

        _dataDirectories = directories
    End Sub

    Private Function FindDataFiles(ByVal location As dirLocation, ByVal fileName As String) As DataFile
        ' Create the file location string and check it exists
        Dim fileLocation As String = Path.Combine(getDataDir(location), fileName)
        initialiseDirectory(location)

        ' See if the file exists
        If Not File.Exists(fileLocation) Then
            ' Ok so the file is missing, this is not a problem depending on the directory we are searching in
            If location = dirLocation.userDir Then
                ' TODO: Impliment this
                Return New DataFile(fileName, False)
            Else
                ' If we are here, well its a fresh install I'm afraid. So I'll let the user know
                MessageBox.Show(My.Resources.Resources.MissingDatabaseMsg, My.Resources.Resources.MissingDatabases, MessageBoxButton.OK, MessageBoxImage.Error)
                Return Nothing
            End If
        Else
            ' Return the datafile information
            Return New DataFile(fileName, True)
        End If
    End Function

    Private Function FindDirectory(ByVal location As dirLocation) As DataDirectory
        FindDirectory = New DataDirectory
        FindDirectory.dataFiles = New List(Of DataFile)

        ' Get the directory to look for
        FindDirectory.path = getDataDir(location)

        ' Find out which directory to look for
        Select Case location
            Case dirLocation.globalDir
                ' Create a list of the databases to check for
                Dim dbList As New List(Of String)
                dbList.Add("rssFeeds.sdf")

                ' Now go through them all
                For Each db In dbList
                    Dim foundFile As DataFile = FindDataFiles(location, db)

                    ' Ensure that it the file is there otherwise its the end of the road
                    If IsNothing(foundFile) Then
                        Application.Current.Shutdown()
                    Else
                        FindDirectory.dataFiles.Add(foundFile)
                    End If
                Next
            Case dirLocation.userDir

        End Select

        ' Return the completed directory search
        Return FindDirectory
    End Function

    ''' <summary>
    ''' Get the data directory depending on the dirLocation Enum value parsed in
    ''' </summary>
    ''' <param name="location">The location marker</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getDataDir(ByVal location As dirLocation) As String
        getDataDir = Nothing

        ' Work out the directory of where data is stored by using the dirLocation Enum
        Select Case location
            Case dirLocation.userDir
                getDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ToyerTechnologies", "EvE Command")
            Case dirLocation.globalDir
                getDataDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location), "Databases")
        End Select

        Return getDataDir
    End Function
#End Region

#Region "Settings File Management"
#Region "Non Serialisable Properties"
    ''' <summary>
    ''' The file name of the autosave settings backup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlIgnore()>
    Private Shared ReadOnly Property backupSettings() As String
        Get
            Return String.Format("{0}.backup", settingsFileName)
        End Get
    End Property

    ''' <summary>
    ''' The settings file name, with no directory information. The first time it's called it ensures it initialises the settings file name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlIgnore()>
    Public Shared ReadOnly Property settingsFileName() As String
        Get
            SyncLock threadLock
                If IsNothing(_settingsFileName) Then
                    ' Create the settings file name
                    _settingsFileName = "settings.xml"
#If DEBUG Then
                    _settingsFileName = "debug-settings.xml"
#End If
                End If
            End SyncLock

            Return _settingsFileName
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
        SyncLock threadLock
            If settingsInstance Is Nothing Then
                settingsInstance = Load()
            End If

            ' Return the settings
            Return settingsInstance
        End SyncLock
    End Function

    ''' <summary>
    ''' Saves settings 
    ''' </summary>
    Public Sub Save()
        ' Save the file
        SyncLock threadLock
            ' Create a new file variable for the file stream
            Dim settingsStream As FileStream

            ' Create an xml serialiser variable
            Dim settingsAsXml As XmlSerializer = New XmlSerializer(GetType(Settings))

            ' Create a new filestream
            settingsStream = New FileStream(GetSettingsLocation(settingsFileName), FileMode.Create, FileAccess.Write)

            ' Serialise and save the settings
            settingsAsXml.Serialize(settingsStream, Settings.NewInstance)

            ' Dispose of the variables
            settingsStream.Close()
        End SyncLock
    End Sub
#End Region

#Region "Private Methods"
    ''' <summary>
    ''' Loads the settings from the file
    ''' </summary>
    ''' <remarks>
    ''' If the settings file is 0 length or fails to load, then look for a backup
    ''' copy and ask if that is to be used. Once a settings file is loaded, a backup is taken as a 'last good settings file'
    ''' </remarks>
    ''' <returns>A Settings object loaded from file</returns>
    Private Shared Function Load() As Settings
        ' Construct a location for the settings actual and backup file
        Dim backupLocation As String = GetSettingsLocation(backupSettings)
        Dim settingsLocation As String = GetSettingsLocation(settingsFileName)

        ' Create a settings file object
        Dim _settings As Settings = New Settings

        ' See if either the settings file exists
        If File.Exists(settingsLocation) Then
            _settings = LoadSettingsFile(settingsLocation)
        End If

        ' Ok either the settings loaded or not. Lets see if they did load, and either create an auto backup or try to restore for a previous back up 
        If Not IsNothing(_settings) Then
            ' Create an auto backup
            Try
                File.Copy(settingsLocation, backupLocation, True)
            Catch ex As Exception
                ' Log the error and inform the user of it
                Dim errorLog As logger = New logger(logName.errorLog)
                errorLog.writeToLog(ex.Message)
                MessageBox.Show(My.Resources.Resources.AutoBackupErrorMessage, My.Resources.Resources.BackupError, MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        ElseIf File.Exists(backupLocation) Then
            ' Attempt to restore the backup settings
            _settings = RestoreBackupFile(backupLocation, settingsLocation)
        Else
            ' Create a new settings file
            _settings = GetNewSettings()
        End If

        _settings.CheckAppVersion()
        _settings.CheckForDataFiles()

        Return _settings
    End Function

    ''' <summary>
    ''' Creates the location string for the settings file
    ''' </summary>
    ''' <param name="file"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetSettingsLocation(ByVal file As String) As String
        ' Ensure the directory exists
        initialiseDirectory(dirLocation.userDir)
        Return Path.Combine(getDataDir(dirLocation.userDir), file)
    End Function

    ''' <summary>
    ''' Does the heavy lifting of getting the settings file from the location
    ''' </summary>
    ''' <param name="settingsLocation"></param>
    ''' <remarks></remarks>
    Private Shared Function LoadSettingsFile(ByVal settingsLocation As String) As Settings
        Try
            ' Try loading the settings
            LoadSettingsFile = LoadSettings(settingsLocation)
        Catch ex As Exception
            ' Log the error and let the user know
            Dim errorLog As logger = New logger(logName.errorLog)
            errorLog.writeToLog(ex.Message)
            MessageBox.Show(My.Resources.Resources.SettingsLoadErrorMessage, My.Resources.Resources.SettingsLoadError, MessageBoxButton.OK, MessageBoxImage.Error)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Evaluate the result of the restore message box and either creates a new settings file or loads the backup
    ''' </summary>
    ''' <param name="backupLocation">The location of the backup file</param>
    ''' <param name="settingsLocation">The location of where the settings file will end up</param>
    ''' <param name="restoreStatus">The status of the message box</param>
    ''' <remarks></remarks>
    Private Shared Function evaluateBackupResult(ByVal backupLocation As String, ByVal settingsLocation As String, ByVal restoreStatus As System.Windows.MessageBoxResult) As Settings
        If restoreStatus = MessageBoxResult.Yes Then
            ' Get the settings file
            evaluateBackupResult = LoadSettingsFile(backupLocation)

            ' If the settings loaded OK, copy to the main settings file, then copy back to stamp date
            If IsNothing(evaluateBackupResult) Then
                ' TODO: Tell the user that restoring the backup settings failed and that a new settings file will be created

                ' Create a new settigns file
                evaluateBackupResult = GetNewSettings()
            Else
                Try
                    File.Copy(backupLocation, settingsLocation, True)
                    File.Copy(settingsLocation, backupLocation, True)
                Catch ex As Exception
                    ' TODO: Potentially let the user know that fact that the copying of the settings file failed

                    ' Log the error
                    Dim errorLog As logger = New logger(logName.errorLog)
                    errorLog.writeToLog(ex.Message)

                    ' Create a news settings object
                    evaluateBackupResult = GetNewSettings()
                End Try
            End If
        Else
            ' Create a new settings file
            evaluateBackupResult = GetNewSettings()
        End If
    End Function

    ''' <summary>
    ''' Creates a new settings file
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetNewSettings() As Settings
        Dim _settings As Settings
        _settings = New Settings(Assembly.GetExecutingAssembly.GetName.Version.ToString())
        Return _settings
    End Function

    ''' <summary>
    ''' Attempts to restore the autosaved backup file
    ''' </summary>
    ''' <param name="backupLocation"></param>
    ''' <param name="settingsLocation"></param>
    ''' <remarks></remarks>
    Private Shared Function RestoreBackupFile(ByVal backupLocation As String, ByVal settingsLocation As String) As Settings
        Try
            ' Get the date and time the backup was made
            Dim localWriteTime As Date = (New FileInfo(backupLocation)).LastWriteTime.ToLocalTime
            Dim lastUsed As String = String.Format("{0} at {1}", localWriteTime.ToShortDateString, localWriteTime.ToShortTimeString)

            ' Tell the user we are going to try the auto backup
            Dim restoreStatus As System.Windows.MessageBoxResult
            restoreStatus = MessageBox.Show(String.Format(My.Resources.Resources.SettingsFileCorrupt, lastUsed), My.Resources.Resources.SettingsCorruptTitle, MessageBoxButton.YesNo, MessageBoxImage.Exclamation)

            ' Evaluate the result and restore the backup
            RestoreBackupFile = evaluateBackupResult(backupLocation, settingsLocation, restoreStatus)
        Catch ex As Exception
            ' Log the error and let the user know
            Dim errorLog As logger = New logger(logName.errorLog)
            errorLog.writeToLog(ex.Message)
            MessageBox.Show("EvE Command has been unable to load the backup file information. A new settings file will be created", My.Resources.Resources.BackupError, MessageBoxButton.OK, MessageBoxImage.Error)

            ' Create a new settings file
            RestoreBackupFile = GetNewSettings()
        End Try
    End Function

    ''' <summary>
    ''' Loads the settings from a specified file path
    ''' </summary>
    ''' <param name="settingsLocation">The fully qualified filename for the settings file to be loaded</param>
    ''' <returns>The Settings object loaded</returns>
    Private Shared Function LoadSettings(ByVal settingsLocation As String) As Settings
        ' Create a new file variable for the file stream
        Dim settingsStream As FileStream = New FileStream(settingsLocation, FileMode.Open, FileAccess.Read)

        ' Create an xml serialiser variable
        Dim loadedSettings As XmlSerializer = New XmlSerializer(GetType(Settings))

        ' Get the settings
        LoadSettings = DirectCast(loadedSettings.Deserialize(settingsStream), Settings)

        ' Dispose of the variables
        settingsStream.Close()

        Return LoadSettings
    End Function
#End Region
#End Region
End Class