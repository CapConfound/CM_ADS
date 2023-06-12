namespace CM_ADS;

// https://neerc.ifmo.ru/wiki/index.php?title=%D0%94%D0%B5%D1%80%D0%B5%D0%B2%D0%BE_%D0%BF%D0%BE%D0%B8%D1%81%D0%BA%D0%B0,_%D0%BD%D0%B0%D0%B8%D0%B2%D0%BD%D0%B0%D1%8F_%D1%80%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D1%8F
public class BinaryTree<T> where T : IComparable
{
    // Корень дерева
    public Node<T> root;

    public BinaryTree()
    {
        root = null;
    }

    // вставка нового узла
    public void Insert(int key, T value)
    {
        if (root == null)
        {
            root = new Node<T>(key, value);
            return;
        }

        var current = root;

        while (true)
        {
            if (key < current.Key)
            {
                if (current.LNode == null)
                {
                    current.LNode = new Node<T>(key, value);
                    current.LNode.Parent = current;
                    break;
                }

                current = current.LNode;
            }
            else
            {
                if (current.RNode == null)
                {
                    current.RNode = new Node<T>(key, value);
                    current.RNode.Parent = current;
                    break;
                }

                current = current.RNode;
            }
        }
    }

    // поиск узла по ключу
    public Node<T> Find(int key)
    {
        Node<T> current = root;

        while (current != null)
        {
            if (current.Key == key) return current;
            if (current.Key > key)
            {
                if (current.LNode != null)
                    current = current.LNode;
                else
                    return new Node<T>();
            }
            if (current.Key < key)
            {
                if (current.RNode != null) 
                    current = current.RNode;
                else 
                    return new Node<T>();
            }
        }

        return current;
    }

    private Node<T> FindSuccessor(Node<T> current)
    {
        Node<T> successor = current.RNode;

        while (successor.LNode != null) successor = successor.LNode;

        return successor;
    }

    // удаление по ключу
    public void DeleteR(int key)
    {
        root = DeleteRecursive(root, key);
    }

    private Node<T> DeleteRecursive(Node<T> current, int key)
    {
        if (current == null) return current;

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
            if (current.LNode == null) return current.RNode;
            if (current.RNode == null) return current.LNode;

            current.Value = FindMin(current.RNode).Value;
            current.Key = FindMin(current.RNode).Key;

            current.RNode = DeleteRecursive(current.RNode, current.Key);
        }

