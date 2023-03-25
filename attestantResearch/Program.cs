global using attestant;
using attestant.Algorithms;
using attestant.InputReaders;
using attestantResearch;
using attestantResearch.InputReaders;

Console.OutputEncoding = System.Text.Encoding.UTF8;

List<WordSet> wordSets = WordReader.ReadWords();

List<LanguageDevelopment> langDevs = SoundLawReader.GetLanguageDevelopments();
LanguageDevelopment irishDevelopment = langDevs[0];
LanguageDevelopment welshDevelopment = langDevs[1];
WordTransformer irishTransformer = new(irishDevelopment.SoundLaws);
WordTransformer welshTransformer = new(welshDevelopment.SoundLaws);

List<LanguageDevelopment> spellingDevs = SoundLawReader.GetSpelling();
LanguageDevelopment irishSpelling = spellingDevs[0];
LanguageDevelopment welshSpelling = spellingDevs[1];
WordTransformer irishPhoneticizer = new(irishSpelling.SoundLaws);
WordTransformer welshPhoneticizer = new(welshSpelling.SoundLaws);

foreach (var wordSet in wordSets)
{
    wordSet.OldIrish = irishPhoneticizer.Transform(wordSet.OldIrishSpelling).Value;
    wordSet.MiddleWelsh = welshPhoneticizer.Transform(wordSet.MiddleWelshSpelling).Value;
    wordSet.ProtoCeltic = wordSet.ProtoCelticSpelling;
}

int i = 0;

foreach (var wordSet in wordSets)
{
    i++;
    wordSet.ConstructedIrish = irishTransformer.Transform(wordSet.ProtoCeltic);
    wordSet.ConstructedWelsh = welshTransformer.Transform(wordSet.ProtoCeltic);
    Console.WriteLine($"[{i}]" + wordSet.ToString());
}
