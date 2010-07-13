Imports EVEC.Data.Database
Imports System.Data.SqlServerCe
Imports System.IO

Public Class ApiBase
    Const strApiUri As String = "http://api.eve-online.com"
    Public objCachedUntilTime As DateTime
    Public strApiKey As String
    Public strUserID As String
    Public strCharacterID As String
    Public strCorporationID As String

    ''' <summary>
    ''' Checks to see if a new call can be made to the api by checking the cachedUntil time in the database
    ''' </summary>
    ''' <param name="database">The database to get the data from</param>
    ''' <param name="tableName">The table the api info is stored in</param>
    ''' <returns>Returns true if an update can be obtained and false if it cannot</returns>
    ''' <remarks></remarks>
    Private Function CheckCachedUntilTime(ByVal database As String, ByVal tableName As String) As Boolean
        CheckCachedUntilTime = CheckCachedUntilTime(database, tableName, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Checks to see if a new call can be made to the api by checking the cachedUntil time in the database
    ''' </summary>
    ''' <param name="database">The database to get the data from</param>
    ''' <param name="tableName">The table the api info is stored in</param>
    ''' <param name="conditionField">The condition field to look in</param>
    ''' <param name="conditionValue">The condition value to look for</param>
    ''' <returns>Returns true if an update can be obtained and false if it cannot</returns>
    ''' <remarks></remarks>
    Private Function CheckCachedUntilTime(ByVal database As String, ByVal tableName As String, ByVal conditionField As String, ByVal conditionValue As String) As Boolean
        Dim strCommand As String
        Dim tblResults As DataTable

        ' Check for conditions
        If conditionField = Nothing AndAlso conditionValue = Nothing Then
            ' Create the sql select string
            strCommand = String.Format("SELECT cachedUntil FROM {0}", tableName)
        Else
            ' Create the sql select string
            strCommand = String.Format("SELECT cachedUntil FROM {0} WHERE {1} = '{2}'", tableName, conditionField, conditionValue)
        End If


        ' Get the result of the query
        tblResults = DataTables.Create(strCommand, database)

        ' Check to see if a datatable is returned
        If (tblResults IsNot Nothing) AndAlso (tblResults.Rows.Count > 0) Then
            ' Check the cache time to see if an update can be aquired
            For Each row In tblResults.Rows
                Dim objCachedTime As DateTime = row!cachedUntil

                ' Compare the dates
                If (Now - objCachedTime).TotalSeconds < 0 Then
                    CheckCachedUntilTime = False
                Else
                    CheckCachedUntilTime = True
                End If
            Next
        Else
            CheckCachedUntilTime = True
        End If

        Return CheckCachedUntilTime
    End Function

    Public Function CreateUpdateParameters(ByVal currentRow As XElement, ByVal cachedUntil As DateTime) As List(Of Parameter)
        Dim objUpdateValues As New List(Of Parameter)
        ' Create the parameter array for each attribute value in the row
        For Each a In currentRow.Attributes
            objUpdateValues.Add(New Parameter(a.Name.ToString, a.Value))
        Next
        objUpdateValues.Add(New Parameter("cachedUntil", cachedUntil))

        Return objUpdateValues
    End Function

    Public Function CreateUpdateParameters(ByVal currentRow As XElement, ByVal cachedUntil As DateTime, ByVal customValues As List(Of Parameter)) As List(Of Parameter)
        Dim objUpdateValues As New List(Of Parameter)

        ' Add the custom values to the update values
        objUpdateValues = customValues

        ' Create the parameter array for each attribute value in the row
        For Each a In currentRow.Attributes
            objUpdateValues.Add(New Parameter(a.Name.ToString, a.Value))
        Next
        objUpdateValues.Add(New Parameter("cachedUntil", cachedUntil))

        Return objUpdateValues
    End Function

    Public Function GetCacheUntilDate(ByVal currentTime As String, ByVal cachedUntil As String) As DateTime
        Dim objTime As DateTime
        Dim objCurrentTime As DateTime
        Dim objCachedUntil As DateTime

        ' Get the current time
        objTime = Now

        ' Convert the strings
        objCurrentTime = currentTime
        objCachedUntil = cachedUntil

        ' Set the cache until time
        GetCacheUntilDate = objCachedUntil + (objTime - objCurrentTime)
    End Function

