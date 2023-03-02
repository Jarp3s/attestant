using System.Data;

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

    public static HashSet<SoundLaw> Parse(string inputLaw)
    {
        throw new NotImplementedException();
    }

    public Word ApplyOnWord(Word word)
    {
        var transformedWord = new Word();
        int? curIndex = 0;

        // Note: does not apply law correctly in both
        // recursive & consecutive way
        while (curIndex < word.Count - SymbolCount)
        {
            var newIndex = curIndex;
            
            foreach (var symbol in _antecedent)
            {
                newIndex = newIndex is null 
                    ? throw new InvalidExpressionException() 
                    : symbol.GiveNextIndexIfApplicable(word, (int)newIndex);

                if (newIndex is null)
                    break;
            }

            if (newIndex is null)
            {
                var symbol = curIndex is null
                    ? throw new InvalidExpressionException()
                    : word[(int)curIndex];
                transformedWord.Add(symbol);
                curIndex++;
            }
            else
            {
                transformedWord.AddRange(_consequent);
                curIndex = newIndex;
            }
        }
        
        return transformedWord;
    }
}