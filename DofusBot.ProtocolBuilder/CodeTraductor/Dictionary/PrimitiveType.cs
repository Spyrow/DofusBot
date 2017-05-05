using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusBot.ProtocolBuilder.CodeTraductor.Dictionary
{
    public class PrimitiveType:Dictionary<string,Type>//<readMethode,type>
    {
        public static PrimitiveType singleton = new PrimitiveType();

        public PrimitiveType()
        {
            Add("boolean", typeof(bool));
            Add("byte", typeof(byte));
            Add("double", typeof(double));
            Add("float", typeof(float));
            Add("int", typeof(int));
            Add("short", typeof(short));
            Add("uint", typeof(uint));
            Add("ushort", typeof(ushort));
            Add("sbyte", typeof(sbyte));
            Add("utf", typeof(string));
            Add("utfbytes", typeof(string));
            Add("string", typeof(string));
            Add("number", typeof(double));
            Add("varuhshort", typeof(ushort));
            Add("varshort", typeof(short));
            Add("varuhint", typeof(uint));
            Add("varint", typeof(int));
            Add("varlong", typeof(long));
            Add("varuhlong",typeof(ulong));
            Add("varushort", typeof(ushort));
        }

        public static Type GetTypeByReadMethode(string methode)
        {
            methode = methode.Replace("Read", "");
            if (singleton.ContainsKey(methode.ToLower()))
                return singleton[methode.ToLower()];
            else
                throw new Exception("Unknow type");
        }
    }
}
