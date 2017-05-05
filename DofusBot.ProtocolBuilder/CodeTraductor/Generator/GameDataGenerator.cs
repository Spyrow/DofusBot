
using DofusBot.ProtocolBuilder.CodeTraductor.Template;
using DofusBot.ProtocolBuilder.CodeTraductor.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using Microsoft.CSharp;
using DofusBot.ProtocolBuilder.CodeTraductor.BulkGenerator;
using System.IO;
using DofusBot.ProtocolBuilder.CodeTraductor.Parsing;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Generator
{
    public class GameDataGenerator
    {
        private GameDataClass _class;
        private CodeCompileUnit _codeBase;
        private Dictionary<string, Type> _primitiveType;
    

        public GameDataGenerator(GameDataClass classToGenerate, string folderPath)
        {
            this._class = classToGenerate;
            InitializePrimitiveType();
            Generate();
            GeneratorUtility.GenerateCode(new CSharpCodeProvider(), _codeBase, folderPath + "\\" + _class.ClassName + ".cs");
        }

        private void InitializePrimitiveType()
        { 
            _primitiveType = new Dictionary<string, Type>();
            _primitiveType.Add("Byte", typeof(byte));
            _primitiveType.Add("Int", typeof(int));
            _primitiveType.Add("Short", typeof(short));
            _primitiveType.Add("UTF", typeof(string));
            _primitiveType.Add("String", typeof(string));
            _primitiveType.Add("UnsignedShort", typeof(ushort));
            _primitiveType.Add("UnsignedByte", typeof(byte));
            _primitiveType.Add("Boolean", typeof(bool));
            _primitiveType.Add("Double", typeof(double));
            _primitiveType.Add("int", typeof(int));
            _primitiveType.Add("uint", typeof(uint));
            _primitiveType.Add("UnsignedInt", typeof(int));
            _primitiveType.Add("Float", typeof(double));
            _primitiveType.Add("Number", typeof(double));
        }

        private void Generate()
        {
            _codeBase = new CodeCompileUnit();

            CodeNamespace Namespace = new CodeNamespace(_class.Namespace);

            CodeTypeDeclaration Class = new CodeTypeDeclaration(_class.ClassName);
           //   if (_class.Interface != null)
          //      Class.BaseTypes.Add(_class.Interface);

        
            Class.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable"));
            Class.BaseTypes.Add("IData");
            Namespace.Types.Add(Class);
            _codeBase.Namespaces.Add(Namespace);
            _codeBase.Namespaces[0].Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            _codeBase.Namespaces[0].Imports.Add(new CodeNamespaceImport("DofusBot.Protocol"));

            foreach (string import in _class.Import)
            {
                System.CodeDom.CodeNamespaceImport Import = new System.CodeDom.CodeNamespaceImport(import);
                _codeBase.Namespaces[0].Imports.Add(Import);
            }

            foreach (GameDataVariable var in _class.Variables)
            {
                AddClassVariableToCodeBase(var);
            }

           

            if (_class.Parent != "" && _class.Parent != null)
            {
                Class.BaseTypes.Insert(0, new CodeTypeReference(_class.Parent));
            }

            AddSampleConstructor();
           // AddConstructor();
        }

        private void AddImplementedConstructor()
        {
            throw new NotImplementedException();
        }
        private void AddClassVariableToCodeBase(GameDataVariable classVariable)
        {
            if (classVariable.VariableType.Contains("List"))
            {

                CodeMemberProperty publicProperty = new CodeMemberProperty();
                publicProperty.Attributes = MemberAttributes.Public;
                publicProperty.HasGet = true;
                publicProperty.HasSet = true;
                publicProperty.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(classVariable.PrivateVariableName), new CodeVariableReferenceExpression("value")));
                publicProperty.Name = classVariable.PublicVariableName;
                publicProperty.Type = new CodeTypeReference(classVariable.VariableType);
                CodeExpression getter = new CodeSnippetExpression(string.Format("return {0}", classVariable.PrivateVariableName));
                publicProperty.GetStatements.Add(getter);
                _codeBase.Namespaces[0].Types[0].Members.Add(publicProperty);

                CodeMemberField var = new CodeMemberField(classVariable.VariableType, classVariable.PrivateVariableName);
                var.Attributes = MemberAttributes.Private;
                var.InitExpression = new CodeSnippetExpression("new " + classVariable.VariableType + "()");
                _codeBase.Namespaces[0].Types[0].Members.Add(var);
            }
            else if (classVariable.VariableType.Contains("I18n"))
            {
                CodeMemberProperty publicProperty = new CodeMemberProperty();
                publicProperty.Attributes = MemberAttributes.Public;
                publicProperty.HasGet = true;
                publicProperty.HasSet = true;
                publicProperty.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(classVariable.PrivateVariableName), new CodeVariableReferenceExpression("value")));
                publicProperty.Name = classVariable.PublicVariableName;
                publicProperty.Type = new CodeTypeReference(typeof(string));
                CodeExpression getter = new CodeSnippetExpression(string.Format("return {0}", classVariable.PrivateVariableName + ".GetText()"));
                publicProperty.GetStatements.Add(getter);
                _codeBase.Namespaces[0].Types[0].Members.Add(publicProperty);

                CodeMemberField privateVar = new CodeMemberField("I18nProperty", classVariable.PrivateVariableName);
                privateVar.Attributes = MemberAttributes.Private;
                _codeBase.Namespaces[0].Types[0].Members.Add(privateVar);
            }
            else if (_primitiveType.ContainsKey(classVariable.VariableType))
            {
                Type Type = _primitiveType[classVariable.VariableType];
                CodeMemberProperty publicProperty = new CodeMemberProperty();
                publicProperty.Attributes = MemberAttributes.Public;
                publicProperty.HasGet = true;
                publicProperty.HasSet = true;
                publicProperty.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(classVariable.PrivateVariableName), new CodeVariableReferenceExpression("value")));
                publicProperty.Name = classVariable.PublicVariableName;
                publicProperty.Type = new CodeTypeReference(Type);
                CodeExpression getter = new CodeSnippetExpression(string.Format("return {0}", classVariable.PrivateVariableName));
                publicProperty.GetStatements.Add(getter);
                _codeBase.Namespaces[0].Types[0].Members.Add(publicProperty);

                CodeMemberField privateVar = new CodeMemberField(Type, classVariable.PrivateVariableName);
                privateVar.Attributes = MemberAttributes.Private;
                _codeBase.Namespaces[0].Types[0].Members.Add(privateVar);
            }
            else
            {
                CodeMemberProperty publicProperty = new CodeMemberProperty();
                publicProperty.Attributes = MemberAttributes.Public;
                publicProperty.HasGet = true;
                publicProperty.HasSet = true;
                publicProperty.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(classVariable.PrivateVariableName), new CodeVariableReferenceExpression("value")));
                publicProperty.Name = classVariable.PublicVariableName;
                publicProperty.Type = new CodeTypeReference(classVariable.VariableType.Replace(";", ""));
                CodeExpression getter = new CodeSnippetExpression(string.Format("return {0}", classVariable.PrivateVariableName));
                publicProperty.GetStatements.Add(getter);
                _codeBase.Namespaces[0].Types[0].Members.Add(publicProperty);

                CodeMemberField var = new CodeMemberField(classVariable.VariableType.Replace(";", ""), classVariable.PrivateVariableName);
                _codeBase.Namespaces[0].Types[0].Members.Add(var);
            }
        }

        private void AddSampleConstructor()
        {
            CodeConstructor ctor = new CodeConstructor();
            ctor.Name = _class.ClassName;
            ctor.Attributes = MemberAttributes.Public;
            _codeBase.Namespaces[0].Types[0].Members.Add(ctor);
        }

        private CodeParameterDeclarationExpressionCollection GenerateParametters(List<GameDataVariable> variables)
        {
            CodeParameterDeclarationExpressionCollection retVal = new CodeParameterDeclarationExpressionCollection();
            retVal.Add(new CodeParameterDeclarationExpression(typeof(string), "I18nFile"));
            foreach(var v in variables)
            {
                retVal.Add(GetParametter(v));
            }
            return retVal;
        }

        private CodeParameterDeclarationExpression GetParametter(GameDataVariable v)
        {
            if (v.IsVector)
                return new CodeParameterDeclarationExpression("List<object>", v.VariableName);
            else if (PrimitiveType.singleton.ContainsKey(v.VariableType))
                return new CodeParameterDeclarationExpression(_primitiveType[v.VariableType], v.VariableName);
            else
                return new CodeParameterDeclarationExpression(v.VariableType, v.VariableName);
        }

  
       
    }
}
