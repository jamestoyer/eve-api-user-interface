Public Class [Error]
    Dim objErrorType As ErrorTypes
    Private objError As Object

    ''' <summary>
    ''' The type of error created as an error type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property errorType() As ErrorTypes
        Get
            Return objErrorType
        End Get
        Set(ByVal value As ErrorTypes)
            objErrorType = value
        End Set
    End Property

    ''' <summary>
    ''' The error created as an object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property [error]() As Object
        Get
            Return objError
        End Get
        Set(ByVal value As Object)
            objError = value
        End Set
    End Property

    ''' <summary>
    ''' Create a new error object
    ''' </summary>
    ''' <param name="errorType">The error as an error type enumaration</param>
    ''' <param name="error">The error as an object</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal errorType As ErrorTypes, ByVal [error] As Object)
        objError = [error]
        objErrorType = errorType
    End Sub
End Class
