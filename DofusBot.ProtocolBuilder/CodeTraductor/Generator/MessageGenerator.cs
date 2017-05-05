using DofusBot.ProtocolBuilder.CodeTraductor.Enums;
using DofusBot.ProtocolBuilder.CodeTraductor.Template;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Generator
{
    public class MessageGenerator
    {
        #region Declarations

        private Class classToGenerate;
        private CodeCompileUnit classGenerated;

        #endregion

        #region Constructeurs

        public MessageGenerator(Class classGenerate, string outputFileDirectory)
        {
            classToGenerate = classGenerate;
            Generate();
            GeneratorUtility.GenerateCode(new CSharpCodeProvider(), classGenerated, (outputFileDirectory + "\\" + classToGenerate.Name + ".cs"));
        }

        #endregion

        #region Generations methode

        #region Generation Method

        private void Generate()
        {
            classGenerated = new CodeCompileUnit();
            CodeNamespace nameSpace = new CodeNamespace(classToGenerate.NameSpace);
            nameSpace.Imports.AddRange(GenerateImports());
            CodeTypeDeclaration Class = new CodeTypeDeclaration(classToGenerate.Name);
            Class.IsClass = true;
            if (classToGenerate.Parent != string.Empty)
                Class.BaseTypes.Add(classToGenerate.Parent);
            if (classToGenerate.Interface != string.Empty)
                Class.BaseTypes.Add(classToGenerate.Interface);
            Class.Members.AddRange(GenerateProperity());       
            Class.Members.Add(GenerateInitMethod());
            if (classToGenerate.Variables.Length > 0)
                Class.Members.Add(GenerateEmptyConstructor());
            Class.Members.Add(GenerateSerializeMethod("IDataWriter"));
            Class.Members.Add(GenerateDeserializeMethod("IDataReader"));
            nameSpace.Types.Add(Class);
            classGenerated.Namespaces.Add(nameSpace);
        }

        #region Imports

        private CodeNamespaceImport[] GenerateImports()
        {
            List<CodeNamespaceImport> retVal = new List<CodeNamespaceImport>();
            foreach (string import in classToGenerate.Imports)
                retVal.Add(new CodeNamespaceImport(import));
            return retVal.ToArray();
        }

        #endregion

        #region Serialize method

        private CodeMemberMethod GenerateSerializeMethod(string writerType)
        {
            CodeMemberMethod retVal = new CodeMemberMethod();
            retVal.Name = "Serialize";
            retVal.ReturnType = new CodeTypeReference(typeof(void));
            retVal.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            if (classToGenerate.Parent != "NetworkMessage" && classToGenerate.Parent != "NetworkType")
                retVal.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeSnippetExpression("base"), "Serialize", new CodeExpression[] { new CodeVariableReferenceExpression("writer") })));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(writerType), "writer"));
            retVal.Statements.AddRange(GenerateSerializeStatements());
            return retVal;
        }

        private CodeStatement[] GenerateSerializeStatements()
        {
            List<CodeStatement> retVal = new List<CodeStatement>();
            int boolcount = 0;
            foreach (Variable var in classToGenerate.Variables)
            {
                if (var.MethodeType == ReadMethodeType.BooleanByteWraper)
                {
                    if (boolcount == 0)
                        retVal.Add(new CodeVariableDeclarationStatement(typeof(byte), "flag", new CodeObjectCreateExpression(typeof(byte), new CodeExpression[] { })));
                    boolcount += 1;
                }
            }
            foreach (Variable var in classToGenerate.Variables)
            {
                switch (var.TypeOfVar)
                {
                    case VarType.Object:
                        retVal.AddRange(GenerateSerializeObjectStatements(var));
                        break;
                    case VarType.Primitive :
                        retVal.AddRange(GenerateSerializePrimitiveStatements(var));
                        break;
                    case VarType.Vector:
                        retVal.AddRange(GenerateSerializeVectorStatements(var));
                        break;
                }
                if (var.MethodeType== ReadMethodeType.BooleanByteWraper)
                    if (Convert.ToUInt32(var.ReadMethode) == boolcount-1)
                        retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), "WriteByte", new CodeExpression[] { new CodeVariableReferenceExpression("flag") })));
            }
            return retVal.ToArray();
        }

        private CodeStatement[] GenerateSerializeObjectStatements(Variable var)
        {
            List<CodeStatement> retVal = new List<CodeStatement>();
            switch (var.MethodeType)
            {
                case ReadMethodeType.ProtocolTypeManager:
                    retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), "WriteUShort", new CodeExpression[] { new CodeCastExpression(typeof(ushort), new CodeVariableReferenceExpression("m_" + var.Name + ".ProtocolId")) })));
                    break;
                case ReadMethodeType.SerializeOrDeserialize:
                    break;
            }
            retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("m_" + var.Name), "Serialize", new CodeExpression[] { new CodeVariableReferenceExpression("writer") })));
            return retVal.ToArray();
            }

        private CodeStatement[] GenerateSerializePrimitiveStatements(Variable var)
        {
            List<CodeStatement> retVal = new List<CodeStatement>();
            switch (var.MethodeType)
            {
                case ReadMethodeType.Primitive :
                    retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), var.WriteMethode, new CodeExpression[] { new CodeVariableReferenceExpression("m_" + var.Name) })));
                    break;
                case ReadMethodeType.BooleanByteWraper :
                    retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("BooleanByteWrapper"), "SetFlag", new CodeExpression[] { new CodePrimitiveExpression(Convert.ToInt32(var.ReadMethode)), new CodeVariableReferenceExpression("flag"),new CodeVariableReferenceExpression("m_"+var.Name) })));
                    break;
            }
            return retVal.ToArray();
        }

        private CodeStatement[] GenerateSerializeVectorStatements(Variable var)
        {
            List<CodeStatement> retVal = new List<CodeStatement>();
            retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), "WriteShort", new CodeExpression[] { new CodeCastExpression(typeof(short), new CodeVariableReferenceExpression("m_" + var.Name + ".Count")) })));
            retVal.Add(new CodeVariableDeclarationStatement(typeof(int), var.Name + "Index"));
            CodeIterationStatement forStatement = GenerateForStatement(new CodeVariableReferenceExpression("m_" + var.Name + ".Count"), new CodeVariableReferenceExpression(var.Name + "Index"));
            switch (var.MethodeType)
            {
                case ReadMethodeType.ProtocolTypeManager:
                    forStatement.Statements.Add(new CodeVariableDeclarationStatement(var.ObjectType, "objectToSend", new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("m_" + var.Name), new CodeExpression[] { new CodeVariableReferenceExpression(var.Name + "Index") })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), "WriteUShort", new CodeExpression[] { new CodeCastExpression(typeof(ushort), new CodeVariableReferenceExpression("objectToSend.ProtocolId")) })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("objectToSend"), "Serialize", new CodeExpression[] { new CodeVariableReferenceExpression("writer") })));
                    break;
                case ReadMethodeType.SerializeOrDeserialize:
                    forStatement.Statements.Add(new CodeVariableDeclarationStatement(var.ObjectType, "objectToSend", new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("m_" + var.Name), new CodeExpression[] { new CodeVariableReferenceExpression(var.Name + "Index") })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("objectToSend"), "Serialize", new CodeExpression[] { new CodeVariableReferenceExpression("writer") })));
                    break;
                case ReadMethodeType.Primitive:
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), var.WriteMethode, new CodeExpression[] { new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("m_" + var.Name), new CodeExpression[] { new CodeVariableReferenceExpression(var.Name + "Index") }) })));
                    break;
            }
            retVal.Add(forStatement);
            return retVal.ToArray();
        }

        #endregion

        #region Deserialize method

        private CodeMemberMethod GenerateDeserializeMethod(string readerType)
        {
            CodeMemberMethod retVal = new CodeMemberMethod();
            retVal.Name = "Deserialize";
            retVal.ReturnType = new CodeTypeReference(typeof(void));     
            retVal.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            if (classToGenerate.Parent != "NetworkMessage" && classToGenerate.Parent != "NetworkType")
                retVal.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeSnippetExpression("base"), "Deserialize", new CodeExpression[] { new CodeVariableReferenceExpression("reader") })));
            retVal.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(readerType), "reader"));
            retVal.Statements.AddRange(GenerateDeserializeStatements());
            return retVal;
        }

        private CodeStatement[] GenerateDeserializeStatements()
        {
            List<CodeStatement> retVal = new List<CodeStatement>();

            bool boolFinded = false;

            foreach (Variable var in classToGenerate.Variables)
            {
                if (var.MethodeType == ReadMethodeType.BooleanByteWraper && !boolFinded)
                {
                    boolFinded = true;
                    retVal.Add(new CodeVariableDeclarationStatement(typeof(byte), "flag", new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "ReadByte", new CodeExpression[] { })));
                }
            }

            int boolCount = 0;

            foreach (Variable var in classToGenerate.Variables)
            {
                switch (var.TypeOfVar)
                {
                    case VarType.Object:
                        retVal.AddRange(GenerateDeserializeObjectStatements(var));
                        continue;
                    case VarType.Primitive:
                        retVal.AddRange(GenerateDeserializePrimitiveStatements(var));
                        if (var.MethodeType == ReadMethodeType.BooleanByteWraper)
                        {
                            boolCount += 1;
                        }
                       if (boolCount == 8)
                       {
                           retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("flag"), new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "ReadByte", new CodeExpression[] { })));
                           boolCount = 0;
                       }
                        continue;
                    case VarType.Vector:
                        retVal.AddRange(GenerateDeserializeVectorStatements(var));
                        continue;
                }
            }
            return retVal.ToArray();
        }

        private CodeStatement[] GenerateDeserializeObjectStatements(Variable var)
        { 
            List<CodeStatement> retVal = new List<CodeStatement>();
            switch (var.MethodeType)
            {
                case ReadMethodeType.ProtocolTypeManager:
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("ProtocolManager"), "GetTypeInstance", new CodeTypeReference(var.ObjectType)), new CodeExpression[] { new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "ReadUShort", new CodeExpression[] { }) })));
                    break;
                case ReadMethodeType.SerializeOrDeserialize:
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeObjectCreateExpression(var.ObjectType, new CodeExpression[] { })));
                    break;
            }
            retVal.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("m_" + var.Name), "Deserialize", new CodeExpression[] { new CodeVariableReferenceExpression("reader") })));
            return retVal.ToArray();
        }

        private CodeStatement[] GenerateDeserializePrimitiveStatements(Variable var)
        {
            List<CodeStatement> retVal = new List<CodeStatement>();
            switch (var.MethodeType)
            {
                case ReadMethodeType.Primitive:
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), var.ReadMethode, new CodeExpression[] { })));
                    break;
                case ReadMethodeType.BooleanByteWraper:
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("BooleanByteWrapper"), "GetFlag", new CodeExpression[] { new CodeVariableReferenceExpression("flag"),new CodePrimitiveExpression(Convert.ToInt32(var.ReadMethode))})));
                    break;
            }
            return retVal.ToArray();
        }

        private CodeStatement[] GenerateDeserializeVectorStatements(Variable var)
        {
            List<CodeStatement> retVal = new List<CodeStatement>();
            retVal.Add(new CodeVariableDeclarationStatement(typeof(int), var.Name + "Count", new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "ReadUShort", new CodeExpression[] { })));
            retVal.Add(new CodeVariableDeclarationStatement(typeof(int), var.Name + "Index"));
            CodeIterationStatement forStatement = GenerateForStatement(new CodeVariableReferenceExpression(var.Name + "Count"), new CodeVariableReferenceExpression(var.Name + "Index"));
            var listType = new CodeTypeReference(typeof(List<>));
            switch (var.MethodeType)
            {
                case ReadMethodeType.ProtocolTypeManager:
                    listType.TypeArguments.Add(new CodeTypeReference(var.ObjectType));
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeObjectCreateExpression(listType, new CodeExpression[] { })));
                    forStatement.Statements.Add(new CodeVariableDeclarationStatement(var.ObjectType, "objectToAdd", new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("ProtocolManager"), "GetTypeInstance", new CodeTypeReference(var.ObjectType)), new CodeExpression[] { new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "ReadUShort", new CodeExpression[] { }) })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("objectToAdd"), "Deserialize", new CodeExpression[] { new CodeVariableReferenceExpression("reader") })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("m_" + var.Name), "Add", new CodeExpression[] { new CodeVariableReferenceExpression("objectToAdd") })));
                    break;
                case ReadMethodeType.SerializeOrDeserialize:
                    listType.TypeArguments.Add(new CodeTypeReference(var.ObjectType));
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeObjectCreateExpression(listType, new CodeExpression[] { })));
                    forStatement.Statements.Add(new CodeVariableDeclarationStatement(var.ObjectType, "objectToAdd", new CodeObjectCreateExpression(new CodeTypeReference(var.ObjectType), new CodeExpression[] { })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("objectToAdd"), "Deserialize", new CodeExpression[] { new CodeVariableReferenceExpression("reader") })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("m_" + var.Name), "Add", new CodeExpression[] { new CodeVariableReferenceExpression("objectToAdd") })));
                    break;
                case ReadMethodeType.Primitive:
                    listType.TypeArguments.Add(var.Type);
                    retVal.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + var.Name), new CodeObjectCreateExpression(listType, new CodeExpression[] { })));
                    forStatement.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("m_" + var.Name), "Add", new CodeExpression[] { new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), var.ReadMethode, new CodeExpression[] { }) })));
                    break;
            }
            retVal.Add(forStatement);
            return retVal.ToArray();
        }

        #endregion

        #region Init method

        private CodeConstructor GenerateEmptyConstructor()
        {
           CodeConstructor constructor = new CodeConstructor();
           constructor.Name = classToGenerate.Name;
           constructor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
           return constructor;
        }

        private CodeConstructor GenerateInitMethod()
        {
            CodeConstructor retVal = new CodeConstructor();
            retVal.Name = classToGenerate.Name;
            retVal.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            //retVal.ReturnType = new CodeTypeReference(typeof(void));
            foreach (Variable v in classToGenerate.Variables)
            {
                switch (v.TypeOfVar)
                {
                    case VarType.Object:
                        retVal.Parameters.Add(new CodeParameterDeclarationExpression(v.ObjectType, ToLowerFirstChar(v.Name)));
                        break;
                    case VarType.Primitive:
                        retVal.Parameters.Add(new CodeParameterDeclarationExpression(v.Type, ToLowerFirstChar(v.Name)));
                        break;
                    case VarType.Vector:
                        if (v.MethodeType == ReadMethodeType.Primitive)
                            retVal.Parameters.Add(new CodeParameterDeclarationExpression("List<" + v.Type.ToString() + ">", ToLowerFirstChar(v.Name)));
                        else
                            retVal.Parameters.Add(new CodeParameterDeclarationExpression("List<" + v.ObjectType + ">", ToLowerFirstChar(v.Name)));
                        break;
                }
            }
            foreach (Variable v in classToGenerate.Variables)
                retVal.Statements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("m_" + v.Name), new CodeVariableReferenceExpression(ToLowerFirstChar(v.Name))));
            return retVal;
        }

        #endregion

        #region Properity and const

        private CodeTypeMember[] GenerateProperity()
        {
            List<CodeTypeMember> retVal = new List<CodeTypeMember>();

            CodeMemberField ProtocolIdField = new CodeMemberField();
            ProtocolIdField.Name = "ProtocolId";
            ProtocolIdField.Attributes = MemberAttributes.Const | MemberAttributes.Public;
            ProtocolIdField.Type = new CodeTypeReference(typeof(int));
            ProtocolIdField.InitExpression = new CodePrimitiveExpression(classToGenerate.ProtocolId);
            CodeMemberProperty ProtocolIdProperty = new CodeMemberProperty();
            if (classToGenerate.Parent == "NetworkMessage")
                ProtocolIdProperty.Name = "MessageID";
            else if(classToGenerate.Parent == "NetworkType")
                ProtocolIdProperty.Name = "TypeID";
            else if (classToGenerate.Parent.Contains("Message"))
                ProtocolIdProperty.Name = "MessageID";
            else
                ProtocolIdProperty.Name = "TypeID";
            ProtocolIdProperty.Attributes = MemberAttributes.Override | MemberAttributes.Public;
            ProtocolIdProperty.Type = new CodeTypeReference(typeof(int));
            ProtocolIdProperty.HasSet = false;
            ProtocolIdProperty.HasGet = true;
            ProtocolIdProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ProtocolId")));
            retVal.Add(ProtocolIdField);
            retVal.Add(ProtocolIdProperty);

            foreach (Variable v in classToGenerate.Variables)
            {
                CodeMemberField privateVar = new CodeMemberField();
                privateVar.Name = "m_" + v.Name;
                CodeMemberProperty properity = new CodeMemberProperty();
                properity.HasGet = true;
                properity.HasSet = true;
                properity.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression(privateVar.Name)));
                properity.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(privateVar.Name), new CodeVariableReferenceExpression("value")));
                properity.Name = ToUpperFirstChar(v.Name);
                properity.Attributes = MemberAttributes.Public;
                switch (v.TypeOfVar)
                {
                    case VarType.Primitive:
                        properity.Type = new CodeTypeReference(v.Type);
                        break;
                    case VarType.Object:
                        properity.Type = new CodeTypeReference(v.ObjectType);
                        break;
                    case VarType.Vector:
                        if (v.ObjectType != null)
                            properity.Type = new CodeTypeReference("List<" + v.ObjectType + ">");
                        else
                            properity.Type = new CodeTypeReference("List<" + v.Type.ToString() + ">");
                        break;
                }
                privateVar.Type = properity.Type;
                retVal.Add(privateVar);
                retVal.Add(properity);
            }
            return retVal.ToArray();
        }

        #endregion

        #endregion

        #region Utility

        private string ToLowerFirstChar(string str)
        {
            string chr = str.Substring(0, 1);
            chr = chr.ToLower();
            return chr + str.Remove(0, 1);
        }

        private string ToUpperFirstChar(string str)
        {
            string chr = str.Substring(0, 1);
            chr = chr.ToUpper();
            return chr + str.Remove(0, 1);
        }

        private CodeIterationStatement GenerateForStatement(CodeExpression VarCount, CodeExpression VarIndex)
        {
            CodeIterationStatement retVal = new CodeIterationStatement();
            retVal.InitStatement = new CodeAssignStatement(VarIndex, new CodePrimitiveExpression(0));
            retVal.TestExpression = new CodeBinaryOperatorExpression(VarIndex, CodeBinaryOperatorType.LessThan, VarCount);
            retVal.IncrementStatement = new CodeAssignStatement(VarIndex, new CodeBinaryOperatorExpression(VarIndex, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1)));
            return retVal;
        }

        #endregion

        #endregion
        
        #region Public methode


        #endregion
    }
}