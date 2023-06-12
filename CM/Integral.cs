namespace CM_ADS.CM;

public delegate double InputIntegral(double input_x);
public delegate double InputDoubleIntegral(double input_x, double input_y);
class Integral
{
    // Метод прямоугольника
    public static double Rectangle(double a, double b, double eps, InputIntegral f)
    {
        int n; double h;
        double fpr, fcur, s;
        double difference;

        n = 1; h = (b - a) / n;
        s = f(a); fpr = s * h;
        
        do
        {
            n *= 2; h = (b - a) / n;
            for (int i = 1; i < n; i += 2)
            {
                s += f(a + i * h);
            }
            fcur = s * h;
            difference = fcur - fpr;
            fpr = fcur;
        }
        while (Math.Abs(difference) > eps);
        return fpr;
    }

    // Метод трапеции
    public static double Trapezoid(double a, double b, double eps, InputIntegral f)
    {
        int n; double h;
        double fpr, fcur, s;
        double difference;

        n = 1; h = (b - a) / n;
        s = f(a) + f(b); fpr = s * h / 2;
        do
        {
            n *= 2; h = (b - a) / n;
            for (int i = 1; i < n; i += 2)
            {
                s += f(a + i * h);
            }
            fcur = s * h;
            difference = fcur - fpr;
            fpr = fcur;
        }
        while (Math.Abs(difference) > eps);
        return fpr;

    }

    // Метод Симпсона
    public static double Simpsons(double a, double b, double eps, InputIntegral f)
    {
        int n = 1; double h;
        double sum_1, sum_2 = 0;
        double fpr = 0, fcur = 1, difference = fpr - fcur;
        double f_0 = f(a) + f(b);

        while (Math.Abs(difference) > eps)
        {
            n *= 2; h = (b - a) / n;
            sum_1 = 0;
            for (int i = 1; i < n; i += 2)
                sum_1 += f(a + i * h);
            fcur = h / 3 * (f_0 + 2 * sum_2 + 4 * sum_1);
            difference = fpr - fcur;
            fpr = fcur;
            sum_2 += sum_1;
        }
        return fpr;
    }

    // Вычисление двойного интерграла
    public static double DoubleIntegral(double a, double A, double b, double B, int n, int m, InputDoubleIntegral f)
    {
        double h = (A - a) / (2 * n);
        double k = (B - b) / (2 * m);
        double sum = 0;

        int[] first_last = new int[2 * n + 1];
        first_last[0] = 1; first_last[2 * n] = 1;
        bool flag = true;
        for (int i = 1; i < first_last.Length - 1; i++)
        {
            if (flag)
                first_last[i] = 4;
            else
                first_last[i] = 2;
            flag = !flag;
        }


        for (int i = 0; i < 2 * n + 1; i++)
        {
            for (int j = 0; j < 2 * m + 1; j++)
            {
                int lambda = first_last[i];
                if (j > 0 && j < 2 * m)
                {
                    if (j % 2 == 1)
                        lambda *= 4;
                    else
                        lambda *= 2;
                }
                double fcur = f(a + h * i, b + k * j);
                sum += lambda * fcur;
            }
        }
        return (h * k * sum) / 9;
    }
}
