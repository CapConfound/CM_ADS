namespace CM_ADS;


public class SingleLinkedList<K,T> where K : IComparable where T : IComparable
{
    private List<K, T> _first = null; // Ссылка на начальный узел
    private int _pos = 0;

    
    // Свойство
    public List<K, T> First 
    {
        get { return _first; }
    } 
    
    //Конструктор
    public SingleLinkedList()
    {
        _first = null;
        _pos = 0;
    }

    public int Count // Свойство
    {
        get {return _pos;}
    }
    
    // Добавить в начало
    public int AddStart(K key, T value)
    {
        List<K, T> s = new List<K, T>(key, value);
        s.Next = _first; _first = s;            
        return this._pos++;
    }

    // Добавить в конец
    public int AddEnd(K key, T value)
    {
        List<K, T> s = new List<K, T>(key, value);
        
        // Если список пустой    
        if (this._first == null) { this._first = s; return this._pos++; }
        
        // Поиск последнего узла
        List<K, T> e = this._first;
        while (e.Next != null)
        {
            e = e.Next;
        }

        // Добавление в конец
        e.Next = s;
        
        return this._pos++;
    }

    // Очистка списка
    public void Clear()
    {
        _first = null;
        _pos = 0;
    }

    // Проверка на значение
    public bool ContainsValue(T value)
    {
        if (_first != null)
        {
            List<K, T> e = _first;
            do
            {
                if (e.Value.CompareTo(value) == 0)
                    return true;

                e = e.Next;
            } while (e != null);
        }
        return false;
    }

    // Получение по индексу
    public ListItem<K, T> getNode(int k)
    {
        if (this._first == null || this.Count < k) return new ListItem<K, T>();
        
        List<K, T> e = this._first;

        for (int i = 0; i < k; i++)
            e = e.Next;
        
        return e;
    }

    public void PrintList()
    {
        for (int k = 0; k < Count; k++)
        {
            Console.Write("{0}", this.getNode(k));
            if (k != Count - 1)
                Console.Write('\t');
        }
        Console.WriteLine();
    }
    
    // вставка после заданного узла
    public int AddAfter(int k, List<K, T> item)
    {
        if (this._first == null || this.Count < k)
        {
            AddStart(item.Key, item.Value);
            return this._pos;
        }

        List<K, T> e = this._first;

        for (int i = 0; i < k; i++)
            e = e.Next;

        List<K, T> temp = e.Next;

        
        e.Next = item;  
        item.Next = temp; 
        
        return this._pos++;
    }

    // Вставка перед заданным узлом
    public int AddBefore(int k, List<K, T> item)
    {
        if (_first == null || Count < k)
        {
            AddStart(item.Key, item.Value);
            return _pos;
        }

        List<K, T> e = _first;

        for (int i = 0; i < k; i++)
        {
            if (e.Next.Key.Equals(k))
                break;
            e = e.Next;
        }
        
        List<K, T> temp = e.Next;
        
        e.Next = item;  
        item.Next = temp; 
        
        return _pos++;
    }
    
    // Удаление начального узла 
    public int DelStart()
    {
        List<K, T> e;
        
        if ((e = _first) == null) return 0;

        if (e.Next != null)
        {
            e = e.Next;
            _first = e;
        }

        return _pos--;
    }

    
    // Удаление конечного узла 
    
    public int DelEnd()
    {
        if (_first == null) return 0;
        
        return _pos--;
    }

    // Копирование списка в другую переменную
    public SingleLinkedList<K, T> Copy()
    {
        SingleLinkedList<K, T> result = new SingleLinkedList<K, T>();
        List<K, T> elem = _first;
        while (elem.Next != null)
        {
            result.AddStart(elem.Key, elem.Value);
            elem = elem.Next;
        }

        return result;
    }
    
    // Переворот списка
    public void Flip()
    {
        SingleLinkedList<K, T> temp = Copy();
        Clear();
        
        List<K, T> elem = temp._first;
        for (int k = 0; k < temp.Count; k++)
        {
            AddEnd(elem.Key, elem.Value);
            elem = elem.Next;
        }
    }
}


