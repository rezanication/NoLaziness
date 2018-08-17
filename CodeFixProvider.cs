using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;

namespace NoLaziness
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(NoLazinessCodeFixProvider)), Shared]
    public class NoLazinessCodeFixProvider : CodeFixProvider
    {
        private const string title = "Choose a meaningful name.";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(NoLazinessAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {

        }
    }
}