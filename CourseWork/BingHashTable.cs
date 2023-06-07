namespace CM_ADS.CourseWork.Bing;


using System;
using System.Collections;

public class MyHashtable
{
    /// <summary>
    /// Двумерный контейнер _buckets. 
    /// </summary>
    private ArrayList[] _buckets;
    
    /// <summary>
    /// Счётчик элементов
    /// </summary>
    private int _count;
    
    /// <summary>
    /// Критический процент заполненности.
    /// При превышениии необходимо увеличить емкость HashMap
    /// </summary>
    private float _loadFactor;

    /// <summary>
    /// Конструктор класса по умолчанию
    /// </summary>
    /// <param name="capacity">Емкость структуры</param>
    /// <param name="loadFactor">Критический процент заполненности</param>
    public MyHashtable(int capacity = 10, float loadFactor = 0.75f)
    {
        _buckets = new ArrayList[capacity];
        _loadFactor = loadFactor;
        _count = 0;
    }

    /// <summary>
    /// Добавление одного элемента в HashTable
    /// </summary>
    /// <param name="key">Ключ элемента</param>
    /// <param name="value">Значение элемента</param>
    /// <exception cref="ArgumentException">Выкидывается когда пытаетесь вставить по существующему ключу</exception>
    public void Add(object key, object value)
    {
        // Проверка заполненности
        if (_count >= _buckets.Length * _loadFactor)
        {
            Resize();
        }

        // Вычисление хэша ключа
        int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

        if (_buckets[index] == null)
        {
            _buckets[index] = new ArrayList();
        }

        // Проверка наличия ключа
        foreach (DictionaryEntry entry in _buckets[index])
        {
            if (entry.Key.Equals(key))
            {
                throw new ArgumentException("An element with the same key already exists in the Hashtable.");
            }
        }
        
        // Добавление элемента по ключу
        _buckets[index].Add(new DictionaryEntry(key, value));
        
        // После успешного добавления, увеличиваю счётчик элементов
        _count++;
    }
    
    /// <summary>
    /// Перегрузка метода Add(object key, object value)
    /// Добавление элемента внутреннего HashTable
    /// </summary>
    /// <param name="outerKey">Ключ в таблице верхнего уровня</param>
    /// <param name="innerKey">Ключ в таблице нижнего уровня</param>
    /// <param name="value">Значение</param>
    /// <exception cref="ArgumentException"></exception>
    public void Add(object outerKey, object innerKey, object value)
    {
        if (!ContainsKey(outerKey))
        {
            Add(outerKey, new MyHashtable());
        }

        MyHashtable innerHashtable = (MyHashtable)Get(outerKey);
        
        if (!innerHashtable.ContainsKey(innerKey))
        {
            innerHashtable.Add(innerKey, value);
        }
        
        else
        {
            throw new ArgumentException("An element with the same key already exists in the Hashtable.");
        }
    }

    /// <summary>
    /// Поиск элемента по ключу
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public object Get(object key)
    {
        int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

        if (_buckets[index] == null)
        {
            throw new ArgumentException("The specified key does not exist in the Hashtable.");
        }

        foreach (DictionaryEntry entry in _buckets[index])
        {
            if (entry.Key.Equals(key))
            {
                return entry.Value;
            }
        }
        
        throw new ArgumentException("The specified key does not exist in the Hashtable.");
    }
    
    /// <summary>
    /// Перегрузка метода Get(object key) с возможностью поиска во внутреннем HashTable.
    /// </summary>
    /// <param name="outerKey">Ключ в таблице верхнего уровня</param>
    /// <param name="innerKey">Ключ в таблице нижнего уровня</param>
    /// <returns>Хранящийся объект</returns>
    /// <exception cref="ArgumentException"></exception>
    public object Get(object outerKey, object innerKey)
    {
        if (!ContainsKey(outerKey))
        {
            throw new ArgumentException("The specified outer key does not exist in the Hashtable.");
        }

        object value = Get(outerKey);
        
        if (!(value is MyHashtable))
        {
            throw new ArgumentException("The value associated with the specified outer key is not a Hashtable.");
        }
        
