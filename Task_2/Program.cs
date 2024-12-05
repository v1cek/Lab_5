using System;

class MathOperations
{
    // перевантажений метод Add для чисел
    public int Add(int a, int b)
    {
        return a + b;
    }

    // перевантажений метод Add для масивів
    public int[] Add(int[] array1, int[] array2)
    {
        if (array1.Length != array2.Length)
            throw new ArgumentException("Масиви повинні мати однакову довжину.");

        int[] result = new int[array1.Length];
        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = array1[i] + array2[i];
        }
        return result;
    }

    // перевантажений метод Add для матриць
    public int[,] Add(int[,] matrix1, int[,] matrix2)
    {
        if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
            throw new ArgumentException("Матриці повинні мати однакові розміри.");

        int rows = matrix1.GetLength(0);
        int cols = matrix1.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }
        return result;
    }

    // метод Subtract для матриць
    public int[,] Subtract(int[,] matrix1, int[,] matrix2)
    {
        if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
            throw new ArgumentException("Матриці повинні мати однакові розміри.");

        int rows = matrix1.GetLength(0);
        int cols = matrix1.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }
        return result;
    }

    // метод Multiply для матриць
    public int[,] Multiply(int[,] matrix1, int[,] matrix2)
    {
        if (matrix1.GetLength(1) != matrix2.GetLength(0))
            throw new ArgumentException("Кількість стовпців першої матриці повинна дорівнювати кількості рядків другої матриці.");

        int rows = matrix1.GetLength(0);
        int cols = matrix2.GetLength(1);
        int common = matrix1.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < common; k++)
                {
                    result[i, j] += matrix1[i, k] * matrix2[k, j];
                }
            }
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        var math = new MathOperations();

        // операції з числами
        Console.WriteLine("Add числа: " + math.Add(5, 10));

        // операції з масивами
        int[] array1 = { 1, 2, 3 };
        int[] array2 = { 4, 5, 6 };
        int[] arrayResult = math.Add(array1, array2);
        Console.WriteLine("Add масиви: " + string.Join(", ", arrayResult));

        // операції з матрицями
        int[,] matrix1 = { { 1, 2 }, { 3, 4 } };
        int[,] matrix2 = { { 5, 6 }, { 7, 8 } };

        int[,] addResult = math.Add(matrix1, matrix2);
        Console.WriteLine("Add матриці:");
        PrintMatrix(addResult);

        int[,] subResult = math.Subtract(matrix1, matrix2);
        Console.WriteLine("Subtract матриці:");
        PrintMatrix(subResult);

        int[,] multiplyResult = math.Multiply(matrix1, matrix2);
        Console.WriteLine("Multiply матриці:");
        PrintMatrix(multiplyResult);
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
