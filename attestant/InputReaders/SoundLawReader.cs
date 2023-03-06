using System.Text.Json;

namespace attestant.InputReaders;


public static class SoundLawReader
{
    public static List<LanguageDevelopment> GetLanguageDevelopments()
    {
        List<LanguageDevelopment> languageDevelopments = new();

        var currentDirectoryPath = Directory.GetCurrentDirectory();
            
        var fileName = "WelshLaws.json";
        var filePath = 
            Path.GetFullPath(Path.Combine(currentDirectoryPath, "..", "..", "Resources", fileName)); 
        var jsonString = File.ReadAllText(filePath);
        languageDevelopments.Add(JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!);
            
        fileName = "OldIrishLaws.json";
        filePath = 
            Path.GetFullPath(Path.Combine(currentDirectoryPath, "..", "..", "Resources", fileName));
        jsonString = File.ReadAllText(filePath);
        languageDevelopments.Add(JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!);
            
        return languageDevelopments;
    }
}

public class LanguageDevelopment
{
    public string Language { get; } = null!;
    private SoundLawRepresentation[] SoundLawRepresentations { get; } = null!;
    public List<SoundLaw> SoundLaws 
        => (List<SoundLaw>) SoundLawRepresentations.Select(slr => slr.ToSoundLaw());
    
    public class SoundLawRepresentation
    {
        private readonly string _soundLaw = null!;

        public SoundLaw ToSoundLaw()
        {
            return SoundLaw.Parse(_soundLaw);
        }
    }
}
