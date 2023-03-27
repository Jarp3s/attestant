namespace attestantResearch.InputReaders;


public static class WordReader
{
    //private static readonly Spelling WelshSpelling = new(new List<SoundLaw>());
    //private static readonly Spelling IrishSpelling = new(new List<SoundLaw>());
    //private static readonly Spelling ProtoCelticSpelling = new(new List<SoundLaw>());

    public static List<WordSet> FetchWords(string fileName)
    {
        var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Resources", fileName));
        FileStream stream = new(filePath, FileMode.Open);
        StreamReader reader = new(stream);
        
        List<WordSet> words = new();
        while (reader.ReadLine() is { } line)
            words.Add(WordSet.Parse(line)); // Removed Phonetic

        return words;
    }
    /*
    private static WordSet Phonetic(WordSet words)
    {
        words.ProtoCeltic = ProtoCelticSpelling.Phonetic(words.ProtoCeltic);
        words.OldIrish = IrishSpelling.Phonetic(words.OldIrish);
        words.MiddleWelsh = WelshSpelling.Phonetic(words.MiddleWelsh);

        return words;
    }
    */
}
