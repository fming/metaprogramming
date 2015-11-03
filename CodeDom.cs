using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace helloworldCodeDom1
{
    partial class Program
    {
        static void Main(string[] args)
        {
            CodeNamespace prgNamespace = BuildProgram();
            var compilerOptions = new CodeGeneratorOptions()
            {
                IndentString = " ",
                BracingStyle = "C",
                BlankLinesBetweenMembers=false
            
            };

            var codeText = new StringBuilder();
            using(var codeWriter = new StringWriter(codeText))
            {
                CodeDomProvider.CreateProvider("C#")
                    .GenerateCodeFromNamespace(prgNamespace, codeWriter, compilerOptions);
            }

            var script = codeText.ToString();
            Console.WriteLine(script);
            Console.ReadLine();
        }
    }

    partial class Program
    {
        static CodeNamespace BuildProgram()
        {
            var ns = new CodeNamespace("MetaWorld");
            var systemImport = new CodeNamespaceImport("System");
            ns.Imports.Add(systemImport);
            var programClass = new CodeTypeDeclaration("Program");
            ns.Types.Add(programClass);
            var methodMain = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Static,
                Name = "Main"
            };

            methodMain.Statements.Add
                (
                new CodeMethodInvokeExpression(
                    new CodeSnippetExpression("Console"),
                    "WriteLine",
                    new CodePrimitiveExpression("Hello, world!")
                    ));
            programClass.Members.Add(methodMain);
            return ns;
        }
    }
}
