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
    public List<HashSet<UNode<string, SoundLaw>>> Develop(string word)
    {
        List<HashSet<UNode<string, SoundLaw>>> wordDevelopments = new();
        
        foreach (var lanDev in _languageDevelopments)
        {
            var dft = new DepthFirstTransformation(lanDev.SoundLaws);
            wordDevelopments.Add(dft.TransformWord(word));
        }
        
        return wordDevelopments;
    }
}
