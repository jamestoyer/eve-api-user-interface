Imports EVEC.Data
Imports EVEC.Data.DEO
Imports System.IO

Public Class CharacterList
    Inherits List(Of Character)

    Public Shadows Sub Clear()
        If Me.Count > 0 Then
            For Each c In Me
                c.DownloadImage()
            Next
        End If
        MyBase.Clear()
    End Sub

    Public Function Compare(ByVal x As Character, ByVal y As Character) As Integer
        Return String.Compare(x.CharacterName, y.CharacterName)
    End Function
End Class

Public Class Character
    Dim strCharacterID As String
    Dim strUserID As String
    Dim strApiKey As String
    Dim strImageLocation As String
    Dim strCacheLocation As String
    Dim bmpImage As BitmapFrame
    Private objUserdata As New UserdataEntities(ConnectionString.Create("Userdata.sdf"))
    Private objCharacterSheet As New CharacterSheet
    Dim objCharacterSkills As New List(Of CharacterSkill)

    Public ReadOnly Property CharacterName() As String
        Get
            Return objCharacterSheet.name
        End Get
    End Property

    Public Property CharacterID() As String
        Get
            Return strCharacterID
        End Get
        Set(ByVal value As String)
            strCharacterID = value
            GetCharacterSheet()
        End Set
    End Property

    Public ReadOnly Property PortraitSource() As ImageSource
        Get
            Return bmpImage
        End Get
    End Property

    Public Property CharacterSheet() As EVEC.Data.DEO.CharacterSheet
        Get
            Return objCharacterSheet
        End Get
        Set(ByVal value As EVEC.Data.DEO.CharacterSheet)
            objCharacterSheet = value
        End Set
    End Property

    Public ReadOnly Property Balance() As String
        Get
            Return CDec(objCharacterSheet.balance).ToString("#,##0.00") & " ISK"
        End Get
    End Property

    Public ReadOnly Property CloneSkillPoints() As String
        Get
            Return objCharacterSheet.cloneName & " (" & CInt(objCharacterSheet.cloneSkillPoints).ToString("#,##0") & " SP)"
        End Get
    End Property

    Public ReadOnly Property SkillPoints() As String
        Get
            ' Todo: This will need to change once the skills class has been created and a skill is in training
            Dim intSkillPoints As Integer
            For Each skill In objCharacterSkills
                intSkillPoints += skill.Skill.skillpoints
            Next

            Return intSkillPoints.ToString("#,##0")
        End Get
    End Property

    Public ReadOnly Property Skills() As List(Of CharacterSkill)
        Get
            Return objCharacterSkills
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal CharacterID As String, ByVal UserID As String, ByVal ApiKey As String)
        strCharacterID = CharacterID
        strUserID = UserID
        strApiKey = ApiKey

        ' Get the character sheet and the image
        GetCharacterSheet()
        GetImage()
    End Sub

    Private Sub GetImage()
        ' Get the image path
        strCacheLocation = String.Format("{0}{1}Cache{1}Images{1}{2}.png", Settings.DataDir, Path.DirectorySeparatorChar, strCharacterID)

        ' Check to see if the character image exists already
        If File.Exists(strCacheLocation) = False Then
            ' Set the location string to get it straight from the server
            strImageLocation = "http://img.eve.is/serv.asp?s=256&c=" & strCharacterID
        Else
            strImageLocation = strCacheLocation
        End If

        ' Attempt to get the image
        Try
            bmpImage = BitmapFrame.Create(New Uri(strImageLocation, UriKind.RelativeOrAbsolute))
        Catch ex1 As Exception
            bmpImage = BitmapFrame.Create(New Uri("Images/IconNotFound", UriKind.Relative))
        End Try
    End Sub

    Public Sub DownloadImage()
        If File.Exists(strCacheLocation) = False AndAlso bmpImage.PixelHeight > 1 Then
            Try
                Dim objStream As New FileStream(strCacheLocation, FileMode.Create)
                Dim objEncoder As New PngBitmapEncoder()

                ' Save the image
                objEncoder.Frames.Add(bmpImage)
                objEncoder.Save(objStream)
                objStream.Close()
            Catch ex As Exception
                If File.Exists(strCacheLocation) = True Then
                    File.Delete(strCacheLocation)
                End If
            End Try
        End If
    End Sub

    Private Sub GetCharacterSheet()
        Dim objCharacterAPI As New API.Character(strUserID, strApiKey, strCharacterID)
        Dim objApiResult As Object

        ' Get the character sheet for the character
        objApiResult = objCharacterAPI.GetCharacterSheet()
        If TypeOf objApiResult Is API.ApiError Then
            Dim objError As API.ApiError
            objError = objApiResult

            If objError.Category <> API.UserErrors.BaseCategory Then
                Return
            End If
        End If

        ' TODO: fix this so that the where clause actually works. Currently it seems to think parsing it a string for the character ID gives it a data type of ntext or image
        Dim result = From c In objUserdata.CharacterSheet _
                     Where c.Characters.active = True 

        If result.Count() > 0 Then
            For Each cs In result
                If cs.characterID = strCharacterID Then
                    objCharacterSheet = cs
                End If
            Next
            ' Load the skills in
            LoadSkills()
        End If
    End Sub

    Private Sub LoadSkills()
        ' Load skills into the character sheet object
        objCharacterSheet.csSkills.Load(System.Data.Objects.MergeOption.OverwriteChanges)

        If objCharacterSheet.csSkills.Count > 0 Then
            ' Get the skills and add them to the list
            objCharacterSkills.Clear()
            Try
                For Each s In objCharacterSheet.csSkills
                    Dim objSkill As New CharacterSkill

                    objSkill.Skill = s

                    objCharacterSkills.Add(objSkill)
                Next
            Catch ex As Exception

            End Try
            
        End If
    End Sub
End Class
