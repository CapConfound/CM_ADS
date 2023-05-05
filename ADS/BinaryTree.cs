namespace CM_ADS;

public class BinaryTree <T> where T : IComparable
{
    private Node <T> _root; // Корень дерева
    // вставка нового узла
    public void Insert(T value, int key)
    {
        Node <T> node = new Node <T> (value, key);

        if (_root == null) 
        {
            _root = node;
            return;
        } else {
            
        }
    }
    // поиск узла по ключу
    public Node<T> Find(int key)
    {
        return new Node<T>();
    }
    // удаление по ключу
    public void Delete(int key) 
    {
        Node<T> delete = Find(key);

        if (delete == null) return;

        Node<T> right = delete.Right;
        Node<T> left = delete.Left;

        if (right == null && left == null) 
        {
            // удаление листа 
            DeleteLeaf(delete);
        } 
        else if (right == null) 
        {
            DeleteOneChild(delete, left);
        } 
        else if (left == null) 
        {
            DeleteOneChild(delete, right);
        } 
        else 
        {
            DeleteTwoChildren(delete, right, left);
        }
    }

    private void DeleteLeaf(Node <T> node) 
    {
        
    }

    private void DeleteOneChild(Node <T> node, Node <T> child) 
    {
        
    }

    private void DeleteTwoChildren(Node <T> node, Node <T> right, Node <T> left) 
    {
        Node <T> min = FindMin(right);
    }

    private void Replace(Node<T> n1, Node<T> n2) 
    {
        
        Node<T> n1Parent = n1.GetParent();
        Node<T> n2Parent = n2.GetParent();

    }
    // просмотр дерева
    public void ViewTree() 
    {
        ViewTree(_root);
        Console.WriteLine();
    }
    // рекурсивный метод просмотра поддерева
    public void ViewTree(Node <T> node) 
    {
        if (node == null) return;
        // просмотр левого поддерева
        ViewTree(node.Left);
        // информация о текущем узле
        Visit(node);
        // просмотр правого поддерева
        ViewTree(node.Right);
    }

    private void Visit(Node <T> node) 
    {
        
    }

    // поиск узла с минимальным значением ключа начиная с узла node
    public Node <T> FindMin(Node <T> node) 
    {
        while (node.Left != null) 
        {
            node = node.Left;
        }
        return node;
    }
    // поиск узла с максимальным значением ключа начиная с узла node
    public Node <T> FindMax(Node <T> node) 
    {
        return new Node<T>();
    }
    // поиск узла со следующим значением ключа чем t.key
    public Node <T> Next(Node <T> t) 
    {
        if (t != null) {
            if (t.Right != null) return new Node<T>(t.Right.value, 0);
            Node <T> y = t.Parent;
            while (y != null && t == y.Right) 
            {
                t = y;
                y = y.Parent;
            }
            return y;
        }
        return null;
    }
    // поиск узла с предыдущем значением ключа чем t.key
    public Node <T> Prev(Node <T> t)
    {
        return new Node<T>();
    }

    
    // просмотр дерева по возрастанию ключа с использованием метода Next
    // просмотр дерева по убыванию ключа с использованием метода Prev
    // Левый и правый повороты (см. книгу Кормена)
}