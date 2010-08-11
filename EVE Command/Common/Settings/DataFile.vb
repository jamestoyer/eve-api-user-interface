Imports System.Xml.Serialization

<Serializable()> _
Public Class DataFile
    ''' <summary>
    ''' The file name of the data file without it's path
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlElement()> _
    Public Property FileName() As String

    ''' <summary>
    ''' Indicates that the file has actually been found when checking for it
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlElement()> _
    Public Property Found() As Boolean

    Public Sub New()
    End Sub

    Public Sub New(ByVal FileName As String, ByVal Found As Boolean)
        Me.FileName = FileName
        Me.Found = Found
    End Sub
End Class
