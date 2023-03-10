using attestant.Utilities;

namespace attestant.InputReaders;


public class WordReader
{
    Spelling WelshSpelling;
    Spelling IrishSpelling;

    internal List<WordSet> ReadWords()
    {
        WelshSpelling = new();
        IrishSpelling = new();

        string fileName = "cognates.txt";
        string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Resources", fileName));
        FileStream s = new(filePath, FileMode.Open);
        StreamReader r = new StreamReader(s);


        List<WordSet> words = new();
        while (r.ReadLine() is { } line)
            words.Add(Phonetic(WordSet.Parse(line)));

        return words;
    }

    private WordSet Phonetic(WordSet words)
    {
        // Proto-Celtic forms are fine already
        words.OldIrish = IrishSpelling.Phonetic(words.OldIrish);
        words.MiddleWelsh = WelshSpelling.Phonetic(words.MiddleWelsh);

        return words;
    }
}
