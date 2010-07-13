Imports EVEC.Data
Imports EVEC.Data.API
Imports EVEC.Data.Database
Imports System.Data.SqlServerCe
Imports System.Text.RegularExpressions

Partial Public Class wpfAccountModifier
    ''' <summary>
    ''' Shows if the characters have been retrieved
    ''' </summary>
    ''' <remarks></remarks>
    Private blnCharactersRetrieved As Boolean
    Dim objConnection As New Connection
    Private blnOkPressed As Boolean
    Dim objUserdata As Userdata
    Private objCharacterView As BindingListCollectionView
    Private blnModifyForm As Boolean
    Private strUserID As String

    Public Function Modify(ByVal modifyUserID As String) As Boolean
        blnModifyForm = True
        strUserID = modifyUserID

        Me.ShowDialog()
        Return blnOkPressed
    End Function

    Public Function Add() As Boolean
        Me.ShowDialog()
        Return blnOkPressed
    End Function

    Private Sub lnkAPI_RequestNavigate(ByVal sender As System.Object, ByVal e As System.Windows.Navigation.RequestNavigateEventArgs)
        ' Navigate to the link
        System.Diagnostics.Process.Start(lnkAPI.NavigateUri.ToString)
    End Sub

    Private Sub wpfAccountModifier_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Close the connection
        objConnection.Close()
    End Sub

    Private Sub wpfAccountModifier_Loaded() Handles MyBase.Loaded
        ' Open the connection, begin the transaction and connect the user data object to the database
        objConnection.Open("Userdata.sdf")
        objUserdata = New Userdata(objConnection.dbConnection)

        If blnModifyForm = True Then
            Dim objUser = From users In objUserdata.UserInfo _
                          Where users.UserID = strUserID _
                          Select users.UserID, users.ApiKey

            For Each result In objUser
                txtApiKey.Text = result.ApiKey
                txtUserID.Text = result.UserID
            Next

            ' Populate the listview and disable it
            BindDataGrid()
            lstCharacters.IsEnabled = False
        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim sqlTransaction As SqlCeTransaction
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Check for errors
        If EvaluateErrors() = True Then Return

        ' Act according to the characters retrieved boolean
        If blnCharactersRetrieved = False Then
            ' Attempt to get the characters for the user
            blnCharactersRetrieved = GetCharacters(objConnection, sqlTransaction)
            If blnCharactersRetrieved = True Then
                txtApiKey.IsEnabled = False
                txtUserID.IsEnabled = False
                btnOk.Content = "Ok"
                lstCharacters.IsEnabled = True
            End If
        ElseIf blnCharactersRetrieved = True Then
            Dim blnSuccess As Boolean

            ' Update the database
            Try
                objUserdata.SubmitChanges()
                blnSuccess = True
            Catch ex As Exception
                blnSuccess = False
            End Try

            ' Attempt to submit the changes
            If blnSuccess = True Then
                Try
                    sqlTransaction.Commit()
                Catch ex As Exception
                    sqlTransaction.Rollback()
                End Try
            End If
            blnOkPressed = True
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Determins the error and takes the appropriate action
    ''' </summary>
    ''' <returns>If there is an error true is returned otherwise false</returns>
    ''' <remarks></remarks>
    Private Function EvaluateErrors() As Boolean
        Dim strMessageBoxString As String = ""

        ' Reset the text box backgrounds
        txtApiKey.Background = System.Windows.Media.Brushes.Transparent
        txtUserID.Background = System.Windows.Media.Brushes.Transparent

        ' Check the userID text box has an entry
        If txtUserID.Text.Trim.Length = 0 Or (Not Regex.Match(txtUserID.Text.Trim, "^[0-9]*$").Success) Then
            txtUserID.Background = System.Windows.Media.Brushes.MistyRose
            EvaluateErrors = True

            ' Create a message for the user
            strMessageBoxString = "Please enter a valid numeric user ID"
        End If

        ' Check the apiKey text box for a value and that it is the correct length
        If txtApiKey.Text.Trim.Length < 64 Or (Not Regex.Match(txtApiKey.Text.Trim, "^\w*$").Success) Then
            txtApiKey.Background = System.Windows.Media.Brushes.MistyRose
            EvaluateErrors = True

            ' Create a message for the user
            If strMessageBoxString.Length > 0 Then
                strMessageBoxString &= " and an alpha numeric API key of 64 characters"
            Else
                strMessageBoxString = "Please enter a valid alpha numeric API key of 64 characters"
            End If
        End If

        ' Inform the user of the problem
        If EvaluateErrors = True Then
            MessageBox.Show(strMessageBoxString)
        End If
    End Function

    Private Function GetCharacters(ByVal connection As Connection, ByVal transaction As SqlCeTransaction) As Boolean
        Dim objAccount As New Account(txtUserID.Text.Trim, txtApiKey.Text.Trim)
        Dim objResult As Object

        ' Check the users details
        objResult = objAccount.CheckUserDetails(connection, transaction)
        If (TypeOf objResult Is Boolean) = True Then
            ' Commit the transaction
            transaction.Commit()

            ' Populate the data grid view
            objResult = objAccount.GetCharacters
            If (TypeOf objResult Is Boolean) = True Then
                BindDataGrid()
            Else
                ' Evaluate the error
                Dim objError As ApiError = objResult
                If objError.FullCode <> (UserErrors.BaseCategory * 100) + UserErrors.UpdateTooEarly Then
                    ' TODO: See if this can be more informative
                    MessageBox.Show(objResult.ToString)
                    Return False
                Else
                    BindDataGrid()
                End If
            End If

        Else
            ' Rollback the transaction
            transaction.Rollback()
            ' TODO: See if this can be more informative
            MessageBox.Show(objResult.ToString)
            Return False
        End If
        Return True
    End Function

    Private Sub BindDataGrid()
        Dim objCharacters = From c In objUserdata.Characters _
                            Where c.UserID = txtUserID.Text.Trim _
                            Order By c.Name

        Me.DataContext = objCharacters
        Me.objCharacterView = CType(CollectionViewSource.GetDefaultView(Me.DataContext),  _
                                BindingListCollectionView)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Close()
    End Sub
End Class
