using System.Text;
using System.Text.RegularExpressions;
using attestant.Utilities;

namespace attestant;


/// <summary>
///     IPA-encoded word, containing several representations in order to comply with different functionality.
/// </summary>
public class Word
{
    /// <summary>
    ///     A word represented as an array of phonemes.
    /// </summary>
    private readonly string _value;

    /// <summary>
    ///     A word represented as an array of binary feature embeddings.
    /// </summary>
    public ulong[] EmbeddedPhonemes
    {
        get
        {
            var completeReplaced = Regex.Replace(_value, @"\P{M}\p{M}+?", match 
                => Phoneme.Characterization.Forward[match.Value].ToString());
            completeReplaced = Regex.Replace(completeReplaced, @".ʷʲ+", match
                => Phoneme.Characterization.Forward[match.Value].ToString());
            completeReplaced = Regex.Replace(completeReplaced, @".[ʷʲ]+", match
                => Phoneme.Characterization.Forward[match.Value].ToString());
            
            return completeReplaced.Select(phoneme 
                => Phoneme.Embedding.Forward[Regex.Replace(phoneme.ToString(), @"[Ⅰ-Ⅹᚠ-ᛪ]", match 
                    => Phoneme.Characterization.Reverse[char.Parse(match.Value)])]).ToArray();
        }
    }
    
    /// <summary>
    ///     A word represented as an array of characterized phonemes.
    /// </summary>
    public string CharacterizedPhonemes
    {
        get
        {
            var partialReplaced = Regex.Replace(_value, @"\P{M}\p{M}+?", match
                => Phoneme.Characterization.Forward[match.Value].ToString());
            partialReplaced = Regex.Replace(partialReplaced, @".ʷʲ+", match
                => Phoneme.Characterization.Forward[match.Value].ToString());
            partialReplaced = Regex.Replace(partialReplaced, @".[ʷʲ]+", match
                => Phoneme.Characterization.Forward[match.Value].ToString());
            return Regex.Replace(partialReplaced, @".ʷ", match
                => Phoneme.Characterization.Forward[match.Value].ToString());
        }
    }

    public int Length => EmbeddedPhonemes.Length;

    public Word(string value)
    {
        _value = value.Normalize(NormalizationForm.FormC);
    }

    public override string ToString() => _value;

    public static implicit operator string(Word wrd) => wrd._value;

    public static implicit operator Word(string str) => new(str);

    // Memoization Matrix
    private static float[,] _table = null!;

    public float NormalizedEditDistance(Word other)
        => EditDistance(other) / Math.Max(Length, other.Length);

    /// <summary>
    ///     Dynamic programming algorithm that calculates the Levenshtein distance, where
    ///     the substitution cost between 2 phonemes varies, depending on the similarity reflected in their encoding.
    /// </summary>
    public float EditDistance(Word other)
    {
        var otherEmbeddedPhonemes = other.EmbeddedPhonemes;
        _table = new float[EmbeddedPhonemes.Length + 1, otherEmbeddedPhonemes.Length + 1];

        for (var i = 0; i <= EmbeddedPhonemes.Length; i++)
            for (var j = 0; j <= otherEmbeddedPhonemes.Length; j++)
                _table[i, j] = -1; // Allows to check if the value has not been set yet
            
        return CalculateDistance(EmbeddedPhonemes, otherEmbeddedPhonemes, EmbeddedPhonemes.Length, otherEmbeddedPhonemes.Length);
    }

    private static float CalculateDistance(ulong[] word1, ulong[] word2, int i, int j)
    {
        if (i is 0)
            return j;
        if (j is 0)
            return i;
        if (_table[i, j] is not -1)
            return _table[i, j];

        if (word1[i - 1] == word2[j - 1])
            return CalculateDistance(word1, word2, i - 1, j - 1);

        float insertionCost = CalculateDistance(word1, word2, i, j - 1) + 1;
        float deletionCost = CalculateDistance(word1, word2, i - 1, j) + 1;
        float substitutionCost = CalculateDistance(word1, word2, i - 1, j - 1) + CalculateSubstitutionCost(word1, word2, i-1, j-1);

        if (CalculateSubstitutionCost(word1, word2, i - 1, j - 1) > 1)
            throw new ArgumentOutOfRangeException();

        float distance = Math.Min(Math.Min(deletionCost, insertionCost), substitutionCost);

        _table[i, j] = distance;
        return distance;
    }

    private static float CalculateSubstitutionCost(ulong[] word1, ulong[] word2, int i, int j)
    {
        var phoneme1 = word1[i]; // To get the bit array of the sound
        var phoneme2 = word2[j];

        var isConsonant1 = phoneme1 >> 63 == 1; // To see if the sound is a consonant
        var isConsonant2 = phoneme2 >> 63 == 1;

        if (isConsonant1 && isConsonant2)
        {
            float bitsum = CalculateBitSum(phoneme1 ^ phoneme2);
            if (bitsum > 12)
                throw new ArgumentOutOfRangeException();
            return bitsum / 12f; // Maximally 12 bits difference for consonants.
        }
        if (!isConsonant1 && !isConsonant2)
        {
            float bitsum = CalculateBitSum(phoneme1 ^ phoneme2);
            if (bitsum > 8)
                throw new ArgumentOutOfRangeException();
            return bitsum / 8f; // Maximally 8 bits difference for vowels.
        }
        return 1; // If a vowel and consonant, the difference is max
    }

    private static int CalculateBitSum(ulong difference)
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