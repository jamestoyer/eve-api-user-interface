﻿#ExternalChecksum("..\..\..\..\Controls\CharacterDetails.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","2B333BF0223888FF33239F79A0A1AEF4")
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
'''CharacterDetails
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class CharacterDetails
    Inherits System.Windows.Controls.UserControl
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",10)
    Friend WithEvents Details As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",35)
    Friend WithEvents lblName As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",36)
    Friend WithEvents lblRace As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",37)
    Friend WithEvents lblGender As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",38)
    Friend WithEvents lblBloodline As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",39)
    Friend WithEvents lblCorporation As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",40)
    Friend WithEvents lblSkillpoints As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",41)
    Friend WithEvents lblClone As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",42)
    Friend WithEvents lblWallet As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",44)
    Friend WithEvents lblSkill As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",45)
    Friend WithEvents lblTimeRemaining As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",46)
    Friend WithEvents lblEndTime As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",57)
    Friend WithEvents pnlSkills As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",71)
    Friend WithEvents lblNoOfSkills As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",72)
    Friend WithEvents lblSkillPointsPanel As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",77)
    Friend WithEvents pnlSkillListPanel As System.Windows.Controls.StackPanel
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",91)
    Friend WithEvents pnlTrainingQueue As System.Windows.Controls.StackPanel
    
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
        Dim resourceLocater As System.Uri = New System.Uri("/EVE Command1;component/controls/characterdetails.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\Controls\CharacterDetails.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.Details = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 2) Then
            Me.lblName = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 3) Then
            Me.lblRace = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 4) Then
            Me.lblGender = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 5) Then
            Me.lblBloodline = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 6) Then
            Me.lblCorporation = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 7) Then
            Me.lblSkillpoints = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 8) Then
            Me.lblClone = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 9) Then
            Me.lblWallet = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 10) Then
            Me.lblSkill = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 11) Then
            Me.lblTimeRemaining = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 12) Then
            Me.lblEndTime = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 13) Then
            Me.pnlSkills = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 14) Then
            Me.lblNoOfSkills = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 15) Then
            Me.lblSkillPointsPanel = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 16) Then
            Me.pnlSkillListPanel = CType(target,System.Windows.Controls.StackPanel)
            Return
        End If
        If (connectionId = 17) Then
            Me.pnlTrainingQueue = CType(target,System.Windows.Controls.StackPanel)
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class
