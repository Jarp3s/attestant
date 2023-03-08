using System.Text.RegularExpressions;
using attestant.Utilities;

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
    ///     Applies the sound law on the given word,
    ///     possibly transforming it into new word(s).
    /// </summary>
    public IEnumerable<string> Apply(string word)
        => _consequents.Select(consequent => _antecedent.Replace(word, consequent));
    
    /// <summary>
    ///     Converts a SoundLaw to a string.
    /// </summary>
    public override string ToString()
    {
        return _soundLaw;
    }

    // TODO: does not account for anomalies denoted between parentheses after law
    /// <summary>
    ///     Converts a string to a SoundLaw.
    /// </summary>
    public static SoundLaw Parse(string inputLaw) // Example input: *i, *u > *e, *o /_$a(C)#
    {                                             // Example input: *o > *ö /_$ī, i, ü, ö, ẹ, j
        var law = Regex.Replace(inputLaw, @"[\s*]", @"");
        var lawSegments = Regex.Split(law, @"[+>/]");

        return new SoundLaw(GetAntecedent(), GetConsequents(), inputLaw);
        
        
        // Creates a RegEx pattern-string (antecedent) by grouping the sound-context with the sound
        string GetAntecedent()
        {
            var replacedSound = Regex.Split(lawSegments[0], @",");
            var replacedSymbols = "";
            foreach (var symbol in replacedSound)
            {
                // TODO: convert special symbols to RegEx
                var parsedSymbol = Regex.Replace(symbol, @"[A-Z]", match 
                    => CoverSymbol.ToRegexString(char.Parse(match.Value)));
                replacedSymbols += $"|{parsedSymbol}";
            }
            replacedSymbols = new Regex(@"\|").Replace(replacedSymbols, @"", 1);
            
            var soundSegments = Regex.Split(GetSoundContext(), @"_");
            return $"({soundSegments[0]})({replacedSymbols})({soundSegments[1]})";
        }

        // Create RegEx replacement-strings (consequents) by processing each symbol individually
        HashSet<string> GetConsequents()
        {
            var consequentSymbols = Regex.Split(lawSegments[1], @",");
            HashSet<string> consequents = new();
            foreach (var symbol in consequentSymbols)
            {
                // TODO: convert special symbols to RegEx
                var parsedSymbol = Regex.Replace(symbol, @"Ø", @"");
                consequents.Add($"$1{parsedSymbol}$3");
            }

            return consequents;
        }
        
        // Create a RegEx pattern-string (sound-context) by parsing non-literal law-symbols
        string GetSoundContext()
        {
            var sound = lawSegments![2];

            // Parse non-literals (i.e. operator-symbols) to their RegEx equivalent
            sound = Regex.Replace(sound, @"$", @"C*");
            sound = Regex.Replace(sound, @"^#", @"^");
            sound = Regex.Replace(sound, @"#$", @"$");
            sound = Regex.Replace(sound, @"(\()(.)(\))", @"$2?");
            sound = Regex.Replace(sound, @"[A-Z]", match 
                => CoverSymbol.ToRegexString(char.Parse(match.Value)));

            // TODO: Convert OR-relation (,) to RegEx alternation construct
            
            return sound;
        }
    }
}
