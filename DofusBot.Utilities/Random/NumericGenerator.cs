using System;
using System.Text;

namespace DofusBot.Utilities.Random
{
    public static class NumericGenerator
    {

        public static int GetRandomInt(int LENGTH)
        {
            return Convert.ToInt32(GetRandomNumeric(LENGTH));
        }

        public static uint GetRandomUInt(int LENGTH)
        {
            return Convert.ToUInt32(GetRandomNumeric(LENGTH));
        }

        private static string GetRandomNumeric(int length)
        {
            var random = new System.Random();
            string characters = "0123456789";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

    }
}
