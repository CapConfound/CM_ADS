public class ListItem<K,T>
{
    protected K key; // Ключ
    protected T value; // Значение
    public T Value // Свойство
    {
        get { return this.value; }
        set { this.value = value; }
    }
    public K Key // Свойство
    {
        get { return this.key; }
        set { this.key = value; }
    }
    public override string ToString()
    {
        return string.Format("(key={0},Value={1})", Key, Value);
    }
    // Конструкторы
    public ListItem()
    {
        this.key = default(K);  this.value = default(T);
    }

    public ListItem(K key, T value)
    {
        this.key = key; this.value = value;
    }
}