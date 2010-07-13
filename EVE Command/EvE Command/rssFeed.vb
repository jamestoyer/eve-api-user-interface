Imports System.Net
Public Class rssFeed
    Public Shared Function getNewsFeed(ByVal feedUrl As String, ByVal channelId As Integer) As List(Of newsItem)
        Dim items As New List(Of newsItem)

        Try
            ' Set up the web request and response variables
            Dim feedRequest As WebRequest
            Dim feedResponse As WebResponse

            feedRequest = WebRequest.Create(feedUrl)
            feedResponse = feedRequest.GetResponse

            ' Convert the response into a stream
            Dim rssStream As System.IO.Stream
            rssStream = feedResponse.GetResponseStream

            ' Put the stream into an XDocument
            Dim rssDoc As XDocument
            rssDoc = XDocument.Load(rssStream)

            If rssDoc.Root.Name.LocalName = "rss" Then
                items = rss2(rssDoc, channelId)
            ElseIf rssDoc.Root.Name.LocalName = "feed" Then
                items = atom(rssDoc, channelId)
            ElseIf rssDoc.Root.Name.LocalName = "RDF" Then
                items = rss1(rssDoc, channelId)
            Else
                ' TODO: Expand to allow the user to let me know what format it was so I can add it
                MessageBox.Show("Sorry this feed type is not supported")
            End If

        Catch ex As Exception
            ' TODO: Make this an informative error
            MessageBox.Show("oops an error")
        End Try

        ' Send back the list
        Return items
    End Function

    Private Shared Function atom(ByVal rssDoc As XDocument, ByVal channelId As Integer) As List(Of newsItem)
        Dim atomNs As XNamespace = "http://www.w3.org/2005/Atom"
        atom = New List(Of newsItem)

        ' Get the items from the xml
        Dim items = From c In rssDoc.Element(atomNs + "feed").Elements
                  Where c.Name = atomNs + "entry"
                  Select c

        ' Go through the stories and pull out the vital info
        For Each entry In items
            Dim currentItem As New newsItem
            currentItem.channelId = channelId
            currentItem.title = IIf(IsNothing(entry.Element(atomNs + "title").Value), "", entry.Element(atomNs + "title").Value)
            currentItem.link = IIf(IsNothing(entry.Element(atomNs + "link").@href), "", entry.Element(atomNs + "link").@href)
            currentItem.content = IIf(IsNothing(entry.Element(atomNs + "content").Value), "", entry.Element(atomNs + "content").Value)

            ' Add it to the list
            atom.Add(currentItem)
        Next
    End Function

    Private Shared Function rss1(ByVal rssDoc As XDocument, ByVal channelId As Integer) As List(Of newsItem)
        rss1 = New List(Of newsItem)

        Dim rss1Ns As XNamespace = "http://purl.org/rss/1.0/"
        Dim rdfNs As XNamespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#"
        rss1 = New List(Of newsItem)

        ' Get the items from the xml
        Dim items = From c In rssDoc.Element(rdfNs + "RDF").Elements
                  Where c.Name = rss1Ns + "item"
                  Select c

        ' Go through the stories and pull out the vital info
        For Each entry In items
            Dim currentItem As New newsItem
            currentItem.channelId = channelId
            currentItem.title = IIf(IsNothing(entry.Element(rss1Ns + "title").Value), "", entry.Element(rss1Ns + "title").Value)
            currentItem.link = IIf(IsNothing(entry.Element(rss1Ns + "link").Value), "", entry.Element(rss1Ns + "link").Value)
            currentItem.content = IIf(IsNothing(entry.Element(rss1Ns + "description").Value), "", entry.Element(rss1Ns + "description").Value)

            ' Add it to the list
            rss1.Add(currentItem)
        Next
    End Function

    Private Shared Function rss2(ByVal rssDoc As XDocument, ByVal channelId As Integer) As List(Of newsItem)
        rss2 = New List(Of newsItem)

        ' Go through the xml document and pull out all the news items
        For Each item In rssDoc...<item>
            Dim currentItem As New newsItem
            currentItem.channelId = channelId
            currentItem.title = IIf(IsNothing(item...<title>.Value), "", item...<item>.Value)
            currentItem.link = IIf(IsNothing(item...<link>.Value), "", item...<item>.Value)
            currentItem.content = IIf(IsNothing(item...<description>.Value), "", item...<description>.Value)

            ' Add it to the list
            rss2.Add(currentItem)
        Next

    End Function

End Class
