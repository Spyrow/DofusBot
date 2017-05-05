using DofusBot.Utilities.Thread;
using System.Text;

namespace DofusBot.Utilities.Random
{
    public static class StringGenerator
    {

        public static string GetRandomString(int length)
        {
            var random = new AsyncRandom();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        public static string GetCustomRandomString(int length, string characters)
        {
            var random = new System.Random();
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

    }
}
