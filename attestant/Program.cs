using attestant;
using attestant.DataStructures;
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;


var law = Regex.Replace(Console.ReadLine()!, @"[\s*]", @"");
var segments = Regex.Split(law, @"[+>/]");
foreach (var seg in segments)
    Console.WriteLine(seg);

/*var result = Regex.Replace(Console.ReadLine()!, @"(.*)(_)(.*)", "$1x$3");
Console.WriteLine(result);*/

/*
List<SoundLaw> soundLaws = new();
const string word = "";

var dft = new DepthFirstTransformation(soundLaws);
HashSet<UNode<string>> reconstructions = dft.TransformWord(word);
*/