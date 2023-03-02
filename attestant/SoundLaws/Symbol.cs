using attestant.DataStructures;

namespace attestant.SoundLaws;


public enum SymbolType
{
    Phoneme,
    CoverAnd,
    CoverOr
}

public enum SymbolFrequency
{
    Once,
    Optional,
    OptionalMultiple,
    Multiple
}

public readonly record struct Symbol(uint Encoding, SymbolType SymbolType, SymbolFrequency SymbolFrequency)
{
    private delegate bool BitOperation(uint s1, uint s2);

    private BitOperation Operation => SymbolType switch
    {
        SymbolType.Phoneme  => (s1, s2) => s1 == s2,
        SymbolType.CoverAnd => (s1, s2) => (s1 & s2) == s2,
        SymbolType.CoverOr  => (s1, s2) => ((s1 ^ s2) & s2) != s2,
        _                   => throw new ArgumentOutOfRangeException()
    };

    public int? GiveNextIndexIfApplicable(Word word, int index)
    {
        switch (SymbolFrequency)
        {
            case SymbolFrequency.Once:
                return Operation(Encoding, word[index].Encoding) ? ++index : null;
            
            case SymbolFrequency.Optional:
                return Operation(Encoding, word[index].Encoding) ? ++index : index;
            
            case SymbolFrequency.OptionalMultiple:
                while (Operation(Encoding, word[index].Encoding))
                    index++;
                return index;
            
            case SymbolFrequency.Multiple:
                var i = index;
                while (Operation(Encoding, word[i].Encoding))
                    i++;
                return i == index ? null : i;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static readonly Table<string, uint> IpaTable = new (
        // Examples of entries:
        ("p", 0b_1011_0000_0001_0000_0000_0000_0000_0100),
        ("b", 0b_1101_0000_0001_0000_0000_0000_0000_0100),
        ("m", 0b_1101_0000_0000_1000_0000_0000_0000_0100),
        ("m̥", 0b_1011_0000_0000_1000_0000_0000_0000_0100),
        ("ɸ", 0b_1011_0000_0000_0100_0000_0000_0000_0100),
        ("β", 0b_1101_0000_0000_0100_0000_0000_0000_0100),
        ("f", 0b_1010_1000_0000_0100_0000_0000_0000_0100),
        ("v", 0b_1100_1000_0000_0100_0000_0000_0000_0100),
        ("t", 0b_1010_0110_0001_0000_0000_0000_0000_0100),
        ("d", 0b_1100_0110_0001_0000_0000_0000_0000_0100),
        ("n", 0b_1100_0110_0000_1000_0000_0000_0000_0100),
        ("n̥", 0b_1010_0110_0000_1000_0000_0000_0000_0100),
        ("r", 0b_1100_0110_0000_0000_1000_0000_0000_0100),
        ("r̥", 0b_1010_0110_0000_0000_1000_0000_0000_0100),
        ("θ", 0b_1010_0100_0000_0100_0000_0000_0000_0100),
        ("ð", 0b_1100_0100_0000_0100_0000_0000_0000_0100),
        ("s", 0b_1010_0010_0000_0100_0000_0000_0000_0100),
        ("z", 0b_1100_0010_0000_0100_0000_0000_0000_0100),
        ("l", 0b_1100_0110_0000_0011_1000_0000_0000_0100),
        ("ɬ", 0b_1100_0110_0000_0101_1000_0000_0000_0100),
        ("j", 0b_1100_0001_0000_0010_0000_0000_0000_0100),
        ("k", 0b_1010_0000_1001_0000_0000_0000_0000_0100),
        ("g", 0b_1100_0000_1001_0000_0000_0000_0000_0100),
        ("ŋ", 0b_1100_0000_1000_1000_0000_0000_0000_0100),
        ("ŋ̊", 0b_1010_0000_1000_1000_0000_0000_0000_0100),
        ("x", 0b_1010_0000_1000_0100_0000_0000_0000_0100),
        ("ɣ", 0b_1100_0000_1000_0100_0000_0000_0000_0100),
        ("h", 0b_1010_0000_0100_0100_0000_0000_0000_0100),
        ("kʷ",0b_1010_0000_1001_0000_0000_0000_0000_1000),
        ("gʷ",0b_1100_0000_1001_0000_0000_0000_0000_1000),
        ("w", 0b_1100_0000_0010_0010_0000_0000_0000_1000),
        ("i", 0b_0100_0000_0000_0000_0110_0100_0000_0100),
        ("y", 0b_0100_0000_0000_0000_0110_0100_0000_1000),
        ("ɨ", 0b_0100_0000_0000_0000_0101_0100_0000_0100),
        ("ʉ", 0b_0100_0000_0000_0000_0101_0100_0000_1000),
        ("u", 0b_0100_0000_0000_0000_0100_1100_0000_1000),
        ("e", 0b_0100_0000_0000_0000_0110_0001_0000_0100),
        ("ø", 0b_0100_0000_0000_0000_0110_0001_0000_1000),
        ("o", 0b_0100_0000_0000_0000_0100_1001_0000_1000),
        ("ɛ", 0b_0100_0000_0000_0000_0110_0000_0100_0100),
        ("ɔ", 0b_0100_0000_0000_0000_0100_1000_0100_1000),
        ("æ", 0b_0100_0000_0000_0000_0110_0000_0010_0100),
        ("a", 0b_0100_0000_0000_0000_0100_1000_0001_0100),
        ("ɑ", 0b_0100_0000_0000_0000_0100_1000_0001_0100),
        ("ə", 0b_0100_0000_0000_0000_0101_0000_1000_0100),
        ("#", 0b_0000_0000_0000_0000_0000_0000_0000_0000) // word boundary
    );
}