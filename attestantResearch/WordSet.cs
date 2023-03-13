namespace attestantResearch;


public class WordSet
{
    public string OldIrish; 
    public string MiddleWelsh; 
    public string ProtoCeltic;
    public string Reconstructed { get; } = null!;

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
}
