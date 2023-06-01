using System.Text;

namespace CM_ADS;

public class TestADS
{
    public static void Test()
    {
        
        StringBuilder taskString = new StringBuilder();

        taskString.AppendLine("Выберите тему:");
        taskString.AppendLine("✓1) Множества");
        taskString.AppendLine("✓2) Сортировки");
        taskString.AppendLine("✓3) Списки");
        taskString.AppendLine("✓4) Хэш-таблицы");
        taskString.AppendLine("5) Графы");
        taskString.AppendLine("6) Бинарное дерево");

        Console.WriteLine(taskString);
        
        String input = Console.ReadLine();
        
        Console.Clear();

        switch (input) {
            case "1":
                TestSet();
                break;
            
            case "2":
                TestSort();
                break;
            
            case "3":
                TestList();
                break;
            
            case "4":
                TestHashTable();
                break;
            
            case "5":
                TestGraph();
                break;
            
            case "6":
                TestBinaryTree();
                break;    
            
            default:
                return;
        }
        
    }
    
    
    private static void TestSet()
    {
        Set<int> s1 = new Set<int>(4);
        s1.Add(5);
        s1.Add(4);
        s1.Add(6);

        Set<int> s2 = new Set<int>(4);
        s2.Add(7);
        s2.Add(4);
        s2.Add(8);

        Set<int> s3 = s1 + s2;

        Console.WriteLine("s1{0}+{1}={2}", s1, s2, s3);
        Console.WriteLine("s1{0}*{1}={2}", s1, s2, s1*s2);
        Console.WriteLine("s1{0}-{1}={2}", s1, s2, s1-s2);
        Console.WriteLine("All subsets {0}", s1);

        List<Set<int>> ss = s1.SubSets();
        foreach (var s in ss)
            Console.WriteLine(s);

        Console.WriteLine("Множество множеств {0}", s3);
        List<Set<int>> ss2 = s3.AllSubSets();
        foreach (var s in ss2)
            Console.WriteLine(s);

    }
    
    private static void TestSort()
    {
        Console.WriteLine("Тестирование сортировок:");
        // Пример использования сортировки
        
        int[] ab = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Console.WriteLine("Bubble Sorting");
        Sort.BubbleSort<int>(ab);
        foreach (int el in ab) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();

        Console.WriteLine("Max Elem Sorting");
        int[] abc = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Sort.MaxElemSort<int>(abc);
        foreach (int el in abc) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();

        Console.WriteLine("Ins Sorting");
        int[] abcd = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Sort.InsertionSort<int>(abcd);
        foreach (int el in abcd) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();

        Console.WriteLine("Shell Sorting");
        int[] abcds = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Sort.ShellSort<int>(abcds);
        foreach (int el in abcds) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();

        
        Console.WriteLine("Radix Sorting");
        int[] abcdr = { 4, 5, 18, 0, 8, 3, 9, 14 };
        Console.WriteLine("Orig:");
        foreach (int el in abcdr) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();
        Sort.RadixSort(abcdr, abcdr.Length);
        Console.WriteLine("Sorted:");
        foreach (int el in abcdr) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();

        Console.WriteLine("Quicksort");
        int[] abcdm = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Sort.Quicksort<int>(abcdm, 0, abcdm.Length - 1);
        foreach (int el in abcdm) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();

        Console.WriteLine("MergeSort recursive");
        int[] abcdq = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Sort.MergeSortR<int>(abcdq, 0, abcdm.Length - 1);
        foreach (int el in abcdq) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();
        
        Console.WriteLine("MergeSort non-recursive");
        int[] abcdqm = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Sort.MergeSort<int>(abcdm, abcdm.Length);
        foreach (int el in abcdm) {
            Console.Write("{0} \t", el);
        }
        Console.WriteLine();
    }

    private static void TestList()
    {
        Console.WriteLine("Test Linked Data");
        ListItem<int, string>[] sns = { new ListItem<int, string>(1, "fff"), new ListItem<int, string>(5, "fi"), new ListItem<int, string>(10, "tttt") };
        var sLL = new SingleLinkedList<int , string>();
        sLL.AddStart(2, "Second");
        
        for (int i = 0; i < sns.Length; i++)
        {
            sLL.AddStart(sns[i].Key, sns[i].Value);
        }
        sLL.AddEnd(7, "Seven");
        
        sLL.PrintList();

        sLL.DelStart();

        sLL.PrintList();
        
        sLL.DelEnd();

        sLL.PrintList();

        sLL.AddAfter(1, new List<int, string>(21, "esp"));

        sLL.PrintList();

        sLL.AddBefore(1, new List<int, string>(3, "kazah"));

        sLL.PrintList();
        
        sLL.Flip();

        sLL.PrintList();
    }
    
    private static void TestHashTable()
    {
        HashTable<string, string> books = new HashTable<string, string>();
        books.Add("Baker", "How to live long");
        books.Add("Simon", "I see ya'll");

        // Console.WriteLine(books.GetValue("Baker"));

        foreach (string key in books.Values())
        {
            Console.WriteLine(key);
        }
        Console.WriteLine("");
        
        HashTable<string, string> stores = new HashTable<string, string>();
        stores.Add("Baker", "How to live long");
        stores.Add("Simon", "I see ya'll");

        // Console.WriteLine(books.GetValue("Baker"));

        foreach (string key in stores.Values())
        {
            Console.WriteLine(key);
        }

    }

    private static void TestGraph()
    {
        Graph<int> grapha = new Graph<int>();
        grapha.AddNode(33);
        grapha.AddNode(10);
        grapha.AddNode(1);
        grapha.AddNode(5);
        grapha.AddNode(81);
        
        grapha.AddEdge(1, 33);
        grapha.AddEdge(5, 81);
        grapha.AddEdge(81, 33);
        grapha.AddEdge(10, 1);
        grapha.AddEdge(10, 81);
        grapha.AddEdge(5, 1);
        
        List<int> bfs = grapha.BFS(81);
        foreach (int node in bfs)
        {
            Console.WriteLine(node);
        }
        Console.WriteLine("");
        List<int> dfs = grapha.DFS(81);
        foreach (int node in dfs)
        {
            Console.WriteLine(node);
        }
        return;
    }

    private static void TestBinaryTree()
    {
        BinaryTree<string> tree = new BinaryTree<string>();

        tree.Insert(1, "main");
        tree.Insert(10, "main-left");
        tree.Insert(11, "main-right");
        tree.Insert(0, "main-0");
        
        tree.ViewTree(tree.root);

        tree.Delete(11);
        
        tree.ViewTree(tree.root);

        return;
    }
    
}
