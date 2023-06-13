
public class TwoWayList<K, T> : ListItem<K,T>
{
    // Класс основан на классе ListItem<K,T>, который является информационной частью
    // узла односвязного списка 
    private TwoWayList<K, T> next; // Ссылка на следующий узел
    private TwoWayList<K, T> prev; // Ссылка на предыдущий узел
    // Конструкторы узла
    public TwoWayList(K key, T value):base(key,value)
    {          
        this.next = null;
    }
    public TwoWayList() : base()
    {
        this.next = null;
    }
    public TwoWayList<K, T> Next // Свойство
    {
        get { return this.next; }
        set { this.next = value; }
    }
    
    public TwoWayList<K, T> Prev // Свойство
    {
        get { return this.prev; }
        set { this.prev = value; }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}


