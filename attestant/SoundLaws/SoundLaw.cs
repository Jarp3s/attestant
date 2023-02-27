using attestant.IPA;

namespace attestant.SoundLaws;


public delegate List<Phoneme> SoundChange(List<Phoneme> phonemes);

public class SoundLaw
{
    private readonly SoundChange _soundChange;
    private readonly int _phonemeCount;

    public SoundLaw(Func<List<Phoneme>, List<Phoneme>> soundChange, int phonemeCount)
    {
        _soundChange = new SoundChange(soundChange);
        _phonemeCount = phonemeCount;
    }

    public Word Apply(Word word)
    {
        var transformedWord = new Word();
        
        for (var i = 0; i < word.Count - _phonemeCount; i++) // Note: cannot sequentially apply law in word
        {                                                    // thus: aea > aka > akk w/ VV -> kV
            List<Phoneme> oldPhonemes = word.GetRange(i, _phonemeCount);
            List<Phoneme> newPhonemes = _soundChange(oldPhonemes);
            transformedWord.AddRange(newPhonemes);
        }
        
        return transformedWord;
    }
}