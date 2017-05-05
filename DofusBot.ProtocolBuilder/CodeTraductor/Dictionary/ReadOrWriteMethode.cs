using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Dictionary
{
    public class ReadOrWriteMethode:Dictionary<string,string> //<asMethode,c#methode>  attention, les methode as sont en minuscule mais les methode c# ne le sont pas !!!
    {
        public static ReadOrWriteMethode singleton = new ReadOrWriteMethode();

        public ReadOrWriteMethode()
        {
            Add("boolean", "Boolean");
            Add("byte", "Byte");
            Add("double", "Double");
            Add("float", "Float");
            Add("int", "Int");
            Add("short", "Short");
            Add("unsignedint", "UInt");
            Add("unsignedshort", "UShort");
            Add("unsignedbyte", "SByte");
            Add("utf", "UTF");
            Add("utfbytes", "UTFBytes");
            Add("string", "UTF");
            Add("String", "UTF");
            Add("uint", "UInt");
            Add("number", "Double");
            Add("varint", "VarInt");
            Add("varuhint", "VarUhInt");
            Add("varshort", "VarShort");
            Add("varuhshort", "VarUhShort");
            Add("varlong", "VarLong");
            Add("varuhlong", "VarUhLong");
        }

        public static string GetReadMethode(string key)
        {
            key = key.ToLower();
            key = key.Replace("read", "");
            return "Read" + singleton[key];
        }

        public static string GetWriteMethode(string key)
        {
            key = key.ToLower();
            key = key.Replace("read", "");
            return "Write" + singleton[key];
        }
    }
}
