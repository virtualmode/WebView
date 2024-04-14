using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Samples.Generator
{
    [Generator]
#pragma warning disable RS1036 // Specify analyzer banned API enforcement setting.
    public class DemoSourceGenerator : ISourceGenerator
#pragma warning restore RS1036 // Specify analyzer banned API enforcement setting.
    {
        public void Execute(GeneratorExecutionContext context)
        {
            Debug.WriteLine("Execute code generator");
            // Begin creating the source we'll inject into the users compilation.
            StringBuilder sourceBuilder = new StringBuilder($@"
using System;
using System.Collections.Generic;
using System.Linq;
namespace Samples.Generator
{{
    public static class DemoGenerated
    {{
        public static string GetRazor(string name)
        {{

");
            var files = context.AdditionalFiles;
            var dictionary = files.ToDictionary(x => x.Path, x => x.GetText().ToString().Replace(@"""", @""""""));
            sourceBuilder.AppendLine("var metadata = new Dictionary<string,string>() {");
            foreach (var pair in dictionary)
            {
                sourceBuilder.AppendLine($@"{{ @""{pair.Key}"", @""{pair.Value}"" }},");
            }
            sourceBuilder.AppendLine("};");

            sourceBuilder.AppendLine($@"var foundPair = metadata.FirstOrDefault(x => x.Key.EndsWith(""\\"" + name + "".razor""));");

            sourceBuilder.AppendLine(@"return foundPair.Value;");

            // Finish creating the source to inject.
            sourceBuilder.Append(@"
        }
    }
}");
            // Inject the created source into the users compilation.
            context.AddSource("demoGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            //if (!Debugger.IsAttached)
            //{
            //    Debugger.Launch();
            //}
#endif
        }
    }
}
