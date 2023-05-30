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
        while (current != null && !current.Value.Equals(key)) {
            if (key.CompareTo(current.Value) < 0) {
                current = current.LNode;
            } else {
                current = current.RNode;
            }
        }
        return current;
    }
    
    // удаление по ключу
    public bool Delete(int key) 
    {
        Node<T> parent = null;
        Node<T> current = root;

        while (current != null && !current.Value.Equals(key)) {
            parent = current;

            if (key.CompareTo(current.Value) < 0) {
                current = current.LNode;
            } else {
                current = current.RNode;
            }
        }

        if (current == null) {
            return false;
        }

        if (current.LNode == null && current.RNode == null)
        {
            DeleteLeaf(parent, current);
        } 
        else if (current.LNode != null && current.RNode != null)
        {
            DeleteTwoChildren(parent, current);
        } 
        else
        {
            DeleteOneChild(parent, current);
        }

        return true;
    }

    private void DeleteLeaf(Node<T> parent,  Node <T> current) 
    {
        if (current == root) {
            root = null;
        } else if (parent.LNode == current) {
            parent.LNode = null;
        } else {
            parent.RNode = null;
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

    private void DeleteTwoChildren(Node <T> parent, Node <T> current) 
    {
        Node<T> successorParent = current.RNode;
        Node<T> successor = successorParent.LNode;

        while (successor != null && successor.LNode != null) {
            successorParent = successor;
            successor = successor.LNode;
        }

        if (successor != null && !successor.Value.Equals(current.RNode.Value)) {
            successorParent.LNode = successor.RNode;
            successor.RNode = current.RNode;
        }

        if (current == root) {
            root = successor;
        } else if (parent.LNode == current) {
            parent.LNode = successor;
        } else {
            parent.RNode = successor;
        }

        successor.LNode = current.LNode;
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