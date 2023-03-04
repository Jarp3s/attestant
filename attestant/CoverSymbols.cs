using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attestant
{
    internal class CoverSymbols
    {
        internal Dictionary<char, string[]> coverSymbols; // String because many of these sounds are in fact multiple unicode characters (though they may not look like it)

        internal CoverSymbols() // TODO: Palatalization, Lenition
        {
            coverSymbols = new();
            coverSymbols.Add('V', new string[] { // Vowel
                "i", "y", "ɨ", "ʉ", "u", "ẹ", "e", "ø", "o", "ɛ", "ɔ", "æ", "a", "ə",
                "ī", "ȳ", "ū", "ē", "ō", "ε̄", "ɔ̄", "ǣ", "ā"
            });
            coverSymbols.Add('S', new string[] { // Short vowel
                "i", "y", "ɨ", "ʉ", "u", "ẹ", "e", "ø", "o", "ɛ", "ɔ", "æ", "a", "ə",
            });
            coverSymbols.Add('W', new string[] { // Long vowel
                "ī", "ȳ", "ū", "ē", "ō", "ε̄", "ɔ̄", "ǣ", "ā"
            });
            coverSymbols.Add('U', new string[] { // High vowel
                "i", "y", "ɨ", "ʉ", "u", "ī", "ȳ", "ū"
            });
            coverSymbols.Add('E', new string[] { // Front vowel
                "i", "y", "ẹ", "e", "ø", "ɛ", "æ",
                "ī", "ȳ", "ē", "ε̄", "ǣ"
            });
            coverSymbols.Add('V', new string[] { // Back vowel
                "u","o", "ɔ", "a",
                "ū", "ō", "ɔ̄", "ā"
            });
            coverSymbols.Add('C', new string[] { // Consonant
                "p", "b", "m", "m̥", "ɸ", "β", "f", "v", "t", "d", "n", "n̥", "r", "r̥", "θ", "ð", "s", "z", "l", "ɬ",
                "j", "k", "g", "ŋ", "ŋ̊", "x", "ɣ", "h", "kʷ", "gʷ", "w"
            });
            coverSymbols.Add('T', new string[] { // Plosive
                "p", "b", "t", "d", "k", "g", "kʷ", "gʷ"
            });
            coverSymbols.Add('N', new string[] { // Nasal
                "m", "m̥", "n", "n̥", "ŋ", "ŋ̊"
            });
            coverSymbols.Add('F', new string[] { // Fricative
                "ɸ", "β", "f", "v", "θ", "ð", "s", "z", "x", "ɣ", "h"
            });
            coverSymbols.Add('R', new string[] { // Resonant
                "m", "m̥", "n", "n̥", "r", "r̥", "l", "ɬ", "j", "ŋ", "ŋ̊", "w"
            });
            coverSymbols.Add('L', new string[] { // Liquid
                "r", "r̥", "l", "ɬ",
            });
        }
    }
}