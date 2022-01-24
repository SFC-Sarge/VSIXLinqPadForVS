// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// LINQPad.LanguageServices.CSharpProgram
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using LINQPad;
using LINQPad.LanguageServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

internal class CSharpProgram : RoslynProgram
{
	public const string UserCodePlaceholderComment = "// You can define other methods, fields, classes and namespaces here";

	private static readonly string[] PreProcSymbols = new string[3] { "LINQPAD", "NETCORE", "DEBUG" };

	public static readonly string[] Keywords = "abstract as base bool break byte case catch char checked class const continue decimal default delegate do double else enum event explicit extern false finally fixed float for foreach goto if implicit in int interface internal is lock long namespace new null object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static string struct switch this throw true try typeof uint ulong unchecked unsafe ushort using virtual void volatile while".Split().ToArray();

	public static readonly string[] ContextualKeywords = "add and alias ascending async await descending dynamic equals from get global group into join let on or nameof nint not nuint orderby partial record remove select set value var where with yield".Split().ToArray();

	public static int CompilerVersion => 10;

	protected override string LanguageName => "C#";

	public override string SourceFileExtension => ".cs";

	public override string ImportKeyword => "using";

	public CSharpProgram(ProgramInput input, CSharpProgram parent = null)
		: base(input, parent)
	{
	}

	protected override AdhocWorkspace CreateWorkspace()
	{
		return RoslynProgram.WorkspaceFactory(EditorLanguage.CSharp);
	}

	public static CSharpParseOptions GetParseOptions(string runtimeVersion, bool interactive)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		return new CSharpParseOptions((LanguageVersion)((RoslynEdge.IsActive() || UserOptionsLive.Instance.UseLatestRoslynFeatures) ? 2147483646 : int.MaxValue), (DocumentationMode)1, (SourceCodeKind)(interactive ? 1 : 0), GetPreProcSymbols(runtimeVersion));
	}

	public static IEnumerable<string> GetPreProcSymbols(string runtimeVersion, bool isAutomated = false)
	{
		IEnumerable<string> enumerable = PreProcSymbols.AsEnumerable();
		if (string.IsNullOrEmpty(runtimeVersion))
		{
			runtimeVersion = FrameworkResolver.Default.NetCoreVersion;
		}
		if (runtimeVersion.StartsWith("6."))
		{
			enumerable = LinqExtensions.Append(LinqExtensions.Append(enumerable, "NET5"), "NET6");
		}
		else if (runtimeVersion.StartsWith("5."))
		{
			enumerable = LinqExtensions.Append(enumerable, "NET5");
		}
		return isAutomated ? LinqExtensions.Append(enumerable, "CMD") : enumerable;
	}

	protected override ParseOptions GetRoslynParseOptions(string runtimeVersion, bool interactive)
	{
		return (ParseOptions)(object)GetParseOptions(runtimeVersion, interactive);
	}

	protected internal override SyntaxTree CreateSyntaxTree(string source, string runtimeVersion, bool partialProgram, CancellationToken cancelToken)
	{
		return CSharpSyntaxTree.ParseText(source, GetParseOptions(runtimeVersion, partialProgram), Path.ChangeExtension(Input.AssemblyPath, SourceFileExtension), Encoding.UTF8, cancelToken);
	}

	protected override CompilationOptions GetCompilationOptions()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		int num = ((!Input.IsExe) ? 2 : 0);
		int num2 = (Input.Optimize ? 1 : 0);
		Platform val = (Platform)((Input.X86 && Input.IsExe) ? 4 : 0);
		return (CompilationOptions)(object)new CSharpCompilationOptions((OutputKind)num, false, (string)null, (string)null, (string)null, (IEnumerable<string>)null, (OptimizationLevel)num2, false, true, (string)null, (string)null, default(ImmutableArray<byte>), (bool?)null, val, (ReportDiagnostic)0, 4, (IEnumerable<KeyValuePair<string, ReportDiagnostic>>)null, true, false, (XmlReferenceResolver)null, (SourceReferenceResolver)null, (MetadataReferenceResolver)null, (AssemblyIdentityComparer)null, (StrongNameProvider)null, false, (MetadataImportOptions)0, (NullableContextOptions)0).WithAssemblyIdentityComparer((AssemblyIdentityComparer)(object)DesktopAssemblyIdentityComparer.get_Default());
	}

	protected override bool IsKeyword(SyntaxToken token)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return CSharpExtensions.IsKeyword(token);
	}

	protected override bool IsContextualKeyword(SyntaxToken token)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return CSharpExtensions.IsContextualKeyword(token);
	}

	public override string[] GetKeywords()
	{
		return Keywords;
	}

	public override string[] GetContextualKeywords()
	{
		return ContextualKeywords;
	}
}
