using attestant;
using attestant.DataStructures;
using attestant.IPA;
using attestant.SoundLaws;

Console.OutputEncoding = System.Text.Encoding.UTF8;


List<HashSet<SoundLaw>> soundLaws = new();
Word word = new();

var dft = new DepthFirstTransformation(soundLaws);
HashSet<UNode<Word>> reconstructions = dft.TransformWord(word);
    