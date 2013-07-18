using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameoflife2
{
    class cell
    {
        public char var;

        public cell(char var1)
        {
            var = var1;
        }
    }
    class operations
    {
        public int dead = 0;
        protected int x_main, y_main;
        public int[] dimension(cell[,] medium, int x, int y)
        {
            x_main = x;
            y_main = y;
            int[] dimensions = new int[4];
            int x_start = 0, x_end = 0, y_start = 0, y_end = 0;
            int count = 0, temp = 0, temp1 = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0)
                    {
                        if (medium[i, j].var == 'x')
                        {
                            count++;
                            if (count >= 3)
                                x_start = 1;
                        }
                        if (medium[i, j].var == '-' || j == y - 1)
                            count = 0;
                    }
                    if (i == x - 1)
                    {
                        if (medium[i, j].var == 'x')
                        {
                            count++;
                            if (count >= 3)
                                x_end = 1;
                        }
                        if (medium[i, j].var == '-' || j == y - 1)
                            count = 0;
                    }
                }
            }
            for (int i = 0; i < y; i++)
            {
                temp1 = 0;
                temp1 = 0;
                for (int j = 0; j < x; j++)
                {
                    if (i == 0)
                    {
                        if (medium[j, i].var == 'x')
                        {
                            temp++;
                            if (temp >= 3)
                                y_start = 1;
                        }
                        if (medium[j, i].var == '-' || i == x - 1)
                            temp = 0;
                    }
                    if (i == y - 1)
                    {
                        if (medium[j, i].var == 'x')
                        {
                            temp1++;
                            if (temp1 >= 3)
                                y_end = 1;
                        }
                        if (medium[j, i].var == '-')
                            temp1 = 0;
                    }
                }
            }
            dimensions = new int[] { x_start, x_end, y_start, y_end };
            return dimensions;
        }
        public cell[,] expanding(cell[,] medium, int[] dimensions)
        {
            int out_rows = x_main + dimensions[0] + dimensions[1];
            int out_coloums = y_main + dimensions[2] + dimensions[3];
            cell[,] output = new cell[(out_rows), (out_coloums)];
            for (int i = 0; i < out_rows; i++)
            {
                for (int j = 0; j < out_coloums; j++)
                {
                    output[i, j] = new cell('-');
                }
            }
            for (int i = 0; i < out_rows; i++)
            {
                for (int j = 0; j < out_coloums; j++)
                {
                    if (dimensions[0] == 0 && dimensions[2] == 0)
                    {
                        if (i > x_main - 1 || j > y_main - 1)
                            break;
                        output[i, j] = medium[i, j];
                    }
                    if (dimensions[0] == 1 && dimensions[2] == 0)
                    {
                        if (i > x_main - 1 || j > y_main - 1)
                            break;
                        output[i + 1, j] = medium[i, j];
                    }
                    if (dimensions[0] == 1 && dimensions[2] == 1)
                    {
                        if (i > x_main - 1 || j > y_main - 1)
                            break;
                        output[i + 1, j + 1] = medium[i, j];
                    }
                    if (dimensions[0] == 0 && dimensions[2] == 1)
                    {
                        if (i > x_main - 1 || j > y_main - 1)
                            break;
                        output[i, j + 1] = medium[i, j];
                    }
                }
            }
            return output;
        }
        public cell[,] next_generation(cell[,] output, int[] dimensions)
        {
            int x = x_main + dimensions[0] + dimensions[1];
            int y = y_main + dimensions[2] + dimensions[3];
            cell[,] output_final = new cell[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    output_final[i, j] = new cell('-');
                }
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    int count = 0;
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int v = j - 1; v <= j + 1; v++)
                        {
                            if (k >= (x) || k < 0 || v >= (y) || v < 0)
                            {
                                continue;
                            }
                            if (k == i && v == j)
                            {
                                continue;
                            }
                            if (output[k, v].var == 'x')
                            {
                                count++;
                            }
                        }
                    }
                    if (output[i, j].var == 'x')
                    {
                        if (count > 3 || count < 2)
                        {
                            output_final[i, j].var = '-';
                            continue;
                        }
                        else
                        {
                            output_final[i, j].var = 'x';
                            continue;
                        }
                    }
                    if (output[i, j].var == '-')
                    {
                        if (count == 3)
                        {
                            output_final[i, j].var = 'x';
                            continue;
                        }
                        else
                        {
                            output_final[i, j].var = '-';
                            continue;
                        }
                    }
                }
            }
            return output_final;
        }
        public int show(cell[,] output_final, int[] dimensions)
        {
            int x = x_main + dimensions[0] + dimensions[1];
            int y = y_main + dimensions[2] + dimensions[3];
            int temp = 0, temp1 = 0, temp2 = 0, temp3 = 0, temp4 = 0, temp5 = 0;
            int x_start = 0, x_end = 0, y_start = 0, y_end = 0;
            for (int i = 0; i < x; i++)
            {
                temp = 0;
                temp1 = 0;
                for (int j = 0; j < y; j++)
                {
                    if (output_final[i, j].var == '-' && temp4 == 0)
                    {
                        temp++;
                        if (temp == y)
                            x_start += 1;
                    }
                    else
                        temp4 = 1;
                    if (temp4 == 1)
                    {
                        if (output_final[i, j].var == '-')
                        {
                            temp1++;
                            if (temp1 == y)
                                x_end += 1;
                        }
                        else
                        {
                            x_end = 0;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < y; i++)
            {
                temp2 = 0;
                temp3 = 0;
                for (int j = 0; j < x; j++)
                {
                    if (output_final[j, i].var == '-' && temp5 == 0)
                    {
                        temp2++;
                        if (temp2 == x)
                            y_start += 1;
                    }
                    else
                        temp5 = 1;
                    if (temp5 == 1)
                    {
                        if (output_final[j, i].var == '-')
                        {
                            temp3++;
                            if (temp3 == x)
                                y_end += 1;
                        }
                        else
                        {
                            y_end = 0;
                            break;
                        }
                    }
                }
            }
            for (int i = x_start; i < x - x_end; i++)
            {
                for (int j = y_start; j < y - y_end; j++)
                {
                    Console.Write(output_final[i, j].var);
                    if (output_final[i, j].var == 'x')
                        dead = 1;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return dead;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0, temp = 0, dead = 0;
            do
            {
                Console.WriteLine(" Enter the cordinates of the Grid");
                try
                {
                    x = Int32.Parse(Console.ReadLine());
                    if (x < 0)
                    {
                        temp = 0;
                        Console.WriteLine("please Enter positive Cordinates only ");
                        continue;
                    }
                    y = Int32.Parse(Console.ReadLine());
                    if (y < 0)
                    {
                        temp = 0;
                        Console.WriteLine("please Enter positive Cordinates only ");
                        continue;
                    }
                    if (y == 0 && x == 0)
                    {
                        temp = 0;
                        Console.WriteLine("please Enter positive Cordinates only ");
                        continue;
                    }
                    temp = 1;
                }
                catch
                {
                    temp = 0;
                    Console.WriteLine(" Please Enter Intergers Only ");
                }
            } while (temp == 0);
            string[] input = new string[x];
            cell[,] initial = new cell[x, y];
            Console.WriteLine(" Enter the Input Rows ");
            for (int i = 0; i < x; i++)
            {
                input[i] = Console.ReadLine();
                if (input[i].Length > y)
                {
                    i = i - 1;
                    Console.WriteLine("please Enter correct length input in terms of 'x' and '-' ");
                    continue;
                }
                for (int j = 0; j < y; j++)
                {
                    if (j < input[i].Length)
                        initial[i, j] = new cell(Convert.ToChar(input[i][j]));
                    else
                        initial[i, j] = new cell('-');
                    if (initial[i, j].var != 'x' && initial[i, j].var != '-')
                    {
                        i = i - 1;
                        Console.WriteLine(" Please Enter the last input row again Using 'x' and '-' only ");
                        j = y;
                    }
                }
            }
            operations objt1 = new operations();
            int[] dimensions = new int[4];
            cell[,] final = new cell[x, y];
            cell[,] output_final;
            ConsoleKeyInfo key;
            int x_main;
            int y_main;
            do
            {
                x_main = x;
                y_main = y;
                dimensions = objt1.dimension(initial, x, y);
                final = objt1.expanding(initial, dimensions);
                output_final = objt1.next_generation(final, dimensions);
                dead = objt1.show(output_final, dimensions);
                initial = output_final;
                x = x_main + dimensions[0] + dimensions[1];
                y = y_main + dimensions[2] + dimensions[3];
                if (dead == 0)
                {
                    Console.WriteLine(" There is no Living Cell remaining  ");
                    Console.WriteLine(" Thanks for Playing Game of Life ");
                    break;
                }
                Console.WriteLine("Press Enter period to stop the process else press anyother key");
                key = Console.ReadKey();
                if (key.KeyChar == '.')
                {
                    Console.WriteLine();
                    Console.WriteLine(" Thanks for Playing Game of Life ");
                    break;
                }
            } while (dead == 1);
            Console.ReadLine();
        }
    }
}
