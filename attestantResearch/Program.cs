global using attestant;
using System.Text.RegularExpressions;
using attestant.Algorithms;
using attestant.InputReaders;
using attestantResearch;
using attestantResearch.Algorithms;
using attestantResearch.InputReaders;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<WordSet> wordSets = WordReader.ReadWords();

List<LanguageDevelopment> langDevs = SoundLawReader.GetLanguageDevelopments();
LanguageDevelopment irishDevelopment = langDevs[0];
LanguageDevelopment welshDevelopment = langDevs[1];
WordTransformer irishTransformer = new(irishDevelopment.SoundLaws);
WordTransformer welshTransformer = new(welshDevelopment.SoundLaws);

foreach (var wordSet in wordSets)
{
    wordSet.ConstructedIrish = irishTransformer.Transform(wordSet.ProtoCeltic);
    wordSet.ConstructedWelsh = welshTransformer.Transform(wordSet.ProtoCeltic);
    Console.WriteLine(wordSet.ToString());
}
