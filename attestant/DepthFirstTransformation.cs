using attestant.DataStructures;
using attestant.IPA;
using attestant.SoundLaws;

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

    public HashSet<UNode<Word>> TransformWord(Word word)
    {
        var originalWordNode = new UNode<Word>(word);
        return ApplyLawLayer(new HashSet<UNode<Word>>(), originalWordNode, 0);
    }

    private HashSet<UNode<Word>> ApplyLawLayer(HashSet<UNode<Word>> reconstructions, UNode<Word> wordNode, int layer)
    {
        if (layer == _soundLaws.Count) // Base case
        {
            reconstructions.Add(wordNode);
            return reconstructions;
        }
        
        Word curWord = wordNode.Value;
        Word newWord;
        
        foreach (var soundLaw in _soundLaws[layer]) // Recursive case 1
            if ((newWord = soundLaw.Apply(curWord)).SequenceEqual(curWord))
                ApplyLawLayer(reconstructions, wordNode.AddDescendant(newWord), layer);

        return ApplyLawLayer(reconstructions, wordNode, ++layer); // Recursive case 2
    }
    
    // Note: Can same law happen sequentially in 1 word? e.g. aV -> aa for abc > aac > aaa?
    //       --> In that case, should go through word in every possible sequence? (Non-deterministic?)
    
    // Note: Cannot apply laws of same layer in any order
}