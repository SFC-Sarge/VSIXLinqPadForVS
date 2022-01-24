// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// LINQPad.LanguageServices.RoslynProgram
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LINQPad;
using LINQPad.IO;
using LINQPad.LanguageServices;
using LINQPad.Reflection;
using LINQPad.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;

internal abstract class RoslynProgram : UserProgram
{
	public class RoslynSemanticData : InstanceTrackedObject
	{
		public readonly RoslynProgram Program;

		public readonly SyntaxTree SyntaxTree;

		public readonly SyntaxNode Root;

		public readonly Compilation Compilation;

		public readonly SemanticModel SemanticModel;

		public static async Task<RoslynSemanticData> CreateAsync(RoslynProgram program, CancellationToken cancelToken)
		{
			return new RoslynSemanticData(program, await program.GetSyntaxTreeAsync(cancelToken).CAF(), await program.Project.GetCompilationAsync(cancelToken).CAF());
		}

		public RoslynSemanticData(RoslynProgram program, SyntaxTree mainTree, Compilation compilation)
		{
			Program = program;
			SyntaxTree = mainTree;
			Root = mainTree.GetRoot(default(CancellationToken));
			Compilation = compilation;
			SemanticModel = compilation.GetSemanticModel(mainTree, false);
		}
	}

	private readonly LazyTask<SyntaxTree> _syntaxTreeTask;

	private readonly LazyTask<RoslynSemanticData> _semanticData;

	public readonly AdhocWorkspace Workspace;

	public readonly Document Document;

