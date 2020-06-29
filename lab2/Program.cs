using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab2
{
    class Program
    {
        public const int SIZE = 10;
        public const int INF = 1000000000;

        static void Main(string[] args)
        {
            /*
            int[,] a = {{INF,7,9,INF,INF,14},
                       {7,INF,10,15,INF,INF},  // матрица путей
                       {9,10,INF,11,INF,2},
                       {INF,15,11,INF,6,INF},  // индексы по горизонтали из точки
                       {INF,INF,INF,6,INF,9},
                       {14,INF,2,INF,9,INF}};  // по вертикали в точку, значение - вес
             */
            Stopwatch stopwatch = new Stopwatch();
            
            
            int[,] a = new int[SIZE, SIZE];
            Graffcreate(ref a);    
            Deykstra(ref a, stopwatch);
            if(SIZE < 15)Perebor(ref a, stopwatch);
                
        }

        static void Deykstra(ref int[,] a, Stopwatch stopwatch)
        {
            stopwatch.Start();
            int[] d = new int[SIZE];            // массив найденных кратчайших путей, индексы - вершины графа
            int[] v = new int[SIZE];            // массив меток
            int buff, i;
            int mind, min;

            for (i = 0; i < SIZE; i++)
            {
                d[i] = INF;         // массив путей инициализируется бесконечностью
                v[i] = 1;
            }
            d[0] = 0;
            do
            {                       // начало выполнения алгоритма Дейсктры 
                mind = INF;
                min = INF;
                for (i = 0; i < SIZE; i++)
                {
                    if ((v[i] == 1) && (d[i] < min))
                    {
                        min = d[i];
                        mind = i;
                    }
                }
                if (mind != INF)
                {
                    for (i = 0; i < SIZE; i++)
                    {
                        if (a[mind,i] != INF)
                        {
                            buff = min + a[mind,i];
                            if (buff < d[i])
                                d[i] = buff;
                        }
                    }
                    v[mind] = 0;
                }
            } while (mind < INF);

            for (i = 0; i < SIZE; i++) Console.Write(d[i] + " ");
            Console.WriteLine();
            Console.WriteLine("Время работы алгоритма Дейкстры = " + stopwatch.ElapsedMilliseconds + " мсек");
            stopwatch.Reset();
            Console.ReadKey();
        }


        static void Perebor(ref int[,] a, Stopwatch stopwatch)
        {
            stopwatch.Start();
            
            int buff = 0;
            int[] p = new int[SIZE - 1];            //Массив упорядаченных всех вершин
            for (int i = 0; i < SIZE - 1; i++)
                p[i] = i + 1;

            int[] d = new int[SIZE];
            for (int i = 0; i < SIZE; i++)
                d[i] = INF;
            d[0] = 0;
            do
            {
                if (a[0, p[0]] != INF) { buff = a[0, p[0]]; } else continue;
                //Print(ref p, SIZE - 1);

                if (buff < d[p[0]])
                {
                    d[p[0]] = buff;
                }
                for(int i = 0; i < SIZE - 2; i++)
                {
                    if(a[p[i],p[i+1]] != INF)
                    {
                        buff = buff + a[p[i], p[i + 1]];
                        if(buff < d[p[i + 1]])
                        {
                            d[p[i + 1]] = buff;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            } while (NextSet(ref p, SIZE - 1));
            
            for(int i = 0; i < SIZE; i++)
            {
                Console.Write(d[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Время работы алгоритма полного перебора = " + stopwatch.ElapsedMilliseconds + " мсек");
            stopwatch.Reset();
            Console.ReadKey();
        }        

        static void Swap(ref int[] p, int i, int j)
        {
            int s = p[i];
            p[i] = p[j];
            p[j] = s;
        }

        static bool NextSet(ref int[] p, int n)
        {
            int j = n - 2;
            while (j != -1 && p[j] >= p[j + 1]) j--;
            if (j == -1)
                return false; // больше перестановок нет
            int k = n - 1;
            while (p[j] >= p[k]) k--;
            Swap(ref p, j, k);
            int l = j + 1, r = n - 1; // сортируем оставшуюся часть последовательности
            while (l < r)
                Swap(ref p, l++, r--);
            return true;
        }

        static void Print(ref int[] p, int n)  // вывод перестановки
        {
            for (int i = 0; i < SIZE - 1; i++)
                Console.Write(p[i] + " ");
            Console.WriteLine();
        }

        static void Graffcreate(ref int[,] a)
        {
            Random rnd = new Random();
            for (int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    if (i <= j)
                    {
                        a[i, j] = rnd.Next(5, 25) + j * Math.Abs(i - j);
                        a[j, i] = a[i, j];
                        if(i == j || a[i,j] % 2 == 0 || Math.Abs(i - j) > 5)
                        {
                            a[i, j] =  INF;
                            a[j, i] = INF;
                        }
                    }
                    /*
                    if (a[i, j] != INF)
                    {
                        Console.Write("{0,3}", a[i, j]);
                    }
                    else
                    {
                        Console.Write(" oo");
                    }
                    */
                }
                //Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
