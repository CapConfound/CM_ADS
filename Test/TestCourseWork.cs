using System.Collections;
using CM_ADS.CourseWork.Bing;

namespace CM_ADS.CW;

public class TestCourseWork
{
    public static void TestCW()
    {
        // HashTable<int, HashTable<string, string>> outer = new HashTable<int, HashTable<string, string>>();
        //
        // HashTable<string, string> inner = new HashTable<string, string>();
        //
        // inner.Add("Tank", "Reinhardt");
        // inner.Add("Tank", "Ball");
        // outer.Add(1, inner);


        // List<string> vals = outer.Values();
        //
        //
        // HashTableNested<string, int, bool> texas = new HashTableNested<string, int, bool>();
        // texas.Check();

        
        // Мой класс
        // HashTableNested nest = new HashTableNested();
        // HashTable<int, string> inner1 = new HashTable<int, string>();
        // HashTable<string, bool> inner2 = new HashTable<string, bool>(); 
        //     
        // inner1.Add(69, "N1ce");
        // inner1.Add(420, "FuNnY NuMbEr");
        //
        // inner2.Add("pride", true);
        // inner2.Add("prideRus", false);
        //
        // nest.Add(5, inner1);
        // nest.Add("35", inner2);
        //
        // object val = nest.GetValue("35", "pride");
        //
        // Console.WriteLine(val);
        
        // БИНГ(О)
        // HashTable<int, HashTable<int, string>> nest = new HashTable<int, HashTable<int, string>();
        //
        // HashTable<int, string> inner1 = new HashTable<int, string>();
        // HashTable<int, string> inner2 = new HashTable<int, string>(); 
        //     
        // inner1.AddOrUpdate(69, "N1ce");
        // inner1.AddOrUpdate(420, "FuNnY NuMbEr");
        //
        // inner2.AddOrUpdate(32, "pride" );
        // inner2.AddOrUpdate(91, "prideRus");
        //
        // nest.AddOrUpdate(5, inner1);
        // nest.AddOrUpdate(35, inner2);
        //
        // object val = nest.TryGetValue(35, "pride");
        //
        // Console.WriteLine(val);
        //
        
        MyHashtable nest = new MyHashtable();

        MyHashtable inner1 = new MyHashtable();
        MyHashtable inner2 = new MyHashtable(); 
            
        inner1.Add(69, "N1ce");
        inner1.Add(420, "FuNnY NuMbEr");
        
        inner2.Add(32, "pride" );
        inner2.Add(91, "prideRus");
        
        nest.Add(5, inner1);
        nest.Add(35, inner2);
        nest.Add(135, "inner2");
        
        object val = nest.Get(135);
        
        Console.WriteLine(val);

        
        
    }

    private static void TestDefaultBehavior()
    {
        Hashtable h1 = new Hashtable();
        
        h1.Add(1, "322");
        h1.Add("231", 43);
        h1.Add(6, true);

        object col = h1[1];
        object col1 = h1[6];
        object col2 = h1["231"];
        
        Console.WriteLine(col.ToString());
        Console.WriteLine(col1.ToString());
        Console.WriteLine(col2.ToString());
    }
}