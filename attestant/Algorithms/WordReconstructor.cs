using attestant.DataStructures;
using attestant.InputReaders;

namespace attestant.Algorithms;


/// <summary>
///     Algorithm that calculates all possible word-reconstructions from the given word.
/// </summary>
public class WordReconstructor
{
    private readonly List<LanguageDevelopment> _languageDevelopments;

    public WordReconstructor(List<LanguageDevelopment> languageDevelopments)
    {
        _languageDevelopments = languageDevelopments;
    }

    /// <summary>
    ///     Foreach language, applies dfs on the given word, after which all reconstructions
    ///     appearing in every language (i.e. duplicates) are filtered & returned as final result.
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
