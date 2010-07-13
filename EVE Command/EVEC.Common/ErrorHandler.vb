Public Class ErrorHandler
    Protected Shared objError As [Error]

    ''' <summary>
    ''' Display the error from the error object
    ''' </summary>
    ''' <param name="error">The error as an error object</param>
    ''' <returns>Returns the MessageBoxResult of the error</returns>
    ''' <remarks></remarks>
    Public Shared Function displayError(ByVal [error] As [Error]) As MessageBoxResult
        ' Set the error object and evalute it
        objError = [error]
        Return evaluateErrorType()
    End Function

    ''' <summary>
    ''' Evaluate the error types and act accordingly
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function evaluateErrorType() As MessageBoxResult
        Select Case objError.errorType
            Case ErrorTypes.apiError
                apiErrorEvaluation()
            Case ErrorTypes.customError
                customErrorEvaluation()
            Case ErrorTypes.systemError
                ' Check the object contains an exception and display it
                If TypeOf objError.error Is System.Exception Then
                    Dim objErrorException As Exception
                    objErrorException = objError.error
                    Return ShowError(objErrorException.ToString)
                End If
        End Select
    End Function

    Private Shared Function ShowError(ByVal errorMessage As String) As MessageBoxResult
        Dim objResult As MessageBoxResult
        Dim strErrorMessage As String

        ' Construct the error message
        strErrorMessage = "The following error has occured:" & vbNewLine
        strErrorMessage &= errorMessage & vbNewLine
        strErrorMessage &= "Would you like to retry?"

        ' Show the error
        objResult = MessageBox.Show(strErrorMessage, "Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error, MessageBoxResult.Yes)

        Return objResult
    End Function

    Private Shared Sub apiErrorEvaluation()
        ' TODO add implementation
    End Sub

    Private Shared Sub customErrorEvaluation()
        ' TODO add implementation
    End Sub
End Class