#Region "Rowset Management"
    Public Function InsertRowsetIntoDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, Optional ByVal cachedUntil As Boolean = False) As Boolean
        InsertRowsetIntoDatabase = InsertRowsetIntoDatabase(connection, transaction, rowset, New List(Of Parameter), cachedUntil)
    End Function

    Public Function InsertRowsetIntoDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, ByVal customValues As List(Of Parameter), Optional ByVal cachedUntil As Boolean = False) As Boolean
        ' Create the parameter array for each attribute value in the row
        For Each row In rowset.Elements
            ' Add the custom values to the insert values
            Dim objInsertValues As New List(Of Parameter)
            objInsertValues = New List(Of Parameter)
            For Each p In customValues
                objInsertValues.Add(p)
            Next

            For Each a In row.Attributes
                objInsertValues.Add(New Parameter(a.Name.ToString, a.Value))
            Next

            If cachedUntil = True Then
                objInsertValues.Add(New Parameter("cachedUntil", objCachedUntilTime))
            End If

            ' Insert the values into the database
            InsertRowsetIntoDatabase = Records.Insert(connection, transaction, rowset.@name, objInsertValues)
        Next
    End Function

    ''' <summary>
    ''' Inserts rowset into the database with relation to a user
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AccountRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objCustomValues As New List(Of Parameter)

        ' Send the data to the database
        AccountRowsetToDatabase = AccountRowsetToDatabase(connection, transaction, rowset, objCustomValues, cachedUntil, tablePrefix)
    End Function

    ''' <summary>
    ''' Inserts rowset into the database with relation to a user
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="customValues"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AccountRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, ByVal customValues As List(Of Parameter), Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objUpdateConditions As New List(Of Parameter)
        Dim objUpdateValues As New List(Of Parameter)

        ' Create the update conditions
        objUpdateConditions.Add(New Parameter("userID", strUserID))
        objUpdateConditions.Add(New Parameter("characterID", strCharacterID))

        ' Ensure the CharacterID is not among the update parameters
        For Each item In customValues
            If item.Field <> "characterID" Then
                objUpdateValues.Add(item)
            End If
        Next

        ' Send the data to the database
        AccountRowsetToDatabase = RowsetToDatabase(connection, transaction, rowset, "user", customValues, objUpdateValues, objUpdateConditions, cachedUntil, tablePrefix)
    End Function

    ''' <summary>
    ''' Inserts rowset into the database with relation to a character
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CharacterRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objCustomValues As New List(Of Parameter)

        ' Send the data to the database
        CharacterRowsetToDatabase = CharacterRowsetToDatabase(connection, transaction, rowset, objCustomValues, cachedUntil, tablePrefix)
    End Function

    ''' <summary>
    ''' Inserts rowset into the database with relation to a character
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="customValues"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CharacterRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, ByVal customValues As List(Of Parameter), Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objUpdateConditions As New List(Of Parameter)

        ' Create the update conditions
        objUpdateConditions.Add(New Parameter("characterID", strCharacterID))

        ' Send the data to the database
        CharacterRowsetToDatabase = RowsetToDatabase(connection, transaction, rowset, "character", customValues, customValues, objUpdateConditions, cachedUntil, tablePrefix)
    End Function

    Private Function InsertRow(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal row As XElement, ByVal table As String, ByVal customInsertValues As List(Of Parameter), ByVal cachedUntil As Boolean) As Boolean
        ' Add the custom values to the insert values
        Dim objInsertValues As New List(Of Parameter)
        objInsertValues = New List(Of Parameter)
        For Each p In customInsertValues
            objInsertValues.Add(p)
        Next

        ' Create the parameter array
        For Each a In row.Attributes
            objInsertValues.Add(New Parameter(a.Name.ToString, a.Value))
        Next

        If cachedUntil = True Then
            objInsertValues.Add(New Parameter("cachedUntil", objCachedUntilTime))
        End If

        ' Insert the values into the database
        InsertRow = Records.Insert(connection, transaction, table, objInsertValues)

    End Function

    ''' <summary>
    ''' Inserts rowset into the database that has been obtained without api information
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NoApiInfoRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objCustomValues As New List(Of Parameter)

        ' Send the data to the database
        NoApiInfoRowsetToDatabase = NoApiInfoRowsetToDatabase(connection, transaction, rowset, objCustomValues, cachedUntil, tablePrefix)
    End Function

    ''' <summary>
    ''' Inserts rowset into the database that has been obtained without api information
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="customValues"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NoApiInfoRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, ByVal customValues As List(Of Parameter), Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objUpdateConditions As New List(Of Parameter)

        ' Send the data to the database
        NoApiInfoRowsetToDatabase = RowsetToDatabase(connection, transaction, rowset, "other", customValues, customValues, objUpdateConditions, cachedUntil, tablePrefix)
    End Function

    Private Function RowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, ByVal rowsetType As String, ByVal customInsertValues As List(Of Parameter), ByVal customUpdateValues As List(Of Parameter), ByVal updateConditions As List(Of Parameter), Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim strRowsetKey As String
        Dim strTable As String
        Dim strCommand As String
        Dim objResults As DataTable
        Dim strRowsetTypeID As String = ""

        ' Get the rowset key and the table name
        strTable = rowset.@name
        strRowsetKey = rowset.@key

        ' Check to see if there is a table prefix
        If tablePrefix <> Nothing Then
            strTable = tablePrefix & strTable
        End If

        ' Check to see if this a table with corporation in the title
        If InStr(strTable, "corporation") > 0 Then
            strTable = Replace(strTable, "corporation", "corp")
        End If

        ' Set the appropriate rowtype id
        Select Case rowsetType
            Case "character"
                strRowsetTypeID = strCharacterID
            Case "corporation"
                strRowsetTypeID = strCorporationID
            Case "user"
                strRowsetTypeID = strUserID
            Case "skillType"
                strRowsetTypeID = customInsertValues.Item(0).Value
        End Select

        ' Find out if the rowset contains elements
        If rowset.Elements.Count > 0 Then
            ' Go through each row and decided what needs to be inserted and what needs to be updated
            For Each row In rowset.Elements
                ' Check to ensure the rowset key is not the same as the rowset type
                If (rowsetType & "ID") = strRowsetKey Then
                    strCommand = String.Format("SELECT * FROM {0} WHERE {1}ID = {2}", strTable, rowsetType, strRowsetTypeID)
                ElseIf rowsetType = "other" Then
                    strCommand = String.Format("SELECT * FROM {0} WHERE {1} = {2}", strTable, strRowsetKey, row.Attribute(strRowsetKey).Value)
                ElseIf row.Attribute(strRowsetKey).Value.Length > 10 Then
                    strCommand = String.Format("SELECT * FROM {0} WHERE {1}ID = {2} AND {3} = '{4}'", strTable, rowsetType, strRowsetTypeID, strRowsetKey, row.Attribute(strRowsetKey).Value)
                Else
                    strCommand = String.Format("SELECT * FROM {0} WHERE {1}ID = {2} AND {3} = {4}", strTable, rowsetType, strRowsetTypeID, strRowsetKey, row.Attribute(strRowsetKey).Value)
                End If
                objResults = DataTables.Create(strCommand, connection, transaction)

                ' See if the details need to be updated or inserted
                If objResults IsNot Nothing Then
                    If objResults.Rows.Count < 1 Then
                        RowsetToDatabase = InsertRow(connection, transaction, row, strTable, customInsertValues, cachedUntil)
                    Else
                        RowsetToDatabase = UpdateRow(connection, transaction, row, strRowsetKey, strTable, updateConditions, customUpdateValues, cachedUntil)
                    End If
                End If
            Next
        Else
            RowsetToDatabase = True
        End If
        

    End Function

    ''' <summary>
    ''' Inserts rowset into the database with relation to a skill
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SkillTypeIDRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objCustomValues As New List(Of Parameter)

        ' Send the data to the database
        SkillTypeIDRowsetToDatabase = SkillTypeIDRowsetToDatabase(connection, transaction, rowset, objCustomValues, cachedUntil, tablePrefix)
    End Function

    ''' <summary>
    ''' Inserts rowset into the database with relation to a skill
    ''' </summary>
    ''' <param name="connection"></param>
    ''' <param name="transaction"></param>
    ''' <param name="rowset"></param>
    ''' <param name="customValues"></param>
    ''' <param name="cachedUntil"></param>
    ''' <param name="tablePrefix"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SkillTypeIDRowsetToDatabase(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal rowset As XElement, ByVal customValues As List(Of Parameter), Optional ByVal cachedUntil As Boolean = False, Optional ByVal tablePrefix As String = Nothing) As Boolean
        Dim objUpdateConditions As New List(Of Parameter)
        Dim objUpdateValues As New List(Of Parameter)

        ' Create the update conditions
        objUpdateConditions.Add(customValues.Item(0))

        ' Ensure the skillTypeID is not among the update parameters
        For Each item In customValues
            If item.Field <> "skillTypeID" Then
                objUpdateValues.Add(item)
            End If
        Next

        ' Send the data to the database
        SkillTypeIDRowsetToDatabase = RowsetToDatabase(connection, transaction, rowset, "skillType", customValues, objUpdateValues, objUpdateConditions, cachedUntil, tablePrefix)
    End Function

    Private Function UpdateRow(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal row As XElement, ByVal rowKeyName As String, ByVal table As String, ByVal updateConditions As List(Of Parameter), ByVal customUpdateValues As List(Of Parameter), ByVal cachedUntil As Boolean) As Boolean
        ' Add the custom values to the update values
        Dim objUpdateValues As New List(Of Parameter)
        Dim objUpdateConditions As New List(Of Parameter)
        objUpdateValues = New List(Of Parameter)
        For Each p In customUpdateValues
            objUpdateValues.Add(p)
        Next

        ' Check to see if the characterID condition is assigned
        For Each c In updateConditions
            If c.Field = "characterID" AndAlso c.Value = Nothing Then
                objUpdateConditions.Add(New Parameter(c.Field, row.@characterID))
            Else
                objUpdateConditions.Add(c)
            End If
        Next

        ' Create the parameter array
        For Each a In row.Attributes
            ' Check to see if the parameter is the key and if it is do not add it to the parameter array but add it to the update condition array
            If a.Name.ToString.Trim <> rowKeyName.Trim Then
                objUpdateValues.Add(New Parameter(a.Name.ToString, a.Value))
            Else
                objUpdateConditions.Add(New Parameter(a.Name.ToString, a.Value))
            End If
        Next

        If cachedUntil = True Then
            objUpdateValues.Add(New Parameter("cachedUntil", objCachedUntilTime))
        End If

        ' Insert the values into the database
        UpdateRow = Records.Update(connection, transaction, table, objUpdateValues, objUpdateConditions)
    End Function
#End Region

#Region "API Get Methods"
    ' TODO: Streamline API get methods
#Region "No Extra Input"
    ''' <summary>
    ''' Gets the API from the server 
    ''' </summary>
    ''' <param name="apiName">The name of the api retrieved from the api server</param>
    ''' <param name="apiLocation">The URI of the api</param>
    ''' <param name="database">The database to send the API to</param>
    ''' <returns>The XDocument if successful or an ApiError if unsuccessful</returns>
    ''' <remarks></remarks>
    Public Function GetApi(ByVal apiName As String, ByVal apiLocation As String, ByVal database As String) As Object
        Dim blnCacheResult As Boolean

        ' Check to see if an update can be obtained
        blnCacheResult = CheckCachedUntilTime(database, apiName)
        If blnCacheResult = True Then
            GetApi = GetApiIgnoreCache(apiName, apiLocation)
            Return GetApi
        Else
            Return New ApiError(UserErrors.UpdateTooEarly)
        End If

    End Function

    ''' <summary>
    ''' Gets the API from the server but ignores the cache timer
    ''' </summary>
    ''' <param name="apiName">The name of the api retrieved from the api server</param>
    ''' <param name="apiLocation">The URI of the api</param>
    ''' <returns>The XDocument if successful or an ApiError if unsuccessful</returns>
    ''' <remarks></remarks>
    Public Function GetApiIgnoreCache(ByVal apiName As String, ByVal apiLocation As String) As Object

        Try
            ' Get the data from the api server
            GetApiIgnoreCache = XDocument.Load(String.Format("{0}{1}", strApiUri, apiLocation))

            ' Cache it to the cache directory
            Try
                GetApiIgnoreCache.Save(String.Format("{0}{1}Cache{1}{2}.xml", Settings.DataDir, Path.DirectorySeparatorChar, apiName))
            Catch ex As Exception
            End Try

        Catch ex As Exception
            Return New ApiError(UserErrors.UnableToLoad)
        End Try

        ' Check to see if there was an error
        Dim objError As Object = ApiError.CheckForError(GetApiIgnoreCache)
        If (TypeOf objError Is Boolean) = True Then
            Dim xmlResults As XDocument
            xmlResults = GetApiIgnoreCache

            ' Get the cached until time
            objCachedUntilTime = GetCacheUntilDate(xmlResults...<currentTime>.Value, xmlResults...<cachedUntil>.Value)

            ' Return the xml document
            Return xmlResults
        Else
            Return objError
        End If
    End Function
