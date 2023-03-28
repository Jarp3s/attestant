global using attestant;
using attestantResearch;
using attestantResearch.InputReaders;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<WordSet> wordSets = WordReader.FetchWords("cognates.txt");

var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Resources", "results");
List<object> traces = new();
foreach (var wordSet in wordSets)
{
    wordSet.GenerateTrace();
    traces.Add(wordSet.ToTrace());
}
File.WriteAllText(path, 
    JsonConvert.SerializeObject(traces, (Newtonsoft.Json.Formatting)Formatting.Indented));
