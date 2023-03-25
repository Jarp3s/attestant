using attestant.DataStructures;

namespace attestantResearch;


public class WordSet
{
    public string OldIrish { get; set; }
    public string MiddleWelsh { get; set; }
    public string ProtoCeltic { get; set; }
    public string OldIrishSpelling { get; set; }
    public string MiddleWelshSpelling { get; set; }
    public string ProtoCelticSpelling { get; set; }

    public UNode<Word, SoundLaw> ConstructedIrish; // The Old Irish form that will be made using the laws
    public UNode<Word, SoundLaw> ConstructedWelsh; // The Middle Welsh form that will be made using the laws

    private WordSet(string oir, string mw, string pc)
    {
        OldIrishSpelling = oir;
        MiddleWelshSpelling = mw;
        ProtoCelticSpelling = pc;
    }

    public static WordSet Parse(string inputCognates)
    {
        var cognates = inputCognates.Split(';');
        return new WordSet(cognates[1].TrimStart(), cognates[2].TrimStart(), cognates[0]);
    }

    public override string ToString()
    {
        return
            "--------------------------------------------" +
            "\n" + ProtoCelticSpelling + " => " + ProtoCeltic +
            "\n" + OldIrishSpelling + " => " + OldIrish + " : " + ConstructedIrish.Value + " (" + ConstructedIrish.Value.EditDistance(OldIrish) + ')' + 
            "\n" + MiddleWelshSpelling + " => " + MiddleWelsh + " : " + ConstructedWelsh.Value + " (" + ConstructedWelsh.Value.EditDistance(MiddleWelsh) + ')';
    }
}
