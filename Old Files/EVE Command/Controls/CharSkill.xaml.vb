Imports EVEC.Data.DEO
Partial Public Class CharSkill
    Dim strCharacterID As String
    Dim lngTypeID As Long
    Dim intSkillpoints As Integer
    Dim intLevel As Integer
    Dim blnUnpublished As Boolean
    Dim strSkillName As String
    Dim strGroup As String

    Public Property CharacterID() As String
        Get
            Return strCharacterID
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property TypeID() As Integer
        Get
            Return lngTypeID
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Property Skillpoints() As Integer
        Get
            Return intSkillpoints
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Property Level() As Integer
        Get
            Return intLevel
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Property Unpublished() As Boolean
        Get
            Return blnUnpublished
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property

    Public Property SkillName() As String
        Get
            Return strSkillName
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property Group() As String
        Get
            Return strGroup
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal characterID As String, ByVal typeID As Integer, ByVal skillpoints As Integer, ByVal level As Integer, ByVal unpublished As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Set the variables
        strCharacterID = characterID
        lngTypeID = typeID
        intSkillpoints = skillpoints
        intLevel = level
        blnUnpublished = unpublished

        ' Get the information about the skill from the EveData database
        Dim objEveData As New EveDataEntities(ConnectionString.Create("EveData.sdf"))

        Dim CurrentSkill = From s In objEveData.skills _
                           Where s.typeID = lngTypeID


    End Sub
End Class
