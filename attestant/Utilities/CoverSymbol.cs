using System.Text.RegularExpressions;
using attestant.DataStructures;

namespace attestant.Utilities;


internal static class CoverSymbol
{
    /// <summary>
    ///     Converts the given cover symbol to a RegEx-char covering all phonemes
    /// </summary>
    public static string ToRegexString(char coverSymbol)
    {
        var phonemes = "[";
        foreach (var phoneme in CoverSymbol.Phonemes(coverSymbol))
            phonemes += phoneme;
        phonemes += "]";
        return phonemes;
    }

    public static Table<string, char> Characterization = 
        new(
            ("ε̄", 'Ⅰ'),
            ("ɔ̄", 'Ⅱ'),
            ("m̥", 'Ⅲ'),
            ("n̥", 'Ⅳ'),
            ("r̥", 'Ⅴ'),
            ("ŋ̊", 'Ⅵ'),
            ("xʷ", 'Ⅶ'),
            ("ɣʷ", 'Ⅷ'),
            ("kʷ", 'Ⅸ'),
            ("gʷ", 'Ⅹ')
        );

    // TODO: Palatalization, Lenition
    /// <summary>
    ///     Maps the given cover symbol to a set of phonemes.
    /// </summary>
    private static HashSet<char> Phonemes(char coverSymbol) => coverSymbol switch
    {
        'V' => new HashSet<char> // Vowel
        { 
            'i', 'y', 'ɨ', 'ʉ', 'u', 'ẹ', 'e', 'ø', 'o', 'ɛ', 'ɔ', 'æ', 
            'a', 'ə', 'ī', 'ȳ', 'ū', 'ē', 'ō', 'Ⅰ', 'Ⅱ', 'ǣ', 'ā'
        },
        
        'S' => new HashSet<char> // Short vowel
        { 
            'i', 'y', 'ɨ', 'ʉ', 'u', 'ẹ', 'e', 'ø', 'o', 'ɛ', 'ɔ', 'æ', 'a', 'ə',
        },
        
        'W' => new HashSet<char> // Long vowel
        { 
            'ī', 'ȳ', 'ū', 'ē', 'ō', 'Ⅰ', 'Ⅱ', 'ǣ', 'ā'
        },
        
        'U' => new HashSet<char> // High vowel
        { 
            'i', 'y', 'ɨ', 'ʉ', 'u', 'ī', 'ȳ', 'ū'
        },
        
        'E' => new HashSet<char> // Front vowel
        { 
            'i', 'y', 'ẹ', 'e', 'ø', 'ɛ', 'æ', 'ī', 'ȳ', 'ē', 'Ⅰ', 'ǣ'
        },
        
        'O' => new HashSet<char> // Back vowel
        { 
            'u','o', 'ɔ', 'a',
            'ū', 'ō', 'Ⅱ', 'ā'
        },
        
        'C' => new HashSet<char> // Consonant
        { 
            'p', 'b', 'm', 'Ⅲ', 'ɸ', 'β', 'μ', 'f', 'v', 't', 'd', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'θ', 'ð', 
            's', 'z', 'l', 'ɬ', 'λ', 'j', 'k', 'g', 'ŋ', 'Ⅵ', 'x', 'ɣ', 'Ⅶ', 'Ⅷ', 'h', 'Ⅸ', 'Ⅹ', 'w'
        },
        
        'T' => new HashSet<char> // Plosive
        { 
            'p', 'b', 't', 'd', 'k', 'g', 'Ⅸ', 'Ⅹ'
        },
        'B' => new HashSet<char> // Voiced Plosive
        {
            'b', 'd', 'g', 'Ⅹ'
        },
        'P' => new HashSet<char> // Voiceless Plosive
        {
            'p', 't', 'k', 'Ⅸ'
        },
        'D' => new HashSet<char> // Dental
        {
            't', 'd', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'θ', 'ð', 's', 'z', 'l', 'ɬ', 'λ'        
        },

        'N' => new HashSet<char> // Nasal
        { 
            'm', 'Ⅲ', 'μ', 'n', 'Ⅳ', 'ν', 'ŋ', 'Ⅵ'
        },
     
        'F' => new HashSet<char> // Fricative
        { 
            'ɸ', 'β', 'μ', 'f', 'v', 'θ', 'ð', 's', 'z', 'x', 'Ⅶ', 'ɣ', 'Ⅷ', 'h'
        },
   
        'R' => new HashSet<char> // Resonant
        { 
            'm', 'Ⅲ', 'μ', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'l', 'ɬ', 'λ', 'j', 'ŋ', 'Ⅵ', 'w'
        },
    
        'L' => new HashSet<char> // Liquid
        { 
            'r', 'Ⅴ', 'ρ', 'l', 'ɬ', 'λ'
        },
     
        _ => throw new ArgumentOutOfRangeException(nameof(coverSymbol), coverSymbol, null)
    };
}
