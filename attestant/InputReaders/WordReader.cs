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
        string read;

        while ((read = r.ReadLine()) != null)
        {
            words.Add(Parse(read));
        }

        return words;
    }

    private WordSet Parse(string cognates)
    {
        string[] split = cognates.Split(';');
        WordSet word = new(split[1].TrimStart(), split[2].TrimStart(), split[0]);
        word.ProtoCeltic.Replace('y', 'j');
        return word;
    }

    private WordSet Phonetic(WordSet words)
    {
        // Proto-Celtic forms are fine already
        words.OldIrish = IrishSpelling.Phonetic(words.OldIrish);
        words.MiddleWelsh = IrishSpelling.Phonetic(words.MiddleWelsh);

        return words;
    }
}
