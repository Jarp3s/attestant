using attestant.DataStructures;
using attestant.InputReaders;

namespace attestant;


public class WordReconstructor
{
    private readonly List<LanguageDevelopment> _languageDevelopments;

    public WordReconstructor(List<LanguageDevelopment> languageDevelopments)
    {
        _languageDevelopments = languageDevelopments;
    }

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
