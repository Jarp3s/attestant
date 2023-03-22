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

foreach (WordSet ws in wordSets)
{
    ws.ConstructedIrish = irishTransformer.Transform(ws.ProtoCeltic);
    ws.ConstructedWelsh = welshTransformer.Transform(ws.ProtoCeltic);
    Console.WriteLine(ws.Print());
}

/*
List<WordSet> words = WordReader.ReadWords();
WordReconstructor wordReconstructor = new();
HashSet<string> reconstructedWords = wordReconstructor.Reconstruct(words[0]);

foreach(var word in reconstructedWords)
    Console.WriteLine(word);
*/