	public static Func<EditorLanguage, AdhocWorkspace> WorkspaceFactory = delegate
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		using (RuntimeAssemblyResolver.RuntimeLoadContext.EnterContextualReflection())
		{
			return new AdhocWorkspace();
		}
	};

	private static readonly Dictionary<string, Tuple<FileWithVersionInfo, WeakReference<PortableExecutableReference>>> _metadataCache = new Dictionary<string, Tuple<FileWithVersionInfo, WeakReference<PortableExecutableReference>>>(StringComparer.OrdinalIgnoreCase);

	public Project Project => ((TextDocument)Document).get_Project();

	protected abstract string LanguageName { get; }

	public abstract string ImportKeyword { get; }

	public override bool IsSemanticDataReady => _semanticData.HasValue;

	public RoslynSemanticData SemanticDataIfReady => (!IsSemanticDataReady) ? null : _semanticData.GetValue(CancellationToken.None).GetResult();

	protected RoslynProgram(ProgramInput input, RoslynProgram parent = null)
		: base(input)
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		RoslynProgram roslynProgram = this;
		SourceText val = SourceText.From(Input.Source, Encoding.UTF8, (SourceHashAlgorithm)1);
		string text = Path.ChangeExtension(Input.AssemblyPath, SourceFileExtension);
		if (parent == null || parent.Input.Optimize != input.Optimize || parent.Document == null || parent.Workspace == null || !((Workspace)parent.Workspace).IsDocumentOpen(((TextDocument)parent.Document).get_Id()))
		{
			Workspace = CreateWorkspace();
			PortableExecutableReference[] array = input.References.Select((string x) => GetMetadataReference(x, input.RuntimeVersion)).ToArray();
			ProjectId obj = ProjectId.CreateNewId((string)null);
			VersionStamp val2 = VersionStamp.Create();
			string assemblyName = input.AssemblyName;
			string languageName = LanguageName;
			ParseOptions roslynParseOptions = GetRoslynParseOptions(input.RuntimeVersion, interactive: false);
			ProjectInfo val3 = ProjectInfo.Create(obj, val2, "LINQPad", assemblyName, languageName, (string)null, (string)null, GetCompilationOptions(), roslynParseOptions, (IEnumerable<DocumentInfo>)null, (IEnumerable<ProjectReference>)null, (IEnumerable<MetadataReference>)(object)array, (IEnumerable<AnalyzerReference>)null, (IEnumerable<DocumentInfo>)null, false, (Type)null, (string)null);
			Project val4 = Workspace.AddProject(val3);
			LinqReference[] array2 = input.ExtraSources ?? new LinqReference[0];
			foreach (LinqReference linqReference in array2)
			{
				if (linqReference.SourceToExternalize != null)
				{
					val4 = ((TextDocument)val4.AddDocument(linqReference.FilePath, SourceText.From(linqReference.SourceToExternalize, Encoding.UTF8, (SourceHashAlgorithm)1), (IEnumerable<string>)null, (string)null)).get_Project();
				}
			}
			Document val5 = val4.AddDocument(text, val, (IEnumerable<string>)null, text);
			if (!((Workspace)Workspace).TryApplyChanges(((TextDocument)val5).get_Project().get_Solution()) && Log.DevMachine)
			{
				Debugger.Launch();
				Debugger.Break();
			}
			((Workspace)Workspace).OpenDocument(((TextDocument)val5).get_Id(), true);
			Document = ((Workspace)Workspace).get_CurrentSolution().GetDocument(((TextDocument)val5).get_Id());
		}
		else
		{
			Workspace = parent.Workspace;
			DocumentId id = ((TextDocument)parent.Document).get_Id();
			((Workspace)Workspace).CloseDocument(id);
			Solution val6 = ((Workspace)Workspace).get_CurrentSolution().WithDocumentText(id, val, (PreservationMode)0).WithDocumentName(id, text)
				.WithDocumentFilePath(id, text);
			if (!((Workspace)Workspace).TryApplyChanges(val6) && Log.DevMachine)
			{
				Debugger.Launch();
				Debugger.Break();
			}
			((Workspace)Workspace).OpenDocument(id, true);
			Document = ((Workspace)Workspace).get_CurrentSolution().GetDocument(id);
		}
		if (text != ((TextDocument)Document).get_FilePath() && Log.DevMachine)
		{
			Debugger.Launch();
			Debugger.Break();
		}
		_syntaxTreeTask = new LazyTask<SyntaxTree>((CancellationToken ct) => roslynProgram.Document.GetSyntaxTreeAsync(ct));
		_semanticData = new LazyTask<RoslynSemanticData>((CancellationToken ct) => RoslynSemanticData.CreateAsync(roslynProgram, ct));
	}

	protected abstract AdhocWorkspace CreateWorkspace();

	protected abstract ParseOptions GetRoslynParseOptions(string runtimeVersion, bool interactive);

	protected abstract CompilationOptions GetCompilationOptions();

	internal SourceDiagnostic ToSourceDiagnostic(Compilation c, Diagnostic d)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Invalid comparison between Unknown and I4
		FileLinePositionSpan mappedLineSpan = d.get_Location().GetMappedLineSpan();
		string path = ((FileLinePositionSpan)(ref mappedLineSpan)).get_Path();
		LinePosition startLinePosition = ((FileLinePositionSpan)(ref mappedLineSpan)).get_StartLinePosition();
		int? line = ((LinePosition)(ref startLinePosition)).get_Line();
		startLinePosition = ((FileLinePositionSpan)(ref mappedLineSpan)).get_StartLinePosition();
		int? column = ((LinePosition)(ref startLinePosition)).get_Character();
		TextSpan sourceSpan = d.get_Location().get_SourceSpan();
		ProgramLocation location = new ProgramLocation(path, line, column, ((TextSpan)(ref sourceSpan)).get_Length(), Input.EditorStartLine);
		return new SourceDiagnostic(location, (int)d.get_Severity() == 2 && !d.get_IsWarningAsError(), d.get_Id(), d.get_Id() == "CS1061" || d.get_Id() == "CS0103" || d.get_Id() == "CS0246" || d.get_Id() == "CS0117" || d.get_Id() == "CS0234" || d.get_Id() == "BC30002" || d.get_Id() == "BC30451", d.get_Id() == "CS0067" || d.get_Id() == "CS0219" || d.get_Id() == "CS0168" || d.get_Id() == "CS0169" || d.get_Id() == "CS0649" || d.get_Id() == "CS0414" || d.get_Id() == "CS0105" || d.get_Id() == "CS8321", () => d.get_Id() + " " + d.GetMessage((IFormatProvider)CultureInfo.InvariantCulture), () => d.get_Id() + " " + d.GetMessage((IFormatProvider)CultureInfo.CurrentCulture), () => d.get_Id() + " " + d.GetMessage((IFormatProvider)CultureInfo.CurrentCulture) + " " + location.ToString(), (c != null) ? (from a in c.GetUnreferencedAssemblyIdentities(d)
			select a.get_Name()).ToArray() : null);
	}

	public Task<SyntaxTree> GetSyntaxTreeAsync(CancellationToken cancelToken)
	{
		return _syntaxTreeTask.GetValue(cancelToken);
	}

	public Task<RoslynSemanticData> GetSemanticDataAsync(CancellationToken cancelToken)
	{
		return _semanticData.GetValue(cancelToken);
	}

	public override async Task<SourceDiagnostic[]> ParseAsync(CancellationToken cancelToken)
	{
		RoslynSemanticData semanticData = await GetSemanticDataAsync(cancelToken).CAF();
		return (from d in await Task.Run(() => semanticData.Compilation.GetDiagnostics(cancelToken)).CAF()
			where (int)d.get_Severity() >= 2
			select ToSourceDiagnostic(semanticData.Compilation, d)).ToArray();
	}

	public override async Task<SourceDiagnostic[]> EmitAsync(string outputPath, CancellationToken cancelToken)
	{
		if (Log.DevMachine && outputPath != Input.AssemblyPath)
		{
			Debugger.Launch();
			Debugger.Break();
		}
		Compilation compilation = (await GetSemanticDataAsync(cancelToken).CAF()).Compilation;
		EmitOptions emitOptions = new EmitOptions(false, (DebugInformationFormat)2, (string)null, (string)null, 0, 0uL, false, default(SubsystemVersion), (string)null, false, true, default(ImmutableArray<InstrumentationKind>), (HashAlgorithmName?)null, (Encoding)null, (Encoding)null);
		FileStream dllStream = File.Create(outputPath);
		EmitResult emitResult;
		try
		{
			FileStream pdbStream = File.Create(Path.ChangeExtension(outputPath, ".pdb"));
			try
			{
				FileStream xmlStream = ((!Input.EmitXmlDocFile) ? null : File.Create(Path.ChangeExtension(outputPath, ".xml")));
				try
				{
					emitResult = await Task.Run(() => compilation.Emit((Stream)dllStream, (Stream)pdbStream, (Stream)xmlStream, (Stream)null, (IEnumerable<ResourceDescription>)null, emitOptions, (IMethodSymbol)null, (Stream)null, (IEnumerable<EmbeddedText>)null, (Stream)null, cancelToken)).CAF();
				}
				finally
				{
					if (xmlStream != null)
					{
						((IDisposable)xmlStream).Dispose();
					}
				}
			}
			finally
			{
				if (pdbStream != null)
				{
					((IDisposable)pdbStream).Dispose();
				}
			}
		}
		finally
		{
			if (dllStream != null)
			{
				((IDisposable)dllStream).Dispose();
			}
		}
		if (!emitResult.get_Success())
		{
			FileInfo fi = new FileInfo(outputPath);
			if (fi.Exists)
			{
				fi.Delete();
			}
		}
		return (from d in emitResult.get_Diagnostics()
			where (int)d.get_Severity() >= 2
			select ToSourceDiagnostic(compilation, d) into d
			where !d.IsUnusedSymbol
			select d).ToArray();
	}

	protected internal abstract SyntaxTree CreateSyntaxTree(string source, string runtimeVersion, bool partialProgram, CancellationToken cancelToken);

	protected abstract bool IsKeyword(SyntaxToken token);

	protected abstract bool IsContextualKeyword(SyntaxToken token);

	public override string ToString()
	{
		return "{" + GetType().Name + " Length = " + Input.Source.Length + "}";
	}

	public static PortableExecutableReference GetMetadataReference(string path, string runtimeVersion)
	{
		lock (_metadataCache)
		{
			if (_metadataCache.TryGetValue(path, out var value) && value.Item1.IsCurrent() && value.Item2.TryGetTarget(out var target))
			{
				return target;
			}
			XmlDocProvider doc = XmlDocProvider.Get(path, runtimeVersion);
			_metadataCache[path] = Tuple.Create(new FileWithVersionInfo(path), new WeakReference<PortableExecutableReference>(target = Retry.LocalFileIO(() => CreateMetadataReference(path, doc))));
			return target;
		}
	}

	private static PortableExecutableReference CreateMetadataReference(string path, XmlDocProvider doc)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		PortableExecutableReference val = MetadataReference.CreateFromFile(path, default(MetadataReferenceProperties), (DocumentationProvider)(object)doc);
		if (path.EndsWithOrdinal(".dll", ignoreCase: true) && FastAssemblyReader.HasComTypes(path))
		{
			val = val.WithEmbedInteropTypes(true);
		}
		return val;
	}
}
