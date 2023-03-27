global using attestant;
using attestantResearch;
using attestantResearch.InputReaders;
using System.Text.RegularExpressions;
using attestant.Utilities;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<WordSet> wordSets = WordReader.FetchWords("cognates.txt");

foreach (var wordSet in wordSets)
    wordSet.GenerateTrace();
/*
var law = SoundLaw.Parse("1. Ø > ʲ /C_");
Console.WriteLine(law._antecedent);
Console.WriteLine(law._consequent);

var wrd1 = new Word("wxxx");
var wrd2 = new Word("kʷʲxxx");
foreach (var num in wrd2.EmbeddedPhonemes)
    Console.WriteLine(num);

Console.WriteLine(law.Apply(wrd1));
Console.WriteLine(law.Apply(wrd2));
*/