#End Region

#Region "userID + apiKey Input"
    ''' <summary>
    ''' Gets the API from the server 
    ''' </summary>
    ''' <param name="apiName">The name of the api retrieved from the api server</param>
    ''' <param name="apiLocation">The URI of the api</param>
    ''' <param name="database">The database to send the API to</param>
    ''' <param name="userID">The user ID required for the API</param>
    ''' <param name="apiKey">The API key required to the get the API</param>
    ''' <returns>The XDocument if successful or an ApiError if unsuccessful</returns>
    ''' <remarks></remarks>
    Public Function GetApi(ByVal apiName As String, ByVal apiLocation As String, ByVal database As String, ByVal userID As String, ByVal apiKey As String) As Object
        Dim blnCacheResult As Boolean

        ' Check to see if an update can be obtained
        blnCacheResult = CheckCachedUntilTime(database, apiName, "userID", userID)
        If blnCacheResult = True Then
            GetApi = GetApiIgnoreCache(apiName, apiLocation, userID, apiKey)
            Return GetApi
        Else
            Return New ApiError(UserErrors.UpdateTooEarly)
        End If

    End Function

    ''' <summary>
    ''' Gets the API from the server but ignores the cache timer
    ''' </summary>
    ''' <param name="apiName">The name of the api retrieved from the api server</param>
    ''' <param name="apiLocation">The URI of the api</param>
    ''' <param name="userID">The user ID required for the API</param>
    ''' <param name="apiKey">The API key required to the get the API</param>
    ''' <returns>The XDocument if successful or an ApiError if unsuccessful</returns>
    ''' <remarks></remarks>
    Public Function GetApiIgnoreCache(ByVal apiName As String, ByVal apiLocation As String, ByVal userID As String, ByVal apiKey As String) As Object

        Try
            ' Get the data from the api server
            GetApiIgnoreCache = XDocument.Load(String.Format("{0}{1}?userID={2}&apiKey={3}", strApiUri, apiLocation, userID, apiKey))

            ' Cache it to the cache directory
            Try
                GetApiIgnoreCache.Save(String.Format("{0}{1}Cache{1}Temp{1}{2}", Settings.DataDir, Path.DirectorySeparatorChar, apiName & userID & ".xml"))
            Catch ex As Exception
            End Try

        Catch ex As Exception
            Return New ApiError(UserErrors.UnableToLoad)
        End Try

        ' Check to see if there was an error
        Dim objError As Object = ApiError.CheckForError(GetApiIgnoreCache)
        If (TypeOf objError Is Boolean) = True Then
            Dim xmlResults As XDocument
            xmlResults = GetApiIgnoreCache

            ' Get the cached until time
            objCachedUntilTime = GetCacheUntilDate(xmlResults...<currentTime>.Value, xmlResults...<cachedUntil>.Value)

            ' Return the xml document
            Return xmlResults
        Else
            Return objError
        End If
    End Function
#End Region

#Region "userID + apiKey + characterID Input"
    ''' <summary>
    ''' Gets the API from the server 
    ''' </summary>
    ''' <param name="apiName">The name of the api retrieved from the api server</param>
    ''' <param name="apiLocation">The URI of the api</param>
    ''' <param name="database">The database to send the API to</param>
    ''' <param name="userID">The user ID required for the API</param>
    ''' <param name="apiKey">The API key required to the get the API</param>
    ''' <param name="characterID">The character ID required for the API</param>
    ''' <returns>The XDocument if successful or an ApiError if unsuccessful</returns>
    ''' <remarks></remarks>
    Public Function GetApi(ByVal apiName As String, ByVal apiLocation As String, ByVal database As String, ByVal userID As String, ByVal apiKey As String, ByVal characterID As String) As Object
        Dim blnCacheResult As Boolean

        ' Check to see if an update can be obtained
        blnCacheResult = CheckCachedUntilTime(database, apiName, "characterID", characterID)
        If blnCacheResult = True Then
            GetApi = GetApiIgnoreCache(apiName, apiLocation, userID, apiKey, characterID)
            Return GetApi
        Else
            Return New ApiError(UserErrors.UpdateTooEarly)
        End If

    End Function

    ''' <summary>
    ''' Gets the API from the server but ignores the cache timer
    ''' </summary>
    ''' <param name="apiName">The name of the api retrieved from the api server</param>
    ''' <param name="apiLocation">The URI of the api</param>
    ''' <param name="userID">The user ID required for the API</param>
    ''' <param name="apiKey">The API key required to the get the API</param>
    ''' <param name="characterID">The character ID required for the API</param>
    ''' <returns>The XDocument if successful or an ApiError if unsuccessful</returns>
    ''' <remarks></remarks>
    Public Function GetApiIgnoreCache(ByVal apiName As String, ByVal apiLocation As String, ByVal userID As String, ByVal apiKey As String, ByVal characterID As String) As Object

        Try
            ' Get the data from the api server
            GetApiIgnoreCache = XDocument.Load(String.Format("{0}{1}?userID={2}&apiKey={3}&characterID={4}", strApiUri, apiLocation, userID, apiKey, characterID))

            ' Cache it to the cache directory
            Try
                GetApiIgnoreCache.Save(String.Format("{0}{1}Cache{1}{2}", Settings.DataDir, Path.DirectorySeparatorChar, apiName & characterID & ".xml"))
            Catch ex As Exception
            End Try

        Catch ex As Exception
            Return New ApiError(UserErrors.UnableToLoad)
        End Try

        ' Check to see if there was an error
        Dim objError As Object = ApiError.CheckForError(GetApiIgnoreCache)
        If (TypeOf objError Is Boolean) = True Then
            Dim xmlResults As XDocument
            xmlResults = GetApiIgnoreCache

            ' Get the cached until time
            objCachedUntilTime = GetCacheUntilDate(xmlResults...<currentTime>.Value, xmlResults...<cachedUntil>.Value)

            ' Return the xml document
            Return xmlResults
        Else
            Return objError
        End If
    End Function
