﻿#ExternalChecksum("..\..\wpfAccountsManager.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","23FFBBD1579DC8890B51EB59BBC42C83")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.4918
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports EVEC1
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Forms.Integration
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes


'''<summary>
'''wpfAccountsManager
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class wpfAccountsManager
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\wpfAccountsManager.xaml",24)
    Friend WithEvents btnAdd As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfAccountsManager.xaml",25)
    Friend WithEvents btnModify As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfAccountsManager.xaml",26)
    Friend WithEvents btnDelete As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfAccountsManager.xaml",30)
    Friend WithEvents lstAccounts As System.Windows.Controls.ListBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfAccountsManager.xaml",38)
    Friend WithEvents btnOk As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\wpfAccountsManager.xaml",39)
    Friend WithEvents btnExit As System.Windows.Controls.Button
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/EVE Command1;component/wpfaccountsmanager.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\wpfAccountsManager.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.btnAdd = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\wpfAccountsManager.xaml",24)
            AddHandler Me.btnAdd.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnAdd_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.btnModify = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\wpfAccountsManager.xaml",25)
            AddHandler Me.btnModify.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnModify_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            Me.btnDelete = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\wpfAccountsManager.xaml",26)
            AddHandler Me.btnDelete.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnDelete_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 4) Then
            Me.lstAccounts = CType(target,System.Windows.Controls.ListBox)
            Return
        End If
        If (connectionId = 5) Then
            Me.btnOk = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\wpfAccountsManager.xaml",38)
            AddHandler Me.btnOk.Click, New System.Windows.RoutedEventHandler(AddressOf Me.CloseForm)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.btnExit = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\wpfAccountsManager.xaml",39)
            AddHandler Me.btnExit.Click, New System.Windows.RoutedEventHandler(AddressOf Me.CloseForm)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

