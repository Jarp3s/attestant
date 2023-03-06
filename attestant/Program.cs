using attestant;
using attestant.DataStructures;
using System.Text.RegularExpressions;
using attestant.InputReaders;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<LanguageDevelopment> languageDevelopments = SoundLawReader.GetLanguageDevelopments();
const string word = "";

List<HashSet<UNode<string>>> reconstructions = new();
foreach (var lanDev in languageDevelopments)
{
    var dft = new DepthFirstTransformation(lanDev.SoundLaws);
    reconstructions.Add(dft.TransformWord(word));
}
