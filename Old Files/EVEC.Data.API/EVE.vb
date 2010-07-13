Imports EVEC.Data.Database
Imports EVEC.Data.DEO
Imports EVEC.Data.Settings
Imports System.Data.SqlServerCe
Imports System.IO

Public Class EVE
    Inherits ApiBase

#Region "URI Constants"
    Const strGetAllianceList As String = "/eve/AllianceList.xml.aspx"
    Const strGetCertificateTree As String = "/eve/CertificateTree.xml.asp"
    Const strGetConquerableStationList As String = "/eve/ConquerableStationList.xml.aspx"
    Const strGetErrorList As String = "/eve/ErrorList.xml.aspx"
    Const strGetFacWarTopStats As String = "/eve/FacWarTopStats.xml.aspx"
    Const strGetCharacterName As String = "/eve/CharacterName.xml.aspx"
    Const strGetCharacterID As String = "/eve/CharacterID.xml.aspx"
    Const strGetRefTypes As String = "/eve/RefTypes.xml.aspx"
    Const strGetSkillTree As String = "/eve/SkillTree.xml.aspx"
#End Region

    Public Sub New()

    End Sub

#Region "Skill Tree API"
    Dim xmlSkillTree As XDocument

    ''' <summary>
    ''' Gets the entire skill tree for eve
    ''' </summary>
    ''' <returns>True is given if successful otherwise an apiError is returned</returns>
    ''' <remarks></remarks>
    Public Function GetSkillTree()
        Dim objResults As Object

        ' Get the api from the server
        objResults = GetApi("skills", strGetSkillTree, "EveData.sdf")

        ' Check the object for the next course of action
        If (TypeOf objResults Is XDocument) = True Then
            xmlSkillTree = objResults
        Else
            Return objResults
        End If

        ' Commit the results to the database
        GetSkillTree = CommitSkillTree()
    End Function

    Private Function CommitSkillTree()
        Dim sqlTransaction As SqlCeTransaction
        Dim objConnection As New Connection
        Dim blnSuccess As Boolean

        ' Open the connection and begin the transaction
        objConnection.Open("EveData.sdf")
        sqlTransaction = objConnection.dbConnection.BeginTransaction

        ' Add a week to the cached until time
        objCachedUntilTime = objCachedUntilTime.AddDays(7)

        ' Create the values to be inserted or updated
        For Each rowset In xmlSkillTree...<rowset>
            ' Check to see what rowset has been returned
            If rowset.@name = "skillGroups" Then
                ' Add the groups to the database
                blnSuccess = NoApiInfoRowsetToDatabase(objConnection, sqlTransaction, rowset)
            ElseIf rowset.@name = "skills" Then
                For Each row In rowset.<row>
                    Dim objValues As New List(Of Parameter)
                    Dim xmlRowsets As XElement = <results></results>

                    ' Create the values to be inserted or updated
                    For Each a In row.Attributes
                        ' Create insert detail
                        objValues.Add(New Parameter(a.Name.ToString, a.Value))
                    Next
                    For Each element In row.Elements
                        If element.Name = "requiredAttributes" Then
                            CreateRequiredAttributes(objValues, element)
                        ElseIf element.Name = "rowset" Then
                            xmlRowsets.Add(element)
                        Else
                            ' Create insert detail
                            objValues.Add(New Parameter(element.Name.ToString, element.Value))
                        End If
                    Next

                    ' Add the cached until variable
                    objValues.Add(New Parameter("cachedUntil", objCachedUntilTime))

                    ' Get the type ID of the skill
                    Dim intTypeID As Integer
                    intTypeID = row.@typeID

                    ' Check to see if the skill exists already
                    Dim objEveData As New EveDataEntities(ConnectionString.Create("EveData.sdf"))
                    Dim result = From s In objEveData.skills _
                                 Where s.typeID = intTypeID

                    ' Act according to the existance of the character sheet in the database
                    If result.Count() < 1 Then
                        blnSuccess = Records.Insert(objConnection, sqlTransaction, "skills", objValues)
                    Else
                        Dim objUpdateConditions As New List(Of Parameter)

                        ' Create the update conditions
                        objUpdateConditions.Add(New Parameter("typeID", intTypeID))

                        ' Ensure the typeID is not among the update parameters
                        For Each item In objValues
                            If item.Field = "typeID" Then
                                objValues.Remove(item)
                                Exit For
                            End If
                        Next

                        ' Update the database
                        blnSuccess = Records.Update(objConnection, sqlTransaction, "skills", objValues, objUpdateConditions)
                    End If

                    ' Check to see if the rowsets should be updated
                    If blnSuccess = True Then
                        Dim objCustomValues As New List(Of Parameter)
                        objCustomValues.Add(New Parameter("skillTypeID", intTypeID))

                        ' Send the rowset to the database
                        For Each childRowset In xmlRowsets.Elements
                            If blnSuccess = True Then
                                Try
                                    blnSuccess = SkillTypeIDRowsetToDatabase(objConnection, sqlTransaction, childRowset, objCustomValues, False)
                                Catch ex As Exception
                                    Console.WriteLine(ex)
                                End Try
                                Console.WriteLine(blnSuccess)
                            End If
                        Next
                    End If
                Next
            End If
            If blnSuccess = False Then Exit For
        Next

        ' Act according the the success
        If blnSuccess = True Then
            sqlTransaction.Commit()
        Else
            sqlTransaction.Rollback()
        End If

        ' Close the connection
        objConnection.Close()

        ' Create an error if the update was unsuccessful
        If blnSuccess <> True Then
            Return New ApiError(UserErrors.UnableToSave)
        End If

        Return blnSuccess
    End Function

    Private Sub CreateRequiredAttributes(ByRef parameters As List(Of Parameter), ByVal elements As XElement)
        For Each a In elements.Elements
            ' Create insert detail
            parameters.Add(New Parameter(a.Name.ToString, a.Value))
        Next
    End Sub
#End Region
End Class
