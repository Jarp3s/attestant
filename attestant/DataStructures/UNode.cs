namespace attestant.DataStructures;


/// <summary>
///    Undirected node w/ both link to direct ancestor & direct descendants
/// </summary>

public class UNode<T>
{
    public T Value { get; }
    public UNode<T>? Ancestor { get; }
    public List<UNode<T>> Descendants { get; } = new();
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

    public UNode<T> AddDescendant(T value)
    {
        var node = new UNode<T>(value, this, Root);
        Descendants.Add(node);
        return node;
    }

    public List<UNode<T>> AddDescendants(params T[] values)
    {
        return values.Select(AddDescendant).ToList();
    }

    public void TraverseDown(Action<T> action)
    {
        action(Value);
        foreach (var descendent in Descendants)
            descendent.TraverseDown(action);
    }

    public void TraverseUp(Action<T> action)
    {
        action(Value);
        Ancestor?.TraverseUp(action);
    }
}