using System.Text.Json;
using System.Text.Json.Serialization;

namespace attestant.InputReaders;


/// <summary>
///     Input reader that reads & parses sound laws from JSON-files.
/// </summary>
public static class SoundLawReader
{
    /// <summary>
    ///     Reads JSON from specified files, parses it to language-developments;
    ///     which are used to store information about the development within a language.
    ///     Returns these language-developments.
    /// </summary>
    public static List<LanguageDevelopment> GetLanguageDevelopments()
    {
        List<LanguageDevelopment> languageDevelopments = new()
        {
            FetchDevelopment("OldIrishLaws.json"),
            FetchDevelopment("MiddleWelshLaws.json")
        };

        return languageDevelopments;
    }

    public static List<LanguageDevelopment> GetSpelling()
    {
        List<LanguageDevelopment> spellings = new()
        {
            FetchDevelopment("OldIrishSpelling.json"),
            FetchDevelopment("MiddleWelshSpelling.json")
        };

        return spellings;
    }

    private static LanguageDevelopment FetchDevelopment(string fileName)
    {
        var currentDirectoryPath = Directory.GetCurrentDirectory();

        var filePath =
            Path.GetFullPath(Path.Combine(currentDirectoryPath, "..", "..", "..", "..", "Resources", fileName));
        var jsonString = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<LanguageDevelopment>(jsonString)!;
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
        [JsonPropertyName("number")]
        public string Number { get; set; } = null!;

        [JsonPropertyName("law")]
        public string Law { get; set; } = null!;

        public SoundLaw ToSoundLaw() => SoundLaw.Parse($"{Number}. {Law}");
    }
}
