﻿'------------------------------------------------------------------------------
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

<Assembly: Global.System.Data.Objects.DataClasses.EdmSchemaAttribute("12402258-9642-42c9-887c-f33f16d173c0"),  _
 Assembly: Global.System.Data.Objects.DataClasses.EdmRelationshipAttribute("EveDataModel", "requiredSkills_skilss_FK", "skills", Global.System.Data.Metadata.Edm.RelationshipMultiplicity.One, GetType(DEO.skills), "requiredSkills", Global.System.Data.Metadata.Edm.RelationshipMultiplicity.Many, GetType(DEO.requiredSkills)),  _
 Assembly: Global.System.Data.Objects.DataClasses.EdmRelationshipAttribute("EveDataModel", "SkillGroups_Skills_FK", "skillGroups", Global.System.Data.Metadata.Edm.RelationshipMultiplicity.One, GetType(DEO.skillGroups), "skills", Global.System.Data.Metadata.Edm.RelationshipMultiplicity.Many, GetType(DEO.skills)),  _
 Assembly: Global.System.Data.Objects.DataClasses.EdmRelationshipAttribute("EveDataModel", "skillBonusCollection_skills_FK", "skills", Global.System.Data.Metadata.Edm.RelationshipMultiplicity.One, GetType(DEO.skills), "skillBonusCollection", Global.System.Data.Metadata.Edm.RelationshipMultiplicity.Many, GetType(DEO.skillBonusCollection))> 