        MyHashtable innerHashtable = (MyHashtable)value;
        
        if (!innerHashtable.ContainsKey(innerKey))
        {
            throw new ArgumentException("The specified inner key does not exist in the Hashtable.");
        }
        
        return innerHashtable.Get(innerKey);
    }

    /// <summary>
    /// Удаление элемента по ключу
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <exception cref="ArgumentException"></exception>
    public void Remove(object key)
    {
        int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

        if (_buckets[index] != null)
        {
            throw new ArgumentException("The specified key does not exist in the Hashtable.");
        }
        
        for (int i = 0; i < _buckets[index].Count; i++)
        {
            DictionaryEntry entry = (DictionaryEntry)_buckets[index][i];

            if (entry.Key.Equals(key))
            {
                _buckets[index].RemoveAt(i);
                _count--;
                return;
            }
        }

        throw new ArgumentException("The specified key does not exist in the Hashtable.");
    }
    
    /// <summary>
    /// Перегрузка метода Remove(object key)
    /// Удаляет элемент внутреннего HashMap
    /// </summary>
    /// <param name="outerKey">Ключ в таблице верхнего уровня</param>
    /// <param name="innerKey">Ключ в таблице нижнего уровня</param>
    /// <exception cref="ArgumentException"></exception>
    public void Remove(object outerKey, object innerKey)
    {
        if (!ContainsKey(outerKey))
        {
            throw new ArgumentException("The specified outer key does not exist in the Hashtable.");
        }

        MyHashtable innerHashtable = (MyHashtable)Get(outerKey);

        if (!innerHashtable.ContainsKey(innerKey))
        {
            throw new ArgumentException("The specified inner key does not exist in the Hashtable.");
        }

        innerHashtable.Remove(innerKey);

        if (innerHashtable._count == 0)
        {
            Remove(outerKey);
        }
    }

    private void Resize()
    {
        ArrayList[] newBuckets = new ArrayList[_buckets.Length * 2];

        for (int i = 0; i < _buckets.Length; i++)
        {
            if (_buckets[i] != null)
            {
                foreach (DictionaryEntry entry in _buckets[i])
                {
                    int index = Math.Abs(entry.Key.GetHashCode()) % newBuckets.Length;

                    if (newBuckets[index] == null)
                    {
                        newBuckets[index] = new ArrayList();
                    }

                    newBuckets[index].Add(entry);
                }
            }
        }

        _buckets = newBuckets;
    }
    
    /// <summary>
    /// Проверка yаличия ключа в HashTable
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <returns>
    /// true - Есть ключ в HashTable.
    /// false - Нет ключа в HashTable.
    /// </returns>
    public bool ContainsKey(object key)
    {
         int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

         if (_buckets[index] != null)
         {
             foreach (DictionaryEntry entry in _buckets[index])
             {
                 if (entry.Key.Equals(key))
                 {
                     return true;
                 }
             }
         }

         return false;
     }
    
    /// <summary>
    /// Поиск по двум ключам в текущем и вложенном HashTable.
    /// </summary>
    /// <param name="outerKey">Ключ в таблице верхнего уровня</param>
    /// <param name="innerKey">Ключ в таблице нижнего уровня</param>
    /// <returns>
    /// true - Внутренний ключ найден в талице с внешним ключём.
    /// false - Не найден один из ключей, либо оба.
    /// </returns>
    public bool ContainsKey(object outerKey, object innerKey)
    {
        if (!ContainsKey(outerKey))
        {
            return false;
        }
        
        MyHashtable innerHashtable = (MyHashtable)Get(outerKey);

        return innerHashtable.ContainsKey(innerKey);
    }
    
    // public ICollection GetOuterKeys()
    // {
    //     return Keys;
    // }
    //
    // public ICollection GetInnerKeys(object outerKey)
    // {
    //     if (!ContainsKey(outerKey))
    //     {
    //         throw new ArgumentException("The specified outer key does not exist in the Hashtable.");
    //     }
    //
    //     MyHashtable innerHashtable = (MyHashtable)Get(outerKey);
    //
    //     return innerHashtable.Keys;
    // }
    
}
