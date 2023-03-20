namespace attestant.DataStructures;


/// <summary>
///    Undirected node w/ both link to direct ancestor & direct descendants.
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
    private T2 Label { get; }

    /// <summary>
    ///     The direct ancestor (i.e. parent) of the node.
    /// </summary>
    public UNode<T1, T2>? Previous { get; }
    
    /// <summary>
    ///     The direct descendants (i.e. children) of the node.
    /// </summary>
    public UNode<T1, T2>? Next { get; private set; }
    
    /// <summary>
    ///     The root of the tree the node is part of.
    /// </summary>
    public UNode<T1, T2> Root { get; }

    public UNode(T1 value, T2 label = default!)
    {
        Value = value;
        Label = label;
        Root = this;
    }

    private UNode(T1 value, T2 label, UNode<T1, T2> previous, UNode<T1, T2> root)
    {
        Value = value;
        Label = label;
        Previous = previous;
        Root = root;
    }

    /// <summary>
    ///     Adds a direct descendent with the given value to the node.
    /// </summary>
    public UNode<T1, T2> Add(T1 value, T2 label)
    {
        UNode<T1, T2> node = new(value, label, this, Root);
        Next = node;
        return node;
    }

    /// <summary>
    ///     Applies the given action on all nodes in the tree using pre-order traversal.
    /// </summary>
    public void TraverseDown(Action<T1, T2> action)
    {
        action(Value, Label);
        Next?.TraverseDown(action);
    }

    /// <summary>
    ///     Applies the given action on all nodes in the node's branch by traversing up.
    /// </summary>
    public void TraverseUp(Action<T1, T2> action)
    {
        action(Value, Label);
        Previous?.TraverseUp(action);
    }
}
