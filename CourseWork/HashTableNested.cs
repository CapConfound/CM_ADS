// namespace CM_ADS;
//
// public class HashTableNested
// {
//     private readonly List<KeyValuePair<object, object>>[] _items;
//     
//     private int _count;
//     
//     private int _capacity;
//
//     // Коэффициент загрузки хеш-таблицы
//     private const double LoadFactor = 0.75;
//
//     // Емкость хеш-таблицы по умолчанию
//     private const int DefaultCapacity = 10;
//
//     public HashTableNested()
//     {
//         _capacity = DefaultCapacity;
//         _count = 0;
//         _items = new List<KeyValuePair<object, object>>[_capacity];
//     }
//     
//     public HashTableNested(int capacity)
//     {
//         _capacity = capacity;
//         _count = 0;
//         _items = new List<KeyValuePair<object, object>>[capacity];
//     }
//     
//     public int GetSize()
//     {
//         return _capacity;
//     }
//
//     public int GetCount()
//     {
//         return _count;
//     }
//     
//     private int GetIndex(object key)
//     {
//         int hashCode = key.GetHashCode();
//         int index = hashCode % _capacity;
//
//         // Убедитесь, что индекс неотрицательный
//         if (index < 0)
//         {
//             index += _capacity;
//         }
//
//         return index;
//     }
//     
//     public void Add(object key, object value)
//     {
//         // При необходимости изменяем размер внутреннего массива
//         if ((double)_count / _capacity >= LoadFactor)
//         {
//             Resize();
//         }
//
//         // Находим индекс слота, в который нужно добавить новый элемент
//         int index = GetIndex(key);
//
//         // Если ключ уже существует, заменяем значение
//         if (_items[index] != null)
//         {
//             _items[index] = new KeyValuePair<object, object>(key, value);
//         }
//         else // В противном случае добавляем новую пару ключ-значение
//         {
//             _items[index] = new KeyValuePair<object, object>(key, value);
//             _count++;
//         }
//     }
//
//     public object GetValue(TKey key, TKeyInner keyInner)
//     {
//         // Находим индекс слота, содержащего элемент
//         int index = this.GetIndex(key);
//
//         // Если ключ существует, вернуть его значение
//         if (_items[index].Key != null && _items[index].Key.Equals(key))
//         {
//             var value = _items[index].Value;
//             if (value.GetType().Equals(typeof(HashTable<TKey, TValue>)))
//             {
//                 HashTable<TKey, TValue> tval = new HashTable<TKey, TValue>();
//                 tval.Add(key, value);
//                 return tval.GetInnerValue(keyInner);
//             }
//             return ((HashTable<object, object>)_items[index].Value).GetValue(keyInner);
//             
//             
//             //return innerTable; //.GetValue(keyInner);
//         }
//
//         // В противном случае выбрасываем исключение
//         throw new KeyNotFoundException();
//     }
//
//     
//     // Удаляет пару ключ-значение из хеш-таблицы
//     public bool Remove(object key)
//     {
//         // Находим индекс слота, содержащего удаляемый элемент
//         int index = GetIndex(key);
//
//         // Если ключ существует, удаляем пару ключ-значение
//         if (_items[index].Key != null && _items[index].Key.Equals(key))
//         {
//             _items[index] = new KeyValuePair<object, object>(default, default);
//             _count--;
//             return true;
//         }
//
//         return false;
//     }
//     
//     public TValue GetInnerValue(TKeyInner key)
//     {
//         // Находим индекс слота, содержащего элемент
//         int index = GetIndex(key);
//
//         // Если ключ существует, вернуть его значение
//         if (_items[index].Key != null && _items[index].Key.Equals(key))
//         {
//             return _items[index].Value;
//         }
//
//         // В противном случае выбрасываем исключение
//         throw new KeyNotFoundException();
//     }
//     
//     private void Resize()
//     {
//         _capacity *= 2;
//         KeyValuePair<object, object>[] old_items = _items;
//         _items = new KeyValuePair<object, object>[_capacity];
//         _count = 0;
//
//         // Повторно добавляем все элементы в новый массив
//         foreach (KeyValuePair<object, object> elem in old_items)
//         {
//             if (elem.Key != null)
//             {
//                 Add(elem.Key, elem.Value);
//             }
//         }
//     }
//     
//     
// }