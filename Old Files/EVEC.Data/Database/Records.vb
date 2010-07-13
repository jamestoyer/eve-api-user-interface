Imports EVEC.Data.ErrorHandling
Imports System.Data.SqlServerCe

Namespace Database
    Public Class Records
        ''' <summary>
        ''' Deletes a record
        ''' </summary>
        ''' <remarks>This allows the user to delete a record with multiple conditions. It is dynamic so can accept any number of conditions</remarks>
        ''' <param name="connection">The connection variable</param>
        ''' <param name="transaction">The transaction variable</param>
        ''' <param name="table">The table of the record</param>
        ''' <param name="conditions">The list of conditions to allow the delete to execute</param>
        ''' <param name="deleteType">The type of condition for the delete. Either AND or OR condition</param>
        ''' <returns>Boolean of success</returns>
        Public Shared Function Delete(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal table As String, ByVal conditions As List(Of Parameter), Optional ByVal deleteType As ConditionType = ConditionType.AndAll) As Boolean
            ' Command variable 
            Dim objDelete As New SqlCeCommand

            ' Boolean to signify first item
            Dim blnFirstTime As Boolean

            ' Create command
            objDelete.CommandText = "DELETE FROM " & table & " WHERE "
            blnFirstTime = True

            ' Set the insert fields
            For Each item In conditions
                If blnFirstTime = True Then
                    objDelete.CommandText &= item.Field & " = @" & item.Field
                    objDelete.Parameters.AddWithValue("@" & item.Field, item.Value)
                    blnFirstTime = False
                Else
                    If deleteType = ConditionType.AndAll Then
                        objDelete.CommandText &= " AND "
                    Else
                        objDelete.CommandText &= " OR "
                    End If
                    objDelete.CommandText &= item.Field & "@" & item.Field
                    objDelete.Parameters.AddWithValue("@" & item.Field, item.Value)
                End If
            Next

            ' Set the connection and the transaction property
            objDelete.Connection = connection.dbConnection
            objDelete.Transaction = transaction

            ' Attempt to update
            Try
                objDelete.ExecuteNonQuery()
            Catch ex As Exception
                ErrorHandler("Gemenon.Database.Records.Delete", ex)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Deletes a record
        ''' </summary>
        ''' <remarks>This allows the user to delete a record with multiple conditions. It is dynamic so can accept any number of conditions</remarks>
        ''' <param name="connection">The connection variable</param>
        ''' <param name="transaction">The transaction variable</param>
        ''' <param name="table">The table of the record</param>
        ''' <param name="customConditions">A custom condition for the delete. Does not require WHERE at the beginning of the statement</param>
        ''' <returns>Boolean of success</returns>
        Public Shared Function Delete(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal table As String, ByVal customConditions As String) As Boolean
            ' Command variable 
            Dim objDelete As New SqlCeCommand

            ' Create command
            objDelete.CommandText = "DELETE FROM " & table

            ' Set the condition field
            If customConditions.Trim.Length > 0 Then
                objDelete.CommandText &= " WHERE " & customConditions.Trim
            End If

            ' Set the connection and the transaction property
            objDelete.Connection = connection.dbConnection
            objDelete.Transaction = transaction

            ' Attempt to update
            Try
                objDelete.ExecuteNonQuery()
            Catch ex As Exception
                ErrorHandler("Gemenon.Database.Records.Delete", ex)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Inserts a record
        ''' </summary>
        ''' <remarks>This allows the user to insert a record with multiple fields. It is dynamic so can accept any number of fields</remarks>
        ''' <param name="connection">The connection variable</param>
        ''' <param name="transaction">The transaction variable</param>
        ''' <param name="table">The table of the record</param>
        ''' <param name="insertFields">The list of fields to insert</param>
        ''' <returns>Boolean of success</returns>
        Public Shared Function Insert(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal table As String, ByVal insertFields As List(Of Parameter)) As Boolean
            ' Command variable 
            Dim objInsert As New SqlCeCommand

            ' Boolean to signify first item
            Dim blnFirstTime As Boolean

            ' Create command
            objInsert.CommandText = "INSERT INTO " & table & "("
            blnFirstTime = True

            ' Set the insert fields
            For Each item In insertFields
                If blnFirstTime = True Then
                    objInsert.CommandText &= item.Field
                    blnFirstTime = False
                Else
                    objInsert.CommandText &= ", " & item.Field
                End If
            Next
            objInsert.CommandText &= ") VALUES ("

            ' Set the insert values
            blnFirstTime = True
            For Each item In insertFields
                If blnFirstTime = True Then
                    objInsert.CommandText &= "@" & item.Field
                    objInsert.Parameters.AddWithValue("@" & item.Field, item.Value)
                    blnFirstTime = False
                Else
                    objInsert.CommandText &= ", @" & item.Field
                    objInsert.Parameters.AddWithValue("@" & item.Field, item.Value)
                End If
            Next
            objInsert.CommandText &= ")"

            ' Set the connection and the transaction property
            objInsert.Connection = connection.dbConnection
            objInsert.Transaction = transaction

            ' Attempt to update
            Try
                objInsert.ExecuteNonQuery()
            Catch ex As Exception
                ErrorHandler("EVEC.Data.Database.Records.Insert", ex)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Updates a record
        ''' </summary>
        ''' <remarks>This allows the user to update a record with multiple fields and conditions. It is dynamic so can accept any number of fields and conditions</remarks>
        ''' <param name="connection">The connection variable</param>
        ''' <param name="transaction">The transaction variable</param>
        ''' <param name="table">The table of the record</param>
        ''' <param name="updateFields">The list of fields to update</param>
        ''' <param name="conditions">The conditions the required to update the record</param>
        ''' <param name="updateType">The type of condition for the update. Either AND or OR condition</param>
        ''' <returns>Boolean of success</returns>
        Public Shared Function Update(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal table As String, ByVal updateFields As List(Of Parameter), ByVal conditions As List(Of Parameter), Optional ByVal updateType As ConditionType = ConditionType.AndAll) As Boolean
            ' Command variable 
            Dim objUpdate As New SqlCeCommand

            ' Boolean to signify first item
            Dim blnFirstTime As Boolean

            ' Create command
            objUpdate.CommandText = "UPDATE " & table & " SET "
            blnFirstTime = True

            ' Set the update fields
            For Each item In updateFields
                If blnFirstTime = True Then
                    objUpdate.CommandText &= item.Field & " = @" & item.Field
                    objUpdate.Parameters.AddWithValue("@" & item.Field, item.Value)
                    blnFirstTime = False
                Else
                    objUpdate.CommandText &= ", " & item.Field & " = @" & item.Field
                    objUpdate.Parameters.AddWithValue("@" & item.Field, item.Value)
                End If
            Next

            ' Set the condition fields
            If conditions.Count > 0 Then
                objUpdate.CommandText &= " WHERE "
                blnFirstTime = True
                For Each item In conditions
                    If blnFirstTime = True Then
                        objUpdate.CommandText &= item.Field & " = @con" & item.Field
                        objUpdate.Parameters.AddWithValue("@con" & item.Field, item.Value)
                        blnFirstTime = False
                    Else
                        If updateType = ConditionType.AndAll Then
                            objUpdate.CommandText &= " AND "
                        Else
                            objUpdate.CommandText &= " OR "
                        End If
                        objUpdate.CommandText &= item.Field & " = @con" & item.Field
                        objUpdate.Parameters.AddWithValue("@con" & item.Field, item.Value)
                    End If
                Next
            End If

            ' Set the connection and the transaction property
            objUpdate.Connection = connection.dbConnection
            objUpdate.Transaction = transaction

            ' Attempt to update
            Try
                objUpdate.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                ErrorHandler("Gemenon.Database.Records.Update", ex)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Updates a record
        ''' </summary>
        ''' <remarks>This allows the user to update a record with multiple fields and conditions. It is dynamic so can accept any number of fields and conditions</remarks>
        ''' <param name="connection">The connection variable</param>
        ''' <param name="transaction">The transaction variable</param>
        ''' <param name="table">The table of the record</param>
        ''' <param name="updateFields">The list of fields to update</param>
        ''' <param name="customConditions">A custom condition for the update. Does not require WHERE at the beginning of the statement</param>
        ''' <returns>Boolean of success</returns>
        Public Shared Function Update(ByVal connection As Connection, ByVal transaction As SqlCeTransaction, ByVal table As String, ByVal updateFields As List(Of Parameter), ByVal customConditions As String) As Boolean
            ' Command variable 
            Dim objUpdate As New SqlCeCommand

            ' Boolean to signify first item
            Dim blnFirstTime As Boolean

            ' Create command
            objUpdate.CommandText = "UPDATE " & table & " SET "
            blnFirstTime = True

            ' Set the update fields
            For Each item In updateFields
                If blnFirstTime = True Then
                    objUpdate.CommandText &= item.Field & " = @" & item.Field
                    objUpdate.Parameters.AddWithValue("@" & item.Field, item.Value)
                    blnFirstTime = False
                Else
                    objUpdate.CommandText &= ", " & item.Field & " = @" & item.Field
                    objUpdate.Parameters.AddWithValue("@" & item.Field, item.Value)
                End If
            Next

            ' Set the condition field
            If customConditions.Trim.Length > 0 Then
                objUpdate.CommandText &= " WHERE " & customConditions.Trim
            End If

            ' Set the connection and the transaction property
            objUpdate.Connection = connection.dbConnection
            objUpdate.Transaction = transaction

            ' Attempt to update
            Try
                objUpdate.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                ErrorHandler("Gemenon.Database.Records.Update", ex)
                Return False
            End Try

            Return True
        End Function
    End Class

    Public Class Parameter

        ''' <summary>
        ''' The value for the parameter
        ''' </summary>
        Private objValue As Object
        ''' <summary>
        ''' The field relating to the value
        ''' </summary>
        Private strField As String

        ''' <summary>
        ''' Creates a new database parameter
        ''' </summary>
        ''' <param name="field">The field of the parameter</param>
        ''' <param name="value">The value relating to the parameter</param>
        Sub New(ByVal field As String, ByVal value As Object)
            ' Set up the item
            strField = field
            objValue = value
        End Sub

        ''' <summary>
        ''' Value for the field
        ''' </summary>
        Public Property Value() As Object
            Get
                Return objValue
            End Get
            Set(ByVal value As Object)
                objValue = value
            End Set
        End Property

        ''' <summary>
        ''' Field relating to the value
        ''' </summary>
        Public Property Field() As String
            Get
                Return strField
            End Get
            Set(ByVal value As String)
                strField = value
            End Set
        End Property

        Overrides Function ToString() As String
            Return strField & " = " & objValue.ToString
        End Function
    End Class

    Public Enum ConditionType

        AndAll = 0
        OrAll = 1
    End Enum
End Namespace

