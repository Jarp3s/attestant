using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attestant.Utilities
{
    internal static class WordDistance
    {
        // For memoisation
        private int[,] table;

        // Dynamic programming algorithm for calculating the Damerau-Levenshtein distance
        // The difference is that the substitution cost varies, depending on how much the sounds are alike (value between 0 and 1)
        public int Distance(string word1, string word2)
        {
            table = new int[word1.Length + 1, word2.Length + 1];

            for (int i = 0; i <= word1.Length; i++)
            {
                for (int j = 0; j <= word2.Length; j++)
                {
                    table[i, j] = -1; // Allows to check if the value has not been set yet
                }
            }

            return CalculateDistance(word1, word2, word1.Length, word2.Length);
        }

        private int CalculateDistance(string word1, string word2, int i, int j)
        {
            if (i == 0)
            {
                return j;
            }
            if (j == 0)
            {
                return i;
            }
            if (table[i, j] != -1)
            {
                return table[i, j];
            }

            if (word1[i - 1] == word2[j - 1])
            {
                return ComputeDistance(word1, word2, i - 1, j - 1);
            }

            int insertion = CalculateDistance(word1, word2, i, j - 1) + 1
            int deletion = CalculateDistance(word1, word2, i - 1, j) + 1;;
            int substitution = CalculateDistance(word1, word2, i - 1, j - 1) + Cost(word1, word2, i-1, j-1);
            
            int distance = Math.Min(
                Math.Min(deletion, insertion),
                substitution);

            if (i > 1 && j > 1 && word1[i - 1] == word2[j - 2] && word1[i - 2] == word2[j - 1])
            {
                int transposition = CalculateDistance(word1, word2, i - 2, j - 2) + 1;
                distance = Math.Min(distance, transposition);
            }

            table[i, j] = distance;
            return distance;
        }

        private int Cost(string word1, string word2, int i, int j)
        {

            uint phon1 = Phoneme.Embedding(word1[i]); // To get the bit array of the sound
            uint phon2 = Phoneme.Embedding(word2[j]);

            bool cons1 = phon1 >> 31 == 1 // To see if the sound is a consonant
            bool cons2 = phon2 >> 31 == 1

            if (cons1 && cons2)
                return BitSum(phon1 ^ phon2) / 17 // 17 bits used for consonants
            else if (!cons1 && !cons2)
                return BitSum(phon1 ^ phon2) / 13 // 13 bits used for vowels
            else return 1; // If a vowel and consonant, the difference is max
        }

        private int BitSum(uint difference)
        {
            int count = 0;
            while (diff > 0) // Each iteration, get the first bit, then bitshift
            {
                count += (int)(n & 1);
                diff >>= 1;
            }
            return count;
        }
    }

}