        return current;
    }

    public void Delete(int key)
    {
        Node<T> toDelete = Find(key);
        
        if (toDelete == null) return;

        Node<T> rightNode = toDelete.RNode;
        Node<T> leftNode = toDelete.LNode;

        // если нет детей
        if (rightNode == null && leftNode == null)
        {
            DeleteLeaf(toDelete);
            return;
        }

        // Если есть только левый ребенок
        if (rightNode == null)
        {
            DeleteOneChild(toDelete, leftNode);
            return;
        }
        
        // Если есть только правый ребенок
        if (leftNode == null)
        {
            DeleteOneChild(toDelete, rightNode);
            return;
        }

        // Оба ребенка есть 
        DeleteBothChildren(toDelete, leftNode, rightNode);

    }
    
    private void DeleteLeaf(Node<T> current)
    {
        if (current.Parent == null)
        {
            root = new Node<T>();
            return;
        }

        if (current.Parent.LNode == current)
        {
            current.Parent.LNode = null;
        }

        current.Parent.RNode = null;
    }

    private void DeleteOneChild(Node<T> current, Node<T> child)
    {
        Node<T> parent = current.Parent;
        
        child.Parent = parent;
        
        if (parent.RNode == current) 
            parent.RNode = child;
        else 
            parent.LNode = child;
    }

    private void DeleteBothChildren(Node<T> current, Node<T> leftNode, Node<T> rightNode)
    {
        Node<T> parent = current.Parent;
        
        // минимальный преемник
        Node<T> minNode = FindMin(rightNode);

        if (minNode == rightNode)
        {
            // сдвигаем левого потомка направо
            minNode.LNode = leftNode;
            leftNode.Parent = minNode;
            minNode.Parent = parent;
            
            if (current == parent.RNode)
                parent.RNode = minNode;
            else
                parent.LNode = minNode;
            
            return;
        }
        
        minNode.LNode = leftNode;
        minNode.RNode = rightNode;
        
        rightNode.Parent = minNode;
        leftNode.Parent = minNode;

        minNode.Parent.LNode = new Node<T>();
        minNode.Parent = parent;

        if (current == parent.RNode)
        {
            parent.RNode = minNode;
        }
        else
        {
            parent.LNode = minNode;
        }
    }


    // просмотр дерева
    public void ViewTree()
    {
        ViewTreeL2R(root);
        Console.WriteLine();
    }

    private void Visit(Node<T> node)
    {
        Console.Write(node.Key + " ");
    }
    
    /*
     * Рекурсивные методы просмотра поддерева
     */
    
    // Слева направо
    public void ViewTreeL2R(Node<T> node)
    {
        if (node == null) return;
        // просмотр левого поддерева
        ViewTreeL2R(node.LNode);
        // информация о текущем узле
        Visit(node);
        // просмотр правого поддерева
        ViewTreeL2R(node.RNode);
    }

    // Справа налево
    public void ViewTreeR2L(Node<T> node)
    {
        if (node == null) return;
        // просмотр правого поддерева
        ViewTreeL2R(node.RNode);
        // информация о текущем узле
        Visit(node);
        // просмотр левого поддерева
        ViewTreeR2L(node.LNode);
    }
    
    // Водопадный
    public void ViewTreeW(Node<T> node)
    {
        if (node == null) return;
        // информация о текущем узле
        Visit(node);
        // просмотр правого поддерева
        ViewTreeL2R(node.RNode);            
        // просмотр левого поддерева
        ViewTreeW(node.LNode);
    }
    
    
    public void TraversalMinToMax()
    {
        Node<T> current = FindMin(root);
        while (current != null)
        {
            Visit(current);
            current = Next(current);
        }
        Console.WriteLine();
    }
    
    
    public void TraversalMaxToMin()
    {
        Node<T> current = FindMax(root);
        while (current != null)
        {
            Visit(current);
            current = Prev(current);
        }
        Console.WriteLine();
    }

    // поиск узла с минимальным значением ключа начиная с узла node
    public Node<T> FindMin(Node<T> node)
    {
        while (node.LNode != null) node = node.LNode;
        return node;
    }

    // поиск узла с максимальным значением ключа начиная с узла node
    public Node<T> FindMax(Node<T> node)
    {
        while (node.RNode != null) node = node.RNode;
        return node;
    }

    // поиск узла со следующим значением ключа чем t.key
    public Node<T> Next(Node<T> t)
    {
        if (t == null) return null;

        if (t.RNode != null)
            // tut minimum()
            return FindMin(t.RNode);
        var y = t.Parent;
        while (y != null && t == y.RNode)
        {
            t = y;
            y = y.Parent;
        }

        return y;
    }
    
    // поиск узла с предыдущем значением ключа чем t.key
    public Node<T> Prev(Node<T> t)
    {
        if (t == null) return null;
        
        if (t.LNode != null) return FindMax(t.LNode);
        
        Node<T> current = t;
        Node<T> parent = current.Parent;
        
        while (parent != null && current == parent.LNode)
        {
            current = parent;
            parent = parent.Parent;
        }
        return parent;
    }

    // TODO: Левый и правый повороты (см. книгу Кормена)

    public void RotateLeft(Node<T> node)
    {
        if (node == null || node.RNode == null)
            return;

        Node<T> pivot = node.RNode;
        node.RNode = pivot.LNode;

        if (pivot.LNode != null)
            pivot.LNode.Parent = node;

        pivot.Parent = node.Parent;

        if (node.Parent == null)
            root = pivot;
        else if (node == node.Parent.LNode)
            node.Parent.LNode = pivot;
        else
            node.Parent.RNode = pivot;

        pivot.LNode = node;
        node.Parent = pivot;
    }

    public void RotateRight(Node<T> node)
    {
        if (node == null || node.LNode == null)
            return;

        Node<T> pivot = node.LNode;
        node.LNode = pivot.RNode;

        if (pivot.RNode != null)
            pivot.RNode.Parent = node;

        pivot.Parent = node.Parent;

        if (node.Parent == null)
            root = pivot;
        else if (node == node.Parent.RNode)
            node.Parent.RNode = pivot;
        else
            node.Parent.LNode = pivot;

        pivot.RNode = node;
        node.Parent = pivot;
    }
}