using System.Text;
using System.Text.RegularExpressions;
using attestant.Utilities;

namespace attestant;


/// <summary>
///     IPA-encoded sound law, which can be applied to transform a word into new word(s).
/// </summary>
public class SoundLaw
{
    private readonly Regex _antecedent;
    private readonly string _consequent;
    
    private readonly string _soundLaw;

    private SoundLaw(string antecedent, string consequent, string soundLaw)
    {
        _antecedent = new Regex(antecedent);
        _consequent = consequent;
        _soundLaw = soundLaw;
    }
    
    /// <summary>
    ///     Applies the sound law on the given word,
    ///     possibly transforming it into new word(s).
    /// </summary>
    public string Apply(string word) => _antecedent.Replace(word, _consequent);
    
    /// <summary>
    ///     Converts a SoundLaw to a string-representation.
    /// </summary>
    public override string ToString() => _soundLaw;

    /// <summary>
    ///     Gets the id from the given sound law.
    /// </summary>
    public static string Id(string soundlaw)
        => Regex.Replace(soundlaw, @".", @"").Split(" ")[0];
    
    /// <summary>
    ///     Gets the law itself from the given sound law.
    /// </summary>
    public static string Law(string soundLaw) 
        => Regex.Replace(soundLaw, @".", @"").Split(" ")[1];
    
    
    /// <summary>
    ///     Converts a string-representation to a SoundLaw.
    /// </summary>
    public static SoundLaw Parse(string inputLaw) // Example input: *i, *u > *e /_$a(C)#
    {                                             // Example input: *o > *ö /_$ī, i, ü, ö, ẹ, j
        var normalizedLaw = inputLaw.Normalize(NormalizationForm.FormC);
        normalizedLaw = Regex.Replace(normalizedLaw, @"\P{M}\p{M}+", match 
            => CoverSymbol.Characterization.Forward[match.Value].ToString());
        
        normalizedLaw = Regex.Replace(normalizedLaw, @"[\s*]", @"");
        var lawSegments = Regex.Split(normalizedLaw, @"[.>/]");

        return new SoundLaw(GetAntecedent(), GetConsequent(), inputLaw);
        
        
        // Creates a RegEx pattern-string (antecedent) by grouping the environment with the sound
        string GetAntecedent()
        {
            var replacedSound = Regex.Replace(lawSegments[1], @",", @"|");
            // TODO: convert special symbols to RegEx
            replacedSound = Regex.Replace(replacedSound, @"[A-Z]", match
                => CoverSymbol.ToRegexString(char.Parse(match.Value)));

            var environment = Regex.Split(GetEnvironment(), @"_");
            return $"({environment[0]})({replacedSound})({environment[1]})";
        }

        // Create RegEx replacement-strings (consequents) by processing each symbol individually
        string GetConsequent()
        {
            var replacingSound = lawSegments[2];
            // TODO: convert special symbols to RegEx
            replacingSound = Regex.Replace(replacingSound, @"Ø", @"");
            return $"$1{replacingSound}$3";
        }
        
        // Create a RegEx pattern-string (sound-context) by parsing non-literal law-symbols
        string GetEnvironment()
        {
            if (lawSegments.Length < 4)
                return "_";
            
            var environmentSound = Regex.Replace(lawSegments[3], @",", @"|");

            // Parse non-literals (i.e. operator-symbols) to their RegEx equivalent
            environmentSound = Regex.Replace(environmentSound, @"$", @"C*");
            environmentSound = Regex.Replace(environmentSound, @"^#", @"^");
            environmentSound = Regex.Replace(environmentSound, @"#$", @"$");
            environmentSound = Regex.Replace(environmentSound, @"(\()(.+)(\))", @"($2)?");
            environmentSound = Regex.Replace(environmentSound, @"[A-Z]", match 
                => CoverSymbol.ToRegexString(char.Parse(match.Value)));

            return environmentSound;
        }
    }
}
