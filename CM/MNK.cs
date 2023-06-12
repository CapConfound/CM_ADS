namespace CM_ADS.CM;

class MNK
{
    public delegate double FuncPsi(double func);
    public FuncPsi[] func;

    private Vector x;
    private Vector y;
    //Вектор параметров
    public Vector Params;


    //Количесвто аргументов и пси-функций
    public int n, m;

    public MNK(Vector x, Vector y, FuncPsi[] func)
    {
        if (x.Size != y.Size) throw new ArithmeticException("Длины векторов не совпадают");
        
        this.x = x;
        this.y = y;
        this.n = x.Size;
        this.func = func;
        this.m = func.Length;

        //Создаем и заполняем матрицу Н
        Matrix H = new Matrix(n, m);
        for (int i = 0; i < n; i++)
        {
            H.SetRow(i, GetFunc(x[i]));
        }
        //Вычисляем вектор параметров
        Matrix Ht = H.Transpose();
        Matrix D = Ht * H;
        Matrix D_Inverse = Matrix.InvertedG(D);
        Matrix Q = D_Inverse * Ht;
        Params = Q * y;
    }

    private Vector GetFunc(double x)
    {
        Vector result = new Vector(m);
        for (int i = 0; i < m; i++)
        {
            result[i] = func[i](x);
        }
        return result;
    }

    public double GetCriteria()
    {
        //Вычисляем критерий МНК
        Vector result = new Vector(n);
        for (int i = 0; i < n; i++)
        {
            result[i] = y[i] - Params * GetFunc(x[i]);
        }
        return result.Norma1();
    }
}