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
    public string Apply(Word word)
    {
        var result = _antecedent.Replace(word.CharacterizedPhonemes, _consequent);
        return Regex.Replace(result, @"[Ⅰ-Ⅹ]", match
            => Phoneme.Characterization.Reverse[char.Parse(match.Value)]);
    }

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
    public static SoundLaw Parse(string inputLaw) // Example input: i,u > e /_C$a$#
    {                                             // Example input: o > ø /_C$ī, C$i, C$ü, C$ö, C$ẹ, C$j
        var normalizedLaw = inputLaw.Normalize(NormalizationForm.FormC);
        normalizedLaw = Regex.Replace(normalizedLaw, @"\P{M}\p{M}+", match 
            => Phoneme.Characterization.Forward[match.Value].ToString());
        
        normalizedLaw = Regex.Replace(normalizedLaw, @"[\s*]", @"");
        var lawSegments = Regex.Split(normalizedLaw, @"[.>/]");

        return new SoundLaw(GetTarget(), GetReplacement(), inputLaw);
        
        
        // Creates a RegEx pattern-string (antecedent) by grouping the environment with the sound
        string GetTarget()
        {
            var targetSound = ToRegex(lawSegments[1]);

            var environment = Regex.Split(GetEnvironment(), @"_");

            var target = $"(?<={environment[0]})({targetSound})(?={environment[1]})";

            return target;
        }

        // Create RegEx replacement-strings (consequents) by processing each symbol individually
        string GetReplacement()
        {
            return ToRegex(lawSegments[2]);
        }
        
        // Create a RegEx pattern-string (sound-context) by parsing non-literal law-symbols
        string GetEnvironment()
        {
            if (lawSegments.Length < 4)
                return "_";

            return ToRegex(lawSegments[3]);
        }

        string ToRegex(string str)
        {
            // Parse non-literals (i.e. operator-symbols) to their RegEx equivalent
            str = Regex.Replace(str, @",", @"|");
            str = Regex.Replace(str, @"\$", @"C*");
            str = Regex.Replace(str, @"^#", @"^");
            str = Regex.Replace(str, @"#$", @"$");
            str = Regex.Replace(str, @"(\()(.+)(\))", @"($2)?");
            str = Regex.Replace(str, @"Ø", @"");
            str = Regex.Replace(str, @"[A-Z]", match
                => CoverSymbol.ToRegexString(char.Parse(match.Value)));

            return str;
        }
    }
}
