﻿#ExternalChecksum("..\..\..\Accounts\winModifyAccount.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","D4202A9E01427AC3C0D45197E04619EA")
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

Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
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
'''winModifyAccount
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class winModifyAccount
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",23)
    Friend WithEvents lnkAPI As System.Windows.Documents.Hyperlink
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",41)
    Friend WithEvents txtUserID As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",45)
    Friend WithEvents txtApiKey As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",62)
    Friend WithEvents lstCharacters As System.Windows.Controls.ListView
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",89)
    Friend WithEvents btnOk As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",90)
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
        Dim resourceLocater As System.Uri = New System.Uri("/EVE Command;component/accounts/winmodifyaccount.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.lnkAPI = CType(target,System.Windows.Documents.Hyperlink)
            
            #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",23)
            AddHandler Me.lnkAPI.RequestNavigate, New System.Windows.Navigation.RequestNavigateEventHandler(AddressOf Me.lnkAPI_RequestNavigate)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.txtUserID = CType(target,System.Windows.Controls.TextBox)
            Return
        End If
        If (connectionId = 3) Then
            Me.txtApiKey = CType(target,System.Windows.Controls.TextBox)
            Return
        End If
        If (connectionId = 4) Then
            Me.lstCharacters = CType(target,System.Windows.Controls.ListView)
            Return
        End If
        If (connectionId = 5) Then
            Me.btnOk = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",89)
            AddHandler Me.btnOk.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnOk_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.btnExit = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\Accounts\winModifyAccount.xaml",90)
            AddHandler Me.btnExit.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnExit_Click)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

