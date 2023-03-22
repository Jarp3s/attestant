using attestant.DataStructures;
using System.Runtime.CompilerServices;

namespace attestantResearch;


public class WordSet
{
    public string OldIrish; 
    public string MiddleWelsh; 
    public string ProtoCeltic;

    public UNode<Word, SoundLaw> ConstructedIrish; // The Old Irish form that will be made using the laws
    public UNode<Word, SoundLaw> ConstructedWelsh; // The Middle Welsh form that will be made using the laws

    private WordSet(string oir, string mw, string pc)
    { 
        OldIrish = oir; 
        MiddleWelsh = mw; 
        ProtoCeltic = pc;
    }
    
    public static WordSet Parse(string inputCognates)
    {
        var cognates = inputCognates.Split(';');
        return new WordSet(cognates[1].TrimStart(), cognates[2].TrimStart(), cognates[0]);
    }

    public string Print()
    {
        return ProtoCeltic + "| " + OldIrish + "; " + ConstructedIrish.Value.Print() + "; " + ConstructedIrish.Value.EditDistance(new Word(OldIrish)) + "| " + MiddleWelsh + "; " + ConstructedWelsh.Value.Print() + "; " + ConstructedWelsh.Value.EditDistance(new Word(MiddleWelsh));
    }
}
