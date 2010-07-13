Imports EVEC.Data
Imports System.Windows.Threading

Partial Public Class winSplashScreen
    Dim tmrStartCheck As New DispatcherTimer
    Dim blnLoaded As Boolean

    Public Property status() As String
        Get
            Return lblStatus.Content
        End Get
        Set(ByVal value As String)
            lblStatus.Content = value
        End Set
    End Property

    Private Sub winSplashScreen_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ' Start a time to wait before calling the application startup script
        tmrStartCheck.Interval = TimeSpan.FromMilliseconds(2000)
        AddHandler tmrStartCheck.Tick, AddressOf RunStartChecks
        tmrStartCheck.Start()
        blnLoaded = True
    End Sub

    Private Sub RunStartChecks(ByVal sender As Object, ByVal e As EventArgs)
        If blnLoaded = True Then
            ' Set the loaded boolean to false
            blnLoaded = False

            ' Initialise the settings
            Me.status = "Intialising Settings"
            Dim objSettings As Settings

            ' Attempt to load the settings
            objSettings = Settings.NewInstance

            Me.status = "Checking Settings"
            If (objSettings.AppVersion <> My.Application.Info.Version.ToString) Then
                ' Update the application version number and set new user to false
                Me.status = "Updating Settings"
                objSettings.AppVersion = My.Application.Info.Version.ToString
            End If

            ' Check that the databases exist
            Me.status = "Checking Datafiles"
            For Each file In objSettings.DataFileNames
                If file.Found = False Then
                    ' See if the user wishes to continue anyway
                    If MessageBox.Show(String.Format("The {0} datafile is missing. Would you like to continue? If you continue to see this message please reinstall EVE Command", file.FileName), "Datafile Missing", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.Yes) = MessageBoxResult.No Then
                        My.Application.Shutdown()
                    End If
                End If
            Next

            ' Save the settings
            Me.status = "Saving Settings"
Save:
            Try
                objSettings.Save()
            Catch ex As Exception
                ' Inform the user of the error
                If (MessageBox.Show("Unable to save the settings. Would you like to retry? If you continue to see this error please contact the support team by using the support form", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error) = MessageBoxResult.Yes) Then
                    ' Attempt to save again
                    GoTo Save
                End If
            End Try

            ' Check to see if the skills api needs to be updated
            Me.status = "Getting skills tree"
            Dim objEveAPI As New API.EVE
            objEveAPI.GetSkillTree()

            ' Stop the timer, open the main form and close the splash screen
            Dim objMain As New winMain
            objMain.Show()
            tmrStartCheck.Stop()
            Me.Close()
        End If
       
    End Sub

End Class
