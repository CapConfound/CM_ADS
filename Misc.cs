

// public class Item<K,T>
// {
//     protected K key;
//     protected T value;
//     public T Value
//     {
//         get { return this.value; }
//         set { this.value = value; }
//     }
//     public K Key
//     {
//         get { return this.key; }
//         set { this.key = value; }
//     }
//     public override string ToString()
//     {
//         return string.Format("(key={0},Value={1})", Key, Value);
//     }
//     public Item() { this.key = default(K);  this.value = default(T); }
//     public Item(K key,T value) { this.key = key; this.value = value; }
// }
// class singleNode<K, T>:Item<K,T>
// {
//     private singleNode<K, T> next;
    
//     public singleNode(K key, T value):base(key,value)
//     {          
//         this.next = null;
//     }
//     public singleNode() : base()
//     {
//         this.next = null;
//     }
//     public singleNode<K, T> Next
//     {
//         get { return this.next; }
//         set { this.next = value; }
//     }
    
//     public override string ToString()
//     {
//         return base.ToString();
//     }
// }
// //Узел  Бинарного дерева
// class BinaryNode<T>
// {
//     public int Key;
//     public T Value;
//     public BinaryNode<T> Parent;
//     public BinaryNode<T> Left;
//     public BinaryNode<T> Right;

//     public BinaryNode(int k, T v)
//     {
//         Key = k;
//         Value = v;
//         Parent = null;
//         Left = null;
//         Right = null;
//     }
//     public BinaryNode()
//     {
//         Key = int.MaxValue;
//         Value = default(T);
//         Parent = null;
//         Left = null;
//         Right = null;
//     }
//     public override string ToString()
//     {
//         string ret =string.Format("(Key = {0} Value = {1})", Key, Value);
//         if (Parent != null) ret = ret + string.Format(" Parent key={0}", Parent.Key);
//         if (Left != null) ret = ret + string.Format(" Left key={0}", Left.Key);
//         if (Right != null) ret = ret + string.Format(" Right key={0}", Right.Key);
//         return ret;
//     }

// }

// class Matrix
// {
//     protected int rows, columns;
//     protected double[,] data;
//     public Matrix(int r, int c)
//     {
//         this.rows = r; this.columns = c;
//         data = new double[rows, columns];
//         for (int i = 0; i < rows; i++)
//             for (int j = 0; j < columns; j++) data[i, j] = 0;
//     }
//     public Matrix(double[,] mm)
//     {
//         this.rows = mm.GetLength(0); this.columns = mm.GetLength(1);
//         data = new double[rows, columns];
//         for (int i = 0; i < rows; i++)
//             for (int j = 0; j < columns; j++)
//                 data[i, j] = mm[i, j];
//     }
//     public int Rows { get { return rows; } }
//     public int Columns { get { return columns; } }

//     public double this[int i, int j]
//     {
//         get
//         {
//             if (i < 0 && j < 0 && i >= rows && j >= columns)
//             {
//                 // Console.WriteLine(" Индексы вышли за пределы матрицы ");
//                 return Double.NaN;
//             }
//             else
//                 return data[i, j];
//         }
//         set
//         {
//             if (i < 0 && j < 0 && i >= rows && j >= columns)
//             {
//                 //Console.WriteLine(" Индексы вышли за пределы матрицы ");
//             }
//             else
//                 data[i, j] = value;
//         }
//     }
//     public Vector GetRow(int r)
//     {
//         if (r >= 0 && r < rows)
//         {
//             Vector row = new Vector(columns);
//             for (int j = 0; j < columns; j++) row[j] = data[r, j];
//             return row;
//         }
//         return null;
//     }
//     public Vector GetColumn(int c)
//     {
//         if (c >= 0 && c < columns)
//         {
//             Vector column = new Vector(rows);
//             for (int i = 0; i < rows; i++) column[i] = data[i, c];
//             return column;
//         }
//         return null;
//     }
//     public bool SetRow(int index, Vector r)
//     {
//         if (index < 0 || index > rows) return false;
//         if (r.Size != columns) return false;
//         for (int k = 0; k < columns; k++) data[index, k] = r[k];
//         return true;
//     }
//     public bool SetColumn(int index, Vector c)
//     {
//         if (index < 0 || index > columns) return false;
//         if (c.Size != rows) return false;
//         for (int k = 0; k < rows; k++) data[k, index] = c[k];
//         return true;
//     }
//     public void SwapRows(int r1, int r2)
//     {
//         if (r1 < 0 || r2 < 0 || r1 >= rows || r2 >= rows || (r1 == r2)) return;
//         Vector v1 = GetRow(r1);
//         Vector v2 = GetRow(r2);
//         SetRow(r2, v1);
//         SetRow(r1, v2);
//     }
//     public Matrix Copy()
//     {
//         Matrix r = new Matrix(rows, columns);
//         for (int i = 0; i < rows; i++)
//             for (int j = 0; j < columns; j++) r[i, j] = data[i, j];
//         return r;
//     }
//     public Matrix Trans()
//     {
//         Matrix transposeMatrix = new Matrix(columns, rows);
//         for (int i = 0; i < rows; i++)
//         {
//             for (int j = 0; j < columns; j++)
//             {
//                 transposeMatrix.data[j, i] = data[i, j];
//             }
//         }
//         return transposeMatrix;
//     }
//     //Сложение матриц
//     public static Matrix operator +(Matrix m1, Matrix m2)     //Сложение матриц
//     {
//         if (m1.rows != m2.rows || m1.columns != m2.columns)
//         {
//             throw new Exception("Матрицы не совпадают по размерности");
//         }
//         Matrix result = new Matrix(m1.rows, m1.columns);

//         for (int i = 0; i < m1.rows; i++)
//         {
//             for (int j = 0; j < m2.columns; j++)
//             {
//                 result[i, j] = m1[i, j] + m2[i, j];
//             }
//         }
//         return result;
//     }
// }
