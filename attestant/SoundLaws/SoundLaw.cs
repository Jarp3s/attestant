namespace attestant.SoundLaws;


//public delegate List<int> SoundChange(List<int> phonemes);

public class SoundLaw
{
    private readonly List<Phoneme> _antecedent;
    private readonly List<Phoneme> _consequent;
    private int PhonemeCount => _antecedent.Count;

    public SoundLaw(List<Phoneme> antecedent, List<Phoneme> consequent)
    {
        _antecedent = antecedent;
        _consequent = consequent;
    }

    public Word ApplyOnWord(Word word)
    {
        var transformedWord = new Word();
        
        for (var i = 0; i < word.Count - PhonemeCount; i++) // Note: cannot sequentially apply law in word
        {                                                   // thus: aea > aka > akk w/ VV -> kV
            List<Phoneme> oldPhonemes = word.GetRange(i, PhonemeCount);
            List<Phoneme> newPhonemes = ApplyOnPhonemes(oldPhonemes);
            transformedWord.AddRange(newPhonemes);
        }
        
        return transformedWord;
    }

    private List<Phoneme> ApplyOnPhonemes(IReadOnlyList<Phoneme> phonemes)
    {
        for (var i = 0; i < PhonemeCount; i++)
        {
            var wrdPhoneme = phonemes[i];
            var lawPhoneme = _antecedent[i];
            // TODO: Compare phonemes in equality;
            //       if not applicable: return phonemes;
        }
        
        return _consequent;
            
        // Phoneme equality:
        // 0b_0100_0010_0000_0001_0000_0000_0100_0000
        // 0b_0100_0010_0000_0001_0000_0000_0100_0000
        // ------------------------------------------ =
        // true
    
        // All-equality:
        // 0b_0100_0010_0000_0001_0000_0000_0100_0000
        // 0b_0100_0010_0000_0000_0000_0000_0000_0000
        // ------------------------------------------ &
        // 0b_0100_0010_0000_0000_0000_0000_0000_0000
        // 0b_0100_0010_0000_0000_0000_0000_0000_0000
        // ------------------------------------------ =
        // true
    
        // ≥1-equality:
        // 0b_0100_0010_0000_0001_0000_0000_0100_0000
        // 0b_1110_0000_0000_0000_0000_0000_0000_0000
        // ------------------------------------------ ^
        // 0b_1010_0010_0000_0001_0000_0000_0100_0000
        // 0b_1110_0000_0000_0000_0000_0000_0000_0000
        // ------------------------------------------ &
        // 0b_1010_0000_0000_0000_0000_0000_0000_0000
        // 0b_1110_0000_0000_0000_0000_0000_0000_0000
        // ------------------------------------------ !=
        // true
    }
}