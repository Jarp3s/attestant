using System.Text.Json;
using System.Text.Json.Serialization;

namespace attestant.InputReaders;


public static class SoundLawReader
{
    public static List<LanguageDevelopment> GetLanguageDevelopments()
    {
        List<LanguageDevelopment> languageDevelopments = new();
        var currentDirectoryPath = Directory.GetCurrentDirectory();

        var fileName = "WelshLaws.json";
        var filePath = 
            Path.GetFullPath(Path.Combine(currentDirectoryPath, "..", "..", "..", "..", "Resources", fileName)); 
        var jsonString = File.ReadAllText(filePath);
        languageDevelopments.Add(JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!);
            
        fileName = "OldIrishLaws.json";
        filePath = 
            Path.GetFullPath(Path.Combine(currentDirectoryPath, "..", "..", "..", "..", "Resources", fileName));
        jsonString = File.ReadAllText(filePath);
        languageDevelopments.Add(JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!);
            
        return languageDevelopments;
    }
}

public class LanguageDevelopment
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = null!;
    
    [JsonPropertyName("soundLawRepresentations")]
    public List<SoundLawRepresentation> SoundLawRepresentations { get; set; } = null!;

    public List<SoundLaw> SoundLaws 
        => SoundLawRepresentations.Select(slr => slr.ToSoundLaw()).ToList();
    
    public class SoundLawRepresentation
    {
        [JsonPropertyName("law")]
        public string Law { get; set; } = null!;

        public SoundLaw ToSoundLaw()
        {
            return SoundLaw.Parse(Law);
        }
    }
}
