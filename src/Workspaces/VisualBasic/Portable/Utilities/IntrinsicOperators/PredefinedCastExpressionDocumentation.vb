﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Threading
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Namespace Microsoft.CodeAnalysis.VisualBasic.Utilities.IntrinsicOperators
    Friend NotInheritable Class PredefinedCastExpressionDocumentation
        Inherits AbstractIntrinsicOperatorDocumentation

        Private ReadOnly resultingType As ITypeSymbol
        Private ReadOnly keywordText As String

        Public Sub New(keywordKind As SyntaxKind, compilation As Compilation)
            resultingType = compilation.GetTypeFromPredefinedCastKeyword(keywordKind)
            keywordText = SyntaxFacts.GetText(keywordKind)
        End Sub

        Public Overrides ReadOnly Property DocumentationText As String
            Get
                Return String.Format(VBWorkspaceResources.ConvertsToDataType, resultingType.ToDisplayString())
            End Get
        End Property

        Public Overrides Function GetParameterDocumentation(index As Integer) As String
            Select Case index
                Case 0
                    Return VBWorkspaceResources.ExpressionToConvert
                Case Else
                    Throw New ArgumentException("index")
            End Select
        End Function

        Public Overrides Function GetParameterName(index As Integer) As String
            Select Case index
                Case 0
                    Return VBWorkspaceResources.Expression1
                Case Else
                    Throw New ArgumentException("index")
            End Select
        End Function

        Public Overrides ReadOnly Property IncludeAsType As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property ParameterCount As Integer
            Get
                Return 1
            End Get
        End Property

        Public Overrides ReadOnly Property PrefixParts As IEnumerable(Of SymbolDisplayPart)
            Get
                Return {New SymbolDisplayPart(SymbolDisplayPartKind.Keyword, Nothing, keywordText),
                        New SymbolDisplayPart(SymbolDisplayPartKind.Punctuation, Nothing, "(")}
            End Get
        End Property

        Public Overrides ReadOnly Property ReturnTypeMetadataName As String
            Get
                Return resultingType.ContainingNamespace.Name + "." + resultingType.MetadataName
            End Get
        End Property
    End Class
End Namespace
