''' <summary>
    ''' Represents an instance of an API error
    ''' </summary>
    ''' <remarks></remarks>
    Public Class apiError
        Private strCategory As String
        Private strNumber As String
        Private strText As String

        ''' <summary>
        ''' Create a new error type object
        ''' </summary>
        ''' <param name="errorCode">The code of the error</param>
        ''' <param name="errorText">The error text</param>
        Public Sub New(ByVal errorCode As String, ByVal errorText As String)
            strCategory = Left(errorCode.ToString, 1)
            strNumber = Right(errorCode.ToString, 2)
            strText = errorText
        End Sub

        ''' <summary>
        ''' The category of the error
        ''' </summary>
        Public ReadOnly Property Category() As Integer
            Get
                Return CInt(strCategory)
            End Get
        End Property

        ''' <summary>
        ''' The full error code
        ''' </summary>
        Public ReadOnly Property FullCode() As String
            Get
                Return strCategory & strNumber
            End Get
        End Property

        ''' <summary>
        ''' The number of the error
        ''' </summary>
        Public ReadOnly Property Number() As Integer
            Get
                Return CInt(strNumber)
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

        Public Shared Widening Operator CType(ByVal objError As apiError) As String
            Return objError.ToString
        End Operator

        Public Overrides Function ToString() As String
            Return String.Format("Error {0}: {1}", FullCode, strText)
        End Function

        ''' <summary>
        ''' Checks to see if the api document has an error in it
        ''' </summary>
        ''' <param name="xmlDocument">The API xml document to check</param>
        ''' <returns>Returns nothing if there is no error and an ApiError instance if there is an error</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckForError(ByVal xmlDocument As XDocument) As apiError
            'Check to see if there is an error in the document
            If (xmlDocument...<error>.Count) > 0 Then
                Dim objError As New apiError(xmlDocument...<error>.@code, xmlDocument...<error>.Value)

                ' Return the error
                Return objError
            End If

            ' Return nothing for no error
            Return Nothing
        End Function
    End Class
