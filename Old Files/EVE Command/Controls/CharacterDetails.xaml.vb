Imports EVEC.Data.DEO
Partial Public Class CharacterDetails


    Private Sub CharacterDetails_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim tes As New EVEC.Data.API.Character(3475819, "XcvoussGyHEADizZ9RRiVCwNbMojvrO7bvCZPYkK9uAL57RT4drDHXzwuzYHXkvW", 926431023)
        tes.GetCharacterSheet()
        Dim test As New UserdataEntities(ConnectionString.Create("Userdata.sdf"))
        Dim results = From cs In test.CharacterSheet _
                      Where (cs.characterID = 926431023)
        'Dim tester As CharacterSheet
        'tester.Characters.UserInfo.
        Me.Details.DataContext = results

        For Each cs In results
            lblClone.Content = cs.cloneName & " (" & CInt(cs.cloneSkillPoints).ToString("#,##0") & " SP)"
            lblWallet.Content = CDec(cs.balance).ToString("#,##0.00") & " ISK"

        Next
    End Sub

End Class
