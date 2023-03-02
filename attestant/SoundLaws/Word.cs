namespace attestant.SoundLaws;


public class Word : List<Symbol>
{
    public static Word Parse(string inputWord)
    {
        return (Word) inputWord.Select(
            phoneme => new Symbol(Symbol.IpaTable.Row[phoneme], SymbolType.Phoneme, SymbolFrequency.Once));
    }
}