using attestant.DataStructures;

namespace attestant;


public record struct Phoneme(int Encoding);

public class Word : List<Phoneme>
{
}

public record Ipa
{
    public static Table<char, int> EncodingTable = new (
        // Examples of entries:
        ('a', 0b_0000_0000_0000_0000_0000_0000_0000_0000),
        ('b', 0b_0000_0000_0000_0000_0000_0000_0000_0001),
        ('c', 0b_0000_0000_0000_0000_0000_0000_0000_0010),
        ('d', 0b_0000_0000_0000_0000_0000_0000_0000_0100)
        
    );
}