Imports System.Windows.Forms

''' <summary>Methods for error handling</summary>
Class ErrorHandling

    ''' <summary>
    ''' Handles exceptions thrown in the code and returns a custom message
    ''' </summary>
    ''' <param name="theError">The exception created</param>
    ''' <param name="customMessage">Message to show the user instead of the error code</param>
    Public Shared Sub ErrorHandler(ByVal theError As Exception, ByVal customMessage As String)
        ' Report an error to the user.
        On Error Resume Next

        MessageBox.Show(customMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ' Log the error
        My.Application.Log.WriteException(theError)
        LogException(theError)
    End Sub

    ''' <summary>
    ''' Handles exceptions thrown in the code
    ''' </summary>
    ''' <param name="theError">The exception created</param>
    ''' <param name="methodName">The name of the method where the exception occured</param>
    Public Shared Sub ErrorHandler(ByVal methodName As String, ByVal theError As Exception)
        ' Report an error to the user.
        On Error Resume Next

        MessageBox.Show("The following error occurred at location '" & methodName & "':" & vbCrLf & vbCrLf & theError.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ' Log the error
        My.Application.Log.WriteException(theError)
        LogException(theError)
    End Sub


    ''' <summary>
    ''' Logs the exception to the debug line. Only included in debug builds
    ''' </summary>
    Private Shared Sub LogException(ByVal theError As Exception)
#If DEBUG Then
        Dim trace As New StackTrace(theError, True)
        Dim frame As StackFrame = trace.GetFrame(trace.FrameCount - 1)

        Debug.WriteLine("")
        Debug.WriteLine(String.Format("{0} {1} at {2} line {3}", "Caught and Thrown", theError.[GetType]().Name, frame.GetFileName(), frame.GetFileLineNumber()), "Exception")
        Debug.WriteLine(theError.Message, "Exception Message")
        Debug.WriteLine("")
#End If
    End Sub
End Class