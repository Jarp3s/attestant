using System.Text.RegularExpressions;

namespace attestant;


/// <summary>
///     IPA-encoded sound law, which can be applied to transform a word into new word(s).
/// </summary>
public class SoundLaw
{
    private readonly Regex _antecedent;
    private readonly HashSet<string> _consequents;

    private readonly string _soundLaw;

    public SoundLaw(string antecedent, HashSet<string> consequents, string soundLaw)
    {
        _antecedent = new Regex(antecedent);
        _consequents = consequents;
        _soundLaw = soundLaw;
    }

    /// <summary>
    ///     Convert a string to a SoundLaw.
    /// </summary>
    public static SoundLaw Parse(string inputLaw)
    { 
        var law = Regex.Replace(inputLaw, @"[\s*]", @"");
        var lawSegments = Regex.Split(law, @"[+>/]");
        var sound = lawSegments[2];

        /*
         * replace all cover symbols w/ [lookup(cover)]
         * replace all other special symbols
         * replace symbol between '()' w/ ? 
         * replace '#' at start w/ ^ & at end w/ $
         */
        
        // Create a RegEx char-group using by processing all symbols in the antecedent
        var antecedentSymbols = Regex.Split(lawSegments[0], @",");
        var replacedSymbols = "[";
        foreach (var symbol in antecedentSymbols)
        {
            // TODO: convert special symbols to RegEx
            replacedSymbols += symbol;
        }
        replacedSymbols += "]";

        // Create a RegEx pattern-string (antecedent) by grouping the 3 segments of the antecedent
        var soundSegments = Regex.Split(sound, @"_");
        var antecedent = $"({soundSegments[0]})({replacedSymbols})({soundSegments[1]})";
        
        // Create RegEx replacement-strings (consequents) by processing each symbol individually
        var consequentSymbols = Regex.Split(lawSegments[1], @",");
        HashSet<string> consequents = new();
        foreach (var symbol in consequentSymbols)
        {
            // TODO: convert special symbols to RegEx
            consequents.Add($"$1{symbol}$3");
        }

        return new SoundLaw(antecedent, consequents, inputLaw);
    }

    /// <summary>
    ///     Convert a SoundLaw to a string.
    /// </summary>
    public override string ToString()
    {
        return _soundLaw;
    }

    /// <summary>
    ///     Apply the sound law on the given word,
    ///     possibly transforming it into new word(s).
    /// </summary>
    public IEnumerable<string> Apply(string word)
    {
        return _consequents.Select(consequent => _antecedent.Replace(word, consequent));
    }
}