Partial Public Class PhotoListItem
    Public Property Portrait() As ImageSource
        Get
            Return imgPortrait.Source
        End Get
        Set(ByVal value As ImageSource)
            imgPortrait.Source = value
        End Set
    End Property

    Public Property ImageText() As String
        Get
            Return lblText.Content
        End Get
        Set(ByVal value As String)
            lblText.Content = value
        End Set
    End Property
End Class
