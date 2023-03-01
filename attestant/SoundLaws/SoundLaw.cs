namespace attestant.SoundLaws;


public class SoundLaw
{
    private readonly List<Symbol> _antecedent;
    private readonly List<Symbol> _consequent;
    private int SymbolCount => _antecedent.Count;

    public SoundLaw(List<Symbol> antecedent, List<Symbol> consequent)
    {
        _antecedent = antecedent;
        _consequent = consequent;
    }

    public static HashSet<SoundLaw> Parse(string law)
    {
        throw new NotImplementedException();
    }

    public Word ApplyOnWord(Word word)
    {
        var transformedWord = new Word();
        
        for (var i = 0; i < word.Count - SymbolCount; i++) // Note: does not apply law correctly in both
        {                                                  // recursive & consecutive way
            List<Symbol> phonemes = word.GetRange(i, SymbolCount);

            if (IsApplicable(phonemes))
                transformedWord.AddRange(_consequent);
            else
                transformedWord.Add(word[i]);
        }
        
        return transformedWord;
    }

    private bool IsApplicable(IReadOnlyList<Symbol> phonemes)
    {
        for (var i = 0; i < SymbolCount; i++)
        {
            var wrdPhoneme = phonemes[i];
            var lawSymbol = _antecedent[i];

            if (!Symbol.BitOperations[lawSymbol.SymbolType](wrdPhoneme.Encoding, lawSymbol.Encoding))
                return false;
        }
        
        return true;
    }
}