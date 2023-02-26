using attestant;
using attestant.IPA;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<HashSet<SoundLaw>> soundLaws = new();
List<Word> words = new();

var dft = new DepthFirstTransformation(soundLaws);
foreach (var word in words)
    dft.TransformWord(word);
    