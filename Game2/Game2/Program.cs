using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            char[,] map = ReadMap("map.txt");
            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;
            bool game = true;
            int maxHealth = 3;
            int health = 3;
            
            while(game)
            {
                
                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(33, 0);
                Console.Write($"Score:{score}");
                DrawBar(health,maxHealth);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write("@");
                
                ConsoleKeyInfo pressedKey=Console.ReadKey();
                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score, ref health);

                if (health == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Game Over");
                    game= false;
                    Console.ReadKey();

                }
                if (score == 275)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("You win");
                    game = false;
                    Console.ReadKey();

                }
                Console.Clear();

            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");
            char[,] map = new char[GetMaxLengthOfLine(file),file.Length];
            for(int x = 0; x < map.GetLength(0); x++)
            {
                for(int y = 0; y < map.GetLength(1); y++)
                {
                    map[x,y] =file[y][x];
                }
            }
            return map;
        }

        private static void DrawMap(char[,] map)
        
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
            for (int x = 0; x < map.GetLength(0); x++)
            
                {
                    Console.Write(map[x,y]);
                }

            Console.WriteLine();

            }
            
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach(var line in lines)
            {
                if(line.Length > maxLength)
                    maxLength = line.Length;
                
            }
            return maxLength;
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score, ref int health)
        
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (map[pacmanX, pacmanY-1] != '#')
                    {
                        pacmanY--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[pacmanX, pacmanY+1] != '#')
                    {
                        pacmanY++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[pacmanX-1, pacmanY] != '#')
                    {
                        pacmanX--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (map[pacmanX+1, pacmanY] != '#')
                    {
                        pacmanX++;
                    }
                    break;
               
            }
            if (map[pacmanX, pacmanY] == '.')
            {
                map[pacmanX, pacmanY] = ' ';
                score++;
            }
            if(map[pacmanX, pacmanY] == '*')
            {
                map[pacmanX, pacmanY] = ' ';
                health--;
            }
            
        }

        static void DrawBar(int value, int maxValue, ConsoleColor color= ConsoleColor.Green)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            string bar = "";
            for (int i = 0; i < value; i++)
            {
                bar += " ";
            }
            Console.SetCursorPosition(33,1);
            Console.Write("Жизни:");
            Console.SetCursorPosition(33, 2);
            Console.Write("[");
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaultColor;

            bar = "";

            for (int i = value; i < maxValue; i++)
            {
                bar += " ";
            }
            Console.Write(bar + "]");
        }

    }
}
