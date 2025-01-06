﻿// Copyright (c) 2019 DHGMS Solutions and Contributors. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Dhgms.GripeWithRoslyn.Analyzer.Analyzers.Runtime;
using Dhgms.GripeWithRoslyn.UnitTests.Analyzer.Analyzers.EfCore;
using Microsoft.CodeAnalysis;

namespace Dhgms.GripeWithRoslyn.UnitTests.Analyzer.Analyzers.Runtime
{
    /// <summary>
    /// Unit test for <see cref="UseFileProviderOverloadAnalyzer"/>.
    /// </summary>
    public sealed class UseFileProviderOverloadAnalyzerTests : AbstractAnalyzerTest<UseFileProviderOverloadAnalyzer>
    {
        /// <inheritdoc/>
        protected override ExpectedDiagnosticModel[] GetExpectedDiagnosticLines()
        {
            return
            [
                new ExpectedDiagnosticModel(
                    "Runtime\\Extensions\\FileProviderProof.cs",
                    DiagnosticSeverity.Error,
                    19,
                    12)
            ];
        }
    }
}
