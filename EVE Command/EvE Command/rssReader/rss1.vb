Imports <xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#">
Imports <xmlns:dc="http://purl.org/dc/elements/1.1/">
Imports <xmlns:sy="http://purl.org/rss/1.0/modules/syndication/">
Imports <xmlns="http://purl.org/rss/1.0/">

Namespace rssReader
    Public Class rss1
        Public Shared Function getItems(ByVal rssDoc As XDocument, ByVal feedId As Guid) As List(Of newsItem)
            getItems = New List(Of newsItem)

            ' Go through the xml document and pull out all the news items
            For Each item In rssDoc...<item>
                Dim currentItem As New newsItem
                currentItem.feedId = feedId
                currentItem.title = item.<title>.Value
                currentItem.description = IIf(IsNothing(item.<description>.Value), DBNull.Value, item.<description>.Value)
                currentItem.link = item.<link>.Value
                currentItem.publishDate = IIf(IsNothing(item.<dc:date>.Value), DBNull.Value, CType(item.<dc:date>.Value, DateTime))
                currentItem.author = IIf(IsNothing(item.<dc:creator>.Value), DBNull.Value, item.<dc:creator>.Value)
                currentItem.dateAcquired = Now

                ' Add it to the list
                getItems.Add(currentItem)
            Next
        End Function
    End Class
End Namespace
