global using attestant;
using attestant.Algorithms;
using attestant.InputReaders;
using attestantResearch;
using attestantResearch.InputReaders;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine((long)0b_0000_0000_0000_0000_0000_0000_0000_0001);

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
    wordSet.OldIrish = irishPhoneticizer.Transform(wordSet.OldIrish).Value;
    wordSet.MiddleWelsh = welshPhoneticizer.Transform(wordSet.MiddleWelsh).Value;
}

foreach (var wordSet in wordSets)
{
    wordSet.ConstructedIrish = irishTransformer.Transform(wordSet.ProtoCeltic);
    wordSet.ConstructedWelsh = welshTransformer.Transform(wordSet.ProtoCeltic);
    Console.WriteLine(wordSet.ToString());
}
