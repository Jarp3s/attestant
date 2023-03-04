using attestant.DataStructures;

namespace attestant;


/// <summary>
///    Algorithm that performs dfs by applying a number of sound laws on a given word;
///    this way the given word is transformed into other phoneme-arrangements.
/// </summary>
public class DepthFirstTransformation
{
    private readonly List<SoundLaw> _soundLaws;
    private CoverSymbols = new CoverSymbols();

    public DepthFirstTransformation(List<SoundLaw> soundLaws)
    {
        _soundLaws = soundLaws;
    }

    /// <summary>
    ///     Perform word-reconstruction by transforming the given word
    ///     using a list of sound laws, returns the set of all reconstructed words.
    /// </summary>
    public HashSet<UNode<string>> TransformWord(string word)
    {
        UNode<string> originalWordNode = new (word);
        return ApplyLaws(originalWordNode, 0);
    }

    private HashSet<UNode<string>> ApplyLaws(UNode<string> wordNode, int lawNumber)
    {
        HashSet<UNode<string>> reconstructions = new();
        
        for (var i = lawNumber; i < _soundLaws.Count; i++)
        {
            var curWord = wordNode.Value;
            IEnumerable<string> newWords = _soundLaws[i].Apply(curWord);

            var isTransformed = false;
            foreach (var word in newWords)
                if (word != curWord)
                {
                    isTransformed = true;
                    reconstructions.UnionWith(ApplyLaws(wordNode.AddDescendant(word), i));
                }

            if (isTransformed)
                break;
        }

        reconstructions.Add(wordNode);
        return reconstructions;
    }
}