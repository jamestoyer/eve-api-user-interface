Partial Public Class AccountViewItem
    Dim objItems As New AccountCharacterList

    Public Property Items() As AccountCharacterList
        Get
            Return objItems
        End Get
        Set(ByVal value As AccountCharacterList)
            objItems.Clear()
            objItems = value
            lstCharacters.DataContext = objItems
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return pnlAccountDetails.Header
        End Get
        Set(ByVal value As String)
            pnlAccountDetails.Header = value
            Me.Name = "obj" & pnlAccountDetails.Header
        End Set
    End Property
End Class
