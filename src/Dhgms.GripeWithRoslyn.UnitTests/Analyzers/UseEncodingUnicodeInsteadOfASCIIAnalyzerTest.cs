﻿// Copyright (c) 2019 DHGMS Solutions and Contributors. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Dhgms.GripeWithRoslyn.Analyzer;
using Dhgms.GripeWithRoslyn.Analyzer.Analyzers;
using Dhgms.GripeWithRoslyn.UnitTests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;
using CodeFixVerifier = Dhgms.GripeWithRoslyn.UnitTests.Verifiers.CodeFixVerifier;

namespace Dhgms.GripeWithRoslyn.UnitTests.Analyzers
{
    /// <summary>
    /// Unit Tests for <see cref="UseEncodingUnicodeInsteadOfASCIIAnalyzer"/>.
    /// </summary>
    public class UseEncodingUnicodeInsteadOfASCIIAnalyzerTest : CodeFixVerifier
    {
        /// <summary>
        /// Test to ensure bad code returns a warning.
        /// </summary>
        [Fact]
        public void ReturnsWarning()
        {
            var test = @"
    namespace ConsoleApplication1
    {
        using System.Text;

        class TypeName
        {
            public void MethodName()
            {
                char[] testValue = null;
                ASCIIEncoding.GetBytes(testValue, 0, 0);
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = DiagnosticIdsHelper.UseEncodingUnicodeInsteadOfAscii,
                Message = "Consider usage of Unicode Encoding instead of ASCII.",
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 11, 17)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        /// <inheritdoc />
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new UseEncodingUnicodeInsteadOfASCIIAnalyzer();
        }
    }
}