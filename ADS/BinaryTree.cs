namespace CM_ADS;
// https://neerc.ifmo.ru/wiki/index.php?title=%D0%94%D0%B5%D1%80%D0%B5%D0%B2%D0%BE_%D0%BF%D0%BE%D0%B8%D1%81%D0%BA%D0%B0,_%D0%BD%D0%B0%D0%B8%D0%B2%D0%BD%D0%B0%D1%8F_%D1%80%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D1%8F
public class BinaryTree <T> where T : IComparable
{
    // Корень дерева
    public Node <T> root;
    
    public BinaryTree()
    {
        root = null;
    }
    
    // вставка нового узла
    public void Insert(int key, T value)
    {
        if (root == null) {
            root = new Node<T>(key, value);
            return;
        }
        
        Node<T> current = root;
        
        while (true) {
            if (value.CompareTo(current.Value) < 0) {
                if (current.LNode == null) {
                    current.LNode = new Node<T>(key, value);
                    break;
                }
                current = current.LNode;
            } else if (value.CompareTo(current.Value) > 0) {
                if (current.RNode == null) {
                    current.RNode = new Node<T>(key, value);
                    break;
                }
                current = current.RNode;
            } else {
                break;
            }
        }
    }
    
    // поиск узла по ключу
    public Node<T> Find(int key)
    {
        Node<T> current = root;
        
        while (current != null && current.Key != key)
        {
            current = key < current.Key ? current.LNode : current.RNode;
        }

        return current;
    }
    
    private Node<T> FindSuccessor(Node<T> toDelete)
    {
        var successor = toDelete.RNode;

        while (successor.LNode != null)
        {
            successor = successor.LNode;
        }

        return successor;
    }
    
    // удаление по ключу
    public void Delete(int key)
    {
        root = DeleteRecursive(root, key);
    }

    private Node<T> DeleteRecursive(Node<T> current, int key)
    {
        if (current == null)
        {
            return current;
        }

        if (key < current.Key)
        {
            current.LNode = DeleteRecursive(current.LNode, key);
        }
        else if (key > current.Key)
        {
            current.RNode = DeleteRecursive(current.RNode, key);
        }
        else
        {
            if (current.LNode == null)
            {
                return current.RNode;
            }
            else if (current.RNode == null)
            {
                return current.LNode;
            }

            current.Value = FindMin(current.RNode).Value;
            current.Key = FindMin(current.RNode).Key;

            current.RNode = DeleteRecursive(current.RNode, current.Key);
        }

        return current;
    }

    
    private Node<T> FindParent(Node<T> node, int key) {
        Node<T> parent = null;

        while (!node.Value.Equals(key)) {
            parent = node;

            if (key.CompareTo(Convert.ToInt32(node.Value)) < 0) {
                node = node.LNode;
            } else {
                node = node.RNode;
            }
        }

        return parent;
    }

    private void DeleteLeaf( Node <T> current) 
    {
        if (current == root)
        {
            root = null;
            return;
        }

        if (current.Parent.LNode == current)
        {
            current.Parent.LNode = null;
        }
        else
        {
            current.Parent.RNode = null;
        }
    }

    private void DeleteOneChild(Node <T> parent, Node <T> current) 
    {
        Node<T> child;

        if (current.LNode != null) { 
            child = current.LNode; 
        } else { 
            child = current.RNode; 
        }

        if (current == root) { 
            root = child; 
        } else if (parent.LNode == current) { 
            parent.LNode = child; 
        } else { 
            parent.RNode = child; 
        } 
    }

    

    // просмотр дерева
    public void ViewTree() 
    {
        ViewTree(root);
        Console.WriteLine();
    }
    // рекурсивный метод просмотра поддерева
    public void ViewTree(Node <T> node) 
    {
        if (node == null) return;
        // просмотр левого поддерева
        ViewTree(node.LNode);
        // информация о текущем узле
        Visit(node);
        // просмотр правого поддерева
        ViewTree(node.RNode);
    }

    private void Visit(Node <T> node) 
    {
        Console.Write(node.Key + " ");
    }

    // поиск узла с минимальным значением ключа начиная с узла node
    public Node <T> FindMin(Node <T> node) 
    {
        while (node.LNode != null) 
        {
            node = node.LNode;
        }
        return node;
    }
    // поиск узла с максимальным значением ключа начиная с узла node
    public Node <T> FindMax(Node <T> node) 
    {
        while (node.RNode != null)
        {
            node = node.RNode;
        }
        return node;return new Node<T>();
    }
    
    // поиск узла со следующим значением ключа чем t.key
    public Node <T> Next(Node <T> t) 
    {
        if (t == null)
        {
            return null;
        }

        if (t.RNode != null)
        {
            // tut minimum()
            return FindMin(t.RNode);
        }
        Node <T> y = t.Parent;
        while (y != null && t == y.RNode) 
        {
            t = y;
            y = y.Parent;
        }
        return y;

    }
    // поиск узла с предыдущем значением ключа чем t.key
    public Node <T> Prev(Node <T> t)
    {
        if (t == null)
        {
            return null;
        }

        if (t.LNode != null)
        {
            // tut minimum()
            return FindMax(t.LNode);
        }
        Node <T> y = t.Parent;
        while (y != null && t == y.LNode) 
        {
            t = y;
            y = y.Parent;
        }
        return y;
    }

    
    // просмотр дерева по возрастанию ключа с использованием метода Next
    // просмотр дерева по убыванию ключа с использованием метода Prev
    // Левый и правый повороты (см. книгу Кормена)
}