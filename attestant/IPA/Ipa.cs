using attestant.DataStructures;

namespace attestant.IPA;


public record Ipa
{
    public static Table<char, int> EncodingTable = new ();
}