Class MainWindow 

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        ' See if the thing actually works
        'If url IsNot Nothing Then
        '    Dim test As List(Of newsItem)

        '    test = rssReader.rssFeed.getFeed(url.Text, 0)

        'End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        rssReader.rssFeed.refreshFeeds()
    End Sub
End Class
