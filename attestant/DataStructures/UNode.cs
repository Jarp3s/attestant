namespace attestant.DataStructures;


/// <summary>
///    Undirected node w/ both link to direct ancestor & direct descendants.
/// </summary>
public class UNode<T>
{
    /// <summary>
    ///     The value assigned to the node.
    /// </summary>
    public T Value { get; }
    
    /// <summary>
    ///     The direct ancestor (i.e. parent) of the node.
    /// </summary>
    public UNode<T>? Ancestor { get; }
    
    /// <summary>
    ///     The direct descendants (i.e. children) of the node.
    /// </summary>
    public List<UNode<T>> Descendants { get; } = new();
    
    /// <summary>
    ///     The root of the tree the node is part of.
    /// </summary>
    public UNode<T> Root { get; }

    public UNode(T value)
    {
        Value = value;
        Root = this;
    }
    
    public UNode(T value, UNode<T>? ancestor, UNode<T> root)
    {
        Value = value;
        Ancestor = ancestor;
        Root = root;
    }

    public UNode<T> this[int i] => Descendants[i];

    /// <summary>
    ///     Add a direct descendent with the given value to the node.
    /// </summary>
    public UNode<T> AddDescendant(T value)
    {
        var node = new UNode<T>(value, this, Root);
        Descendants.Add(node);
        return node;
    }

    /// <summary>
    ///     Apply the given action on all nodes in the tree using pre-order traversal.
    /// </summary>
    public void TraverseDown(Action<T> action)
    {
        action(Value);
        foreach (var descendent in Descendants)
            descendent.TraverseDown(action);
    }

    /// <summary>
    ///     Apply the given action on all nodes in the node's branch by traversing up.
    /// </summary>
    public void TraverseUp(Action<T> action)
    {
        action(Value);
        Ancestor?.TraverseUp(action);
    }
}