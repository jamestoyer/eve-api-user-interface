Imports System.Data.SqlServerCe

Namespace Database
    ' TODO: This datatable class needs to be made so it accepts parameters to prevent issues
    ''' <summary>
    ''' Methods for manipulating data tables
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DataTables
        ''' <summary>
        ''' Create a new data table
        ''' </summary>
        ''' <param name="sqlText">The SQL syntax for selecting the information</param>
        ''' <param name="dataFileName">The file name of the database to populate the data table from</param>
        ''' <returns>The data table if successfull, or nothing if unsuccessful</returns>
        ''' <remarks></remarks>
        Public Shared Function Create(ByVal sqlText As String, ByVal dataFileName As String) As DataTable
            Dim objDataTable As New Connection
            Dim sqlCommand As SqlCeCommand
            Dim sqlAdapter As SqlCeDataAdapter
            Dim tblNewTable As New DataTable

            ' Open the connection
            If objDataTable.Open(dataFileName) = False Then Return Nothing

            ' Create the command and instantiate the adapter
            sqlCommand = New SqlCeCommand(sqlText, objDataTable.dbConnection)
            sqlAdapter = New SqlCeDataAdapter(sqlCommand)

            Try
                ' Fill the table from the adapter
                sqlAdapter.Fill(tblNewTable)
            Catch ex As Exception
                Return Nothing
            Finally
                ' Close the connection
                objDataTable.Close()
            End Try

            ' Return the table
            Return tblNewTable
        End Function

        ''' <summary>
        ''' Create a new data table
        ''' </summary>
        ''' <param name="sqlText">The SQL syntax for selecting the information</param>
        ''' <param name="connection">The EVEC.Data.Database connection</param>
        ''' <param name="transaction">The transaction for the datatable</param>
        ''' <returns>The data table if successful, or nothing if unsuccessful</returns>
        ''' <remarks></remarks>
        Public Shared Function Create(ByVal sqlText As String, ByVal connection As Connection, ByVal transaction As SqlCeTransaction) As DataTable
            Dim sqlCommand As SqlCeCommand
            Dim sqlAdapter As SqlCeDataAdapter
            Dim tblNewTable As New DataTable

            ' Create the command and instantiate the adapter
            sqlCommand = New SqlCeCommand(sqlText, connection.dbConnection, transaction)
            sqlAdapter = New SqlCeDataAdapter(sqlCommand)

            Try
                ' Fill the table from the adapter
                sqlAdapter.Fill(tblNewTable)
            Catch ex As Exception
                Return Nothing
            End Try

            ' Return the table
            Return tblNewTable
        End Function
    End Class
End Namespace