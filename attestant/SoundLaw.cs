using System.Text.RegularExpressions;

namespace attestant;


/// <summary>
///     IPA-encoded sound law, which can be applied to transform a word into new word(s).
/// </summary>
public class SoundLaw
{
    private readonly Regex _antecedent;
    private readonly HashSet<string> _consequents;

    public SoundLaw(string antecedent, HashSet<string> consequents)
    {
        _antecedent = new Regex(antecedent);
        _consequents = consequents;
    }

    /// <summary>
    ///     Convert a string to a SoundLaw.
    /// </summary>
    public static HashSet<SoundLaw> Parse(string inputLaw)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Convert a SoundLaw to a string.
    /// </summary>
    public override string ToString()
    {
        throw new NotImplementedException();
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