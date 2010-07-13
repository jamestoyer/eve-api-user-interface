Partial Public Class AccountItem
    Dim objItems As New CharacterList

    Public Property Items() As CharacterList
        Get
            Return objItems
        End Get
        Set(ByVal value As CharacterList)
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
