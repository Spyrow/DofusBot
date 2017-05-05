using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Generator
{
    public class GeneratorUtility
    {
        public static void GenerateCode(CodeDomProvider codeDomProvider, CodeCompileUnit codeBase, string file)
        {
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.VerbatimOrder = true;
            options.BracingStyle = "C";
            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(file,false), "\t");
            codeDomProvider.GenerateCodeFromCompileUnit(codeBase, tw, options);
            tw.Close();
        }
    }
}
