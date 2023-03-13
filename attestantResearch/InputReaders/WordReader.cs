using attestant.Utilities;

namespace attestantResearch.InputReaders;


public static class WordReader
{
    private static readonly Spelling WelshSpelling = new();
    private static readonly Spelling IrishSpelling = new();

    public static List<WordSet> ReadWords()
    {
        var fileName = "cognates.txt";
        var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Resources", fileName));
        FileStream stream = new(filePath, FileMode.Open);
        StreamReader reader = new(stream);
        
        List<WordSet> words = new();
        while (reader.ReadLine() is { } line)
            words.Add(Phonetic(WordSet.Parse(line)));

        return words;
    }

    private static WordSet Phonetic(WordSet words)
    {
        // Proto-Celtic forms are fine already
        words.OldIrish = IrishSpelling.Phonetic(words.OldIrish);
        words.MiddleWelsh = WelshSpelling.Phonetic(words.MiddleWelsh);

        return words;
    }
}
