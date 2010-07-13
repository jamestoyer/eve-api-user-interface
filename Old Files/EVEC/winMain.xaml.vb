Imports EVEC.Common
Imports EVEC.Data.Database
Imports System.Threading

Partial Public Class winMain
    ' Declare a new character image list
    Dim objCharacters As New CharacterList
    ' TODO: Multi threading stuff
    'Private Shared objTest As New listTest
    'Private Shared waitHandles() As WaitHandle = {New AutoResetEvent(False), New AutoResetEvent(False)}
    'Private Shared r As New Random()

    Private Sub winMain_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed
        My.Application.Shutdown()
    End Sub

    Private Sub winMain_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ' Load the character portraits
        If LoadCharacters() = False Then
            Return
        End If
    End Sub

    Private Sub mnuManageAccounts_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim frmAccountManager As New winAccountsManager

        ' Show the add account form
        If frmAccountManager.ManageCharacters = True Then
            ' Load the character portraits
            LoadCharacters()
        End If
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Loads the wanted characters into the form
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function LoadCharacters() As Boolean
        ' Connect to the database
        Dim objConnection As New Connection()
        If objConnection.Open("Userdata.sdf") = False Then
            MessageBox.Show("Unable to load the characters", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
            Return False
        End If

        ' Declare the userdata database to LinQ object and assign the transaction to it
        Dim objUserdata As New Userdata(objConnection.dbConnection)

GetCharacters:
        ' Check to see if the character already exists
        Dim result = From Characters In objUserdata.Characters _
                     Where Characters.Active = True _
                     Order By Characters.Name

        ' Clear the list box and reload it
        objCharacters.Clear()

        ' Check to see how many result there are
        If result.Count < 1 Then
            Dim AddAccount As New winModifyAccount

            MessageBox.Show("It appears thats you have no accounts associtated with running EvE Command. Please add your account details to begin.", "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)

            If AddAccount.Add = True Then
                ' Attempt to get the characters
                GoTo GetCharacters
            End If

            LoadCharacters = False
        Else

            ' Get the character information
            For Each c In result
                ' TODO: Multi threading stuff
                'objTest.Add(New test(New AutoResetEvent(False), c))

                Dim objCharacter As New Character(c.CharacterID, c.UserID, c.UserInfo.ApiKey)
                objCharacters.Add(objCharacter)
                ' TODO: Multi threading stuff
                'Dim test As New Thread(AddressOf LoadIndividualCharacter)
                'Dim taste As New WaitCallback(AddressOf LoadIndividualCharacter, )

                'ThreadPool.QueueUserWorkItem(AddressOf LoadIndividualCharacter, objTest.it)
            Next
            ' TODO: Multi threading stuff
            '' Create the threads
            'For Each t In objTest
            '    ThreadPool.QueueUserWorkItem(AddressOf LoadIndividualCharacter, t)
            'Next

            'WaitHandle.WaitAll(objTest.WaitHandles)

            '' Ensure the characters are in the correct order
            'objCharacters.Sort(AddressOf objCharacters.Compare)

            ' Set the datacontext of the list
            lstCharacters.ItemsSource = objCharacters
            lstCharacters.Items.Refresh()
            lstCharacters.SelectedIndex = 0

            LoadCharacters = True
        End If

        ' close the connection
        objConnection.Close()

        ' TODO: Multi threading stuff
        'Dim tester As New Thread(AddressOf testmain)
        'tester.SetApartmentState(ApartmentState.MTA)
        'tester.Start()
        'tester.Join()
    End Function

    ' TODO: Multi threading stuff
    '    <MTAThread()> _
    '    Shared Sub testmain()
    '        Thread.CurrentThread.SetApartmentState(ApartmentState.MTA)
    '        Try
    '            ' Queue two tasks on two different threads; 
    '            ' wait until all tasks are completed.
    '            Dim dt As DateTime = DateTime.Now
    '            Console.WriteLine("Main thread is waiting for BOTH tasks to complete.")
    '            ThreadPool.QueueUserWorkItem(AddressOf DoTask, waitHandles(0))
    '            ThreadPool.QueueUserWorkItem(AddressOf DoTask, waitHandles(1))
    '            WaitHandle.WaitAll(waitHandles)
    '            ' The time shown below should match the longest task.
    '            Console.WriteLine("Both tasks are completed (time waited={0})", _
    '                (DateTime.Now - dt).TotalMilliseconds)

    '            ' Queue up two tasks on two different threads; 
    '            ' wait until any tasks are completed.
    '            dt = DateTime.Now
    '            Console.WriteLine()
    '            Console.WriteLine("The main thread is waiting for either task to complete.")
    '            ThreadPool.QueueUserWorkItem(AddressOf DoTask, waitHandles(0))
    '            ThreadPool.QueueUserWorkItem(AddressOf DoTask, waitHandles(1))
    '            Dim index As Integer = WaitHandle.WaitAny(waitHandles)
    '            ' The time shown below should match the shortest task.
    '            Console.WriteLine("Task {0} finished first (time waited={1}).", _
    '                index + 1, (DateTime.Now - dt).TotalMilliseconds)
    '        Catch ex As Exception

    '        End Try


    '    End Sub

    '    Shared Sub DoTask(ByVal state As [Object])
    '        Dim are As AutoResetEvent = CType(state, AutoResetEvent)
    '        Dim time As Integer = 1000 * r.Next(2, 10)
    '        Console.WriteLine("Performing a task for {0} milliseconds.", time)
    '        Thread.Sleep(time)
    '        are.Set()

    '    End Sub 'DoTask


    '    Private Sub LoadIndividualCharacter(ByVal testObject As Object)
    '        SyncLock Me
    '            If TypeOf testObject Is test Then
    '                Dim objTest As test = testObject

    '                If objTest IsNot Nothing Then
    '                    Dim objCharacter As New Character(objTest.Data.CharacterID, objTest.Data.UserID, objTest.Data.UserInfo.ApiKey)
    '                    objCharacters.Add(objCharacter)
    '                    Dim are As AutoResetEvent = CType(objTest.ThreadHandle, AutoResetEvent)
    '                    are.Set()
    '                End If
    '            End If
    '        End SyncLock
    '    End Sub

    '    ''' <summary>
    '    ''' Loads the character portaits into the list box
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function LoadCharacterPortraits() As Boolean

    '    End Function
    'End Class

    'Public Class listTest
    '    Inherits List(Of test)
    '    Dim objWaitHandles() As WaitHandle

    '    Public Property WaitHandles() As WaitHandle()
    '        Get
    '            HandlesToArray()
    '            Return objWaitHandles
    '        End Get
    '        Set(ByVal value As WaitHandle())
    '            objWaitHandles = value
    '        End Set
    '    End Property

    '    Private Sub HandlesToArray()
    '        ' Count the number of items
    '        Array.Resize(objWaitHandles, Me.Count)

    '        Dim intTest As Integer = 0
    '        For Each t In Me
    '            objWaitHandles(intTest) = t.ThreadHandle
    '            intTest += 1
    '        Next
    '    End Sub
    'End Class

    'Public Class test
    '    Private objThreadHandle As WaitHandle
    '    Private objData As Object

    '    Public Sub New(ByVal threadHandle As WaitHandle, ByVal data As Object)
    '        objThreadHandle = threadHandle
    '        objData = data
    '    End Sub

    '    Public Property ThreadHandle() As WaitHandle
    '        Get
    '            Return objThreadHandle
    '        End Get
    '        Set(ByVal value As WaitHandle)
    '            objThreadHandle = value
    '        End Set
    '    End Property

    '    Public Property Data() As Object
    '        Get
    '            Return objData
    '        End Get
    '        Set(ByVal value As Object)
    '            objData = value
    '        End Set
    '    End Property
End Class
