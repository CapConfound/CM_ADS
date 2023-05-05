using System.Text;

public class TestCM
{
    
    public void Test()
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
                TestSquaures();
                break;    
            
            case "5":
                TestSpline();
                break;    
            
            case "6":
                TestSpline();
                break;    
            
            default:
                return;
        }
        
        TestRootsSearch();
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

        Console.WriteLine("Обратная матрица методом Гаусса-Жордана");
        Matrix mG = new Matrix(3, 3);
        mG = Matrix.InvertedG(m1);
        Matrix.PrintMatrix(mG);
        
        Console.WriteLine("Обратная матрица. Метод квадратных корней");
        
    }

    private static void TestSpline()
    {
    }

    private static void TestSquaures()
    {
    }


    private static void TestDiff()
    {
    }

    private static void TestIntegral()
    {
    }
    
    
    
}