using System;
using System.Threading;

namespace Project
{
    class Program
    {
        static (int x, int y)[] snakeBody = new (int, int)[1];

        public static void Coordinate(int yemX, int yemY, int gainedPoints)
        {
            string[,] coordinate = new string[16, 36];

            Console.Clear();
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 36; j++)
                {
                    if (i == 0 || i == 15 || j == 0 || j == 35)
                        coordinate[i, j] = "*";
                    else if (i == yemY && j == yemX)
                        coordinate[i, j] = "o";
                    else
                        coordinate[i, j] = " ";
                }
            }

            foreach (var part in snakeBody)
            {
                coordinate[part.y, part.x] = "*";
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 36; j++)
                {
                    Console.Write(coordinate[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nPoints: {gainedPoints}");
        }

        public static void GameOver(int gainedPoints)
        {
            Console.Clear();
            Console.WriteLine("\n\n                              Game Over!\n");
            Console.WriteLine($"                              Result: {gainedPoints}\n");
        }

        static void Main(string[] args)
        {
            int gainedPoints = 0;
            Random rnd = new Random();

            int snakeX = rnd.Next(1, 35);
            int snakeY = rnd.Next(1, 15);
            snakeBody[0] = (snakeX, snakeY);

            int yemX = rnd.Next(1, 35);
            int yemY = rnd.Next(1, 15);

            bool gameOver = false;

            ConsoleKeyInfo userInput;

            Console.WriteLine("\n\n               Welcome to the Snake game! Press ENTER to start\n\n");

            userInput = Console.ReadKey();
            if (userInput.Key == ConsoleKey.Enter)
            {
                Coordinate(yemX, yemY, gainedPoints);

                while (!gameOver)
                {
                    if (Console.KeyAvailable)
                        userInput = Console.ReadKey(true);

                    snakeX = snakeBody[snakeBody.Length - 1].x;
                    snakeY = snakeBody[snakeBody.Length - 1].y;

                    if (userInput.Key == ConsoleKey.DownArrow) snakeY++;
                    if (userInput.Key == ConsoleKey.UpArrow) snakeY--;
                    if (userInput.Key == ConsoleKey.RightArrow) snakeX++;
                    if (userInput.Key == ConsoleKey.LeftArrow) snakeX--;

                    if (snakeY <= 0 || snakeX <= 0 || snakeY >= 15 || snakeX >= 35)
                    {
                        gameOver = true; break;
                    }

                    Array.Resize(ref snakeBody, snakeBody.Length + 1);
                    snakeBody[snakeBody.Length - 1] = (snakeX, snakeY);

                    if (snakeX == yemX && snakeY == yemY)
                    {
                        gainedPoints++;
                        yemX = rnd.Next(1, 35);
                        yemY = rnd.Next(1, 15);
                    }
                    else
                    {
                        for (int i = 1; i < snakeBody.Length; i++)
                            snakeBody[i - 1] = snakeBody[i];
                        Array.Resize(ref snakeBody, snakeBody.Length - 1);
                    }

                    Coordinate(yemX, yemY, gainedPoints);
                    Thread.Sleep(200);
                }
            }

            GameOver(gainedPoints);
        }
    }
}