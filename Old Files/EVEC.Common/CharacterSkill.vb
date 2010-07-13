Imports EVEC.Data.DEO
Public Class CharacterSkill

    Private objEveData As New EveDataEntities(ConnectionString.Create("EveData.sdf"))
    Private objSkillInfo As New skills
    Private objCharacterSkill As New csSkills
    Private strNameAndRank As String
    Private strSkillPoints As String
    Private bmpImage As BitmapFrame

    Public ReadOnly Property SkillInfo() As EVEC.Data.DEO.skills
        Get
            Return objSkillInfo
        End Get
    End Property

    Public ReadOnly Property NameAndRankString() As String
        Get
            Return strNameAndRank
        End Get
    End Property

    Public ReadOnly Property SkillPointsString() As String
        Get
            Return strSkillPoints
        End Get
    End Property

    Public Property Skill() As csSkills
        Get
            Return objCharacterSkill
        End Get
        Set(ByVal value As csSkills)
            objCharacterSkill = value
            GetSkillInfo()
        End Set
    End Property

    Public ReadOnly Property CurrentLevelString() As String
        Get
            Return "Level " & objCharacterSkill.level
        End Get
    End Property

    Public ReadOnly Property SkillImage() As ImageSource
        Get
            Return bmpImage
        End Get
    End Property
    Private Sub GetSkillInfo()
        Dim result = From s In objEveData.skills _
                    Where s.typeID = objCharacterSkill.typeID

        objSkillInfo = result.First
        objSkillInfo.skillGroupsReference.Load(System.Data.Objects.MergeOption.OverwriteChanges)
        strNameAndRank = String.Format("{0} ({1}x)", objSkillInfo.typeName, objSkillInfo.rank)
        strSkillPoints = GetSkillpointFraction()
        SetImage()
    End Sub

    Private Function GetSkillpointFraction() As String
        ' Total skill point variable
        Dim decTotalSkillPoints As Decimal

        ' Set the beginning of the string
        GetSkillpointFraction = "SP: " & objCharacterSkill.skillpoints.ToString("#,##0")

        ' Find out if the skill is at level 5
        If objCharacterSkill.level < 5 Then
            decTotalSkillPoints = (2 ^ ((2.5 * (objCharacterSkill.level + 1)) - 2.5)) * 250 * objSkillInfo.rank
            GetSkillpointFraction &= "/" & (Math.Ceiling(decTotalSkillPoints)).ToString("#,##0")
        End If
    End Function

    Private Sub SetImage()
        If objCharacterSkill.level < 5 Then
            bmpImage = BitmapFrame.Create(Application.GetResourceStream(New Uri("pack://application:,,,/EVEC.Common;Component/Images/icon50_13.png", UriKind.RelativeOrAbsolute)).Stream)
        ElseIf objCharacterSkill.level = 5 Then
            bmpImage = BitmapFrame.Create(Application.GetResourceStream(New Uri("pack://application:,,,/EVEC.Common;Component/Images/icon50_14.png", UriKind.RelativeOrAbsolute)).Stream)
        End If
    End Sub
End Class
