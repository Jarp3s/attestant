namespace attestant;


public class WordSet
{
    private readonly string _oldIrish; 
    private readonly string _middleWelsh; 
    private readonly string _protoCeltic;
    public string Reconstructed { get; } = null!;

    private WordSet(string oldIrish, string middleWelsh, string protoCeltic)
    { 
        _oldIrish = oldIrish; 
        _middleWelsh = middleWelsh; 
        _protoCeltic = protoCeltic;
    }
    
    public static WordSet Parse(string inputCognates)
    {
        var cognates = inputCognates.Split(';');
        return new WordSet(cognates[1].TrimStart(), cognates[2].TrimStart(), cognates[0]);
    }
}
