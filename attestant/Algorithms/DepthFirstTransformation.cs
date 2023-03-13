using attestant.DataStructures;

namespace attestant.Algorithms;


/// <summary>
///     Algorithm that performs dfs by applying a number of sound laws on a given word;
///     this way the given word is transformed into other phoneme-arrangements.
/// </summary>
public class DepthFirstTransformation
{
    private readonly List<SoundLaw> _soundLaws;

    public DepthFirstTransformation(List<SoundLaw> soundLaws)
    {
        _soundLaws = soundLaws;
    }

    /// <summary>
    ///     Performs word-development by transforming the given word
    ///     using a list of sound laws, returns the set of all developed words.
    /// </summary>
    public HashSet<UNode<string, SoundLaw>> TransformWord(string word)
    {
        UNode<string, SoundLaw> originalWordNode = new (word);
        return ApplyLaws(originalWordNode, 0);
    }

    private HashSet<UNode<string, SoundLaw>> ApplyLaws(UNode<string, SoundLaw> wordNode, int lawNumber)
    {
        HashSet<UNode<string, SoundLaw>> wordDevelopments = new();

        var isTransformed = false;
        for (var i = lawNumber; i < _soundLaws.Count; i++)
        {
            var curWord = wordNode.Value;
            IEnumerable<string> newWords = _soundLaws[i].Apply(curWord);
            
            foreach (var word in newWords)
                if (word != curWord)
                {
                    isTransformed = true;
                    wordDevelopments.UnionWith(ApplyLaws(wordNode.AddDescendant(word, _soundLaws[i]), i));
                }

            if (isTransformed)
                break;
        }
        
        if (!isTransformed)
            wordDevelopments.Add(wordNode);
        
        return wordDevelopments;
    }
}
