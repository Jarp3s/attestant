global using attestant;
using attestantResearch;
using attestantResearch.InputReaders;
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<WordSet> wordSets = WordReader.FetchWords("cognates.txt");

foreach (var wordSet in wordSets)
    wordSet.GenerateTrace();
