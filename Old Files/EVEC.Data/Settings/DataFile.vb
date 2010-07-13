Imports System.Xml.Serialization

<XmlRoot()> _
Public Class DataFile
    Private strFileName As String
    Private blnFound As Boolean

    <XmlElement()> _
    Public Property FileName() As String
        Get
            Return strFileName
        End Get
        Set(ByVal value As String)
            strFileName = value
        End Set
    End Property

    <XmlElement()> _
    Public Property Found() As Boolean
        Get
            Return blnFound
        End Get
        Set(ByVal value As Boolean)
            blnFound = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal FileName As String, ByVal Found As Boolean)
        strFileName = FileName
        blnFound = Found
    End Sub
End Class
