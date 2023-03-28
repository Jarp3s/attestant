namespace attestant.DataStructures;


/// <summary>
///    Undirected node w/ link to both previous & next node.
/// </summary>
public class UNode<T1, T2>
{
    /// <summary>
    ///     The value assigned to the node.
    /// </summary>
    public T1 Value { get; }

    /// <summary>
    ///     The context-information assigned to the node.
    /// </summary>
    public T2 Label { get; }

    /// <summary>
    ///     The previous node
    /// </summary>
    public UNode<T1, T2>? Previous { get; }
    
    /// <summary>
    ///     The next node.
    /// </summary>
    public UNode<T1, T2>? Next { get; private set; }
    
    /// <summary>
    ///     The first node of the nodes linked to.
    /// </summary>
    public UNode<T1, T2> First { get; }

    public UNode(T1 value, T2 label = default!)
    {
        Value = value;
        Label = label;
        First = this;
    }

    private UNode(T1 value, T2 label, UNode<T1, T2> previous, UNode<T1, T2> first)
    {
        Value = value;
        Label = label;
        Previous = previous;
        First = first;
    }

    /// <summary>
    ///     Adds a new node with the given value & label.
    /// </summary>
    public UNode<T1, T2> Add(T1 value, T2 label)
    {
        UNode<T1, T2> node = new(value, label, this, First);
        Next = node;
        return node;
    }

    /// <summary>
    ///     Applies the given action on all nodes in the link by traversing down
    /// </summary>
    public void TraverseDown(Action<T1, T2> action)
    {
        action(Value, Label);
        Next?.TraverseDown(action);
    }

    /// <summary>
    ///     Applies the given action on all nodes in the link by traversing up.
    /// </summary>
    public void TraverseUp(Action<T1, T2> action)
    {
        action(Value, Label);
        Previous?.TraverseUp(action);
    }
}
