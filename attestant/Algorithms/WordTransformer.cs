using attestant.DataStructures;

namespace attestant.Algorithms;


/// <summary>
///     Algorithm that applies a number of sound laws on a given word;
///     this way the given word is transformed into other phoneme-arrangements.
/// </summary>
public class WordTransformer
{
    private readonly List<SoundLaw> _soundLaws;

    public WordTransformer(List<SoundLaw> soundLaws)
    {
        _soundLaws = soundLaws;
    }

    /// <summary>
    ///     Performs word-development by transforming the given word
    ///     using a list of sound laws, returns the set of all developed words.
    /// </summary>
    public UNode<string, SoundLaw> TransformWord(string word)
    {
        UNode<string, SoundLaw> originalWordNode = new (word);

        var curWordNode = originalWordNode;
        foreach (var soundLaw in _soundLaws)
        {
            var curWord = curWordNode.Value;
            var newWord = soundLaw.Apply(curWord);
            if (newWord != curWord)
                curWordNode = curWordNode.AddDescendant(newWord, soundLaw);
        }

        return curWordNode;
    }
}
