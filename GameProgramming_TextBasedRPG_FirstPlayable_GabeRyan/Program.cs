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

        static bool Playing = true;

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
                PlayerDraw();
                DisplayMap();
                
                
                Thread.Sleep(17);

                Console.SetCursorPosition(28, 0);
                
            }
            
        }
        static void DisplayMap()
        {
            if (File.Exists("mapData.txt"))
            {
                
                map = File.ReadAllLines("mapData.txt");
                
                for (int i = 0; i < map.Length; i++)
                {
                    Console.SetCursorPosition(1, 1 + i);
                    Console.WriteLine(map[i]);
                    
                }


                for(int BoarderX = 0; BoarderX < map[0].Length; BoarderX++)
                {
                    Console.SetCursorPosition(BoarderX + 1, 0);
                    Console.Write(boarder[0]);
                    Console.SetCursorPosition(BoarderX + 1, map.Length + 1);
                    Console.Write(boarder[0]);
                }

                for(int BoarderY = 0; BoarderY < map.Length; BoarderY++)
                {
                    Console.SetCursorPosition(0,BoarderY + 1);
                    Console.Write(boarder[1]);
                    Console.SetCursorPosition(map[0].Length + 1,BoarderY + 1);
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



            // Checks if player is trying to go into water or trees

            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '~')
            {
                playerYpos += 1;



            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '~')
            {
                playerYpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '~')
            {
                playerXpos += 1;

               
            }
            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '~')
            {
                playerXpos -= 1;
            }

            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '^')
            {
                playerYpos += 1;



            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '^')
            {
                playerYpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '^')
            {
                playerXpos += 1;
            }
            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '^')
            {
                playerXpos -= 1;
            }
            // cage boarders
            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '│')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '│')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '│')
            {
                playerXpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '│')
            {
                playerXpos += 1;
            }


            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '└')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '└')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '└')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '└')
            {
                playerYpos += 1;
            }
            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '┘')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '┘')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '┘')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '┘')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '┌')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '┌')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '┌')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '┌')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '┐')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '┐')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '┐')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '┐')
            {
                playerYpos += 1;
            }

            if (playerXinput == 1 && map[playerYpos - 1][playerXpos - 1] == '─')
            {
                playerXpos -= 1;
            }
            if (playerXinput == -1 && map[playerYpos - 1][playerXpos - 1] == '─')
            {
                playerXpos += 1;
            }
            if (playerYinput == 1 && map[playerYpos - 1][playerXpos - 1] == '─')
            {
                playerYpos -= 1;
            }
            if (playerYinput == -1 && map[playerYpos - 1][playerXpos - 1] == '─')
            {
                playerYpos += 1;
            }

            //map boarder



        }

        static void PlayerDraw()
        {
            
            Console.SetCursorPosition(playerXpos, playerYpos);
            Console.Write("O");

            Console.SetCursorPosition(0, 0);
        }
    }
}
