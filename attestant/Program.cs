using attestant;
using attestant.DataStructures;
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<SoundLaw> soundLaws = new();
const string word = "";

var dft = new DepthFirstTransformation(soundLaws);
HashSet<UNode<string>> reconstructions = dft.TransformWord(word);
