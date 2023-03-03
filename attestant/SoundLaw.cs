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
        var segments = Regex.Split(law, @"[+>/]");
        var sound = segments[2];

        /*
         * replace all cover symbols w/ [lookup(cover)]
         * replace all other special symbols
         * replace symbol between '()' w/ ? 
         * replace '#' at start w/ ^ & at end w/ $
         * _antecedent  = replace '_' w/ ([symbols])
         * _consequents = foreach symbol, replace _ w/ symbol in a way of substitution.
         */ 
        
        throw new NotImplementedException();
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