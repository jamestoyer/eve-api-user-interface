﻿Class MainWindow
    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        rssReader.rssFeed.refreshFeeds()
        Dim feedReader As rssFeedsEntities = New rssFeedsEntities
        'feeds.ItemsSource = feedReader.feeds
        newsList.ItemsSource = feedReader.feeds
    End Sub

    Private Sub WebBrowser_Initialized(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim htmlString As String = String.Empty
        ' Load the data into the web browser
        Dim webbrowsers As WebBrowser = CType(sender, WebBrowser)
        htmlString = "<body scroll=""no"">" & (CType(webbrowsers.DataContext, newsItem)).description
        webbrowsers.NavigateToString(htmlString)
    End Sub

    Private Sub WebBrowser_LoadCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Navigation.NavigationEventArgs)
        resizeWebBrowser(sender)
    End Sub

    Private Sub WebBrowser_SizeChanged(ByVal sender As System.Object, ByVal e As System.Windows.SizeChangedEventArgs)
        resizeWebBrowser(sender)
    End Sub

    Private Shared Sub resizeWebBrowser(ByVal sender As System.Object)
        ' Set the browser control to auto size to the content
        Dim browser As WebBrowser = CType(sender, WebBrowser)
        Dim item As mshtml.HTMLDocument = CType(browser.Document, mshtml.HTMLDocument)
        If Not IsNothing(item) AndAlso Not IsNothing(item.body) Then
            Dim Body As mshtml.IHTMLElement2 = CType(item.body, mshtml.IHTMLElement2)
            browser.Height = Body.scrollHeight
        End If
    End Sub

    Private Sub WebBrowser_DataContextChanged(ByVal sender As System.Object, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        Dim htmlString As String = String.Empty
        ' Load the data into the web browser
        Dim webbrowsers As WebBrowser = CType(sender, WebBrowser)

        Dim tryCasting As newsItem = TryCast(webbrowsers.DataContext, newsItem)
        If Not IsNothing(tryCasting) Then
            htmlString = "<body scroll=""no"">" & tryCasting.description
            webbrowsers.NavigateToString(htmlString)
            resizeWebBrowser(sender)
        End If
    End Sub
End Class
