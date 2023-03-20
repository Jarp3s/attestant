using System.Text;
using System.Text.RegularExpressions;
using attestant.Utilities;

namespace attestant;


public class Word : IEquatable<Word>
{
    public string Phonemes { get; }

    internal uint[] EmbeddedPhonemes
        => CharacterizedPhonemes.Select(phoneme 
            => Phoneme.Embedding.Forward[Phoneme.Characterization.Reverse[phoneme]]).ToArray();

    internal string CharacterizedPhonemes 
        => Regex.Replace(Phonemes, @"\P{M}\p{M}+", match 
            => Phoneme.Characterization.Forward[match.Value].ToString());

    public Word(string phonemes)
    {
        Phonemes = phonemes.Normalize(NormalizationForm.FormC);
    }

    public bool Equals(Word? other) => Phonemes == other?.Phonemes;

    // For memoization
    private static int[,] _table = null!;

    // Dynamic programming algorithm for calculating the Damerau-Levenshtein distance
    // The difference is that the substitution cost varies, depending on how much the sounds are alike (value between 0 and 1)
    public int EditDistance(Word other)
    {
        var otherEmbeddedPhonemes = other.EmbeddedPhonemes;
        _table = new int[otherEmbeddedPhonemes.Length + 1, otherEmbeddedPhonemes.Length + 1];

        for (var i = 0; i <= otherEmbeddedPhonemes.Length; i++)
            for (var j = 0; j <= otherEmbeddedPhonemes.Length; j++)
                _table[i, j] = -1; // Allows to check if the value has not been set yet
            
        return CalculateDistance(EmbeddedPhonemes, otherEmbeddedPhonemes, EmbeddedPhonemes.Length, otherEmbeddedPhonemes.Length);
    }

    private static int CalculateDistance(uint[] word1, uint[] word2, int i, int j)
    {
        if (i is 0)
            return j;
        if (j is 0)
            return i;
        if (_table[i, j] is not -1)
            return _table[i, j];

        if (word1[i - 1] == word2[j - 1])
            return CalculateDistance(word1, word2, i - 1, j - 1);

        var insertionCost = CalculateDistance(word1, word2, i, j - 1) + 1;
        var deletionCost = CalculateDistance(word1, word2, i - 1, j) + 1;
        var substitutionCost = CalculateDistance(word1, word2, i - 1, j - 1) + CalculateSubstitutionCost(word1, word2, i-1, j-1);
            
        var distance = Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);

        if (i > 1 && j > 1 && word1[i - 1] == word2[j - 2] && word1[i - 2] == word2[j - 1])
        {
            int transposition = CalculateDistance(word1, word2, i - 2, j - 2) + 1;
            distance = Math.Min(distance, transposition);
        }

        _table[i, j] = distance;
        return distance;
    }

    private static int CalculateSubstitutionCost(uint[] word1, uint[] word2, int i, int j)
    {

        var phoneme1 = word1[i]; // To get the bit array of the sound
        var phoneme2 = word2[j];

        var isConsonant1 = phoneme1 >> 31 == 1; // To see if the sound is a consonant
        var isConsonant2 = phoneme2 >> 31 == 1;

        if (isConsonant1 && isConsonant2)
            return CalculateBitSum(phoneme1 ^ phoneme2) / 16; // 17 bits used for consonants. One simply marks it as consonant.
        if (!isConsonant1 && !isConsonant2)
            return CalculateBitSum(phoneme1 ^ phoneme2) / 12; // 13 bits used for vowels. One simply marks it as vowel.
        return 1; // If a vowel and consonant, the difference is max
    }

    private static int CalculateBitSum(uint difference)
    {
        var count = 0;
        while (difference > 0) // Each iteration, get the first bit, then bitshift
        {
            count += (int) (difference & 1);
            difference >>= 1;
        }
        return count;
    }
}