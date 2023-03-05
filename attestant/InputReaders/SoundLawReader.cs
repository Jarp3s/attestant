using System.Text.Json;

namespace attestant.InputReaders;


public static class SoundLawReader
{
    public static List<LanguageDevelopment> LanguageDevelopments
    {
        get
        {
            List<LanguageDevelopment> languageDevelopments = new();

            var fileName = "";
            var jsonString = File.ReadAllText(fileName);
            languageDevelopments.Add(JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!);
            
            fileName = "";
            jsonString = File.ReadAllText(fileName);
            languageDevelopments.Add(JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!);
            
            return languageDevelopments;
        }
    }
}

public class LanguageDevelopment
{
    public string Language { get; } = null!;
    public SoundLaw[] SoundLaws { get; } = null!;

    public class SoundLaw
    {
        public string Number { get; } = null!;
        public string Law { get; } = null!;
        public string Context { get; } = null!;
    }
}