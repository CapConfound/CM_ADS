namespace CM_ADS;

public class Node <T> where T : IComparable
{
    public T Value;
    public int Key;
    public Node<T> Parent;
    public Node<T> LNode;
    public Node<T> RNode;
    
    public Node(int key, T value)
    {
        Value = value;
        Key = key;
        LNode = null;
        RNode = null;
        Parent = null;
    }
    
    public Node()
    {
        Value = default;
        Key = 0;
        LNode = null;
        RNode = null;
        Parent = null;
    }
}
