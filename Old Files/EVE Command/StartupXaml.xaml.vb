Imports EVEC.Data.Database
Imports EVEC

Partial Public Class StartupXaml

    Private Sub StartupXaml_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed
        My.Application.Shutdown()
    End Sub

    Private Sub StartupXaml_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ' Load the character portraits
        If LoadCharacterPortraits() = False Then
            Return
        End If

        Dim test As New Data.API.EVE

        Dim testthread As New System.Threading.Thread(AddressOf test.GetSkillTree)
        testthread.Start()

     
    End Sub

    Private Function LoadCharacterPortraits() As Boolean
        ' Connect to the database
        Dim objConnection As New Connection()
        If objConnection.Open("Userdata.sdf") = False Then
            MessageBox.Show("Unable to load the characters", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
            Return False
        End If

        ' Declare the userdata database to LinQ object and assign the transaction to it
        Dim objUserdata As New Userdata(objConnection.dbConnection)

GetCharacters:
        ' Check to see if the character already exists
        Dim result = From Characters In objUserdata.Characters _
                     Where Characters.Active = True _
                     Order By Characters.Name

        ' Clear the list box and reload it
        lstPhotoListBox.Items.Clear()

        ' Check to see how many result there are
        If result.Count < 1 Then
            Dim AddAccount As New wpfAccountModifier

            MessageBox.Show("It appears thats you have no accounts associtated with running EvE Command. Please add your account details to begin.", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)

            If AddAccount.Add = True Then
                ' Attempt to get the characters
                GoTo GetCharacters
            End If
        Else
            For Each c In result
                Dim lstNewItem As New ListBoxItem
                Dim objCharPortrait As New PhotoListItem

                ' Create a new portrait
                objCharPortrait.Portrait = BitmapFrame.Create(New Uri("http://img.eve.is/serv.asp?s=256&c=" & c.CharacterID))
                objCharPortrait.ImageText = c.Name
                lstNewItem.Content = objCharPortrait
                lstNewItem.Name = c.Name

                ' Add the portrait to the list items
                lstPhotoListBox.Items.Add(lstNewItem)
            Next

            lstPhotoListBox.SelectedIndex = 0
        End If

        ' close the connection
        objConnection.Close()

        Return True
    End Function

    Private Sub mnuManageAccounts_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim frmAccountManager As New wpfAccountsManager

        ' Show the add account form
        If frmAccountManager.ManageCharacters = True Then
            ' Load the character portraits
            LoadCharacterPortraits()
        End If
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Close()
    End Sub
End Class
