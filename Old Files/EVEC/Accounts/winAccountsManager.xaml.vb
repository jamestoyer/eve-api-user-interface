Imports EVEC.Common
Imports EVEC.Common.Controls
Imports EVEC.Data.Database
Imports System.Data.SqlServerCe

Partial Public Class winAccountsManager
    Dim blnCharactersUpdated As Boolean

    Public Function ManageCharacters() As Boolean
        Me.ShowDialog()
        Return blnCharactersUpdated
    End Function

    Private Sub winAccountsManager_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ' Get the accounts
        UpdateAccounts()
    End Sub

    Private Sub CloseForm(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim objAddAccount As New winModifyAccount
        If objAddAccount.Add = True Then
            ' Update the accounts
            UpdateAccounts()
            blnCharactersUpdated = True
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim objConnection As New Connection
        Dim sqlTransaction As SqlCeTransaction

        ' Connect to the database
        objConnection.Open("Userdata.sdf")
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Create and new userdata object
        Dim objUserdata As New Userdata(objConnection.dbConnection)
        objUserdata.Transaction = sqlTransaction

        ' Get the selected record
        If lstAccounts.SelectedIndex > -1 Then
            ' Inform the user of the consequences
            If MessageBox.Show("Are you sure you want to delete the account? All information in EvE Command relating to this account will be deleted.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) = MessageBoxResult.No Then Return

            ' Get the selected item
            Dim lstItem As ListBoxItem
            Dim objAccount As New AccountViewItem
            lstItem = lstAccounts.SelectedItem
            objAccount = lstItem.Content

            Dim objUser = From u In objUserdata.UserInfo _
                          Where u.UserID = objAccount.UserID.Trim

            ' Convert the result into the user info class
            Dim objDeleteUser As New UserInfo
            For Each u In objUser
                objDeleteUser = u
            Next

            ' Attempt to delete the account
            objUserdata.UserInfo.DeleteOnSubmit(objDeleteUser)
            Try
                objUserdata.SubmitChanges()
                sqlTransaction.Commit()
            Catch ex As Exception
                sqlTransaction.Rollback()
                MessageBox.Show("Unable to delete the account. Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
                Return
            Finally
                objConnection.Close()
            End Try

            ' Update the accounts
            UpdateAccounts()
            blnCharactersUpdated = True
        Else
            MessageBox.Show("Please select an account to delete", "Select Account", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK)
        End If
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim objmodifyAccount As New winModifyAccount
        ' Check the selected index of the list box
        If lstAccounts.SelectedIndex > -1 Then
            ' Get the selected item
            Dim lstItem As ListBoxItem
            Dim objAccount As New AccountViewItem
            lstItem = lstAccounts.SelectedItem
            objAccount = lstItem.Content

            ' Open the modify form
            If objmodifyAccount.Modify(objAccount.UserID) = True Then
                ' Update the accounts
                UpdateAccounts()
                blnCharactersUpdated = True
            End If
        Else
            MessageBox.Show("Please select an account to modify", "Select Account", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK)
        End If
    End Sub

    Private Function UpdateAccounts() As Boolean
        Dim objConnection As New Connection
        Dim sqlTransaction As SqlCeTransaction

        ' Connect to the database
        objConnection.Open("Userdata.sdf")
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Create and new userdata object
        Dim objUserdata As New Userdata(objConnection.dbConnection)
        objUserdata.Transaction = sqlTransaction

        ' Clear the listbox of items
        lstAccounts.Items.Clear()

        ' Get the accounts in the database
        Dim objUsers = From u In objUserdata.UserInfo _
                       Order By u.UserID _
                       Select u.UserID

        For Each u In objUsers
            ' Create an account
            Dim objAccount As New AccountViewItem
            Dim strUserID As String
            strUserID = u
            objAccount.UserID = strUserID

            Dim objCharacters = From c In objUserdata.Characters _
                                Where c.UserID = strUserID _
                                Order By c.Name _
                                Select c.Name, c.Active

            ' Get the character list
            Dim objCharacterList As New AccountCharacterList
            For Each c In objCharacters
                Dim objSingleCharacter As New AccountCharacterListItem
                objSingleCharacter.Active = c.Active
                objSingleCharacter.Character = c.Name
                objCharacterList.Add(objSingleCharacter)
            Next

            ' Add the account to the list box
            Dim lstItem As New ListBoxItem
            objAccount.Items = objCharacterList
            lstItem.Content = objAccount
            lstAccounts.Items.Add(lstItem)
        Next

        lstAccounts.SelectedIndex = 0
    End Function
End Class
