namespace CM_ADS;


public class SingleLinkedList<TKey, TValue> where TKey : IComparable where TValue : IComparable
{
    private List<TKey, TValue> _first = null; // Ссылка на начальный узел
    private int _pos = 0;

    
    // Свойство
    public List<TKey, TValue> First 
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
    public int AddStart(TKey key, TValue value)
    {
        List<TKey, TValue> s = new List<TKey, TValue>(key, value);
        s.Next = _first;
        _first = s;            
        return this._pos++;
    }

    // Добавить в конец
    public int AddEnd(TKey key, TValue value)
    {
        List<TKey, TValue> s = new List<TKey, TValue>(key, value);
        
        // Если список пустой    
        if (this._first == null) { this._first = s; return this._pos++; }
        
        // Поиск последнего узла
        List<TKey, TValue> e = this._first;
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
    public bool ContainsValue(TValue value)
    {
        if (_first != null)
        {
            List<TKey, TValue> e = _first;
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
    public ListItem<TKey, TValue> getNode(int k)
    {
        if (this._first == null || this.Count < k) return new ListItem<TKey, TValue>();
        
        List<TKey, TValue> e = this._first;

        for (int i = 0; i < k; i++)
            e = e.Next;
        
        return e;
    }

    public ListItem<TKey, TValue> getNodeByKey(TKey key)
    {
        if (this._first == null) return new ListItem<TKey, TValue>();
        
        List<TKey, TValue> e = this._first;

        while (e.Next != null)
        {
            if (e.Key.Equals(key)) return e;
            e = e.Next;
        }
        
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
    public int AddAfter(int k, List<TKey, TValue> item)
    {
        if (this._first == null || this.Count < k)
        {
            AddStart(item.Key, item.Value);
            return this._pos;
        }

        List<TKey, TValue> e = this._first;

        for (int i = 0; i < k; i++)
            e = e.Next;

        List<TKey, TValue> temp = e.Next;

        
        e.Next = item;  
        item.Next = temp; 
        
        return this._pos++;
    }

    // Вставка перед заданным узлом
    public int AddBefore(int k, List<TKey, TValue> item)
    {
        if (_first == null || Count < k)
        {
            AddStart(item.Key, item.Value);
            return _pos;
        }

        List<TKey, TValue> e = _first;

        for (int i = 0; i < k; i++)
        {
            if (e.Next.Key.Equals(k))
                break;
            e = e.Next;
        }
        
        List<TKey, TValue> temp = e.Next;
        
        e.Next = item;  
        item.Next = temp; 
        
        return _pos++;
    }
    
    // Удаление начального узла 
    public int DelStart()
    {
        List<TKey, TValue> e;
        
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

    public int DelByKey(TKey key)
    {
        if (this._first == null) return _pos;
        
        List<TKey, TValue> e = this._first;

        while (e.Next != null)
        {
            if (e.Key.Equals(key)) break;
            e = e.Next;
        }


        if (e.Next.Next != null) 
        {
            e.Next = e.Next.Next;
        }
        else 
        {
            e.Next = null;
        }
        
        
        return --_pos;
    }

    // Копирование списка в другую переменную
    public SingleLinkedList<TKey, TValue> Copy()
    {
        SingleLinkedList<TKey, TValue> result = new SingleLinkedList<TKey, TValue>();
        List<TKey, TValue> elem = _first;
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
        SingleLinkedList<TKey, TValue> temp = Copy();
        Clear();
        
        List<TKey, TValue> elem = temp._first;
        for (int k = 0; k < temp.Count; k++)
        {
            AddEnd(elem.Key, elem.Value);
            elem = elem.Next;
        }
    }
}


