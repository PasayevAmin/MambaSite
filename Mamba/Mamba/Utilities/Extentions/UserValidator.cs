using Mamba.ViewModels;
using System.Text.RegularExpressions;

namespace Mamba.Utilities.Extentions
{
    public static class UserValidator
    {
        public static string CapitalizeName(this string name)
        {
            string[] words = name.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }
            return string.Join(" ", words);

        }
        public static bool CheckWords(this RegisterVM registerVM,string words)
        {
            if (!words.Trim().Contains(" "))
            {
                return true;
            }
            return false;
        }
        public static bool IsDigit(this  string word)
        {
            return word.Any(char.IsDigit);
        }
        public static bool IsSymbol(this RegisterVM registerVM, string word)
        {
            bool result = false;
            foreach (Char letter in word)
            {
                result = Char.IsSymbol(letter);
            }
            return result;
        }
        public static bool CheckEmail(this RegisterVM registerVM, string word)
        {
            string pattern = "^(?i)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            Regex regex=new Regex(pattern);
            if (regex.IsMatch(word))
            {
                return true;
            }
            return false;
        }

    }
}
