Imports <xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#">
Imports <xmlns:dc="http://purl.org/dc/elements/1.1/">
Imports <xmlns:sy="http://purl.org/rss/1.0/modules/syndication/">
Imports <xmlns="http://purl.org/rss/1.0/">
Imports HTMLConverter
Namespace rssReader
    ''' <summary>
    ''' Module for processing and handling downloaded RSS v1 feeds
    ''' </summary>
    ''' <remarks></remarks>
    Public Module rss1
        ''' <summary>
        ''' Retrives the feed items and adds them to a more friendly list
        ''' </summary>
        ''' <param name="rssDoc">The XDocument containing the feed</param>
        ''' <param name="feedId">The Guid of the feed</param>
        ''' <returns>A List(Of newsItem) containt all the items</returns>
        ''' <remarks></remarks>
        Public Function getItems(ByVal rssDoc As XDocument, ByVal feedId As Guid) As List(Of newsItem)
            getItems = New List(Of newsItem)

            ' Go through the xml document and pull out all the news items
            For Each item In rssDoc...<item>

                Dim currentItem As New newsItem
                currentItem.feedId = feedId
                currentItem.title = item.<title>.Value
                currentItem.description =
                    CType(IIf(IsNothing(item.<description>.Value), DBNull.Value, HtmlToXamlConverter.ConvertHtmlToXaml(item.<description>.Value, True)), String)
                currentItem.link = item.<link>.Value
                currentItem.publishDate =
                    CType(IIf(IsNothing(item.<dc:date>.Value), DBNull.Value, CType(item.<dc:date>.Value, DateTime)), DateTime)
                currentItem.author =
                    CType(IIf(IsNothing(item.<dc:creator>.Value), DBNull.Value, item.<dc:creator>.Value), String)
                currentItem.dateAcquired = Now

                ' Add it to the list
                getItems.Add(currentItem)
            Next
        End Function
    End Module
End Namespace
