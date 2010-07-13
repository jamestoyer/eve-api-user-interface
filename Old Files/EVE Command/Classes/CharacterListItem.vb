Public Class CharacterList
    Inherits List(Of CharacterListItem)
End Class

Public Class CharacterListItem
    Dim strCharacter As String
    Dim blnActive As Boolean
    Const strActive As String = "(Active)"
    Const strInactive As String = "(Not Active)"

    Public Sub New()
        strCharacter = ""
        blnActive = False
    End Sub

    Public Property Character() As String
        Get
            Return strCharacter
        End Get
        Set(ByVal value As String)
            strCharacter = value
        End Set
    End Property

    Public Property Active() As Boolean
        Get
            Return blnActive
        End Get
        Set(ByVal value As Boolean)
            blnActive = value
        End Set
    End Property

    Public ReadOnly Property ActiveString() As String
        Get
            If blnActive = True Then
                Return strActive
            Else
                Return strInactive
            End If
        End Get
    End Property
End Class
