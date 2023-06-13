using System.Drawing;

namespace CM_ADS.CM;

class Spline
{
    private Vector _x;
    private Vector _y;
    
    // Длина диагонали
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
        
        _n = x.Size - 1;
        _c = new Vector(_n);
        
        FindParams();
    }

    private void FindParams()
    {
        _a = new Vector(_n);
        for (int i = 0; i < _n; i++)
            _a[i] = _y[i + 1];
        
        //Формируем вектор h
        Vector h = new Vector(_n);
        for (int i = 1; i <= _n; i++)
            h[i - 1] = _x[i] - _x[i - 1];
     
        //Нижняя диагональ
        Vector underDia = new Vector(_n - 1);
        for (int i = 1; i < _n - 1; i++)
            underDia[i] = h[i];
        underDia[0] = 0;
        
        // Главная диагональ
        Vector mainDia = new Vector(_n - 1);
        for (int i = 0; i < _n - 1; i++)
            mainDia[i] = 2 * (h[i] + h[i + 1]);
        
        //Верхняя диагональ
        Vector overDia = new Vector(_n - 1);
        for (int i = 0; i < _n - 2; i++)
            overDia[i] = h[i + 1];
        overDia[_n - 2] = 0;
        
        //Вектор правых частей
        Vector F = new Vector(_n - 1);
        for (int i = 0; i < Count() - 1; i++)
            F[i] = 6 * ((_y[i + 2] - _y[i + 1]) / h[i + 1]) - ((_y[i + 1] - _y[i]) / h[i]);

        Vector rp = Matrix.Sweep(overDia, mainDia, underDia, F);
        
        for (int i = 0; i < Count() - 1; i++)
        {
            _c[i] = rp[i];
        }
        
        _d = new Vector(Count());
        
        for (int i = 0; i < Count(); i++)
        {
            if (i == 0)
                _d[i] = (_c[i]) / h[i];
            else
                _d[i] = (_c[i] - _c[i - 1]) / h[i];
        }
        
        _b = new Vector(Count());
        
        for (int i = 0; i < Count(); i++)
        {
            _b[i] = h[i] / 2 * _c[i] - h[i] * h[i] / 6 * _d[i] + (_y[i + 1] - _y[i]) / h[i];
        }
    }

}