using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Razor;
using Microsoft.CSharp;

namespace MvcNavigation.Specifications.SpecUtils
{
	public sealed class InMemoryRazorEngine
	{
		public static ExecutionResult Execute<TModel>(string razorTemplate, TModel model, dynamic viewBag, params Assembly[] referenceAssemblies)
		{
			var razorEngineHost = new RazorEngineHost(new CSharpRazorCodeLanguage());
			razorEngineHost.DefaultNamespace = "RazorOutput";
			razorEngineHost.DefaultClassName = "Template";
			razorEngineHost.NamespaceImports.Add("System");
			razorEngineHost.DefaultBaseClass = typeof(RazorTemplateBase<TModel>).FullName;

			var razorTemplateEngine = new RazorTemplateEngine(razorEngineHost);

			using (var template = new StringReader(razorTemplate))
			{
				var generatorResult = razorTemplateEngine.GenerateCode(template);

				var compilerParameters = new CompilerParameters();
				compilerParameters.GenerateInMemory = true;
				compilerParameters.ReferencedAssemblies.Add(typeof(InMemoryRazorEngine).Assembly.Location);
				if (referenceAssemblies != null)
				{
					foreach (var referenceAssembly in referenceAssemblies)
					{
						compilerParameters.ReferencedAssemblies.Add(referenceAssembly.Location);
					}
				}

				var codeProvider = new CSharpCodeProvider();
				var compilerResult = codeProvider.CompileAssemblyFromDom(compilerParameters, generatorResult.GeneratedCode);
				if (compilerResult.NativeCompilerReturnValue > 0)
					throw new Exception(compilerResult.Errors[0].ErrorText);

				var compiledTemplateType = compilerResult.CompiledAssembly.GetExportedTypes().Single();
				var compiledTemplate = Activator.CreateInstance(compiledTemplateType);

				var modelProperty = compiledTemplateType.GetProperty("Model");
				modelProperty.SetValue(compiledTemplate, model, null);

				var viewBagProperty = compiledTemplateType.GetProperty("ViewBag");
				viewBagProperty.SetValue(compiledTemplate, viewBag, null);

				var executeMethod = compiledTemplateType.GetMethod("Execute");
				executeMethod.Invoke(compiledTemplate, null);

				var builderProperty = compiledTemplateType.GetProperty("OutputBuilder");
				var outputBuilder = (StringBuilder)builderProperty.GetValue(compiledTemplate, null);
				var runtimeResult = outputBuilder.ToString();

				return new ExecutionResult(generatorResult, compilerResult, runtimeResult);
			}
		}

		#region Nested type: ExecutionResult

		public sealed class ExecutionResult
		{
			public ExecutionResult(GeneratorResults generatorResult, CompilerResults compilerResult, string runtimeResult)
			{
				GeneratorResult = generatorResult;
				CompilerResult = compilerResult;
				RuntimeResult = runtimeResult;
			}

			public GeneratorResults GeneratorResult { get; private set; }
			public CompilerResults CompilerResult { get; private set; }
			public string RuntimeResult { get; private set; }
		}

		#endregion
	}
}