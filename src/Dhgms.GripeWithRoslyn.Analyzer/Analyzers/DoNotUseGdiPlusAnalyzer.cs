﻿// Copyright (c) 2019 DHGMS Solutions and Contributors. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Dhgms.GripeWithRoslyn.Analyzer.CodeCracker.Extensions;
using Microsoft.CodeAnalysis;

namespace Dhgms.GripeWithRoslyn.Analyzer.Analyzers
{
    /// <summary>
    /// Analyzer to ensure GDI+ is not used.
    /// </summary>
    [Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
    public sealed class DoNotUseGdiPlusAnalyzer : BaseInvocationUsingNamespaceAnalyzer
    {
        internal const string Title = "Do not use GDI+.";

        private const string MessageFormat = Title;

        private const string Category = SupportedCategories.Design;

        private const string Description =
            "GDI+ usage needs to be considered as it is not suitable for web development etc.";

        /// <summary>
        /// Initializes a new instance of the <see cref="DoNotUseGdiPlusAnalyzer"/> class.
        /// </summary>
        public DoNotUseGdiPlusAnalyzer()
            : base(
            DiagnosticIdsHelper.DoNotUseGdiPlus,
            Title,
            MessageFormat,
            Category,
            Description,
            DiagnosticSeverity.Warning)
        {
        }

        /// <inheritdoc />
        protected override string Namespace => "global::System.Drawing";
    }
}
