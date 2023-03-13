using attestant.Algorithms;
using attestant.DataStructures;
using attestant.InputReaders;

namespace attestantResearch.Algorithms;


/// <summary>
///     Algorithm that calculates all possible word-reconstructions from the given word.
/// </summary>
public class WordReconstructor
{
    private readonly List<LanguageDevelopment> _languageDevelopments;

    public WordReconstructor()
    {
        _languageDevelopments = SoundLawReader.GetLanguageDevelopments();
    }

    /// <summary>
    ///     Foreach language, applies dfs on the given word in order to .
    /// </summary>
    public HashSet<string> Reconstruct(string word)
    {
        List<HashSet<UNode<string>>> lanReconstructs = new();
        
        foreach (var lanDev in _languageDevelopments)
        {
            var dft = new DepthFirstTransformation(lanDev.SoundLaws);
            lanReconstructs.Add(dft.TransformWord(word));
        }

        HashSet<string> reconstructs = lanReconstructs[0].Select(uNode => uNode.Value).ToHashSet();
        foreach(HashSet<UNode<string>> reconstruct in lanReconstructs)
            reconstructs.IntersectWith(reconstruct.Select(uNode => uNode.Value));

        return reconstructs;
    }
}
