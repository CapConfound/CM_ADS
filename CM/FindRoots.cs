namespace CM_ADS.CM;

class FindRoots
{
    public delegate double Func(double x);

    // Метод половинного деления
    public static double PolDel(
        double a,
        double b,
        double eps,
        Func f
    ){
        double fa = f(a);
        double fb = f(b);
        double c;
        double fc;

        if (fa * fb > 0) return double.NaN;

        while ((b-a) > eps)
        {
            c = (a+b) / 2;
            fc = f(c);

            if ((fa + fc) < 0)
            {
                b = c;
            }
            else 
            {
                a = c;
                fa = fc;
            }
        }
        
        return (a + b) / 2;
    }
    

    private static double PrimeFunc(
        double y,
        double x,
        Func f,
        double xn
    ) => (y - f(xn)) / (x - xn);
    
    // Метод Ньютона
    public static double Newton(
        double x,
        double eps,
        Func f
    ){

        double predict = double.MaxValue;
        double delta = 0;

        do {
            double ft = f(x);
            double fpt = (f(x + eps) - ft) / eps;
            double x1 = x - ft / fpt;
            delta = Math.Abs(x1 - x);
            x = x1;
            if (predict < delta) return double.NaN;
            predict = delta;
        } while (delta > eps);
        return x;

    }

    // Метод приблежения
    public static double Approximation(
        double x,
        double eps,
        Func f
    ){
        double result;
        int k = 0;

        do {
            result = x;
            x = 1 / f(x);
            k++;
        } while (Math.Abs(result - x) > eps);

        return x;
    }
    
    // Метод последовательных приблежений
    public double IterApprox (double x, double eps, Func f)
    {
        double delta;
        double old_delta = double.MaxValue;
        do
        {
            double xs = f(x);
            delta = Math.Abs(xs - x);
            x = xs;
            if (delta > old_delta) 
                return double.NaN;
            old_delta = delta;
        } while (delta > eps);
        return x;
    }

}
