using System.Text.RegularExpressions;

namespace attestant.Utilities;


internal static class CoverSymbol
{
    /// <summary>
    ///     Converts the given cover symbol to a RegEx-string covering all phonemes
    /// </summary>
    public static string ToRegexString(char coverSymbol)
    {
        var phonemes = "";
        foreach (var phoneme in CoverSymbol.Phonemes(coverSymbol))
            phonemes += $"|{phoneme}";
        return $"({new Regex(@"\|").Replace(phonemes, @"", 1)})";
    }
    
    // TODO: Palatalization, Lenition
    /// <summary>
    ///     Maps the given cover symbol to a set of phonemes.
    /// </summary>
    private static HashSet<string> Phonemes(char coverSymbol) => coverSymbol switch
    {
        'V' => new HashSet<string> // Vowel
        { 
            "i", "y", "ɨ", "ʉ", "u", "ẹ", "e", "ø", "o", "ɛ", "ɔ", "æ", 
            "a", "ə", "ī", "ȳ", "ū", "ē", "ō", "ε̄", "ɔ̄", "ǣ", "ā"
        },
        
        'S' => new HashSet<string> // Short vowel
        { 
            "i", "y", "ɨ", "ʉ", "u", "ẹ", "e", "ø", "o", "ɛ", "ɔ", "æ", "a", "ə",
        },
        
        'W' => new HashSet<string> // Long vowel
        { 
            "ī", "ȳ", "ū", "ē", "ō", "ε̄", "ɔ̄", "ǣ", "ā"
        },
        
        'U' => new HashSet<string> // High vowel
        { 
            "i", "y", "ɨ", "ʉ", "u", "ī", "ȳ", "ū"
        },
        
        'E' => new HashSet<string> // Front vowel
        { 
            "i", "y", "ẹ", "e", "ø", "ɛ", "æ", "ī", "ȳ", "ē", "ε̄", "ǣ"
        },
        
        /*'V' => new HashSet<string> // Back vowel
        { 
            "u","o", "ɔ", "a",
            "ū", "ō", "ɔ̄", "ā"
        },*/
        
        'C' => new HashSet<string> // Consonant
        { 
            "p", "b", "m", "m̥", "ɸ", "β", "f", "v", "t", "d", "n", "n̥", "r", "r̥", "θ", "ð", 
            "s", "z", "l", "ɬ", "j", "k", "g", "ŋ", "ŋ̊", "x", "ɣ", "h", "kʷ", "gʷ", "w"
        },
        
        'T' => new HashSet<string> // Plosive
        { 
            "p", "b", "t", "d", "k", "g", "kʷ", "gʷ"
        },
       
        'N' => new HashSet<string> // Nasal
        { 
            "m", "m̥", "n", "n̥", "ŋ", "ŋ̊"
        },
     
        'F' => new HashSet<string> // Fricative
        { 
            "ɸ", "β", "f", "v", "θ", "ð", "s", "z", "x", "ɣ", "h"
        },
   
        'R' => new HashSet<string> // Resonant
        { 
            "m", "m̥", "n", "n̥", "r", "r̥", "l", "ɬ", "j", "ŋ", "ŋ̊", "w"
        },
    
        'L' => new HashSet<string> // Liquid
        { 
            "r", "r̥", "l", "ɬ",
        },
     
        _ => throw new ArgumentOutOfRangeException(nameof(coverSymbol), coverSymbol, null)
    };
}
