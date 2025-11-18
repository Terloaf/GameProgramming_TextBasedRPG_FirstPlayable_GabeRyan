using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProgramming_TextBasedRPG_FirstPlayable_GabeRyan
{
    internal class Program
    {
        static int playerHealth = 100;
        static int enemyHealth = 1;


        static int playerYpos = 3;
        static int playerXpos = 3;

        static int playerYinput = 0;
        static int playerXinput = 0;

        static int EnemyDirectionY = 0;
        static int EnemyDirectionX = 0;
        static int EnemyXpos = 20;
        static int EnemyYpos = 3;
        static int EnemoveCount = 0;

        static bool Playing = true;
        static bool EnemyMove = true;

        static int mapOffset = 1;
        static int enemyOffset = 2;

        static string[] map;

        



        //static char[,] map = new char[,]
        //{
        //    {'`','`','`','`','`','`','`','`','`','~','~','`','`','`','`','`','`','`','`','`','`','┌','─','─','┐', },
        //    {'`','`','`','`','`','`','`','`','~','~','`','`','`','`','`','`','`','`','`','`','`','│','`','`','│', },
        //    {'`','`','`','`','`','`','`','~','~','`','`','`','`','`','`','`','`','`','`','`','`','│','`','`','│', },
        //    {'`','`','`','~','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','└','`','`','┘', },
        //    {'~','`','~','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','`','`','^','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //    {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`', },
        //};

        static string mapData = "mapData.txt";

        static string[] boarder = { "═", "║" };

        static void Main(string[] args)
        {
            
            Console.CursorVisible = false;
            
            
            while (Playing == true)
            {
                
                PlayerHandler();
                EnemyHandler();
                PlayerDraw();
                
                DisplayMap();
                EnemyDraw();


                Thread.Sleep(17);


                Console.Write(enemyHealth);
            }
            
        }
        static void DisplayMap()
        {
            if (File.Exists("mapData.txt"))
            {
                
                map = File.ReadAllLines("mapData.txt");

                for (int i = 0; i < map.Length; i++)
                {
                    Console.SetCursorPosition(1, i + mapOffset);
                    Console.WriteLine(map[i]);

                    // makes it so theres no flickering
                    if (map[playerYpos - mapOffset][playerXpos - mapOffset] == '`')
                    {
                        Console.SetCursorPosition(playerXpos, playerYpos);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                }


                for(int BoarderX = 0; BoarderX < map[0].Length; BoarderX++)
                {
                    Console.SetCursorPosition(BoarderX + mapOffset, 0);
                    Console.Write(boarder[0]);
                    Console.SetCursorPosition(BoarderX + mapOffset, map.Length + mapOffset);
                    Console.Write(boarder[0]);
                }

                for(int BoarderY = 0; BoarderY < map.Length; BoarderY++)
                {
                    Console.SetCursorPosition(0,BoarderY + mapOffset);
                    Console.Write(boarder[1]);
                    Console.SetCursorPosition(map[0].Length + mapOffset,BoarderY + mapOffset);
                    Console.Write(boarder[1]);

                   
                }
            }

            Console.Write("\n");

            

        }
        

        static void PlayerHandler()
        {
            playerXinput = 0;
            playerYinput = 0;
            if (!Console.KeyAvailable)
            {
                return;
            }
            ConsoleKeyInfo Input = Console.ReadKey(true);

            if (Input.Key == ConsoleKey.W) playerYinput -= 1;
            if (Input.Key == ConsoleKey.S) playerYinput += 1;
            if (Input.Key == ConsoleKey.D) playerXinput += 1;
            if (Input.Key == ConsoleKey.A) playerXinput -= 1;

            playerXpos += playerXinput;
            playerYpos += playerYinput;

            if (playerXinput == -1 && playerXpos == 0)
            {
                playerXpos += 1;
                return;
            }
            if (playerXinput == +1 && playerXpos == map[0].Length + mapOffset)
            {
                playerXpos -= 1;
                return;
            }

            if (playerYinput == -1 && playerYpos == 0)
            {
                playerYpos += 1;
                return;
            }
            if (playerYinput == +1 && playerYpos == map.Length + mapOffset)
            {
                playerYpos -= 1;
                return;
            }

            // Checks if player is trying to go into water or trees

            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '~')
            {
                playerYpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '~')
            {
                playerYpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '~')
            {
                playerXpos += 1;

            }
            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '~')
            {
                playerXpos -= 1;
            }

            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '^')
            {
                playerYpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '^')
            {
                playerYpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '^')
            {
                playerXpos += 1;
            }
            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '^')
            {
                playerXpos -= 1;
            }
            // cage boarders
            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '│')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '│')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '│')
            {
                playerXpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '│')
            {
                playerXpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '└')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '└')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '└')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '└')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┘')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┘')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┘')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┘')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┌')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┌')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┌')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┌')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┐')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┐')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┐')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '┐')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '─')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '─')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '─')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - mapOffset][playerXpos - mapOffset] == '─')
            {
                playerYpos += 1;
            }

            if (playerXpos == EnemyXpos && playerYpos == EnemyYpos)
            {
                enemyHealth -= 1;

                if (enemyHealth > 1)
                {
                    enemyHealth = 0;
                }


            }

        }

        static void EnemyHandler()
        {
            EnemoveCount += 1;

            EnemyDirectionX = 0;
            EnemyDirectionY = 0;

            
            if (EnemoveCount == 20)
            {
                

                if (playerXpos > EnemyXpos)
                {
                    EnemyDirectionX += 1;
                }

                if (playerXpos < EnemyXpos)
                {
                    EnemyDirectionX -= 1;
                }
                if (playerYpos > EnemyYpos)
                {
                    EnemyDirectionY += 1;
                }

                if (playerYpos < EnemyYpos)
                {
                    EnemyDirectionY -= 1;
                }

                if (map[EnemyYpos + EnemyDirectionY - mapOffset][EnemyXpos + EnemyDirectionX - mapOffset] != '`')
                {
                    EnemoveCount = 0;
                    return;

                }

                EnemyXpos += EnemyDirectionX;
                EnemyYpos += EnemyDirectionY;

                EnemoveCount = 0;
            }
            

        }

        static void PlayerDraw()
        {
            
            Console.SetCursorPosition(playerXpos,playerYpos);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("O");
            Console.ResetColor();

            Console.SetCursorPosition(0, 0);

           
        }

        static void EnemyDraw()
        {
           
            Console.SetCursorPosition(EnemyXpos, EnemyYpos);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ResetColor();

            Console.SetCursorPosition(0, 0);
        }
    }
}