'Original file name:
'Generation date: 16/05/2009 13:23:38
Namespace DEO
    '''<summary>
    '''There are no comments for EveDataEntities in the schema.
    '''</summary>
    Partial Public Class EveDataEntities
        Inherits Global.System.Data.Objects.ObjectContext
        '''<summary>
        '''Initializes a new EveDataEntities object using the connection string found in the 'EveDataEntities' section of the application configuration file.
        '''</summary>
        Public Sub New()
            MyBase.New("name=EveDataEntities", "EveDataEntities")
            Me.OnContextCreated
        End Sub
        '''<summary>
        '''Initialize a new EveDataEntities object.
        '''</summary>
        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString, "EveDataEntities")
            Me.OnContextCreated
        End Sub
        '''<summary>
        '''Initialize a new EveDataEntities object.
        '''</summary>
        Public Sub New(ByVal connection As Global.System.Data.EntityClient.EntityConnection)
            MyBase.New(connection, "EveDataEntities")
            Me.OnContextCreated
        End Sub
        Partial Private Sub OnContextCreated()
        End Sub
        '''<summary>
        '''There are no comments for requiredSkills in the schema.
        '''</summary>
        Public ReadOnly Property requiredSkills() As Global.System.Data.Objects.ObjectQuery(Of requiredSkills)
            Get
                If (Me._requiredSkills Is Nothing) Then
                    Me._requiredSkills = MyBase.CreateQuery(Of requiredSkills)("[requiredSkills]")
                End If
                Return Me._requiredSkills
            End Get
        End Property
        Private _requiredSkills As Global.System.Data.Objects.ObjectQuery(Of requiredSkills)
        '''<summary>
        '''There are no comments for skillGroups in the schema.
        '''</summary>
        Public ReadOnly Property skillGroups() As Global.System.Data.Objects.ObjectQuery(Of skillGroups)
            Get
                If (Me._skillGroups Is Nothing) Then
                    Me._skillGroups = MyBase.CreateQuery(Of skillGroups)("[skillGroups]")
                End If
                Return Me._skillGroups
            End Get
        End Property
        Private _skillGroups As Global.System.Data.Objects.ObjectQuery(Of skillGroups)
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        Public ReadOnly Property skills() As Global.System.Data.Objects.ObjectQuery(Of skills)
            Get
                If (Me._skills Is Nothing) Then
                    Me._skills = MyBase.CreateQuery(Of skills)("[skills]")
                End If
                Return Me._skills
            End Get
        End Property
        Private _skills As Global.System.Data.Objects.ObjectQuery(Of skills)
        '''<summary>
        '''There are no comments for skillBonusCollection in the schema.
        '''</summary>
        Public ReadOnly Property skillBonusCollection() As Global.System.Data.Objects.ObjectQuery(Of skillBonusCollection)
            Get
                If (Me._skillBonusCollection Is Nothing) Then
                    Me._skillBonusCollection = MyBase.CreateQuery(Of skillBonusCollection)("[skillBonusCollection]")
                End If
                Return Me._skillBonusCollection
            End Get
        End Property
        Private _skillBonusCollection As Global.System.Data.Objects.ObjectQuery(Of skillBonusCollection)
        '''<summary>
        '''There are no comments for requiredSkills in the schema.
        '''</summary>
        Public Sub AddTorequiredSkills(ByVal requiredSkills As requiredSkills)
            MyBase.AddObject("requiredSkills", requiredSkills)
        End Sub
        '''<summary>
        '''There are no comments for skillGroups in the schema.
        '''</summary>
        Public Sub AddToskillGroups(ByVal skillGroups As skillGroups)
            MyBase.AddObject("skillGroups", skillGroups)
        End Sub
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        Public Sub AddToskills(ByVal skills As skills)
            MyBase.AddObject("skills", skills)
        End Sub
        '''<summary>
        '''There are no comments for skillBonusCollection in the schema.
        '''</summary>
        Public Sub AddToskillBonusCollection(ByVal skillBonusCollection As skillBonusCollection)
            MyBase.AddObject("skillBonusCollection", skillBonusCollection)
        End Sub
    End Class
    '''<summary>
    '''There are no comments for EveDataModel.requiredSkills in the schema.
    '''</summary>
    '''<KeyProperties>
    '''typeID
    '''skillTypeID
    '''</KeyProperties>
    <Global.System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName:="EveDataModel", Name:="requiredSkills"),  _
     Global.System.Runtime.Serialization.DataContractAttribute(IsReference:=true),  _
     Global.System.Serializable()>  _
    Partial Public Class requiredSkills
        Inherits Global.System.Data.Objects.DataClasses.EntityObject
        '''<summary>
        '''Create a new requiredSkills object.
        '''</summary>
        '''<param name="typeID">Initial value of typeID.</param>
        '''<param name="skillLevel">Initial value of skillLevel.</param>
        '''<param name="skillTypeID">Initial value of skillTypeID.</param>
        Public Shared Function CreaterequiredSkills(ByVal typeID As Long, ByVal skillLevel As Byte, ByVal skillTypeID As Long) As requiredSkills
            Dim requiredSkills As requiredSkills = New requiredSkills
            requiredSkills.typeID = typeID
            requiredSkills.skillLevel = skillLevel
            requiredSkills.skillTypeID = skillTypeID
            Return requiredSkills
        End Function
        '''<summary>
        '''There are no comments for Property typeID in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property typeID() As Long
            Get
                Return Me._typeID
            End Get
            Set
                Me.OntypeIDChanging(value)
                Me.ReportPropertyChanging("typeID")
                Me._typeID = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("typeID")
                Me.OntypeIDChanged
            End Set
        End Property
        Private _typeID As Long
        Partial Private Sub OntypeIDChanging(ByVal value As Long)
        End Sub
        Partial Private Sub OntypeIDChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property skillLevel in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillLevel() As Byte
            Get
                Return Me._skillLevel
            End Get
            Set
                Me.OnskillLevelChanging(value)
                Me.ReportPropertyChanging("skillLevel")
                Me._skillLevel = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("skillLevel")
                Me.OnskillLevelChanged
            End Set
        End Property
        Private _skillLevel As Byte
        Partial Private Sub OnskillLevelChanging(ByVal value As Byte)
        End Sub
        Partial Private Sub OnskillLevelChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property skillTypeID in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillTypeID() As Long
            Get
                Return Me._skillTypeID
            End Get
            Set
                Me.OnskillTypeIDChanging(value)
                Me.ReportPropertyChanging("skillTypeID")
                Me._skillTypeID = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("skillTypeID")
                Me.OnskillTypeIDChanged
            End Set
        End Property
        Private _skillTypeID As Long
        Partial Private Sub OnskillTypeIDChanging(ByVal value As Long)
        End Sub
        Partial Private Sub OnskillTypeIDChanged()
        End Sub
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("EveDataModel", "requiredSkills_skilss_FK", "skills"),  _
         Global.System.Xml.Serialization.XmlIgnoreAttribute(),  _
         Global.System.Xml.Serialization.SoapIgnoreAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skills() As skills
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skills)("EveDataModel.requiredSkills_skilss_FK", "skills").Value
            End Get
            Set
                CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skills)("EveDataModel.requiredSkills_skilss_FK", "skills").Value = value
            End Set
        End Property
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        <Global.System.ComponentModel.BrowsableAttribute(false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillsReference() As Global.System.Data.Objects.DataClasses.EntityReference(Of skills)
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skills)("EveDataModel.requiredSkills_skilss_FK", "skills")
            End Get
            Set
                If (Not (value) Is Nothing) Then
                    CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.InitializeRelatedReference(Of skills)("EveDataModel.requiredSkills_skilss_FK", "skills", value)
                End If
            End Set
        End Property
    End Class
    '''<summary>
    '''There are no comments for EveDataModel.skillGroups in the schema.
    '''</summary>
    '''<KeyProperties>
    '''groupID
    '''</KeyProperties>
    <Global.System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName:="EveDataModel", Name:="skillGroups"),  _
     Global.System.Runtime.Serialization.DataContractAttribute(IsReference:=true),  _
     Global.System.Serializable()>  _
    Partial Public Class skillGroups
        Inherits Global.System.Data.Objects.DataClasses.EntityObject
        '''<summary>
        '''Create a new skillGroups object.
        '''</summary>
        '''<param name="groupID">Initial value of groupID.</param>
        '''<param name="groupName">Initial value of groupName.</param>
        Public Shared Function CreateskillGroups(ByVal groupID As Integer, ByVal groupName As String) As skillGroups
            Dim skillGroups As skillGroups = New skillGroups
            skillGroups.groupID = groupID
            skillGroups.groupName = groupName
            Return skillGroups
        End Function
        '''<summary>
        '''There are no comments for Property groupID in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property groupID() As Integer
            Get
                Return Me._groupID
            End Get
            Set
                Me.OngroupIDChanging(value)
                Me.ReportPropertyChanging("groupID")
                Me._groupID = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("groupID")
                Me.OngroupIDChanged
            End Set
        End Property
        Private _groupID As Integer
        Partial Private Sub OngroupIDChanging(ByVal value As Integer)
        End Sub
        Partial Private Sub OngroupIDChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property groupName in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property groupName() As String
            Get
                Return Me._groupName
            End Get
            Set
                Me.OngroupNameChanging(value)
                Me.ReportPropertyChanging("groupName")
                Me._groupName = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("groupName")
                Me.OngroupNameChanged
            End Set
        End Property
        Private _groupName As String
        Partial Private Sub OngroupNameChanging(ByVal value As String)
        End Sub
        Partial Private Sub OngroupNameChanged()
        End Sub
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("EveDataModel", "SkillGroups_Skills_FK", "skills"),  _
         Global.System.Xml.Serialization.XmlIgnoreAttribute(),  _
         Global.System.Xml.Serialization.SoapIgnoreAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skills() As Global.System.Data.Objects.DataClasses.EntityCollection(Of skills)
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedCollection(Of skills)("EveDataModel.SkillGroups_Skills_FK", "skills")
            End Get
            Set
                If (Not (value) Is Nothing) Then
                    CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.InitializeRelatedCollection(Of skills)("EveDataModel.SkillGroups_Skills_FK", "skills", value)
                End If
            End Set
        End Property
    End Class
    '''<summary>
    '''There are no comments for EveDataModel.skills in the schema.
    '''</summary>
    '''<KeyProperties>
    '''typeID
    '''</KeyProperties>
    <Global.System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName:="EveDataModel", Name:="skills"),  _
     Global.System.Runtime.Serialization.DataContractAttribute(IsReference:=true),  _
     Global.System.Serializable()>  _
    Partial Public Class skills
        Inherits Global.System.Data.Objects.DataClasses.EntityObject
        '''<summary>
        '''Create a new skills object.
        '''</summary>
        '''<param name="typeID">Initial value of typeID.</param>
        '''<param name="typeName">Initial value of typeName.</param>
        '''<param name="description">Initial value of description.</param>
        '''<param name="rank">Initial value of rank.</param>
        '''<param name="primaryAttribute">Initial value of primaryAttribute.</param>
        '''<param name="secondaryAttribute">Initial value of secondaryAttribute.</param>
        '''<param name="cachedUntil">Initial value of cachedUntil.</param>
        Public Shared Function Createskills(ByVal typeID As Long, ByVal typeName As String, ByVal description As String, ByVal rank As Short, ByVal primaryAttribute As String, ByVal secondaryAttribute As String, ByVal cachedUntil As Date) As skills
            Dim skills As skills = New skills
            skills.typeID = typeID
            skills.typeName = typeName
            skills.description = description
            skills.rank = rank
            skills.primaryAttribute = primaryAttribute
            skills.secondaryAttribute = secondaryAttribute
            skills.cachedUntil = cachedUntil
            Return skills
        End Function
        '''<summary>
        '''There are no comments for Property typeID in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property typeID() As Long
            Get
                Return Me._typeID
            End Get
            Set
                Me.OntypeIDChanging(value)
                Me.ReportPropertyChanging("typeID")
                Me._typeID = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("typeID")
                Me.OntypeIDChanged
            End Set
        End Property
        Private _typeID As Long
        Partial Private Sub OntypeIDChanging(ByVal value As Long)
        End Sub
        Partial Private Sub OntypeIDChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property typeName in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property typeName() As String
            Get
                Return Me._typeName
            End Get
            Set
                Me.OntypeNameChanging(value)
                Me.ReportPropertyChanging("typeName")
                Me._typeName = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("typeName")
                Me.OntypeNameChanged
            End Set
        End Property
        Private _typeName As String
        Partial Private Sub OntypeNameChanging(ByVal value As String)
        End Sub
        Partial Private Sub OntypeNameChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property description in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property description() As String
            Get
                Return Me._description
            End Get
            Set
                Me.OndescriptionChanging(value)
                Me.ReportPropertyChanging("description")
                Me._description = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("description")
                Me.OndescriptionChanged
            End Set
        End Property
        Private _description As String
        Partial Private Sub OndescriptionChanging(ByVal value As String)
        End Sub
        Partial Private Sub OndescriptionChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property rank in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property rank() As Short
            Get
                Return Me._rank
            End Get
            Set
                Me.OnrankChanging(value)
                Me.ReportPropertyChanging("rank")
                Me._rank = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("rank")
                Me.OnrankChanged
            End Set
        End Property
        Private _rank As Short
        Partial Private Sub OnrankChanging(ByVal value As Short)
        End Sub
        Partial Private Sub OnrankChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property primaryAttribute in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property primaryAttribute() As String
            Get
                Return Me._primaryAttribute
            End Get
            Set
                Me.OnprimaryAttributeChanging(value)
                Me.ReportPropertyChanging("primaryAttribute")
                Me._primaryAttribute = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("primaryAttribute")
                Me.OnprimaryAttributeChanged
            End Set
        End Property
        Private _primaryAttribute As String
        Partial Private Sub OnprimaryAttributeChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnprimaryAttributeChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property secondaryAttribute in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property secondaryAttribute() As String
            Get
                Return Me._secondaryAttribute
            End Get
            Set
                Me.OnsecondaryAttributeChanging(value)
                Me.ReportPropertyChanging("secondaryAttribute")
                Me._secondaryAttribute = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("secondaryAttribute")
                Me.OnsecondaryAttributeChanged
            End Set
        End Property
        Private _secondaryAttribute As String
        Partial Private Sub OnsecondaryAttributeChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnsecondaryAttributeChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property cachedUntil in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property cachedUntil() As Date
            Get
                Return Me._cachedUntil
            End Get
            Set
                Me.OncachedUntilChanging(value)
                Me.ReportPropertyChanging("cachedUntil")
                Me._cachedUntil = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("cachedUntil")
                Me.OncachedUntilChanged
            End Set
        End Property
        Private _cachedUntil As Date
        Partial Private Sub OncachedUntilChanging(ByVal value As Date)
        End Sub
        Partial Private Sub OncachedUntilChanged()
        End Sub
        '''<summary>
        '''There are no comments for requiredSkills in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("EveDataModel", "requiredSkills_skilss_FK", "requiredSkills"),  _
         Global.System.Xml.Serialization.XmlIgnoreAttribute(),  _
         Global.System.Xml.Serialization.SoapIgnoreAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property requiredSkills() As Global.System.Data.Objects.DataClasses.EntityCollection(Of requiredSkills)
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedCollection(Of requiredSkills)("EveDataModel.requiredSkills_skilss_FK", "requiredSkills")
            End Get
            Set
                If (Not (value) Is Nothing) Then
                    CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.InitializeRelatedCollection(Of requiredSkills)("EveDataModel.requiredSkills_skilss_FK", "requiredSkills", value)
                End If
            End Set
        End Property
        '''<summary>
        '''There are no comments for skillGroups in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("EveDataModel", "SkillGroups_Skills_FK", "skillGroups"),  _
         Global.System.Xml.Serialization.XmlIgnoreAttribute(),  _
         Global.System.Xml.Serialization.SoapIgnoreAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillGroups() As skillGroups
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skillGroups)("EveDataModel.SkillGroups_Skills_FK", "skillGroups").Value
            End Get
            Set
                CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skillGroups)("EveDataModel.SkillGroups_Skills_FK", "skillGroups").Value = value
            End Set
        End Property
        '''<summary>
        '''There are no comments for skillGroups in the schema.
        '''</summary>
        <Global.System.ComponentModel.BrowsableAttribute(false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillGroupsReference() As Global.System.Data.Objects.DataClasses.EntityReference(Of skillGroups)
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skillGroups)("EveDataModel.SkillGroups_Skills_FK", "skillGroups")
            End Get
            Set
                If (Not (value) Is Nothing) Then
                    CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.InitializeRelatedReference(Of skillGroups)("EveDataModel.SkillGroups_Skills_FK", "skillGroups", value)
                End If
            End Set
        End Property
        '''<summary>
        '''There are no comments for skillBonusCollection in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("EveDataModel", "skillBonusCollection_skills_FK", "skillBonusCollection"),  _
         Global.System.Xml.Serialization.XmlIgnoreAttribute(),  _
         Global.System.Xml.Serialization.SoapIgnoreAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillBonusCollection() As Global.System.Data.Objects.DataClasses.EntityCollection(Of skillBonusCollection)
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedCollection(Of skillBonusCollection)("EveDataModel.skillBonusCollection_skills_FK", "skillBonusCollection")
            End Get
            Set
                If (Not (value) Is Nothing) Then
                    CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.InitializeRelatedCollection(Of skillBonusCollection)("EveDataModel.skillBonusCollection_skills_FK", "skillBonusCollection", value)
                End If
            End Set
        End Property
    End Class
    '''<summary>
    '''There are no comments for EveDataModel.skillBonusCollection in the schema.
    '''</summary>
    '''<KeyProperties>
    '''bonusType
    '''skillTypeID
    '''</KeyProperties>
    <Global.System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName:="EveDataModel", Name:="skillBonusCollection"),  _
     Global.System.Runtime.Serialization.DataContractAttribute(IsReference:=true),  _
     Global.System.Serializable()>  _
    Partial Public Class skillBonusCollection
        Inherits Global.System.Data.Objects.DataClasses.EntityObject
        '''<summary>
        '''Create a new skillBonusCollection object.
        '''</summary>
        '''<param name="bonusType">Initial value of bonusType.</param>
        '''<param name="skillTypeID">Initial value of skillTypeID.</param>
        '''<param name="bonusValue">Initial value of bonusValue.</param>
        Public Shared Function CreateskillBonusCollection(ByVal bonusType As String, ByVal skillTypeID As Long, ByVal bonusValue As Integer) As skillBonusCollection
            Dim skillBonusCollection As skillBonusCollection = New skillBonusCollection
            skillBonusCollection.bonusType = bonusType
            skillBonusCollection.skillTypeID = skillTypeID
            skillBonusCollection.bonusValue = bonusValue
            Return skillBonusCollection
        End Function
        '''<summary>
        '''There are no comments for Property bonusType in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property bonusType() As String
            Get
                Return Me._bonusType
            End Get
            Set
                Me.OnbonusTypeChanging(value)
                Me.ReportPropertyChanging("bonusType")
                Me._bonusType = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("bonusType")
                Me.OnbonusTypeChanged
            End Set
        End Property
        Private _bonusType As String
        Partial Private Sub OnbonusTypeChanging(ByVal value As String)
        End Sub
        Partial Private Sub OnbonusTypeChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property skillTypeID in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillTypeID() As Long
            Get
                Return Me._skillTypeID
            End Get
            Set
                Me.OnskillTypeIDChanging(value)
                Me.ReportPropertyChanging("skillTypeID")
                Me._skillTypeID = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("skillTypeID")
                Me.OnskillTypeIDChanged
            End Set
        End Property
        Private _skillTypeID As Long
        Partial Private Sub OnskillTypeIDChanging(ByVal value As Long)
        End Sub
        Partial Private Sub OnskillTypeIDChanged()
        End Sub
        '''<summary>
        '''There are no comments for Property bonusValue in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property bonusValue() As Integer
            Get
                Return Me._bonusValue
            End Get
            Set
                Me.OnbonusValueChanging(value)
                Me.ReportPropertyChanging("bonusValue")
                Me._bonusValue = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("bonusValue")
                Me.OnbonusValueChanged
            End Set
        End Property
        Private _bonusValue As Integer
        Partial Private Sub OnbonusValueChanging(ByVal value As Integer)
        End Sub
        Partial Private Sub OnbonusValueChanged()
        End Sub
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("EveDataModel", "skillBonusCollection_skills_FK", "skills"),  _
         Global.System.Xml.Serialization.XmlIgnoreAttribute(),  _
         Global.System.Xml.Serialization.SoapIgnoreAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skills() As skills
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skills)("EveDataModel.skillBonusCollection_skills_FK", "skills").Value
            End Get
            Set
                CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skills)("EveDataModel.skillBonusCollection_skills_FK", "skills").Value = value
            End Set
        End Property
        '''<summary>
        '''There are no comments for skills in the schema.
        '''</summary>
        <Global.System.ComponentModel.BrowsableAttribute(false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property skillsReference() As Global.System.Data.Objects.DataClasses.EntityReference(Of skills)
            Get
                Return CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.GetRelatedReference(Of skills)("EveDataModel.skillBonusCollection_skills_FK", "skills")
            End Get
            Set
                If (Not (value) Is Nothing) Then
                    CType(Me,Global.System.Data.Objects.DataClasses.IEntityWithRelationships).RelationshipManager.InitializeRelatedReference(Of skills)("EveDataModel.skillBonusCollection_skills_FK", "skills", value)
                End If
            End Set
        End Property
    End Class
End Namespace