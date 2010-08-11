Imports System.Xml.Serialization

<XmlType("DataDirectory")>
Public Class DataDirectory
    ''' <summary>
    ''' The path to which the directory resides
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlAttribute("Path")>
    Public Property path As String

    ''' <summary>
    ''' The data files that should be in the directory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <XmlArray("DataFiles")>
    <XmlArrayItem("File")>
    Public Property dataFiles() As List(Of DataFile)
End Class

