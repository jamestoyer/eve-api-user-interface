Imports System.Windows.Forms

Class frmLocateDatabase
    Dim strLocation As String

    Private Sub txtLocation_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocation.Enter
        ' Hightlight the text
        txtLocation.SelectAll()
    End Sub

    Public Function Prompt(ByVal fileLocation)
        strLocation = fileLocation
        ' Show the form as a dialog
        Me.ShowDialog()

        ' Return the location
        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            ' Return the text in the location baox
            Return txtLocation.Text.Trim
        Else
            Return ""
        End If
    End Function

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        ' Check a location is entered
        If txtLocation.Text.Trim = "" Then
            MessageBox.Show("The location is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtLocation.Focus()
            Return
        End If

        ' Set the result of the form to ok
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        ' Open the open file dialog
        ofdLocation.ShowDialog()

        ' Get the result of the dialog
        txtLocation.Text = ofdLocation.FileName
    End Sub

    Private Sub frmLocateDatabase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Set the location to the setting location
        txtLocation.Text = strLocation
    End Sub
End Class