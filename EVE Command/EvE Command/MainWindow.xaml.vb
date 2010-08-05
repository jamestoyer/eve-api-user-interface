Class MainWindow 
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        rssReader.rssFeed.refreshFeeds()
    End Sub

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        rssReader.rssFeed.refreshFeeds()
        Dim feedReader As rssFeedsEntities = New rssFeedsEntities
        feedItems.ItemsSource = feedReader.feeds
    End Sub
End Class
