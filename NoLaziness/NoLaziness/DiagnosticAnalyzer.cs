using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace NoLaziness
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NoLazinessAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "NoLaziness";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.VariableDeclarator, SyntaxKind.Parameter, SyntaxKind.FieldDeclaration,
               SyntaxKind.EnumDeclaration, SyntaxKind.EnumMemberDeclaration, SyntaxKind.PropertyDeclaration, SyntaxKind.MethodDeclaration,
               SyntaxKind.ClassDeclaration, SyntaxKind.InterfaceDeclaration);
        }

        private void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var token = context.Node.DescendantTokens().FirstOrDefault(x => x.Kind() == SyntaxKind.IdentifierToken);
            if (token == null) return;
            if (!token.Text.IsItTooShort()) return;

            var diagnostic = Diagnostic.Create(Rule, token.GetLocation(), token.Text);
            context.ReportDiagnostic(diagnostic);
        }
    }

}
