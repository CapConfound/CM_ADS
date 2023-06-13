class Set<T> where T : IComparable
{        
    int size;
    int n;
    T[] data;
    // bool done;
    //   int[] v;
    public Set(int n)
    {
        size = 0;
        if (n < 0){
            this.n = 0;
            this.data = new T[n];
            return;
        }
        data = new T[n];
        this.n = n;
    }
    public int GetSize() { return n; }
    public int GetCount() { return size; }
    public int Count { get { return size; } } // Количество элементов в множестве

    public int GetIndex(T value)
    {
        int i;
        int select = -1;
        for (i = 0; i < size; i++)
            if (data[i].Equals(value)) {
                select = i; break;
            }
        return select;
    }
    public bool IsContains(T element)
    {
        for (int i = 0; i < size; i++) {
            if (data[i].CompareTo(element) == 0) return true;
        }
        return false;
    }

     public T this[int index]
    {
        get { if(index>=0 && index<size) return data[index]; else return default; }
    }
    public bool Exists(T value)
    {
        if (GetIndex(value) >= 0) return true;
        return false;
    }
    public void Add(T value)
    {
        int i;
        int select = GetIndex(value);
        if (select != -1) return;
        
        if (size < n) {
            data[size] = value; size++; 
        } else {
            T[] temp = new T[n];
            for (i = 0; i < n; i++) temp[i] = data[i];
            data = new T[n * 2];
            for (i = 0; i < n; i++) data[i] = temp[i];
            n = 2 * n;
            data[size] = value; size++;
        }
        
    }
    public void RemoveAt(int index)
    {
        if (index < 0 && index <= size) return;
        
        int i = 0;
        for (i = index; i < size - 1; i++)
            data[i] = data[i + 1];
        data[size] = default(T)!;
        size--;
        
    }
    public void Remove(T value)
    {
        int index = GetIndex(value);
        RemoveAt(index);
    }
    public T GetValue(int index)
    {
        if (index >= 0 && index < size) return data[index];
        return default(T)!;
    }
    public override string ToString()
    {
        string ss = "{";
        int i;
        for (i = 0; i < size; i++)
            if (i < (size - 1)) ss = ss + string.Format("{0},", data[i]);
            else ss = ss + string.Format("{0}", data[i]);
        ss = ss + "}";

        return ss;
    }

    public Set<T> Union(Set<T> ss)
    {
        int nn = this.n + ss.GetSize();
        Set<T> rez = new Set<T>(nn);
        int i;
        for (i = 0; i < size; i++)
            rez.Add(data[i]);
        for (i = 0; i < ss.GetCount(); i++)
            rez.Add(ss.GetValue(i));
        return rez;
    }

    public Set<T> Intersection(Set<T> s2)
    {
        Set<T> newSet = new Set<T>(this.size + s2.size);
        for(int i = 0; i < s2.size; i++)
        {
            if (this.IsContains(s2[i])) newSet.Add(s2[i]);    
        }
        return newSet;
    }

    public Set<T> Except(Set<T> s2)
    {
        Set<T> newSet = new Set<T>(this.size + s2.size);
        for(int i = 0; i < s2.size; i++)
        {
            if (!s2.IsContains(this[i])) newSet.Add(this[i]);
        }
        return newSet;
    }
    public static Set<T> operator +(Set<T> s1, Set<T> s2) => s1.Union(s2);

    public static Set<T> operator *(Set<T> s1, Set<T> s2) => s1.Intersection(s2);

    public static Set<T> operator -(Set<T> s1, Set<T> s2) => s1.Except(s2);

    public List<Set<T>> SubSets()
    {
        List<Set<T>> LL = new List<Set<T>>();
        for(int i=1;i<=size;i++)
        {
            Set<T> L = new Set<T>(i);
            for (int j = 0; j <i; j++)
            {
                L.Add(this.data[j]);
            }
            LL.Add(L);
        }
        return LL;
    }
    
    //множество множеств
    public List<Set<T>> AllSubSets()
    {
        // Вычисляю количество подмноженств
        int n = (int)Math.Pow(2, this.size);
        
        List<Set<T>> LL = new List<Set<T>>();
        
        // Первый элемент - пустое множество
        LL.Add(new Set<T>(0));

        for(int i = 1; i < n; i++)
        {
            Set<T> L = new Set<T>(this.size);

            for (int j = 0; j < n; j++)
            {
                
                if ((i & (1 << j)) > 0)
                    L.Add(this[j]);
            }
            LL.Add(L);
        }
        return LL;
    }
    
}
