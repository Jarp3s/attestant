namespace attestantResearch.Algorithms;


public class Spelling
{
    private readonly List<SoundLaw> _spellingLaws;

    public Spelling(List<SoundLaw> spellingLaws)
    {
        _spellingLaws = spellingLaws;
    }

    public string Phonetic(string written)
    {
        return written; // TODO
    }
}
