namespace attestant.Utilities;


/// <summary>
///    Utility class that offers functionality to operate on cover symbols.
/// </summary>
internal static class CoverSymbol
{
    /// <summary>
    ///     Converts the given cover symbol to a RegEx-char covering all phonemes.
    /// </summary>
    public static string ToRegexString(char coverSymbol)
    {
        var phonemes = "[";
        foreach (var phoneme in CoverSymbol.Phonemes(coverSymbol))
            phonemes += phoneme;
        phonemes += "]";
        return phonemes;
    }
    

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
            's', 'z', 'l', 'ɬ', 'λ', 'j', 'k', 'g', 'ŋ', 'Ⅵ', 'x', 'ɣ', 'Ⅶ', 'Ⅷ', 'h', 'Ⅸ', 'Ⅹ', 'w',
            'ᚠ', 'ᚢ', 'ᚦ', 'ᚨ', 'ᚱ', 'ᚲ', 'ᚷ', 'ᚹ', 'ᚺ', 'ᚾ', 'ᛁ', 'ᛃ', 'ᛇ', 'ᛈ', 'ᛉ', 'ᛊ', 'ᛏ', 'ᛒ', 'ᛖ', 'ᛗ', 'ᛚ', 'ᛜ', 'ᛞ', 'ᛟ', 'ᚡ'
        },
        
        'T' => new HashSet<char> // Plosive
        { 
            'p', 'b', 't', 'd', 'k', 'g', 'Ⅸ', 'Ⅹ', 'ᚠ', 'ᚢ', 'ᚹ', 'ᚺ', 'ᛊ', 'ᛏ', 'ᛚ', 'ᛜ'
        },

        'B' => new HashSet<char> // Voiced Plosive
        {
            'b', 'd', 'g', 'Ⅹ', 'ᚢ', 'ᚺ', 'ᛏ', 'ᛜ'
        },

        'P' => new HashSet<char> // Voiceless Plosive
        {
            'p', 't', 'k', 'Ⅸ', 'ᚠ', 'ᚹ', 'ᛊ', 'ᛚ'
        },

        'D' => new HashSet<char> // Dental
        {
            't', 'd', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'θ', 'ð', 's', 'z', 'l', 'ɬ', 'λ', 'ᚹ', 'ᚺ', 'ᚾ', 'ᛁ', 'ᛃ', 'ᛇ', 'ᛈ'
        },

        'Ð' => new HashSet<char> // Neutral Dental
        {
            't', 'd', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'θ', 'ð', 's', 'z', 'l', 'ɬ'
        },

        'Þ' => new HashSet<char> // Neutral Dental Stops and Fricatives, excluding Sibilants
        {
            't', 'd', 'θ', 'ð'
        },

        'N' => new HashSet<char> // Nasal
        { 
            'm', 'Ⅲ', 'μ', 'n', 'Ⅳ', 'ν', 'ŋ', 'Ⅵ', 'ᛒ', 'ᚦ', 'ᚾ', 'ᚲ'
        },
     
        'F' => new HashSet<char> // Fricative
        { 
            'ɸ', 'β', 'μ', 'f', 'v', 'θ', 'ð', 's', 'z', 'x', 'Ⅶ', 'ɣ', 'Ⅷ', 'h', 'ᚨ', 'ᚱ', 'ᚲ', 'ᚷ', 'ᛃ', 'ᛇ', 'ᛈ', 'ᛖ', 'ᛗ', 'ᛞ', 'ᛟ'
        },
   
        'R' => new HashSet<char> // Resonant
        { 
            'm', 'Ⅲ', 'μ', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'l', 'ɬ', 'λ', 'j', 'ŋ', 'Ⅵ', 'w', 'ᛁ', 'ᛉ', 'ᛒ', 'ᚦ', 'ᚾ', 'ᚡ', 'ᚲ'
        },

        'Q' => new HashSet<char> // Palatal Resonant
        {
             'ᛁ', 'ᛉ', 'ᛒ', 'ᚦ', 'ᚾ', 'ᚡ', 'ᚲ'
        },
    
        'L' => new HashSet<char> // Liquid
        { 
            'r', 'Ⅴ', 'ρ', 'l', 'ɬ', 'λ', 'ᛁ', 'ᛉ'
        },

        'K' => new HashSet<char> // Velar
        {
            'k', 'g', 'x', 'ɣ', 'Ⅸ', 'Ⅹ', 'Ⅶ', 'Ⅷ', 'ᛊ', 'ᛏ', 'ᛖ', 'ᛗ', 'ᛚ', 'ᛜ', 'ᛞ', 'ᛟ'
        },

        'G' => new HashSet<char> // Labio-velar
        {
            'Ⅸ', 'Ⅹ', 'Ⅶ', 'Ⅷ',  'ᛚ', 'ᛜ', 'ᛞ', 'ᛟ'
        },

        'X' => new HashSet<char> // Neutral Consonant
        {
            'p', 'b', 'm', 'Ⅲ', 'ɸ', 'β', 'μ', 'f', 'v', 't', 'd', 'n', 'Ⅳ', 'ν', 'r', 'Ⅴ', 'ρ', 'θ', 'ð',
            's', 'z', 'l', 'ɬ', 'λ', 'j', 'k', 'g', 'ŋ', 'Ⅵ', 'x', 'ɣ', 'Ⅶ', 'Ⅷ', 'h', 'Ⅸ', 'Ⅹ', 'w'
        },

        'J' => new HashSet<char> // Palatal Consonant
        {

            'ᚠ', 'ᚢ', 'ᚦ', 'ᚨ', 'ᚱ', 'ᚲ', 'ᚷ', 'ᚹ', 'ᚺ', 'ᚾ', 'ᛁ', 'ᛃ', 'ᛇ', 'ᛈ', 'ᛉ', 'ᛊ', 'ᛏ', 'ᛒ', 'ᛖ', 'ᛗ', 'ᛚ', 'ᛜ', 'ᛞ', 'ᛟ', 'ᚡ'
        },

        _ => throw new ArgumentOutOfRangeException(nameof(coverSymbol), coverSymbol, null)
    };
}
