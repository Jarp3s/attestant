using attestant.Utilities;

namespace attestant.Algorithms;


internal static class WordDistance
{
    // For memoization
    private static int[,] _table = null!;

    // Dynamic programming algorithm for calculating the Damerau-Levenshtein distance
    // The difference is that the substitution cost varies, depending on how much the sounds are alike (value between 0 and 1)
    public static int EditDistance(string word1, string word2)
    {
        _table = new int[word1.Length + 1, word2.Length + 1];

        for (var i = 0; i <= word1.Length; i++)
        for (var j = 0; j <= word2.Length; j++)
            _table[i, j] = -1; // Allows to check if the value has not been set yet
            
        return CalculateDistance(word1, word2, word1.Length, word2.Length);
    }

    private static int CalculateDistance(string word1, string word2, int i, int j)
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

    private static int CalculateSubstitutionCost(string word1, string word2, int i, int j)
    {

        uint phon1 = Phoneme.Embedding.Forward[word1[i]]; // To get the bit array of the sound
        uint phon2 = Phoneme.Embedding.Forward[word2[j]];

        var isConsonant1 = phon1 >> 31 == 1; // To see if the sound is a consonant
        var isConsonant2 = phon2 >> 31 == 1;

        if (isConsonant1 && isConsonant2)
            return CalculateBitSum(phon1 ^ phon2) / 16; // 17 bits used for consonants. One simply marks it as consonant.
        if (!isConsonant1 && !isConsonant2)
            return CalculateBitSum(phon1 ^ phon2) / 12; // 13 bits used for vowels. One simply marks it as vowel.
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
