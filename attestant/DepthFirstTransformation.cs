using attestant.DataStructures;

namespace attestant;


/// <summary>
///    Algorithm that performs dfs by a layer of sound laws on a given word;
///    this way the given word is transformed into other phoneme-arrangements
/// </summary>

public class DepthFirstTransformation
{
    private readonly List<SoundLaw> _soundLaws;

    public DepthFirstTransformation(List<SoundLaw> soundLaws)
    {
        _soundLaws = soundLaws;
    }

    public HashSet<UNode<string>> TransformWord(string word)
    {
        UNode<string> originalWordNode = new (word);
        return ApplyLawLayer(new HashSet<UNode<string>>(), originalWordNode, 0);
    }

    private HashSet<UNode<string>> ApplyLawLayer(HashSet<UNode<string>> reconstructions, UNode<string> wordNode, int layer)
    {
        throw new NotImplementedException();
    }
}