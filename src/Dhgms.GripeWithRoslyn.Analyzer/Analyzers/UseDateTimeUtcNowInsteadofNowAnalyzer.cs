﻿using Dhgms.GripeWithRoslyn.Analyzer.CodeCracker.Extensions;
using Microsoft.CodeAnalysis;

namespace Dhgms.GripeWithRoslyn.Analyzer.Analyzers
{
    /// <summary>
    /// Analyzer for usages for System.DateTime.Now
    /// </summary>
    [Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
    public sealed class UseDateTimeUtcNowInsteadofNowAnalyzer : BaseSimpleMemberAccessOnTypeAnalyzer
    {
        internal const string Title = "Consider usage of DateTime.UtcNow instead of DateTime.Now.";

        private const string MessageFormat = Title;

        private const string Category = SupportedCategories.Reliability;

        private const string Description =
            "DateTime.Now may cause issues in timezone \\ daylight saving time sensitive scenarios. Elect for DateTime.UtcNow and convert on the front end UI as required.";

        /// <summary>
        /// Creates an instance of UseDateTimeUtcNowInsteadofNowAnalyzer
        /// </summary>
        public UseDateTimeUtcNowInsteadofNowAnalyzer() : base(
            DiagnosticIdsHelper.UseDateTimeUtcNowInsteadofNow,
            Title,
            MessageFormat,
            Category,
            Description,
            DiagnosticSeverity.Warning)
        {
        }

        /// <inhertitdoc />
        protected override string ClassName => "global::System.DateTime";

        /// <inhertitdoc />
        protected override string MemberName => "Now";
    }
}
