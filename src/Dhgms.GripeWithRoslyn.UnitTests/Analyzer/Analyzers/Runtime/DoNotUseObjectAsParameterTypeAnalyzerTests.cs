﻿// Copyright (c) 2019 DHGMS Solutions and Contributors. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Dhgms.GripeWithRoslyn.Analyzer;
using Dhgms.GripeWithRoslyn.Analyzer.Analyzers.Runtime;
using Dhgms.GripeWithRoslyn.UnitTests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;

namespace Dhgms.GripeWithRoslyn.UnitTests.Analyzer.Analyzers.Runtime
{
    /// <summary>
    /// Unit Tests for <see cref="DoNotUseObjectAsParameterTypeAnalyzer"/>.
    /// </summary>
    public sealed class DoNotUseObjectAsParameterTypeAnalyzerTests : CodeFixVerifier
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
        class TypeName
        {
            public void MethodName(object arg)
            {
                var name = SomeEnum.SomeValue.ToString();
            }

            public void MethodName2(System.Object arg)
            {
                var name = SomeEnum.SomeValue.ToString();
            }
        }
    }";
            var expected = new[]
            {
                new DiagnosticResult
                {
                    Id = DiagnosticIdsHelper.DoNotUseObjectAsParameterType,
                    Message = DoNotUseObjectAsParameterTypeAnalyzer.Title,
                    Severity = DiagnosticSeverity.Warning,
                    Locations =
                        [
                            new DiagnosticResultLocation("Test0.cs", 6, 36)
                        ]
                },
                new DiagnosticResult
                {
                    Id = DiagnosticIdsHelper.DoNotUseObjectAsParameterType,
                    Message = DoNotUseObjectAsParameterTypeAnalyzer.Title,
                    Severity = DiagnosticSeverity.Warning,
                    Locations =
                        [
                            new DiagnosticResultLocation("Test0.cs", 11, 37)
                        ]
                }
            };

            VerifyCSharpDiagnostic(
                test,
                expected);
        }

        /// <inheritdoc />
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DoNotUseObjectAsParameterTypeAnalyzer();
        }
    }
}
