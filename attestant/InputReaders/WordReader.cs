namespace attestant.InputReaders;


public class WordReader
{
    internal List<WordSet> ReadWords()
    {
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
        return word;
    }
}
