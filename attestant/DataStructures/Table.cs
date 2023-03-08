namespace attestant.DataStructures;


/// <summary>
///    Bi-directional dictionary w/ (k1 ∈ T1, k2 ∈ T2)-pairs.
///    NOTE: can only be used consistently if f: T1 -> T2 is injective,
///    that is: ∀k1, k1' ∈ T1, k1 ≠ k1' => f(k1) ≠ f(k1').
/// </summary>
public class Table<T1, T2> where T1 : notnull where T2 : notnull
{
    private readonly Dictionary<T1, T2> _forward = new();
    private readonly Dictionary<T2, T1> _reverse = new();

    /// <summary>
    ///     The indexer reasoning from k1 ∈ T1 -> k2 ∈ T2.
    /// </summary>
    public Indexer<T1, T2> Forward { get; }
    
    /// <summary>
    ///     The indexer reasoning from k2 ∈ T2 -> k1 ∈ T1.
    /// </summary>
    public Indexer<T2, T1> Reverse { get; }

    public Table(params (T1, T2)[] entries)
    {
        Forward = new Indexer<T1, T2>(_forward, _reverse);
        Reverse = new Indexer<T2, T1>(_reverse, _forward);

        foreach ((T1 k1, T2 k2) entry in entries)
            Add(entry.k1, entry.k2);
    }

    /// <summary>
    ///     Adds a (k1 ∈ T1, k2 ∈ T2)-pair.
    /// </summary>
    public void Add(T1 k1, T2 k2)
    {
        _forward.Add(k1, k2);
        _reverse.Add(k2, k1);
    }

    /// <summary>
    ///     Indexer that encapsulates functionality of a Table
    ///     to perform indexed additions & changes on 1 dictionary.
    /// </summary>
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
