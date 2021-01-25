
class Brick
{   // We create our brick object with class Brick
    // indicate variables integers m(colums) and n(rows)
    private static int n;
    private static int m;
    //The method second_layer gets parameters int m(colums), int n(rows) and array first(already built first layer)
    public int second_layer(int n, int m, int[,] first)
    {
        // Make the second array with the same size like the first one
        int[,] second = new int[n, m];
        // All cells in array obtain value zero
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                second[i, j] = 0;
            }
        }
        // The first brick get value one
        int brick = 1;
        // Loop from 0 to n
        for (int i = 0; i < n; i++)
        {
            // Loop from 0 to m
            for (int j = 0; j < m; j++)
            {
                if (second[i, j] == 0)
                {
                    second[i, j] = brick;
                    //Conditions for arranging the bricks in the new layer
                    if (i != n - 1 && second[i + 1, j] == 0 && first[i, j] != first[i + 1, j])
                    {
                        if ((j == m - 2 && first[i, j + 1] == first[i + 1, j + 1]) || (j < m - 2 && second[i, j + 1] == 0 && second[i, j + 2] != 0 && first[i, j] != first[i, j + 1]))
                        {
                            second[i, j + 1] = brick;
                            brick++;
                        }
                        else
                        {
                            second[i + 1, j] = brick;
                            brick++;
                        }
                    }
                    //If the brick can be placed vertically, we check can we place it horizontally 
                    else if (j != m - 1 && second[i, j + 1] == 0 && first[i, j] != first[i, j + 1])
                    {
                        second[i, j + 1] = brick;
                        brick++;
                    }
                    // If we can't put in both cases, the builders have to dig up the layer and rebuild it all over again
                    else
                    {
                        Console.WriteLine("No solution exist!");
                        return 0;
                    }
                }
            }
        }
        // Print the results
        // Surround each brick of the second layer with '-' symbol
        Console.WriteLine("Second layer bricks:");
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
            {
                Console.Write("-\t");
                for (int j = 0; j < m; j++)
                {
                    Console.Write("-\t");
                    Console.Write("-\t");
                }
                Console.WriteLine();
            }

            Console.Write("-\t");
            for (int j = 0; j < m; j++)
            {
                Console.Write(second[i, j] + "\t");
                if ((j != m - 1 && second[i, j] != second[i, j + 1]) || j == m - 1)
                {
                    Console.Write("-\t");
                }
                else
                {
                    Console.Write(" \t");
                }
            }
            Console.WriteLine();

            Console.Write("-\t");
            for (int j = 0; j < m; j++)
            {
                if (i != n - 1)
                {
                    if (second[i, j] != second[i + 1, j])
                    {
                        Console.Write("-\t");
                    }
                    else
                    {
                        Console.Write(" \t");
                    }
                    Console.Write("-\t");
                }
                else
                {
                    Console.Write("-\t");
                    Console.Write("-\t");
                }
            }
            Console.WriteLine();
        }
        return 1;
    }
    // Method for start up the programm
    static void Main(string[] args)
    {
        // Place errors for all cases separatelly.
        int errors = 1;
        do
        {
            errors = 0;
            Console.WriteLine("\nEnter even numbers for row and column, separated with space");
            string numRowsColumns = Console.ReadLine();
            // We separate the elements with space 
            string[] arrRowsColumns = numRowsColumns.Split(' ');
            if (arrRowsColumns.Length != 2)
            {
                errors = 1;
            }
            else
            {
                try
                {
                    int numRows = Int32.Parse(arrRowsColumns[0]);
                    if (numRows % 2 == 0 && numRows <= 100)
                    {
                        n = numRows;
                    }
                    else
                    {
                        Console.WriteLine("Please, enter even number and less than 100 for row!");
                        errors = 1;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("error");
                    errors = 1;
                }

                try
                {
                    int numColumns = Int32.Parse(arrRowsColumns[1]);
                    if (numColumns % 2 == 0 && numColumns <= 100)
                    {
                        m = numColumns;
                    }
                    else
                    {
                        Console.WriteLine("Please, enter even number and less than 100 for column!");
                        errors = 1;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("error");
                    errors = 1;
                }
            }
        } while (errors == 1);


        int[,] first = new int[n, m];
        int bricksNum = (n * m) / 2;
        errors = 1;
        do
        {
            errors = 0;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter row #" + ((1+i)-00) + ", separated with space");
                string fullRow = Console.ReadLine();
                // We separate the elements with space 
                string[] arrRow = fullRow.Split(' ');
                if (arrRow.Length != m)
                {
                    errors = 1;
                }
                else
                {
                    for (int j = 0; j < m; j++)
                    {
                        try
                        {
                            int numHalfBrick = Int32.Parse(arrRow[j]);
                            if (numHalfBrick <= bricksNum)
                            {
                                //Conditions for arranging the bricks to not place on more than 2 spaces on row
                                if ((j > 1 && first[i, j - 1] == numHalfBrick && first[i, j - 2] == numHalfBrick) ||
                                    (i > 1 && first[i - 1, j] == numHalfBrick && first[i - 2, j] == numHalfBrick))
                                {
                                    errors = 1;
                                }
                                else
                                {
                                    first[i, j] = numHalfBrick;
                                }
                            }
                            else
                            {
                                errors = 1;
                            }
                        }
                        catch (FormatException)
                        {
                            errors = 1;
                        }
                    }
                }
            }

            if (errors == 1)
            {
                Console.WriteLine("Wrong layer! Please, enter layer again");
            }
        } while (errors == 1);


        Console.WriteLine("\nFirst Layer Bricks:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write(first[i, j] + "\t");
            }
            Console.WriteLine();
        }
        Brick b = new Brick();
        int r = b.second_layer(n, m, first);
    }
}
