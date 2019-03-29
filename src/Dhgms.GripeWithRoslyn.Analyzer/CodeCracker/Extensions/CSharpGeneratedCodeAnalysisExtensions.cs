﻿using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dhgms.GripeWithRoslyn.Analyzer.CodeCracker.Extensions
{
    public static class CSharpGeneratedCodeAnalysisExtensions
    {
        public static bool IsGenerated(this SyntaxNodeAnalysisContext context) => (context.SemanticModel?.SyntaxTree?.IsGenerated() ?? false) || (context.Node?.IsGenerated() ?? false);

        private static readonly string[] generatedCodeAttributes = new string[] { "DebuggerNonUserCode", "GeneratedCode", "DebuggerNonUserCodeAttribute", "GeneratedCodeAttribute" };

        public static bool IsGenerated(this SyntaxNode node) => node.HasAttributeOnAncestorOrSelf(generatedCodeAttributes);

        public static bool IsGenerated(this SyntaxTreeAnalysisContext context) => context.Tree?.IsGenerated() ?? false;

        public static bool IsGenerated(this SymbolAnalysisContext context)
        {
            if (context.Symbol == null) return false;
            foreach (var syntaxReference in context.Symbol.DeclaringSyntaxReferences)
            {
                if (syntaxReference.SyntaxTree.IsGenerated()) return true;
                var root = syntaxReference.SyntaxTree.GetRoot();
                var node = root?.FindNode(syntaxReference.Span);
                if (node.IsGenerated()) return true;
            }
            return false;
        }

        public static bool IsGenerated(this SyntaxTree tree) => (tree.FilePath?.IsOnGeneratedFile() ?? false) || tree.HasAutoGeneratedComment();

        public static bool HasAutoGeneratedComment(this SyntaxTree tree)
        {
            var root = tree.GetRoot();
            if (root == null) return false;
            var firstToken = root.GetFirstToken();
            SyntaxTriviaList trivia;
            if (firstToken == default(SyntaxToken))
            {
                var token = ((CompilationUnitSyntax)root).EndOfFileToken;
                if (!token.HasLeadingTrivia) return false;
                trivia = token.LeadingTrivia;
            }
            else
            {
                if (!firstToken.HasLeadingTrivia) return false;
                trivia = firstToken.LeadingTrivia;
            }

            var comments = trivia.Where(t => t.IsKind(SyntaxKind.SingleLineCommentTrivia) || t.IsKind(SyntaxKind.MultiLineCommentTrivia));
            return comments.Any(t =>
            {
                var s = t.ToString();
                return s.Contains("<auto-generated") || s.Contains("<autogenerated");
            });
        }
    }
}