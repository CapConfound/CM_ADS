namespace CM_ADS;


public class DoubleLinkedList<K, T> where K : IComparable where T : IComparable
{
    private TwoWayList<K, T> _first = null; // Ссылка на начальный узел
    private TwoWayList<K, T> _last = null; // Ссылка на последний узел
    private int _pos = 0;


    // Свойство
    public TwoWayList<K, T> First 
    {
        get { return _first; }
    }

    // Свойство
    public TwoWayList<K, T> Last
    {
        get { return _last; }
    } 
    
    //Конструктор
    public DoubleLinkedList()
    {
        _first = null;
        _last = null;
        _pos = 0;
    }

    public int Count // Свойство
    {
        get { return _pos; }
    }
    
    // Добавить в начало
    public int AddStart(K key, T value)
    {
        TwoWayList<K, T> s = new TwoWayList<K, T>(key, value);
        if (_first == null)
        {
            _first = s;
            _last = s;
            return ++_pos;
        }
        
        s.Next = _first;
        _first = s;
        return ++_pos;
    }

    // Добавить в конец
    public int AddEnd(K key, T value)
    {
        TwoWayList<K, T> s = new TwoWayList<K, T>(key, value);
        
        // Если список пустой    
        if (_first == null)
        {
            _first = s;
            _last = s;
            return _pos++;
        }
        
        // Поиск последнего узла
        TwoWayList<K, T> l = _last;
        
        _last.Next = s;
        _last.Next.Prev = _last;
        _last = s;

        return _pos++;
    }

    // Очистка списка
    public void Clear()
    {
        _first = null;
        _last = null;
        _pos = 0;
    }

    // Проверка на значение
    public bool ContainsValue(T value)
    {
        if (_first != null)
        {
            TwoWayList<K, T> e = _first;
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
        
        TwoWayList<K, T> e = this._first;

        for (int i = 0; i < k; i++)
            e = e.Next;
        
        return e;
    }

    public void PrintTwoWayList()
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
    public int AddAfter(int k, TwoWayList<K, T> item)
    {
        if (this._first == null || this.Count < k)
        {
            AddStart(item.Key, item.Value);
            return _pos;
        }

        TwoWayList<K, T> e = this._first;

        for (int i = 0; i < k; i++)
            e = e.Next;

        TwoWayList<K, T> temp = e.Next;

        
        e.Next = item;  
        item.Next = temp; 
        
        return this._pos++;
    }

    // Вставка перед заданным узлом
    public int AddBefore(int k, TwoWayList<K, T> item)
    {
        if (_first == null || Count < k)
        {
            AddStart(item.Key, item.Value);
            return _pos;
        }

        TwoWayList<K, T> e = _first;

        for (int i = 0; i < k; i++)
            e = e.Next;
        
        
        TwoWayList<K, T> temp = e.Prev;
        
        e.Prev = item;  
        item.Prev = temp; 
        
        return _pos++;
    }
    
    // Удаление начального узла 
    public int DelStart()
    {
        TwoWayList<K, T> e;
        
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
        
        _last = _last.Prev;
        _last.Next = null;
        
        return _pos--;
    }

    // Копирование списка в другую переменную
    public DoubleLinkedList<K, T> Copy()
    {
        DoubleLinkedList<K, T> result = new DoubleLinkedList<K, T>();
        TwoWayList<K, T> elem = _first;
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
        DoubleLinkedList<K, T> temp = Copy();
        Clear();
        
        TwoWayList<K, T> elem = temp._first;
        for (int k = 0; k < temp.Count; k++)
        {
            AddEnd(elem.Key, elem.Value);
            elem = elem.Next;
        }
    }
}


