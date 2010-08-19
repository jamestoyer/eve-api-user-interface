Class MainWindow
    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        rssReader.rssFeed.refreshFeeds()
        Dim feedReader As rssFeedsEntities = New rssFeedsEntities
        Dim asc = (From f In feedReader.feeds
                   Order By f.name Ascending
                  Select f)
        newsList.ItemsSource = asc
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

    Private Sub RefreshMenuItem_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        ' Refresh the rss feeds
        rssReader.refreshFeeds()
    End Sub

    Private Sub ExitMenuItem_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        My.Application.Shutdown()
    End Sub

    Private Sub FlowDocumentScrollViewer_DataContextChanged(ByVal sender As System.Object, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        Dim viewer As FlowDocumentScrollViewer = CType(sender, FlowDocumentScrollViewer)

        Dim tryCasting As newsItem = TryCast(viewer.DataContext, newsItem)
        If Not IsNothing(tryCasting) Then
            ' Encode the string into a byte array and add it to a memorystream
            Dim stream As New System.IO.MemoryStream(Text.UTF8Encoding.UTF8.GetBytes(tryCasting.description))

            ' Load the flow document
            viewer.Document = TryCast(Markup.XamlReader.Load(stream), FlowDocument)
        End If
    End Sub

    Private Sub FlowDocumentScrollViewer_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim linkClicked As Hyperlink = TryCast(e.OriginalSource, Hyperlink)

        ' If the object is not a hyperlink then just exit here
        If IsNothing(linkClicked) Then Return

        ' Lets navigate to the correct page
        System.Diagnostics.Process.Start(linkClicked.NavigateUri.ToString)
    End Sub
End Class
