namespace CM_ADS;

public class HashTable<TKey, TValue> where TKey : IComparable where TValue : IComparable
{
    private KeyValuePair<TKey, TValue>[] _items;

    private int _count;
    
    // Максимальное количество элементов, которые может содержать хеш-таблица
    private int _capacity;

    // Коэффициент загрузки хеш-таблицы
    private const double LoadFactor = 0.75;

    // Емкость хеш-таблицы по умолчанию
    private const int DefaultCapacity = 10;

    // Создает новую хеш-таблицу с емкостью по умолчанию
    public HashTable()
    {
        _capacity = DefaultCapacity;
        _count = 0;
        _items = new KeyValuePair<TKey, TValue>[_capacity];
    }

    // Создает новую хеш-таблицу указанной емкости
    public HashTable(int capacity)
    {
        _capacity = capacity;
        _count = 0;
        _items = new KeyValuePair<TKey, TValue>[_capacity];
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
        if (_items[index].Key != null && _items[index].Key.Equals(key))
        {
            _items[index] = new KeyValuePair<TKey, TValue>(key, value);
        }
        else // В противном случае добавляем новую пару ключ-значение
        {
            _items[index] = new KeyValuePair<TKey, TValue>(key, value);
            _count++;
        }
    }
    
    // Удаляет пару ключ-значение из хеш-таблицы
    public bool Remove (TKey key)
    {
        // Находим индекс слота, содержащего удаляемый элемент
        int index = GetIndex (key);

        // Если ключ существует, удаляем пару ключ-значение
        if (_items[index].Key != null && _items[index].Key.Equals(key))
        {
            _items[index] = new KeyValuePair<TKey, TValue>(default(TKey), default(TValue));
            _count--;
            return true;
        }
        
        return false;
    }
    
    // Извлекает значение, связанное с данным ключом
    public TValue GetValue (TKey key)
    {
        // Находим индекс слота, содержащего элемент
        int index = GetIndex (key);

        // Если ключ существует, вернуть его значение
        if (_items[index].Key != null && _items[index].Key.Equals(key))
        {
            return _items[index].Value;
        }

        // В противном случае выбрасываем исключение
        throw new KeyNotFoundException();
    }

    // Изменяем размер внутреннего массива, чтобы удвоить его текущую емкость
    private void Resize()
    {
        _capacity *= 2;
        KeyValuePair<TKey, TValue>[] oldItems = _items;
        _items = new KeyValuePair<TKey, TValue>[_capacity];
        _count = 0;

        // Повторно добавляем все элементы в новый массив
        foreach (KeyValuePair<TKey, TValue> elem in oldItems)
        {
            if (elem.Key!= null)
            {
                Add(elem.Key, elem.Value);
            }
        }
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

    public List<string> Keys()
    {
        List<string> keysList = new List<string>();
    
        foreach (KeyValuePair<TKey, TValue> pair in _items)
        {
            if (pair.Key == null) continue;
            keysList.Add((pair.Key).ToString());
        }

        return keysList;

    }

    public List<string> Values()
    {
        List<string> valuesList = new List<string>();
    
        foreach (KeyValuePair<TKey, TValue> pair in _items)
        {
            if (pair.Value == null) continue;
            valuesList.Add((pair.Key).ToString());
        }

        return valuesList;

    }
    
}