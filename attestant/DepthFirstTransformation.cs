using attestant.IPA;

namespace attestant;


/// <summary>
///    Algorithm that performs dfs by applying layers of sound laws on a given word;
///    this way the given word is transformed into other phoneme-arrangements
/// </summary>

public class DepthFirstTransformation
{
    private readonly List<HashSet<SoundLaw>> _soundLaws;

    public DepthFirstTransformation(List<HashSet<SoundLaw>> soundLaws)
    {
        _soundLaws = soundLaws;
    }

    public void TransformWord(Word word)
    {
        
    }
}