#End Region

#End Region

#Region "User Management"
    Public Function CheckUserDetails()
        Dim sqlTransaction As SqlCeTransaction
        Dim objConnection As New Connection

        ' Open the connection and begin the transaction
        objConnection.Open("Userdata.sdf")
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Search for the user in the database
        CheckUserDetails = CheckUserDetails(objConnection, sqlTransaction)

        ' Act according the the success
        If CheckUserDetails() <> True Then
            sqlTransaction.Rollback()
        Else
            sqlTransaction.Commit()
        End If
        objConnection.Close()
    End Function

    Public Function CheckUserDetails(ByVal connection As Connection, ByVal transaction As SqlCeTransaction)
        Dim objResult As Object
        ' Check the user's details are correct by getting the character list
        objResult = GetApiIgnoreCache("Characters", "/account/Characters.xml.aspx", strUserID, strApiKey)

        ' Check the result and return it if the result is not an XDocument
        If ((TypeOf objResult Is XDocument) <> True) Then
            Dim objError As ApiError = objResult

            If objError.FullCode <> (UserErrors.BaseCategory * 100) + UserErrors.UpdateTooEarly Then
                Return objResult
            End If
        End If
        ' Search for the user in the database
        Dim objUserdata As New Userdata(connection.dbConnection)
        objUserdata.Transaction = transaction

        ' Check to see if the character already exists
        Dim result = From UserInfo In objUserdata.UserInfo _
        Where (UserInfo.UserID = strUserID)

        If result.Count < 1 Then
            CheckUserDetails = InsertUser(connection, transaction)
        Else
            CheckUserDetails = UpdateUser(connection, transaction)
        End If
    End Function

    Private Function InsertUser(ByVal connection As Connection, ByVal transaction As SqlCeTransaction)
        Dim objValues As New List(Of Parameter)

        ' Create the insert values 
        objValues.Add(New Parameter("userID", strUserID))
        objValues.Add(New Parameter("apiKey", strApiKey))
        objValues.Add(New Parameter("fullKey", True))

        ' Update the users details
        InsertUser = Records.Insert(connection, transaction, "UserInfo", objValues)

        ' Act according the the success
        If InsertUser <> True Then
            InsertUser = New ApiError(UserErrors.UserNotInserted)
        End If
    End Function

    Private Function UpdateUser(ByVal connection As Connection, ByVal transaction As SqlCeTransaction)
        Dim objValues As New List(Of Parameter)
        Dim objUpdateConditions As New List(Of Parameter)

        ' Create the update values and conditions
        objValues.Add(New Parameter("apiKey", strApiKey))
        objUpdateConditions.Add(New Parameter("userID", strUserID))

        ' Update the users details
        UpdateUser = Records.Update(connection, transaction, "UserInfo", objValues, objUpdateConditions)

        ' Act according the the success
        If UpdateUser <> True Then
            UpdateUser = New ApiError(UserErrors.UserNotSaved)
        End If
    End Function
#End Region
End Class
