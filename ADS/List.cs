
public class List<K, T> : ListItem<K,T>
{
    // Класс основан на классе ListItem<K,T>, который является информационной частью
    // узла односвязного списка 
    private List<K, T> next; // Ссылка на следующий узел
    // Конструкторы узла
    public List(K key, T value):base(key,value)
    {          
        this.next = null;
    }
    public List() : base()
    {
        this.next = null;
    }
    public List<K, T> Next // Свойство
    {
        get { return this.next; }
        set { this.next = value; }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}


