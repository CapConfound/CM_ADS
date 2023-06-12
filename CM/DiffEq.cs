namespace CM_ADS.CM;

class DiffEq
{
    public delegate Vector PravDU(double t, Vector x);

        // Метод Эйлера
        public static Matrix Euler(double tn, double tk, Vector xn, int m, PravDU prdu)
        {
            int n = xn.Size;
            Matrix result = new Matrix(n + 1, m + 1);
            Vector pr; Vector xt;
            double dt = (tk - tn) / m;
            double t = tn;
            Vector ColumnRes = new Vector(n + 1);
            
            ColumnRes[0] = t;
            for (int i = 0; i < n; i++)
                ColumnRes[i + 1] = xn[i];
            
            result.SetColumn(0, ColumnRes);
            xt = xn.Copy();
            
            for (int k = 1; k <= m; k++)
            {
                pr = prdu(t, xt);
                xt = xt + pr * dt;
                t = t + dt;
                ColumnRes[0] = t;
                for (int i = 0; i < n; i++)
                    ColumnRes[i + 1] = xt[i];
                
                result.SetColumn(k, ColumnRes);
            }

            return result;
        }

        // Метод Рунге-Кутта 2-го порядка
        public static Matrix RK2(double tn, double tk, Vector xn, int m, PravDU prdu)
        {
            int n = xn.Size;
            Matrix result = new Matrix(n + 1, m + 1);
            Vector pr; Vector pr_1;
            Vector xt; Vector xs;
            double dt = (tk - tn) / m;
            double t = tn;
            Vector ColumnRes = new Vector(n + 1);
            ColumnRes[0] = t;
            for (int i = 0; i < n; i++)
                ColumnRes[i + 1] = xn[i];
            result.SetColumn(0, ColumnRes);
            xt = xn.Copy();
            for (int k = 1; k <= m; k++)
            {
                pr = prdu(t, xt);
                xs = xt + pr * dt;
                t = t + dt;
                pr_1 = prdu(t, xs);
                xt = xt + (pr + pr_1) * dt * 0.5;
                ColumnRes[0] = t;
                for (int i = 0; i < n; i++)
                    ColumnRes[i + 1] = xt[i];
                result.SetColumn(k, ColumnRes);
            }

            return result;
        }

        // Метод Рунге-Кутта 4-го порядка
        public static Matrix RK4(double tn, double tk, Vector xn, int m, PravDU prdu)
        {
            int n = xn.Size;
            Matrix result = new Matrix(n + 1, m + 1);
            Vector pr; Vector pr_1; Vector pr_2; Vector pr_3; Vector pr_4;
            Vector xt;
            Vector xs; Vector xs_1; Vector xs_2; Vector xs_3;
            double dt = (tk - tn) / m;
            double t = tn;
            Vector ColumnRes = new Vector(n + 1);
            ColumnRes[0] = t;
            for (int i = 0; i < n; i++)
                ColumnRes[i + 1] = xn[i];
            result.SetColumn(0, ColumnRes);
            xt = xn.Copy();
            for (int k = 1; k <= m; k++)
            {
                pr = prdu(t, xt);
                xs = xt + pr * dt * 0.5;
                pr_1 = prdu(t + dt * 0.5, xs);
                xs_1 = xt + pr_1 * dt * 0.5;
                pr_2 = prdu(t + dt * 0.5, xs_1);
                xs_2 = xt + pr_2 * dt * 0.5;
                t = t + dt;
                pr_3 = prdu(t, xs_2);
                xs_3 = xt + pr_3 * dt;
                pr_4 = prdu(t, xs_3);
                xt = xt + (dt / 6) * (pr + 2 * pr_1 + 2 * pr_2 + pr_4);
                ColumnRes[0] = t;
                for (int i = 0; i < n; i++)
                    ColumnRes[i + 1] = xt[i];
                result.SetColumn(k, ColumnRes);
            }

            return result;
        }

        // Метод Аддамса
        public static Matrix Addams(double tn, double tk, Vector xn, int m, PravDU prdu)
        {
            int n = xn.Size;
            Matrix result = new Matrix(n + 1, m + 1);
            Vector ColumnRes = new Vector(n + 1);
            Vector xt; double t; double dt;
            Vector xs; Vector xs_1; Vector xs_2; Vector xs_3;
            Vector pr; Vector pr_1; Vector pr_2; Vector pr_3; Vector pr_4;
            List<Vector> list_of_pr = new List<Vector>(4);

            t = tn;
            dt = (tk - tn) / m;
            ColumnRes[0] = t;
            for (int i = 0; i < n; i++)
                ColumnRes[i + 1] = xn[i];
            result.SetColumn(0, ColumnRes);
            xt = xn.Copy();

            for (int k = 1; k <= 3; k++)
            {
                pr = prdu(t, xt);
                list_of_pr.Add(pr);
                xs = xt + pr * dt * 0.5;
                pr_1 = prdu(t + dt * 0.5, xs);
                xs_1 = xt + pr_1 * dt * 0.5;
                pr_2 = prdu(t + dt * 0.5, xs_1);
                xs_2 = xt + pr_2 * dt * 0.5;
                t = t + dt;
                pr_3 = prdu(t, xs_2);
                xs_3 = xt + pr_3 * dt;
                pr_4 = prdu(t, xs_3);
                xt = xt + (dt / 6) * (pr + 2 * pr_1 + 2 * pr_2 + pr_4);
                ColumnRes[0] = t;
                for (int i = 0; i < n; i++)
                    ColumnRes[i + 1] = xt[i];
                result.SetColumn(k, ColumnRes);
            }

            for (int k = 4; k <= m; k++)
            {
                list_of_pr.Add(prdu(t, xt));
                xt = xt + (dt / 24.0) * (55 * list_of_pr[k - 1] - 59 * list_of_pr[k - 2] +
                    37 * list_of_pr[k - 3] - 9 * list_of_pr[k - 4]);
                t = t + dt;
                ColumnRes[0] = t;
                for (int i = 0; i < n; i++)
                    ColumnRes[i + 1] = xt[i];
                result.SetColumn(k, ColumnRes);

            }
            return result;

        }
}