namespace attestant.DataStructures;


/// <summary>
///    Bi-directional dictionary w/ (k1 ∈ T1, k2 ∈ T2)-pairs.
///    NOTE: can only be used consistently if f: T1 -> T2 is injective,
///    that is: ∀k1, k1' ∈ T1, k1 ≠ k1' => f(k1) ≠ f(k1').
/// </summary>

public class Table<T1, T2> where T1 : notnull where T2 : notnull
{
    private readonly Dictionary<T1, T2> _row = new();
    private readonly Dictionary<T2, T1> _column = new();

    public Indexer<T1, T2> Row { get; }
    public Indexer<T2, T1> Column { get; }

    public Table()
    {
        Row = new Indexer<T1, T2>(_row, _column);
        Column = new Indexer<T2, T1>(_column, _row);
    }

    public void Add(T1 k1, T2 k2)
    {
        _row.Add(k1, k2);
        _column.Add(k2, k1);
    }

    public class Indexer<T3, T4> where T3 : notnull where T4 : notnull
    {
        private readonly Dictionary<T3, T4> _dict1;
        private readonly Dictionary<T4, T3> _dict2;

        public Indexer(Dictionary<T3, T4> dict1, Dictionary<T4, T3> dict2)
        {
            _dict1 = dict1;
            _dict2 = dict2;
        }

        public T4 this[T3 index]
        {
            get => _dict1[index];
            set
            {
                _dict1[index] = value;
                _dict2[value] = index;
            }
        }
    }
}