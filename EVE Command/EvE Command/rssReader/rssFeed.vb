Imports eveC.Common
Imports HTMLConverter
Imports System.Net
Namespace rssReader
    ''' <summary>
    ''' This module contains methods to get and store RSS feeds
    ''' </summary>
    ''' <remarks></remarks>
    Public Module rssFeed
        ''' <summary>
        ''' Gets the latest version of an RSS feed from the feed source
        ''' </summary>
        ''' <param name="feedUrl">The URL of the feed</param>
        ''' <param name="feedId">The Guid of the feed assigned in the database</param>
        ''' <returns>A list of all the current items on the feed</returns>
        ''' <remarks></remarks>
        Private Function updateFeed(ByVal feedUrl As String, ByVal feedId As Guid) As List(Of newsItem)
            Dim items As New List(Of newsItem)

            Try
                ' Get the feed
                Dim rssDoc As XDocument = feedWebRequest(feedUrl)

                ' Get the news items
                items = rss1.getItems(rssDoc, feedId)
            Catch ex As Exception
                ' Log the error and tell the user an error occured
                Dim errorLog As logger = New logger(logName.errorLog)
                errorLog.writeToLog(ex.Message)
                MessageBox.Show(My.Resources.Resources.RssFeedDownloadErrorMessage, My.Resources.Resources.RSSFeedUpdateError, MessageBoxButton.OK, MessageBoxImage.Error)
            End Try

            ' Send back the list
            Return items
        End Function

        ''' <summary>
        ''' Refreshes all the RSS feeds contained in the database and updates them 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub refreshFeeds()
            ' Declare a new reference to the entity
            Dim feeds As New rssFeedsEntities

            Try
                For Each f In feeds.feeds
                    Dim retrievedNewsItems As List(Of newsItem) = updateFeed(f.link, f.id)

                    ' Check for new news and news that needs to be updated
                    For Each i In retrievedNewsItems
                        Dim item As newsItem = i
                        Dim results = From n In f.items
                                      Where n.title = item.title And n.link = item.link
                                      Select n

                        ' See how many results are returned and act accordingly
                        If results.Count <> 0 Then
                            ' Check to see how many results there are and update accordingly
                            For Each r In results
                                If (Not IsDBNull(r.publishDate) AndAlso Not IsDBNull(item.publishDate)) AndAlso
                                    r.publishDate < item.publishDate Then

                                    ' Update the details of the item
                                    r.description = HtmlToXamlConverter.ConvertHtmlToXaml(item.description, True)
                                    r.publishDate = item.publishDate
                                    r.author = item.author
                                    r.dateAcquired = Now
                                End If
                            Next
                        Else
                            ' Add the new news feed
                            item.id = Guid.NewGuid
                            feeds.newsItems.AddObject(item)
                        End If
                    Next
                Next

                ' Save the changes
                feeds.SaveChanges()
            Catch ex As Exception
                ' Log the error and tell the user an error occured
                Dim errorLog As logger = New logger(logName.errorLog)
                errorLog.writeToLog(ex.Message)
                MessageBox.Show(My.Resources.Resources.FeedUpdateErrorMessage, My.Resources.Resources.RSSFeedUpdateError, MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Gets the feed from the URL source
        ''' </summary>
        ''' <param name="feedUrl">The location of the feed</param>
        ''' <returns>The feed as a XDocument</returns>
        ''' <remarks></remarks>
        Private Function feedWebRequest(ByVal feedUrl As String) As XDocument
            ' Set up the web request and response variables
            Dim feedRequest As WebRequest = WebRequest.Create(feedUrl)
            Dim feedResponse As WebResponse = feedRequest.GetResponse

            ' Convert the response into a stream
            Dim rssStream As System.IO.Stream = feedResponse.GetResponseStream

            ' Return the stream as an XDocument
            feedWebRequest = XDocument.Load(rssStream)
            Return feedWebRequest
        End Function
    End Module
End Namespace

