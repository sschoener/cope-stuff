#region

using System.Linq;

#endregion

namespace cope
{
    /// <summary>
    /// Different CharTypes used to check if a char fulfills certain specifications.
    /// </summary>
    public enum CharType
    {
        /// <summary>
        /// Contains all 7bit Ascii characters.
        /// </summary>
        Ascii = 0,
        Consonant,
        Control,
        Digit,
        IllegalInFilename,
        IllegalInPath,
        Letter,
        Lower,
        Punctuation,
        Separator,
        Symbol,
        Umlaut,
        Upper,
        Vowel,
        Whitespace
    } ;

    public static class CharTypeChecker
    {
        #region Delegates

        /// <summary>
        /// Delegate that Checks if a Char fulfills certain specifications.
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns></returns>
        public delegate bool IsOfCharType(char c);

        #endregion

        private static readonly char[] s_vowels = {'a', 'e', 'i', 'o', 'u'};
        private static readonly char[] s_umlaute = {'ä', 'ö', 'ü'};
        private static readonly char[] s_illegalInFilename = {':', '"', '/', '\\', '?', '<', '>', '|', '*'};
        private static readonly char[] s_illegalInPath = {'"', '?', '<', '>', '|', '*'};

        /// <summary>
        /// Array collecting the checking functions for different CharTypes; needs to be in the same order as CharType.
        /// </summary>
        private static readonly IsOfCharType[] s_charTypeCheckers = {
                                                                        IsAscii,
                                                                        IsConsonant,
                                                                        char.IsControl,
                                                                        char.IsDigit,
                                                                        IsIllegalInFilename,
                                                                        IsIllegalInPath,
                                                                        char.IsLetter,
                                                                        char.IsLower,
                                                                        char.IsPunctuation,
                                                                        char.IsSeparator,
                                                                        char.IsSymbol,
                                                                        IsUmlaut,
                                                                        char.IsUpper,
                                                                        IsVowel,
                                                                        char.IsWhiteSpace
                                                                    };

        /// <summary>
        /// Returns the Checker for a given CharType.
        /// </summary>
        /// <param name="ct">CharType to get the checker for.</param>
        /// <returns></returns>
        public static IsOfCharType GetCharTypeChecker(CharType ct)
        {
            return s_charTypeCheckers[(int) ct];
        }

        /// <summary>
        /// Checks if the given char is of a certain CharType.
        /// </summary>
        /// <param name="c">Char to check.</param>
        /// <param name="t">CharType to check for.</param>
        /// <returns></returns>
        public static bool IsCharOfType(this char c, CharType t)
        {
            return s_charTypeCheckers[(int) t](c);
        }

        /// <summary>
        /// Checks if the given char is of any of the given CharTypes.
        /// </summary>
        /// <param name="c">Char to check.</param>
        /// <param name="ts">CharTypes to check for.</param>
        /// <returns></returns>
        public static bool IsCharOfType(this char c, params CharType[] ts)
        {
            return ts.Any(t => s_charTypeCheckers[(int) t](c));
        }

        #region TypeCheckers

        /// <summary>
        /// Checks whether or not the char c is representable in 7bit Ascii.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsAscii(char c)
        {
            return c < 127;
        }

        /// <summary>
        /// Checks if char c is a vowel.
        /// </summary>
        /// <param name="c">Char to check.</param>
        /// <returns></returns>
        public static bool IsVowel(char c)
        {
            char d = char.ToLower(c);
            return s_vowels.Any(sc => d == sc);
        }

        /// <summary>
        /// Checks if char c is a consonant.
        /// </summary>
        /// <param name="c">Char to check.</param>
        /// <returns></returns>
        public static bool IsConsonant(char c)
        {
            return !IsVowel(c);
        }

        /// <summary>
        /// Checks if char c is an Umlaut.
        /// </summary>
        /// <param name="c">Char to check.</param>
        /// <returns></returns>
        public static bool IsUmlaut(char c)
        {
            char d = char.ToLower(c);
            return s_umlaute.Any(sc => d == sc);
        }

        /// <summary>
        /// Checks whether the specified char is legal in a Windows file-/foldername.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsIllegalInFilename(char c)
        {
            char d = char.ToLower(c);
            return s_illegalInFilename.Any(sc => d == sc);
        }

        /// <summary>
        /// Checks whether the specified char is legal in a Windows file-/folderpath.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsIllegalInPath(char c)
        {
            char d = char.ToLower(c);
            return s_illegalInPath.Any(sc => d == sc);
        }

        #endregion TypeCheckers
    }
}