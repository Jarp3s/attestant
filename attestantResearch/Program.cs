global using attestant;
using attestantResearch;
using attestantResearch.InputReaders;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<WordSet> wordSets = WordReader.FetchWords("cognates.txt");

var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Resources", "results.json");
List<object> wordList = new();
foreach (var wordSet in wordSets)
{
    wordSet.GenerateTrace();
    wordList.Add(wordSet.ToTrace());
}

var exportableWordList = new { wordSets = wordList };
File.WriteAllText(path, 
    JsonConvert.SerializeObject(exportableWordList, (Newtonsoft.Json.Formatting)Formatting.Indented));
