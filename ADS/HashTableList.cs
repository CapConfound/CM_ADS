namespace CM_ADS;

public class HashTableList<TKey, TValue> where TKey : IComparable where TValue : IComparable
{
    private LinkedList<KeyValuePair<TKey, TValue>>[] _items;

    private int _count;
    
    // Максимальное количество элементов, которые может содержать хеш-таблица
    private int _capacity;

    // Коэффициент загрузки хеш-таблицы
    private const double LoadFactor = 0.75;

    // Емкость хеш-таблицы по умолчанию
    private const int DefaultCapacity = 10;

    // Создает новую хеш-таблицу с емкостью по умолчанию
    public HashTableList()
    {
        _capacity = DefaultCapacity;
        _count = 0;
        _items = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
    }

    // Создает новую хеш-таблицу указанной емкости
    public HashTableList(int capacity)
    {
        _capacity = capacity;
        _count = 0;
        _items = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];

    }

    public int GetSize()
    {
        return _capacity;
    }

    // Добавляет пару ключ-значение в хэш-таблицу
    public void Add(TKey key, TValue value)
    {
        // При необходимости изменяем размер внутреннего массива
        if ((double)_count / _capacity >= LoadFactor)
        {
            Resize();
        }

        // Находим индекс слота, в который нужно добавить новый элемент
        int index = GetIndex(key);


        // Если ключ уже существует, заменяем значение
        if (_items[index] != null)
        {
            _items[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
        }
        else // В противном случае добавляем новую пару ключ-значение
        {
            _items[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            _items[index].AddFirst(new KeyValuePair<TKey, TValue>(key, value));            
        }
        _count++; // Увеличиваем счетчик элементов
    }
    
    // Удаляет пару ключ-значение из хеш-таблицы
    public bool Remove (TKey key)
    {
        // Находим индекс слота, содержащего удаляемый элемент
        int index = GetIndex (key);
        var item = _items[index];
        
        // Если ключ существует, удаляем пару ключ-значение
        if (_items[index] != null)
        {
            var current = item.First;
            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    item.Remove(current);
                    return true;
                }
                current = current.Next;
            }
        }
        
        return false;
    }
    
    // Извлекает значение, связанное с данным ключом
    public TValue GetValue (TKey key)
    {
        // Находим индекс слота, содержащего элемент
        int index = GetIndex (key);


        // Если ключ существует, вернуть его значение
        if (_items[index] != null)
        {
            foreach (var item in _items[index]) 
                if (item.Key.Equals(key)) return item.Value;

        }

        // В противном случае выбрасываем исключение
        throw new KeyNotFoundException();
    }

    // Изменяем размер внутреннего массива, чтобы удвоить его текущую емкость
    private void Resize()
    {
        _capacity *= 2;
        // SingleLinkedList<TKey, TValue>[] oldItems = _items;
        // _items = new SingleLinkedList<TKey, TValue>;
        var newBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];

        // Повторно добавляем все элементы в новый массив
        foreach (var elem in _items)
        {
            if (elem == null) continue;       

            foreach (var item in elem)
            {
                int newIndex = GetIndex(item.Key);
                if (newBuckets[newIndex] == null)
                    newBuckets[newIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();

                newBuckets[newIndex].AddLast(item);
            }   
        }

        _items = newBuckets;
    }

    // Возвращает индекс слота, содержащего указанный ключ
    private int GetIndex (TKey key)
    {
        int hashCode = key.GetHashCode();
        int index = hashCode % _capacity;

        // Убедитесь, что индекс неотрицательный
        if (index < 0)
        {
            index += _capacity;
        }

        return index;
    }

}