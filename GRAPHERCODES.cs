using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace GRAPHER
{
    class Program
    {
      
        static void MinMatrix(int[,] Rmin1_Matrix, int[,] Rmin2_Matrix, int[,] Rmin_Matrix, int c) 
        {


            for (int i = 0; i < Rmin1_Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Rmin1_Matrix.GetLength(1); j++)
                {
                    int m = Rmin1_Matrix[i, j];  // bir önceki matrix
                    int n = Rmin2_Matrix[i, j]; // bir sonraki matrix
                    if (m == 0 && n == 1 && Rmin_Matrix[i, j] == 0)
                    {
                        Rmin_Matrix[i, j] = c;
                    }
                }

            }




        }

        static void MatrixMultiplication(int[,] Matrix, int[,] Matrix_demo1, int[,] Matrix_demo2, int b, int matrix_nodes_counter)
        {
            if (b < matrix_nodes_counter)
            {
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        for (int k = 0; k < Matrix.GetLength(1); k++)
                        {
                            Matrix[i, j] += Matrix_demo1[i, k] * Matrix_demo2[k, j];
                        }
                    }

                }
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        if (Matrix[i, j] > 1)
                        {
                            Matrix[i, j] = 1;
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        Matrix[i, j] = 0;
                    }
                }

            }



        }
        static void Tracing(char[,] board2, int y, int directiony, int x, int directionx, char[] matrix_nodes, char[,] R_matrix, int k)
        {
            bool flag1 = true;
            bool flag2 = true;
            while (flag2)
            {
                while (flag1)
                {
                    if (board2[y + directiony, x + directionx] == '.')
                    {
                        flag1 = false;
                    }
                    else if (board2[y + directiony, x + directionx] == 'X')
                    {
                        for (int m = 0; m < matrix_nodes.Length; m++)
                        {
                            if (matrix_nodes[m] == board2[y + 2 * directiony, x + 2 * directionx])
                            {

                                R_matrix[k + 1, m + 1] = '1';
                                flag1 = false;
                                flag2 = false;


                            }
                        }

                    }
                    else  // + olma durumu
                    {
                        x += directionx;
                        y += directiony;
                    }
                }
                //direction update && flag = true
                if ((board2[y + 1, x] == '+' || board2[y + 1, x] == 'X') && !((y + 1) == y - directiony && x == x - directionx))
                {
                    directiony = 1;
                    directionx = 0;
                    flag1 = true;
                }
                else if ((board2[y + 1, x + 1] == '+' || board2[y + 1, x + 1] == 'X') && !((y + 1) == y - directiony && (x + 1) == x - directionx))
                {
                    directiony = 1;
                    directionx = 1;
                    flag1 = true;
                }
                else if ((board2[y - 1, x] == '+' || board2[y - 1, x] == 'X') && !((y - 1) == y - directiony && x == x - directionx))
                {
                    directiony = -1;
                    directionx = 0;
                    flag1 = true;
                }
                else if ((board2[y, x - 1] == '+' || board2[y, x - 1] == 'X') && !(y == y - directiony && (x - 1) == x - directionx))
                {
                    directiony = 0;
                    directionx = -1;
                    flag1 = true;
                }
                else if ((board2[y - 1, x - 1] == '+' || board2[y - 1, x - 1] == 'X') && !((y - 1) == y - directiony && (x - 1) == x - directionx))
                {
                    directiony = -1;
                    directionx = -1;
                    flag1 = true;
                }
                else if ((board2[y - 1, x + 1] == '+' || board2[y - 1, x + 1] == 'X') && !((y - 1) == y - directiony && (x + 1) == x - directionx))
                {
                    directiony = -1;
                    directionx = 1;
                    flag1 = true;
                }
                else if ((board2[y + 1, x - 1] == '+' || board2[y + 1, x - 1] == 'X') && !((y + 1) == y - directiony && (x - 1) == x - directionx))
                {
                    directiony = 1;
                    directionx = -1;
                    flag1 = true;
                }
                else if ((board2[y, x + 1] == '+' || board2[y, x + 1] == 'X') && !(y == y - directiony && (x + 1) == x - directionx))
                {
                    directiony = 0;
                    directionx = 1;
                    flag1 = true;
                }
            }
        }
        static int counter(char[,] graph, char harf) // board da yazılan harfin tekrar yazılmaması için counter lı fonksiyon
        {
            bool flag = false;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (harf == graph[i, j])
                        flag = true;
                }
            }
            if (flag)
                return 1;
            else
                return 0;
        }
        static void Main(string[] args)
        {


            int cursorx = 5, cursory = 5;

            ConsoleKeyInfo cki;
            char[] nodes = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P' };
            int counterA = 0, counterB = 0, counterC = 0, counterD = 0, counterE = 0, counterF = 0, counterG = 0, counterH = 0, counterI = 0, counterJ = 0, counterK = 0, counterL = 0, counterM = 0, counterN = 0, counterO = 0, counterP = 0;
            int matrix_nodes_counter = 0;
            int b = 1;
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("-----WELCOME TO GRAPHER-----");
            Console.SetCursorPosition(40, 4);
            Console.WriteLine("Please select an option from menu");
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("1-manuel graph drawing");
            Console.SetCursorPosition(40, 6);
            Console.WriteLine("2-reading graph from file");

            string choose = Console.ReadLine();

            if (choose == "1")
            {

                Console.Clear();
                Console.SetCursorPosition(5, 3);
                for (int i = 0; i < 4; i++)
                {


                    Console.Write("1234567890");

                }



                Console.SetCursorPosition(4, 4);
                for (int i = 0; i < 42; i++)
                {
                    Console.Write("#");
                }

                Console.SetCursorPosition(4, 30);
                for (int i = 0; i < 42; i++)
                {
                    Console.Write("#");
                }


                for (int i = 1; i < 10; i++)
                {
                    Console.SetCursorPosition(3, 4 + i);

                    Console.WriteLine(i);
                }

                Console.SetCursorPosition(3, 14);

                Console.WriteLine("0");


                for (int i = 1; i < 10; i++)
                {
                    Console.SetCursorPosition(3, 14 + i);

                    Console.WriteLine(i);
                }

                Console.SetCursorPosition(3, 24);

                Console.WriteLine("0");

                for (int i = 1; i < 6; i++)
                {
                    Console.SetCursorPosition(3, 24 + i);

                    Console.WriteLine(i);
                }




                for (int i = 1; i < 27; i++)
                {
                    Console.SetCursorPosition(4, 4 + i);

                    Console.WriteLine("#");
                }


                for (int i = 1; i < 27; i++)
                {
                    Console.SetCursorPosition(45, 4 + i);

                    Console.WriteLine("#");
                }




                char[,] board2 = new char[25, 40];
                Console.SetCursorPosition(5, 5);

                for (int j = 0; j < board2.GetLength(0); j++)
                {
                    Console.SetCursorPosition(5, 5 + j);
                    for (int i = 0; i < board2.GetLength(1); i++)
                    {
                        board2[j, i] = '.';
                        Console.Write(board2[j, i]);
                    }

                }

                Console.SetCursorPosition(55, 3);
                Console.Write("Press 'Escape' button to see R matrix");

                Console.CursorVisible = true;//graph çizme işlemi
                while (true)
                {
                    counterA = counter(board2, 'A');
                    counterB = counter(board2, 'B');
                    counterC = counter(board2, 'C');
                    counterD = counter(board2, 'D');
                    counterE = counter(board2, 'E');
                    counterF = counter(board2, 'F');
                    counterG = counter(board2, 'G');
                    counterH = counter(board2, 'H');
                    counterI = counter(board2, 'I');
                    counterJ = counter(board2, 'J');
                    counterK = counter(board2, 'K');
                    counterL = counter(board2, 'L');
                    counterM = counter(board2, 'M');
                    counterN = counter(board2, 'N');
                    counterO = counter(board2, 'O');
                    Console.SetCursorPosition(cursorx, cursory);
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);

                        Console.SetCursorPosition(cursorx, cursory);

                        if (cki.Key == ConsoleKey.Backspace)
                        {
                            Console.Write(".");
                            board2[cursory - 5, cursorx - 5] = '.';

                        }

                        if (cki.Key == ConsoleKey.RightArrow && cursorx < 44)
                        {
                            cursorx++;

                        }
                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 5)
                        {
                            cursorx--;

                        }
                        if (cki.Key == ConsoleKey.UpArrow && cursory > 5)
                        {
                            cursory--;

                        }
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 29)
                        {
                            cursory++;

                        }

                        if (cki.Key == ConsoleKey.X)
                        {
                            Console.Write("X");
                            board2[cursory - 5, cursorx - 5] = 'X';  // board gets updated constantly
                        }


                        if (cki.Key == ConsoleKey.Spacebar)
                        {
                            Console.Write("+");
                            board2[cursory - 5, cursorx - 5] = '+';
                        }

                        if (cki.Key == ConsoleKey.A && counterA == 0)
                        {
                            Console.Write(nodes[0]);
                            board2[cursory - 5, cursorx - 5] = nodes[0];


                        }
                        if (cki.Key == ConsoleKey.B && counterB == 0)
                        {
                            Console.Write(nodes[1]);
                            board2[cursory - 5, cursorx - 5] = nodes[1];

                        }
                        if (cki.Key == ConsoleKey.C && counterC == 0)
                        {
                            Console.Write(nodes[2]);
                            board2[cursory - 5, cursorx - 5] = nodes[2];
                        }
                        if (cki.Key == ConsoleKey.D && counterD == 0)
                        {
                            Console.Write(nodes[3]);
                            board2[cursory - 5, cursorx - 5] = nodes[3];


                        }
                        if (cki.Key == ConsoleKey.E && counterE == 0)
                        {
                            Console.Write(nodes[4]);
                            board2[cursory - 5, cursorx - 5] = nodes[4];

                        }
                        if (cki.Key == ConsoleKey.F && counterF == 0)
                        {
                            Console.Write(nodes[5]);
                            board2[cursory - 5, cursorx - 5] = nodes[5];

                        }
                        if (cki.Key == ConsoleKey.G && counterG == 0)
                        {
                            Console.Write(nodes[6]);
                            board2[cursory - 5, cursorx - 5] = nodes[6];

                        }
                        if (cki.Key == ConsoleKey.H && counterH == 0)
                        {
                            Console.Write(nodes[7]);
                            board2[cursory - 5, cursorx - 5] = nodes[7];

                        }
                        if (cki.Key == ConsoleKey.I && counterI == 0)
                        {
                            Console.Write(nodes[8]);
                            board2[cursory - 5, cursorx - 5] = nodes[8];

                        }
                        if (cki.Key == ConsoleKey.J && counterJ == 0)
                        {
                            Console.Write(nodes[9]);
                            board2[cursory - 5, cursorx - 5] = nodes[9];


                        }
                        if (cki.Key == ConsoleKey.K && counterK == 0)
                        {
                            Console.Write(nodes[10]);
                            board2[cursory - 5, cursorx - 5] = nodes[10];

                        }
                        if (cki.Key == ConsoleKey.L && counterL == 0)
                        {
                            Console.Write(nodes[11]);
                            board2[cursory - 5, cursorx - 5] = nodes[11];

                        }
                        if (cki.Key == ConsoleKey.M && counterM == 0)
                        {
                            Console.Write(nodes[12]);
                            board2[cursory - 5, cursorx - 5] = nodes[12];


                        }
                        if (cki.Key == ConsoleKey.N && counterN == 0)
                        {
                            Console.Write(nodes[13]);
                            board2[cursory - 5, cursorx - 5] = nodes[13];

                        }
                        if (cki.Key == ConsoleKey.O && counterO == 0)
                        {
                            Console.Write(nodes[14]);
                            board2[cursory - 5, cursorx - 5] = nodes[14];

                        }
                        if (cki.Key == ConsoleKey.P && counterP == 0)
                        {
                            Console.Write(nodes[15]);
                            board2[cursory - 5, cursorx - 5] = nodes[15];

                        }

                        if (cki.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }

                }

                for (int i = 0; i < board2.GetLength(0); i++) // counter for creating matrix dimensions
                {
                    for (int j = 0; j < board2.GetLength(1); j++)
                    {
                        for (int k = 0; k < nodes.Length; k++)
                        {
                            if (nodes[k] == board2[i, j])
                            {
                                matrix_nodes_counter++;



                            }
                        }



                    }

                }


                char[] matrix_nodes = new char[matrix_nodes_counter]; // finding the type of nodes for using in matrix
                int a = 0;
                for (int i = 0; i < board2.GetLength(0); i++)
                {
                    for (int j = 0; j < board2.GetLength(1); j++)
                    {
                        for (int k = 0; k < nodes.Length; k++)
                        {
                            if (nodes[k] == board2[i, j])
                            {

                                matrix_nodes[a] = nodes[k];
                                a++;


                            }
                        }

                    }

                }
                Array.Sort(matrix_nodes);

                char[,] R_matrix = new char[matrix_nodes_counter + 1, matrix_nodes_counter + 1]; // creating 0 matrix in string form
                for (int i = 0; i < matrix_nodes_counter; i++)
                {
                    R_matrix[0, i + 1] = matrix_nodes[i];
                }
                for (int i = 0; i < matrix_nodes_counter; i++)
                {
                    R_matrix[i + 1, 0] = matrix_nodes[i];
                }
                for (int i = 0; i < matrix_nodes_counter; i++)
                {
                    for (int j = 0; j < matrix_nodes_counter; j++)
                    {
                        R_matrix[j + 1, i + 1] = '0';
                    }
                }


                for (int i = 0; i < board2.GetLength(0); i++)  //tracing
                {
                    for (int j = 0; j < board2.GetLength(1); j++)
                    {
                        for (int k = 0; k < matrix_nodes.Length; k++)
                        {
                            if (matrix_nodes[k] == board2[i, j])
                            {
                                if (board2[i, j + 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 1;
                                    int directiony = 0;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);

                                }
                                if (board2[i + 1, j] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 0;
                                    int directiony = 1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i + 1, j + 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 1;
                                    int directiony = 1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);

                                }
                                if (board2[i - 1, j] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 0;
                                    int directiony = -1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i, j - 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = -1;
                                    int directiony = 0;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i - 1, j - 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = -1;
                                    int directiony = -1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i - 1, j + 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 1;
                                    int directiony = -1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);

                                }
                                if (board2[i + 1, j - 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = -1;
                                    int directiony = 1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }

                            }
                        }
                    }
                }

                Console.SetCursorPosition(55, 5);   // showing R matrix on screen
                Console.Write("R matrix");
                Console.SetCursorPosition(55, 6);
                Console.Write("----------------------");
                for (int j = 0; j < R_matrix.GetLength(0); j++)
                {
                    Console.SetCursorPosition(55, 7 + j);
                    for (int i = 0; i < R_matrix.GetLength(1); i++)
                    {
                        Console.Write(R_matrix[j, i]);
                        Console.Write(" ");
                    }

                }

                char[,] R_Matrix_demo = new char[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1]; // getting spesific indexes to use for multiplication
                for (int k = 0; k < R_Matrix_demo.GetLength(0); k++)
                {
                    for (int m = 0; m < R_Matrix_demo.GetLength(1); m++)
                    {
                        R_Matrix_demo[k, m] = R_matrix[k + 1, m + 1];
                    }

                }
                int[,] R2_Matrix_demo = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1]; // converting to integer
                for (int i = 0; i < R2_Matrix_demo.GetLength(0); i++)
                {
                    for (int j = 0; j < R2_Matrix_demo.GetLength(1); j++)
                    {
                        if (R_Matrix_demo[i, j] == '0')
                        {
                            R2_Matrix_demo[i, j] = 0;
                        }
                        else
                        {
                            R2_Matrix_demo[i, j] = 1;
                        }

                    }
                }


                int[,] R2_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R3_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R4_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R5_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R6_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R7_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R8_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R9_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R10_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R11_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R12_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R13_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R14_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R15_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R16_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] Rmax_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] Rmin_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];


                MatrixMultiplication(R2_Matrix, R2_Matrix_demo, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R3_Matrix, R2_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R4_Matrix, R2_Matrix, R2_Matrix, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R5_Matrix, R4_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R6_Matrix, R5_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R7_Matrix, R6_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R8_Matrix, R7_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R9_Matrix, R8_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R10_Matrix, R9_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R11_Matrix, R10_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R12_Matrix, R11_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R13_Matrix, R12_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R14_Matrix, R13_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R15_Matrix, R14_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R16_Matrix, R15_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                for (int i = 0; i < Rmax_Matrix.GetLength(0); i++) // creating R* Matrix
                {
                    for (int j = 0; j < Rmax_Matrix.GetLength(1); j++)
                    {
                        Rmax_Matrix[i, j] = R2_Matrix_demo[i, j] + R2_Matrix[i, j] + R3_Matrix[i, j] + R4_Matrix[i, j] + R5_Matrix[i, j] + R6_Matrix[i, j] + R7_Matrix[i, j] + R8_Matrix[i, j] + R9_Matrix[i, j] + R10_Matrix[i, j] + R11_Matrix[i, j] + R12_Matrix[i, j] + R13_Matrix[i, j] + R14_Matrix[i, j] + R15_Matrix[i, j] + R16_Matrix[i, j];
                        if (Rmax_Matrix[i, j] > 1)
                        {
                            Rmax_Matrix[i, j] = 1;
                        }
                    }

                }
                for (int i = 0; i < Rmin_Matrix.GetLength(0); i++) // creating Rmin matrix(taslak)
                {
                    for (int j = 0; j < Rmin_Matrix.GetLength(1); j++)
                    {
                        if (R2_Matrix_demo[i, j] == 1)
                        {
                            Rmin_Matrix[i, j] = 1;
                        }
                        else
                        {
                            Rmin_Matrix[i, j] = 0;
                        }
                    }
                }
                MinMatrix(R2_Matrix_demo, R2_Matrix, Rmin_Matrix, 2); // function we used for to create min matrix
                MinMatrix(R2_Matrix, R3_Matrix, Rmin_Matrix, 3);
                MinMatrix(R3_Matrix, R4_Matrix, Rmin_Matrix, 4);
                MinMatrix(R4_Matrix, R5_Matrix, Rmin_Matrix, 5);
                MinMatrix(R5_Matrix, R6_Matrix, Rmin_Matrix, 6);
                MinMatrix(R6_Matrix, R7_Matrix, Rmin_Matrix, 7);
                MinMatrix(R7_Matrix, R8_Matrix, Rmin_Matrix, 8);
                MinMatrix(R8_Matrix, R9_Matrix, Rmin_Matrix, 9);
                MinMatrix(R9_Matrix, R10_Matrix, Rmin_Matrix, 10);
                MinMatrix(R10_Matrix, R11_Matrix, Rmin_Matrix, 11);
                MinMatrix(R11_Matrix, R12_Matrix, Rmin_Matrix, 12);
                MinMatrix(R12_Matrix, R13_Matrix, Rmin_Matrix, 13);
                MinMatrix(R13_Matrix, R14_Matrix, Rmin_Matrix, 14);
                MinMatrix(R14_Matrix, R15_Matrix, Rmin_Matrix, 15);
                MinMatrix(R15_Matrix, R16_Matrix, Rmin_Matrix, 16);

                for (int i = 0; i < matrix_nodes.Length; i++)
                {
                    Console.SetCursorPosition(80, 8 + i);
                    Console.WriteLine(matrix_nodes[i]);
                    Console.SetCursorPosition(82 + (2 * i), 7);
                    Console.Write(matrix_nodes[i]);

                }


                Console.SetCursorPosition(80, 5);   // showing Rmin matrix on screen
                Console.Write("Rmin matrix");
                Console.SetCursorPosition(80, 6);
                Console.Write("----------------------");
                for (int j = 0; j < Rmin_Matrix.GetLength(0); j++)
                {
                    Console.SetCursorPosition(82, 8 + j);
                    for (int i = 0; i < Rmin_Matrix.GetLength(1); i++)
                    {
                        Console.Write(Rmin_Matrix[j, i]);
                        Console.Write(" ");
                    }

                }
                for (int i = 0; i < matrix_nodes.Length; i++)
                {
                    Console.SetCursorPosition(80, 24 + i);
                    Console.WriteLine(matrix_nodes[i]);
                    Console.SetCursorPosition(82 + (2 * i), 23);
                    Console.Write(matrix_nodes[i]);

                }
                Console.SetCursorPosition(80, 21); // showing Rmax matrix on screen
                Console.Write("R* matrix");
                Console.SetCursorPosition(80, 22);
                Console.Write("----------------------");
                for (int j = 0; j < Rmin_Matrix.GetLength(0); j++)
                {
                    Console.SetCursorPosition(82, 24 + j);
                    for (int i = 0; i < Rmin_Matrix.GetLength(1); i++)
                    {
                        Console.Write(Rmax_Matrix[j, i]);
                        Console.Write(" ");
                    }

                }


                Console.SetCursorPosition(50, 18);
                Console.WriteLine("Press [2/3/4/5/6/7/8/9] to see other matrices that you want to see");
                Console.SetCursorPosition(50, 19);
                Console.WriteLine("Press S button to save all matrices and the  new graph ");

                while (true)   // part that we can see the matrices bounds to the input given from user
                {


                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);

                        for (int i = 0; i < matrix_nodes.Length; i++)
                        {
                            Console.SetCursorPosition(55, 24 + i);
                            Console.WriteLine(matrix_nodes[i]);
                            Console.SetCursorPosition(57 + (2 * i), 23);
                            Console.Write(matrix_nodes[i]);

                        }

                        if (cki.Key == ConsoleKey.D2)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R2 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R2_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D3)

                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R3 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R3_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D4)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R4 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R4_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D5)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R5 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R5_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D6)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R6 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R6_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D7)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R7 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R7_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D8)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R8 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R8_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D9)
                        {
                            Console.SetCursorPosition(55, 21);
                            Console.Write("R9 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R9_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }



                        if (cki.Key == ConsoleKey.S) // breaks the loop continuing point for saving codes
                        {
                            break;
                        }
                    }

                }




                System.IO.StreamWriter streamWriter2 = new System.IO.StreamWriter("new graph.txt"); // saving board to text file
                string output2 = "";
                streamWriter2.WriteLine(output2);
                streamWriter2.WriteLine("SAVED BOARD");
                streamWriter2.WriteLine(output2);
                for (int i = 0; i < board2.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < board2.GetUpperBound(1); j++)
                    {
                        output2 += board2[i, j].ToString();
                    }
                    streamWriter2.WriteLine(output2);
                    output2 = "";
                }
                streamWriter2.Close();

                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("matrix.txt");  // the initial start of saving matrices to text file
                string output = "";
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("RMatrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R_matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R2Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R2_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R3Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R3_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R4Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R4_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R5Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R5_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R6Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R6_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R7Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R7_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R8Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R8_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R9Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R9_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R10Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R10_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R11Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R11_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R12Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R12_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R13Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R13_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R14Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R14_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R15Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R15_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R16Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R16_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }

                streamWriter.Close();









            }

            if (choose == "2")
            {

                Console.Clear();




                Console.SetCursorPosition(5, 3);
                for (int i = 0; i < 4; i++)
                {


                    Console.Write("1234567890");

                }


                Console.SetCursorPosition(4, 4);

                Console.WriteLine("##########################################");


                Console.SetCursorPosition(4, 30);

                Console.WriteLine("##########################################");



                for (int i = 1; i < 10; i++)
                {
                    Console.SetCursorPosition(3, 4 + i);

                    Console.WriteLine(i);
                }

                Console.SetCursorPosition(3, 14);

                Console.WriteLine("0");


                for (int i = 1; i < 10; i++)
                {
                    Console.SetCursorPosition(3, 14 + i);

                    Console.WriteLine(i);
                }

                Console.SetCursorPosition(3, 24);

                Console.WriteLine("0");

                for (int i = 1; i < 6; i++)
                {
                    Console.SetCursorPosition(3, 24 + i);

                    Console.WriteLine(i);
                }




                for (int i = 1; i < 27; i++)
                {
                    Console.SetCursorPosition(4, 4 + i);

                    Console.WriteLine("#");
                }


                for (int i = 1; i < 27; i++)
                {
                    Console.SetCursorPosition(45, 4 + i);

                    Console.WriteLine("#");
                }
                char[,] board2 = new char[25, 40];



                StreamReader f = File.OpenText(@"C:\Users\GRAPHER\graph.txt");
                string satir = f.ReadLine();

                for (int j = 0; j < board2.GetLength(0); j++)
                {
                    for (int i = 0; i < board2.GetLength(1); i++)
                    {
                        board2[j, i] = satir[i];
                    }
                    satir = f.ReadLine();
                }


                for (int j = 0; j < board2.GetLength(0); j++)
                {
                    Console.SetCursorPosition(5, 5 + j);
                    for (int i = 0; i < board2.GetLength(1); i++)
                    {
                        Console.Write(board2[j, i]);
                    }

                }


                Console.SetCursorPosition(55, 3);
                Console.Write("Press 'Escape' button to see R matrix");


                Console.CursorVisible = true;//graph çizme işlemi
                while (true)
                {
                    counterA = counter(board2, 'A');
                    counterB = counter(board2, 'B');
                    counterC = counter(board2, 'C');
                    counterD = counter(board2, 'D');
                    counterE = counter(board2, 'E');
                    counterF = counter(board2, 'F');
                    counterG = counter(board2, 'G');
                    counterH = counter(board2, 'H');
                    counterI = counter(board2, 'I');
                    counterJ = counter(board2, 'J');
                    counterK = counter(board2, 'K');
                    counterL = counter(board2, 'L');
                    counterM = counter(board2, 'M');
                    counterN = counter(board2, 'N');
                    counterO = counter(board2, 'O');
                    Console.SetCursorPosition(cursorx, cursory);
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);

                        Console.SetCursorPosition(cursorx, cursory);

                        if (cki.Key == ConsoleKey.Backspace)
                        {
                            Console.Write(".");
                            board2[cursory - 5, cursorx - 5] = '.';

                        }

                        if (cki.Key == ConsoleKey.RightArrow && cursorx < 44)
                        {
                            cursorx++;

                        }
                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 5)
                        {
                            cursorx--;

                        }
                        if (cki.Key == ConsoleKey.UpArrow && cursory > 5)
                        {
                            cursory--;

                        }
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 29)
                        {
                            cursory++;

                        }

                        if (cki.Key == ConsoleKey.X)
                        {
                            Console.Write("X");
                            board2[cursory - 5, cursorx - 5] = 'X';
                        }


                        if (cki.Key == ConsoleKey.Spacebar)
                        {
                            Console.Write("+");
                            board2[cursory - 5, cursorx - 5] = '+';
                        }

                        if (cki.Key == ConsoleKey.A && counterA == 0)
                        {
                            Console.Write(nodes[0]);
                            board2[cursory - 5, cursorx - 5] = nodes[0];


                        }
                        if (cki.Key == ConsoleKey.B && counterB == 0)
                        {
                            Console.Write(nodes[1]);
                            board2[cursory - 5, cursorx - 5] = nodes[1];

                        }
                        if (cki.Key == ConsoleKey.C && counterC == 0)
                        {
                            Console.Write(nodes[2]);
                            board2[cursory - 5, cursorx - 5] = nodes[2];
                        }
                        if (cki.Key == ConsoleKey.D && counterD == 0)
                        {
                            Console.Write(nodes[3]);
                            board2[cursory - 5, cursorx - 5] = nodes[3];


                        }
                        if (cki.Key == ConsoleKey.E && counterE == 0)
                        {
                            Console.Write(nodes[4]);
                            board2[cursory - 5, cursorx - 5] = nodes[4];

                        }
                        if (cki.Key == ConsoleKey.F && counterF == 0)
                        {
                            Console.Write(nodes[5]);
                            board2[cursory - 5, cursorx - 5] = nodes[5];

                        }
                        if (cki.Key == ConsoleKey.G && counterG == 0)
                        {
                            Console.Write(nodes[6]);
                            board2[cursory - 5, cursorx - 5] = nodes[6];

                        }
                        if (cki.Key == ConsoleKey.H && counterH == 0)
                        {
                            Console.Write(nodes[7]);
                            board2[cursory - 5, cursorx - 5] = nodes[7];

                        }
                        if (cki.Key == ConsoleKey.I && counterI == 0)
                        {
                            Console.Write(nodes[8]);
                            board2[cursory - 5, cursorx - 5] = nodes[8];

                        }
                        if (cki.Key == ConsoleKey.J && counterJ == 0)
                        {
                            Console.Write(nodes[9]);
                            board2[cursory - 5, cursorx - 5] = nodes[9];


                        }
                        if (cki.Key == ConsoleKey.K && counterK == 0)
                        {
                            Console.Write(nodes[10]);
                            board2[cursory - 5, cursorx - 5] = nodes[10];

                        }
                        if (cki.Key == ConsoleKey.L && counterL == 0)
                        {
                            Console.Write(nodes[11]);
                            board2[cursory - 5, cursorx - 5] = nodes[11];

                        }
                        if (cki.Key == ConsoleKey.M && counterM == 0)
                        {
                            Console.Write(nodes[12]);
                            board2[cursory - 5, cursorx - 5] = nodes[12];


                        }
                        if (cki.Key == ConsoleKey.N && counterN == 0)
                        {
                            Console.Write(nodes[13]);
                            board2[cursory - 5, cursorx - 5] = nodes[13];

                        }
                        if (cki.Key == ConsoleKey.O && counterO == 0)
                        {
                            Console.Write(nodes[14]);
                            board2[cursory - 5, cursorx - 5] = nodes[14];

                        }
                        if (cki.Key == ConsoleKey.P && counterP == 0)
                        {
                            Console.Write(nodes[15]);
                            board2[cursory - 5, cursorx - 5] = nodes[15];

                        }

                        if (cki.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }

                }




                for (int i = 0; i < board2.GetLength(0); i++) // counter for creating matrix dimensions
                {
                    for (int j = 0; j < board2.GetLength(1); j++)
                    {
                        for (int k = 0; k < nodes.Length; k++)
                        {
                            if (nodes[k] == board2[i, j])
                            {
                                matrix_nodes_counter++;



                            }
                        }



                    }

                }


                char[] matrix_nodes = new char[matrix_nodes_counter]; // finding the type of nodes for using in matrix
                int a = 0;
                for (int i = 0; i < board2.GetLength(0); i++)
                {
                    for (int j = 0; j < board2.GetLength(1); j++)
                    {
                        for (int k = 0; k < nodes.Length; k++)
                        {
                            if (nodes[k] == board2[i, j])
                            {

                                matrix_nodes[a] = nodes[k];
                                a++;


                            }
                        }

                    }

                }
                Array.Sort(matrix_nodes);

                char[,] R_matrix = new char[matrix_nodes_counter + 1, matrix_nodes_counter + 1]; // creating 0 matrix in string form
                for (int i = 0; i < matrix_nodes_counter; i++)
                {
                    R_matrix[0, i + 1] = matrix_nodes[i];
                }
                for (int i = 0; i < matrix_nodes_counter; i++)
                {
                    R_matrix[i + 1, 0] = matrix_nodes[i];
                }
                for (int i = 0; i < matrix_nodes_counter; i++)
                {
                    for (int j = 0; j < matrix_nodes_counter; j++)
                    {
                        R_matrix[j + 1, i + 1] = '0';
                    }
                }


                for (int i = 0; i < board2.GetLength(0); i++)  //tracing
                {
                    for (int j = 0; j < board2.GetLength(1); j++)
                    {
                        for (int k = 0; k < matrix_nodes.Length; k++)
                        {
                            if (matrix_nodes[k] == board2[i, j])
                            {
                                if (board2[i, j + 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 1;
                                    int directiony = 0;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);

                                }
                                if (board2[i + 1, j] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 0;
                                    int directiony = 1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i + 1, j + 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 1;
                                    int directiony = 1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);

                                }
                                if (board2[i - 1, j] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 0;
                                    int directiony = -1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i, j - 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = -1;
                                    int directiony = 0;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i - 1, j - 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = -1;
                                    int directiony = -1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }
                                if (board2[i - 1, j + 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = 1;
                                    int directiony = -1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);

                                }
                                if (board2[i + 1, j - 1] == '+')
                                {
                                    int x = j;
                                    int y = i;
                                    int directionx = -1;
                                    int directiony = 1;
                                    Tracing(board2, y, directiony, x, directionx, matrix_nodes, R_matrix, k);


                                }

                            }
                        }
                    }
                }

                Console.SetCursorPosition(55, 5);   // showing R matrix on screen
                Console.Write("R matrix");
                Console.SetCursorPosition(55, 6);
                Console.Write("----------------------");
                for (int j = 0; j < R_matrix.GetLength(0); j++)
                {
                    Console.SetCursorPosition(55, 7 + j);
                    for (int i = 0; i < R_matrix.GetLength(1); i++)
                    {
                        Console.Write(R_matrix[j, i]);
                        Console.Write(" ");
                    }

                }

                char[,] R_Matrix_demo = new char[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1]; // getting spesific indexes to use for multiplication
                for (int k = 0; k < R_Matrix_demo.GetLength(0); k++)
                {
                    for (int m = 0; m < R_Matrix_demo.GetLength(1); m++)
                    {
                        R_Matrix_demo[k, m] = R_matrix[k + 1, m + 1];
                    }

                }
                int[,] R2_Matrix_demo = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1]; // converting to integer
                for (int i = 0; i < R2_Matrix_demo.GetLength(0); i++)
                {
                    for (int j = 0; j < R2_Matrix_demo.GetLength(1); j++)
                    {
                        if (R_Matrix_demo[i, j] == '0')
                        {
                            R2_Matrix_demo[i, j] = 0;
                        }
                        else
                        {
                            R2_Matrix_demo[i, j] = 1;
                        }

                    }
                }


                int[,] R2_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R3_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R4_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R5_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R6_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R7_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R8_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R9_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R10_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R11_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R12_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R13_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R14_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R15_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] R16_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] Rmax_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];
                int[,] Rmin_Matrix = new int[R_matrix.GetLength(0) - 1, R_matrix.GetLength(0) - 1];


                MatrixMultiplication(R2_Matrix, R2_Matrix_demo, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R3_Matrix, R2_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R4_Matrix, R2_Matrix, R2_Matrix, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R5_Matrix, R4_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R6_Matrix, R5_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R7_Matrix, R6_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R8_Matrix, R7_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R9_Matrix, R8_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R10_Matrix, R9_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R11_Matrix, R10_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R12_Matrix, R11_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R13_Matrix, R12_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R14_Matrix, R13_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R15_Matrix, R14_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                MatrixMultiplication(R16_Matrix, R15_Matrix, R2_Matrix_demo, b, matrix_nodes_counter);
                b++;
                for (int i = 0; i < Rmax_Matrix.GetLength(0); i++) // creating R*Matrix
                {
                    for (int j = 0; j < Rmax_Matrix.GetLength(1); j++)
                    {
                        Rmax_Matrix[i, j] = R2_Matrix_demo[i, j] + R2_Matrix[i, j] + R3_Matrix[i, j] + R4_Matrix[i, j] + R5_Matrix[i, j] + R6_Matrix[i, j] + R7_Matrix[i, j] + R8_Matrix[i, j] + R9_Matrix[i, j] + R10_Matrix[i, j] + R11_Matrix[i, j] + R12_Matrix[i, j] + R13_Matrix[i, j] + R14_Matrix[i, j] + R15_Matrix[i, j] + R16_Matrix[i, j];
                        if (Rmax_Matrix[i, j] > 1)
                        {
                            Rmax_Matrix[i, j] = 1;
                        }
                    }

                }
                for (int i = 0; i < Rmin_Matrix.GetLength(0); i++) // creating Rmin matrix(taslak)
                {
                    for (int j = 0; j < Rmin_Matrix.GetLength(1); j++)
                    {
                        if (R2_Matrix_demo[i, j] == 1)
                        {
                            Rmin_Matrix[i, j] = 1;
                        }
                        else
                        {
                            Rmin_Matrix[i, j] = 0;
                        }
                    }
                }
                MinMatrix(R2_Matrix_demo, R2_Matrix, Rmin_Matrix, 2); // Rmin function part
                MinMatrix(R2_Matrix, R3_Matrix, Rmin_Matrix, 3);
                MinMatrix(R3_Matrix, R4_Matrix, Rmin_Matrix, 4);
                MinMatrix(R4_Matrix, R5_Matrix, Rmin_Matrix, 5);
                MinMatrix(R5_Matrix, R6_Matrix, Rmin_Matrix, 6);
                MinMatrix(R6_Matrix, R7_Matrix, Rmin_Matrix, 7);
                MinMatrix(R7_Matrix, R8_Matrix, Rmin_Matrix, 8);
                MinMatrix(R8_Matrix, R9_Matrix, Rmin_Matrix, 9);
                MinMatrix(R9_Matrix, R10_Matrix, Rmin_Matrix, 10);
                MinMatrix(R10_Matrix, R11_Matrix, Rmin_Matrix, 11);
                MinMatrix(R11_Matrix, R12_Matrix, Rmin_Matrix, 12);
                MinMatrix(R12_Matrix, R13_Matrix, Rmin_Matrix, 13);
                MinMatrix(R13_Matrix, R14_Matrix, Rmin_Matrix, 14);
                MinMatrix(R14_Matrix, R15_Matrix, Rmin_Matrix, 15);
                MinMatrix(R15_Matrix, R16_Matrix, Rmin_Matrix, 16);
                for (int i = 0; i < matrix_nodes.Length; i++)
                {
                    Console.SetCursorPosition(80, 8 + i);
                    Console.WriteLine(matrix_nodes[i]);
                    Console.SetCursorPosition(82 + (2 * i), 7);
                    Console.Write(matrix_nodes[i]);

                }


                Console.SetCursorPosition(80, 5);   // showing Rmin matrix on screen
                Console.Write("Rmin matrix");
                Console.SetCursorPosition(80, 6);
                Console.Write("----------------------");
                for (int j = 0; j < Rmin_Matrix.GetLength(0); j++)
                {
                    Console.SetCursorPosition(82, 8 + j);
                    for (int i = 0; i < Rmin_Matrix.GetLength(1); i++)
                    {
                        Console.Write(Rmin_Matrix[j, i]);
                        Console.Write(" ");
                    }

                }
                for (int i = 0; i < matrix_nodes.Length; i++)
                {
                    Console.SetCursorPosition(80, 24 + i);
                    Console.WriteLine(matrix_nodes[i]);
                    Console.SetCursorPosition(82 + (2 * i), 23);
                    Console.Write(matrix_nodes[i]);

                }
                Console.SetCursorPosition(80, 21); // showing Rmax matrix on screen
                Console.Write("R* matrix");
                Console.SetCursorPosition(80, 22);
                Console.Write("----------------------");
                for (int j = 0; j < Rmin_Matrix.GetLength(0); j++)
                {
                    Console.SetCursorPosition(82, 24 + j);
                    for (int i = 0; i < Rmin_Matrix.GetLength(1); i++)
                    {
                        Console.Write(Rmax_Matrix[j, i]);
                        Console.Write(" ");
                    }

                }


                Console.SetCursorPosition(50, 18);
                Console.WriteLine("Press [2/3/4/5/6/7/8/9] to see other matrices that you want to see");
                Console.SetCursorPosition(50, 19);
                Console.WriteLine("Press S button to save all matrices and the  new graph ");

                while (true)   // part that we can see the matrices bounds to the input given from user
                {
                  

                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);

                        for (int i = 0; i < matrix_nodes.Length; i++)
                        {
                            Console.SetCursorPosition(55, 24 + i);
                            Console.WriteLine(matrix_nodes[i]);
                            Console.SetCursorPosition(57 + (2 * i), 23);
                            Console.Write(matrix_nodes[i]);

                        }

                        if (cki.Key == ConsoleKey.D2)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R2 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R2_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }
                            


                        }
                        if (cki.Key == ConsoleKey.D3)

                        {
                            Console.SetCursorPosition(55, 21);  
                            Console.Write("R3 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R3_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }
                            


                        }
                        if (cki.Key == ConsoleKey.D4)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R4 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R4_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D5)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R5 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R5_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D6)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R6 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++) 
                            {
                                Console.SetCursorPosition(57, 24+ j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R6_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D7)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R7 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R7_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D8)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R8 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R8_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                        if (cki.Key == ConsoleKey.D9)
                        {
                            Console.SetCursorPosition(55, 21);   
                            Console.Write("R9 matrix");
                            Console.SetCursorPosition(55, 22);
                            Console.Write("----------------------");
                            for (int j = 0; j < R16_Matrix.GetLength(0); j++)  
                            {
                                Console.SetCursorPosition(57, 24 + j);
                                for (int i = 0; i < R16_Matrix.GetLength(1); i++)
                                {
                                    Console.Write(R9_Matrix[j, i]);
                                    Console.Write(" ");
                                }

                            }



                        }
                       
                      
                       
                        if (cki.Key == ConsoleKey.S)
                        {
                            break;
                        }
                    }

                }


              
             
                System.IO.StreamWriter streamWriter2 = new System.IO.StreamWriter("new graph.txt"); // saving board to text file
                string output2 = "";
                streamWriter2.WriteLine(output2);
                streamWriter2.WriteLine("SAVED BOARD");
                streamWriter2.WriteLine(output2);
                for (int i = 0; i < board2.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < board2.GetUpperBound(1); j++)
                    {
                        output2 += board2[i, j].ToString();
                    }
                    streamWriter2.WriteLine(output2);
                    output2 = "";
                }
                streamWriter2.Close();

                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("matrix.txt");  // the initial start of saving matrices to text file
                string output = "";
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("RMatrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R_matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R2Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R2_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R3Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R3_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R4Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R4_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R5Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R5_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R6Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R6_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R7Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R7_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R8Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R8_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R9Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R9_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R10Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R10_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R11Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R11_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R12Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R12_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R13Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R13_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R14Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R14_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R15Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R15_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.WriteLine(output);
                streamWriter.WriteLine("R16Matrix");
                streamWriter.WriteLine(output);
                for (int i = 0; i < Rmin_Matrix.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < Rmin_Matrix.GetUpperBound(1); j++)
                    {
                        output += R16_Matrix[i, j].ToString();
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                
                streamWriter.Close();

           



            }

            Console.ReadLine();

        }
    }
}

