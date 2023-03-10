namespace attestant.InputReaders;


public static class WordReader
{
    public static List<WordSet> ReadWords()
    {
        const string fileName = "cognates.txt";
        var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Resources", fileName));
        FileStream stream = new(filePath, FileMode.Open);
        StreamReader reader = new(stream);
        
        List<WordSet> words = new();
        while (reader.ReadLine() is { } line)
            words.Add(WordSet.Parse(line));

        return words;
    }
}
