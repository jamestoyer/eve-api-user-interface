Imports EVEC.Data.Database
Imports EVEC.Data.Settings
Imports System.Data.SqlServerCe
Imports System.IO

Public Class Account
    Inherits ApiBase
#Region "URI Constants"
    Const strGetCharacters As String = "/account/Characters.xml.aspx"
#End Region

    Public Sub New(ByVal userID As String, ByVal apiKey As String)
        strUserID = userID
        strApiKey = apiKey
    End Sub

#Region "Characters API"
    Dim xmlCharacterResults As XDocument

    ''' <summary>
    ''' Gets the characters for the userID and saves them to database
    ''' </summary>
    ''' <returns>True is given if successful otherwise an apiError is returned</returns>
    ''' <remarks></remarks>
    Public Function GetCharacters()
        Dim objResults As Object

        ' Get the api from the server
        objResults = GetApi("Characters", strGetCharacters, "Userdata.sdf", strUserID, strApiKey)

        ' Check the object for the next course of action
        If (TypeOf objResults Is XDocument) = True Then
            xmlCharacterResults = objResults
        Else
            Return objResults
        End If

        ' Commit the results to the database
        GetCharacters = CommitCharacters()
    End Function

    Private Function CommitCharacters()
        Dim objInsertValues As New List(Of Parameter)
        Dim sqlTransaction As SqlCeTransaction
        Dim objConnection As New Connection
        Dim blnSuccess As Boolean

        ' Open the connection and begin the transaction
        objConnection.Open("Userdata.sdf")
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Declare the userdata database to LinQ object and assign the transaction to it
        Dim objUserdata As New Userdata(objConnection.dbConnection)
        objUserdata.Transaction = sqlTransaction

        ' Create custom insert details
        objInsertValues.Add(New Parameter("userID", strUserID))
        objInsertValues.Add(New Parameter("active", True))

        ' Send the rowset to the database
        For Each rowset In xmlCharacterResults...<result>.Elements
            blnSuccess = AccountRowsetToDatabase(objConnection, sqlTransaction, rowset, objInsertValues, True)
        Next
       
        ' Act according the the success
        If blnSuccess = True Then
            sqlTransaction.Commit()
        Else
            sqlTransaction.Rollback()
        End If

        ' Close the connection
        objConnection.Close()

        ' Create an error if the update was unsuccessful
        If blnSuccess <> True Then
            Return New ApiError(UserErrors.UnableToSave)
        End If

        Return blnSuccess
    End Function
#End Region
End Class

