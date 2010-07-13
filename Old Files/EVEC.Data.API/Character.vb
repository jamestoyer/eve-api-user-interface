Imports EVEC.Data.Database
Imports EVEC.Data.Settings
Imports System.Data.SqlServerCe
Imports System.IO

Public Class Character
    Inherits ApiBase

#Region "URI Constants"
    Const strGetAccountBalance As String = "/char/AccountBalance.xml.aspx"
    Const strGetAssetList As String = "/char/AssetList.xml.aspx"
    Const strGetCharacterSheet As String = "/char/CharacterSheet.xml.aspx"
    Const strGetFacWarStats As String = "/char/FacWarStats.xml.aspx"
    Const strGetIndustryJobs As String = "/char/IndustryJobs.xml.aspx"
    Const strGetKillLog As String = "/char/KillLog.xml.aspx"
    Const strGetMarketOrders As String = "/char/MarketOrders.xml.aspx"
    Const strGetMedals As String = "/char/Medals.xml.aspx"
    Const strGetSkillQueue As String = "/char/SkillQueue.xml.aspx"
    Const strGetStandings As String = "/char/Standings.xml.aspx"
    Const strGetWalletJournal As String = "/char/WalletJournal.xml.aspx"
    Const strGetWalletTransactions As String = "/char/WalletTransactions.xml.aspx"
#End Region

    Public Sub New(ByVal userID As String, ByVal apiKey As String)
        strUserID = userID
        strApiKey = apiKey
    End Sub

    Public Sub New(ByVal userID As String, ByVal apiKey As String, ByVal characterID As String)
        strUserID = userID
        strApiKey = apiKey
        strCharacterID = characterID
    End Sub

#Region "Character Sheet API"
    Dim xmlCharacterSheet As XDocument

    ''' <summary>
    ''' Gets the character sheet for a character
    ''' </summary>
    ''' <returns>True is given if successful otherwise an apiError is returned</returns>
    ''' <remarks></remarks>
    Public Function GetCharacterSheet()
        Dim objResults As Object

        ' Get the api from the server
        objResults = GetApi("CharacterSheet", strGetCharacterSheet, "Userdata.sdf", strUserID, strApiKey, strCharacterID)

        ' Check the object for the next course of action
        If (TypeOf objResults Is XDocument) = True Then
            xmlCharacterSheet = objResults
        Else
            Return objResults
        End If

        ' Commit the results to the database
        GetCharacterSheet = CommitCharacterSheet()
    End Function

    Private Function CommitCharacterSheet()
        Dim objValues As New List(Of Parameter)
        Dim xmlRowsets As XElement = <results></results>
        Dim sqlTransaction As SqlCeTransaction
        Dim objConnection As New Connection
        Dim blnSuccess As Boolean

        ' Open the connection and begin the transaction
        objConnection.Open("Userdata.sdf")
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Declare the userdata database to LinQ object and assign the transaction to it
        Dim objUserdata As New Userdata(objConnection.dbConnection)
        objUserdata.Transaction = sqlTransaction

        ' Check to see if the character sheet already exists
        Dim result = From CharacterSheet In objUserdata.CharacterSheet _
        Where (CharacterSheet.CharacterID = strCharacterID)

        ' Create the values to be inserted or updated
        For Each element In xmlCharacterSheet...<result>.Elements
            If element.Name = "attributeEnhancers" Then
                CreateAttributeEnhancers(objValues, element)
            ElseIf element.Name = "attributes" Then
                CreateAttributes(objValues, element)
            ElseIf element.Name = "rowset" Then
                xmlRowsets.Add(element)
            Else
                ' Create insert detail
                objValues.Add(New Parameter(element.Name.ToString, element.Value))
            End If
        Next

        ' Add the cached until variable
        objValues.Add(New Parameter("cachedUntil", objCachedUntilTime))

        ' Act according to the existance of the character sheet in the database
        If result.Count() < 1 Then
            blnSuccess = Records.Insert(objConnection, sqlTransaction, "CharacterSheet", objValues)
        Else
            Dim objUpdateConditions As New List(Of Parameter)

            ' Create the update conditions
            objUpdateConditions.Add(New Parameter("characterID", strCharacterID))

            ' Ensure the CharacterID is not among the update parameters
            For Each item In objValues
                If item.Field = "characterID" Then
                    objValues.Remove(item)
                    Exit For
                End If
            Next

            ' Update the database
            blnSuccess = Records.Update(objConnection, sqlTransaction, "CharacterSheet", objValues, objUpdateConditions)
        End If

        ' Check to see if the rowsets should be updated
        If blnSuccess = True Then
            Dim objCustomValues As New List(Of Parameter)
            objCustomValues.Add(New Parameter("characterID", strCharacterID))

            ' Send the rowset to the database
            For Each rowset In xmlRowsets.Elements
                blnSuccess = CharacterRowsetToDatabase(objConnection, sqlTransaction, rowset, objCustomValues, False, "cs")
            Next
        End If

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

    Sub CreateAttributeEnhancers(ByRef parameters As List(Of Parameter), ByVal elements As XElement)
        For Each e In elements.Elements
            ' Create the field name
            Dim strField As String
            strField = Left(e.Name.ToString, 1) & "a"

            parameters.Add(New Parameter(strField & "Name", e.<augmentatorName>.Value))
            parameters.Add(New Parameter(strField & "Value", e.<augmentatorValue>.Value))
        Next
    End Sub

    Sub CreateAttributes(ByRef parameters As List(Of Parameter), ByVal elements As XElement)
        For Each a In elements.Elements
            ' Create insert detail
            parameters.Add(New Parameter(a.Name.ToString, a.Value))
        Next
    End Sub
#End Region
End Class
