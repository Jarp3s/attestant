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
    ///     Foreach language, apply all given laws in order to transform the word.
    /// </summary>
    public List<UNode<string, SoundLaw>> Develop(string word)
    {
        List<UNode<string, SoundLaw>> wordDevelopments = new();
        
        foreach (var lanDev in _languageDevelopments)
        {
            var transformer = new WordTransformer(lanDev.SoundLaws);
            wordDevelopments.Add(transformer.Transform(word));
        }
        
        return wordDevelopments;
    }
}
