Imports EVEC.Data
Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Startup(ByVal sender As Object, ByVal e As System.Windows.StartupEventArgs) Handles Me.Startup
        'frmSplash.status = "Loading..."
        'System.Threading.Thread.Sleep(2000)

        'frmSplash.status = "Intialising Settings"
        Dim objSettings As Settings

        ' Attempt to load the settings
        objSettings = Settings.NewInstance

        'frmSplash.status = "Checking Settings"
        If (objSettings.AppVersion <> My.Application.Info.Version.ToString) Then
            ' Update the new boolean
            'blnNewUserVersion = True

            ' Update the application version number and set new user to false
            'frmSplash.status = "Updating Settings"
            objSettings.AppVersion = My.Application.Info.Version.ToString
            'objSettings.NewUser = False

            ' Attempt to save the settings
            'frmSplash.status = "Saving Settings"
NewUserSave:
            Try
                objSettings.Save()
            Catch ex As Exception
                ' Inform the user of the error
                If (MessageBox.Show("Unable to save the settings. Would you like to retry? If you continue to see this error please contact the support team by using the support form", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error) = MessageBoxResult.Yes) Then
                    ' Attempt to save again
                    GoTo NewUserSave
                End If
            End Try
        Else
            ' Update the new boolean
            'blnNewUserVersion = False
        End If

        'frmSplash.status = "Attempting to connect to the database"
        ' Connection class
        'Dim ctnTest As New EVEC.Data.Database.Connection

        '' Test the connection to the database
        'If (ctnTest.Open("Userdata.sdf") = False) Then
        '    ' If there is no connection set the database failure boolean
        '    blnDatabaseFailure = True
        'Else
        '    ' If there is a connection close it
        '    ctnTest.Close()
        '    blnDatabaseFailure = False

        '    frmSplash.status = "Checking for database backups"
        '    ' Variable to hold all the backup information
        '    Dim objBackups As New Backup

        '    ' Get the backups filenames
        '    objBackups.CheckForBackups()

        '    Select Case objBackups.Backups.Count
        '        Case Is < 1
        '            ' Show the create backup form
        '            frmMasterDatabaseBackup.ShowDialog()
        '        Case Is > 0
        '            ' Boolean to decide is a backup is required
        '            Dim blnBackup As Boolean = True

        '            ' Check for the last backup date
        '            frmSplash.status = "Checking last database backup date"
        '            For Each BackupFile In objBackups.Backups
        '                ' Get the difference between the dates
        '                If (Now - BackupFile.DateCreated).TotalDays < 7 Then
        '                    blnBackup = False
        '                End If
        '            Next

        '            If blnBackup = True Then
        '                ' Advise to create a new backup
        '                If MessageBox.Show("The last database backup was over 7 days ago. Would you like to backup now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
        '                    frmMasterDatabaseBackup.ShowDialog()
        '                End If
        '            End If
        '    End Select
        'End If
    End Sub
End Class
