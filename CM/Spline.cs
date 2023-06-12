using System.Drawing;

namespace CM_ADS.CM;

class Spline
{
    private Vector _x;
    private Vector _y;
    private int _n;
    private Vector _a;
    private Vector _b;
    private Vector _c;
    private Vector _d;

    public int Count() => _n;
    public Spline(Vector x, Vector y)
    {
        _x = x.Copy();
        _y = y.Copy();
        
        if (x.Size != y.Size)
        {
            throw new IOException("Размеры векторов не совпадают");
        }
        
        _n = x.Size - 1; // число интервалов
        _c = new Vector(_n);
        
        FindParams();
    }

    private void FindParams()
    {
        Vector a = FindA();
        
        Vector h = FindH();
        
        Vector C = FindC(h);
        
        Vector D = FindD(h);
        
        Vector E = FindE(h);
        
        //Вектор правых частей
        Vector B = new Vector(Count() - 1);
        for (int i = 0; i < Count() - 1; i++)
        {
            B[i] = 6 * ((_y[i + 2] - _y[i + 1]) / h[i + 1]) - ((_y[i + 1] - _y[i]) / h[i]);
        }
        Vector rp = Matrix.Sweep( C, D, E, B);
        for (int i = 0; i < Count() - 1; i++)
        {
            _c[i] = rp[i];
        }
        
        //Находим параметр d
        _d = new Vector(Count());
        for (int i = 0; i < Count(); i++)
        {
            if (i == 0)
            {
                _d[i] = (_c[i]) / h[i];
            }
            else
            {
                _d[i] = (_c[i] - _c[i - 1]) / h[i];
            }
        }
        //Находим параметр b
        _b = new Vector(Count());
        for (int i = 0; i < Count(); i++)
        {
            _b[i] = h[i] / 2 * _c[i] - h[i] * h[i] / 6 * _d[i] + (_y[i + 1] - _y[i]) / h[i];
        }
    }

    private Vector FindE(Vector h)
    {
        //Верхняя диагональ
        Vector E = new Vector(Count() - 1);
        for (int i = 0; i < Count() - 2; i++)
        {
            E[i] = h[i + 1];
        }

        E[Count() - 2] = 0;
        return E;
    }

    private Vector FindD(Vector h)
    {
        //Средняя диагональ
        Vector D = new Vector(Count() - 1);
        for (int i = 0; i < Count() - 1; i++)
        {
            D[i] = 2 * (h[i] + h[i + 1]);
        }

        return D;
    }

    private Vector FindC(Vector h)
    {
        //Нижняя диагональ
        Vector C = new Vector(Count() - 1);
        for (int i = 1; i < Count() - 1; i++)
        {
            C[i] = h[i];
        }

        C[0] = 0;
        return C;
    }

    private Vector FindH()
    {
        Vector h = new Vector(Count());
        for (int i = 1; i <= Count(); i++)
        {
            h[i - 1] = _x[i] - _x[i - 1];
        }

        return h;
    }

    private Vector FindA()
    {
        int n = Count();
        Vector h = new Vector(n);
        
        _a = new Vector(n);
        for (int i = 0; i < n; i++)
        {
            _a[i] = _y[i + 1];
        }

        return h;
    }
}