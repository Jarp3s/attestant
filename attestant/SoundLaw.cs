using System.Text.RegularExpressions;

namespace attestant;


public class SoundLaw
{
    private readonly Regex _antecedent;
    private readonly HashSet<string> _consequents;

    public SoundLaw(string antecedent, HashSet<string> consequents)
    {
        _antecedent = new Regex(antecedent);
        _consequents = consequents;
    }

    public static HashSet<SoundLaw> Parse(string inputLaw)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> Apply(string word)
    {
        return _consequents.Select(consequent => _antecedent.Replace(word, consequent));
    }
}