namespace CM_ADS.CourseWork.Bing;


using System;
using System.Collections;

public class MyHashtable
{
    private ArrayList[] _buckets;
    private int _count;
    private float _loadFactor;

    public MyHashtable(int capacity = 10, float loadFactor = 0.75f)
    {
        _buckets = new ArrayList[capacity];
        _loadFactor = loadFactor;
        _count = 0;
    }

    public void Add(object key, object value)
    {
        if (_count >= _buckets.Length * _loadFactor)
        {
            Resize();
        }

        int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

        if (_buckets[index] == null)
        {
            _buckets[index] = new ArrayList();
        }

        foreach (DictionaryEntry entry in _buckets[index])
        {
            if (entry.Key.Equals(key))
            {
                throw new ArgumentException("An element with the same key already exists in the Hashtable.");
            }
        }

        _buckets[index].Add(new DictionaryEntry(key, value));
        _count++;
    }

    public object Get(object key)
    {
        int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

        if (_buckets[index] != null)
        {
            foreach (DictionaryEntry entry in _buckets[index])
            {
                if (entry.Key.Equals(key))
                {
                    return entry.Value;
                }
            }
        }

        throw new ArgumentException("The specified key does not exist in the Hashtable.");
    }

    public void Remove(object key)
    {
        int index = Math.Abs(key.GetHashCode()) % _buckets.Length;

        if (_buckets[index] != null)
        {
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
        }

        throw new ArgumentException("The specified key does not exist in the Hashtable.");
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

    public object Get(object outerKey, object innerKey)
    {
        MyHashtable innerHashtable = (MyHashtable)Get(outerKey);
        return innerHashtable.Get(innerKey);
    }

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
}
