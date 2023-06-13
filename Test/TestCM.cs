using System.Text;


namespace CM_ADS.CM;

public class TestCM
{
    
    public static void Test()
    {

        StringBuilder taskString = new StringBuilder();

        taskString.AppendLine("Выберите тему:");
        taskString.AppendLine("1) Вычисление корней");
        taskString.AppendLine("2) Матричные операции и метод последовательных приближений");
        taskString.AppendLine("3) Spline");
        taskString.AppendLine("4) Метод наименьших квадраов");
        taskString.AppendLine("5) Вычисление определенного интеграла");
        taskString.AppendLine("6) Решение системы дифференциальных уравнений");
        

        Console.WriteLine(taskString);
        
        String input = Console.ReadLine();
        
        
        Console.Clear();

        switch (input) {
            case "1":
                TestRootsSearch();
                break;
            
            case "2":
                TestMatrix();
                break;    
            
            case "3":
                TestSpline();
                break;    
            
            case "4":
                TestMNK();
                break;    
            
            case "5":
                TestIntegral();
                break;    
            
            case "6":
                TestDiff();
                break;    
            
            default:
                return;
        }
        
    }
    
    // Численные методы ЛР 1 Поиск корня уравнения скаларной функции
    private static void TestRootsSearch()
    {
        double xs = FindRoots.PolDel(1, 3, 0.0001, Math.Sin);
        double xc = FindRoots.Newton(30, 0.0001, Math.Sin);
        double xd = FindRoots.Approximation(30, 0.0001, Math.Sin);

        Console.WriteLine(xs);
        Console.WriteLine(xc);
        Console.WriteLine(xd);
    }
    
    private static void TestMatrix()
    {
        double[] v = {4, 2, 5};
        Vector vec1 = new Vector(v);

        double[,] m = {{4, 2, 1}, {4,1,4}, {3,2,1}};
        Matrix m1 = new Matrix(m);

        Matrix mG = new Matrix(3, 3);
        mG = Matrix.InvertedG(m1);
        if (mG != null)
        {
            Console.WriteLine("Обратная матрица методом Гаусса-Жордана");
            Matrix.PrintMatrix(mG);
        }

        Console.WriteLine("Givens");
        double[,] gg = { { 4, 2, 5 }, { 3, 5, 4 }, {7, 1, 8}};
        Matrix mat = new Matrix(gg);
        Matrix.PrintMatrix(mat);
        
        Vector gv = Matrix.Givens(mat, vec1);
        double[] els = gv.GetElements();
        foreach (var el in els)
        {
            Console.Write(el + " ");
        }
        Console.WriteLine("");
        

        Console.WriteLine("MPP");
        double[,] ppv = {{10, 2, -1}, {-2, -6, -1}, {1, -3, 12}};
        Matrix pp = new Matrix(ppv);
        double[] vv = { 5, 24.42, 36 };
        Vector vec = new Vector(vv);
        Vector res = pp.IterativeApprox(pp, vec);

        for (int i = 0; i < res.Size; i++)
        {
            Console.Write(res[i] + " ");
        }
    }

    private static void TestSpline()
    {
    }

    private static void TestMNK()
    {
        
    }


    private static void TestDiff()
    {
    }

    private static void TestIntegral()
    {
    }
    
    
    
}