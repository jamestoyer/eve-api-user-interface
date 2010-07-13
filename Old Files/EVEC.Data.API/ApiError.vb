''' <summary>
''' Represent an instance of an API error
''' </summary>
''' <remarks></remarks>
Public Class ApiError

    Private intCategory As Integer
    Private intNumber As Integer
    Private strText As String
    ''' <summary>
    ''' Create a new error type object
    ''' </summary>
    ''' <param name="customError">The enumation to represent a custom Api Error</param>
    Sub New(ByVal customError As UserErrors)
        intCategory = UserErrors.BaseCategory
        intNumber = customError

        ' Creat the error text according to the error
        Select Case intNumber
            Case UserErrors.UnableToLoad
                strText = "Unable to load the API from the EVE API Server"
            Case UserErrors.UnableToSave
                strText = "Unable to save the API information to the database"
            Case UserErrors.UpdateTooEarly
                strText = "Cannot get API information. cachedUntil time has not passed"
            Case UserErrors.UserNotInserted
                strText = "The user could not be added to the database"
            Case UserErrors.UserNotSaved
                strText = "The user could not be updated"
        End Select

    End Sub

    ''' <summary>
    ''' Create a new error type object
    ''' </summary>
    ''' <param name="errorCode">The code of the error</param>
    ''' <param name="errorText">The error text</param>
    Sub New(ByVal errorCode As Integer, ByVal errorText As String)
        intCategory = CInt(Left(errorCode.ToString, 1))
        intNumber = CInt(Right(errorCode.ToString, 2))
        strText = errorText
    End Sub

    ''' <summary>
    ''' Create a new error type object
    ''' </summary>
    ''' <param name="errorCategory">The error category</param>
    ''' <param name="errorNumber">The error number</param>
    ''' <param name="errorText">The error text</param>
    Sub New(ByVal errorCategory As Integer, ByVal errorNumber As Integer, ByVal errorText As String)
        ' Assign the inputs to the correct places
        intCategory = errorCategory
        intNumber = errorNumber
        strText = errorText
    End Sub
    ''' <summary>
    ''' The category of the error
    ''' </summary>
    Public ReadOnly Property Category() As Integer
        Get
            Return intCategory
        End Get
    End Property

    ''' <summary>
    ''' The full error code
    ''' </summary>
    Public ReadOnly Property FullCode() As Integer
        Get
            Return (intCategory * 100) + intNumber
        End Get
    End Property

    ''' <summary>
    ''' The number of the error
    ''' </summary>
    Public ReadOnly Property Number() As Integer
        Get
            Return intNumber
        End Get
    End Property

    ''' <summary>
    ''' The error text
    ''' </summary>
    Public ReadOnly Property Text() As String
        Get
            Return strText
        End Get
    End Property

    ''' <summary>
    ''' Checks to see if the api document has an error in it
    ''' </summary>
    ''' <param name="apiDocument">The API xml document to check</param>
    ''' <returns>Returns false if there is no error and an ApiError instance if there is an error</returns>
    ''' <remarks></remarks>
    Public Shared Function CheckForError(ByVal apiDocument As XDocument) As Object
        'Check to see if there is an error in the document
        If (apiDocument...<error>.Count) > 0 Then
            Dim objError As New ApiError(apiDocument...<error>.@code, apiDocument...<error>.Value)

            ' Return the error
            Return objError
        End If

        ' Return false for no error
        Return False
    End Function

    Public Shared Widening Operator CType(ByVal objError As ApiError) As String
        Return objError.ToString
    End Operator

    Overrides Function ToString() As String
        Return String.Format("Error {0}: {1}", FullCode, strText)
    End Function


End Class


Public Enum UserErrors
    ''' <summary>
    ''' The category for a user error
    ''' </summary>
    ''' <remarks></remarks>
    BaseCategory = 100
    ''' <summary>
    ''' The API was unable to be loaded
    ''' </summary>
    ''' <remarks></remarks>
    UnableToLoad = 0
    ''' <summary>
    ''' The API was unable to be saved to the database
    ''' </summary>
    ''' <remarks></remarks>
    UnableToSave = 1
    ''' <summary>
    ''' The cachedUntil time has not passed
    ''' </summary>
    ''' <remarks></remarks>
    UpdateTooEarly = 2
    ''' <summary>
    ''' User not added to database
    ''' </summary>
    ''' <remarks></remarks>
    UserNotInserted = 3
    ''' <summary>
    ''' User not updated
    ''' </summary>
    ''' <remarks></remarks>
    UserNotSaved = 4
End Enum

