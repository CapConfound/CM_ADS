namespace CM_ADS;

public class Node <T> where T : IComparable
{
    public Node <T> Left;
    public Node <T> Right;
    public Node <T> Parent;

    public T value;
    public int key;

    public Node(T value, int key) 
    {
        this.value = value;
        this.key = key;
        Left = null;
        Right = null;
        Parent = null;
    }
    
    public Node()
    {
        this.value = default;
        this.key = 0;
        Left = null;
        Right = null;
        Parent = null;
    }
    
    public Node<T> GetParent()
    {
        return new Node<T>();
    }
}