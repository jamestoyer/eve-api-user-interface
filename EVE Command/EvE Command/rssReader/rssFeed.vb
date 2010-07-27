Imports System.Net
Namespace rssReader
    Public Class rssFeed
        Private Shared Function updateFeed(ByVal feedUrl As String, ByVal feedId As Guid) As List(Of newsItem)
            Dim items As New List(Of newsItem)

            Try
                ' Get the feed
                Dim rssDoc As XDocument
                rssDoc = feedWebRequest(feedUrl)

                ' Get the news items
                items = rss1.getItems(rssDoc, feedId)

            Catch ex As Exception
                ' TODO: Make this an informative error
                MessageBox.Show("oops an error")
            End Try

            ' Send back the list
            Return items
        End Function

        Public Shared Sub refreshFeeds()
            Try
                Dim feeds As New rssFeedsEntities

                For Each f In feeds.feeds
                    Dim retrievedNewsItems As New List(Of newsItem)

                    ' Get the news items for this feed
                    retrievedNewsItems = updateFeed(f.link, f.id)

                    ' Check for new news and news that needs to be updated
                    For Each i In retrievedNewsItems
                        Dim item As newsItem = i
                        Dim results = From n In f.items
                                      Where n.title = item.title And n.link = item.link
                                      Select n

                        ' See how many results are returned and act accordingly
                        If results.Count <> 0 Then
                            For Each r In results
                                If IsDBNull(r.publishDate) = False AndAlso IsDBNull(item.publishDate) = False Then
                                    ' Update the details of the item
                                    If r.publishDate < item.publishDate Then
                                        Dim id As New Guid
                                        r.description = item.description
                                        r.publishDate = item.publishDate
                                        r.author = item.author
                                        r.dateAcquired = Now
                                    End If
                                End If
                            Next
                        Else
                            item.id = Guid.NewGuid
                            feeds.newsItems.AddObject(item)
                        End If
                    Next
                Next

                ' Save the changes
                feeds.SaveChanges()
            Catch ex As Exception
                ' TODO: Make this informative
                MessageBox.Show("oops and error!")
            End Try
        End Sub

        Private Shared Function feedWebRequest(ByVal feedUrl As String) As XDocument
            ' Set up the web request and response variables
            Dim feedRequest As WebRequest
            Dim feedResponse As WebResponse

            feedRequest = WebRequest.Create(feedUrl)
            feedResponse = feedRequest.GetResponse

            ' Convert the response into a stream
            Dim rssStream As System.IO.Stream
            rssStream = feedResponse.GetResponseStream

            ' Return the stream as an XDocument
            feedWebRequest = XDocument.Load(rssStream)
            Return feedWebRequest
        End Function
    End Class
End Namespace

