using System;
using System.Collections.Generic;
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

            }
            
        }
        static void DisplayMap()
        {
            if (File.Exists("mapData.txt"))
            {
                map = File.ReadAllLines("mapData.txt");
                for(int i = 0; i < map.Length; i++)
                {
                    Console.WriteLine(map[i]);
                }
                
                
            }
        }
        //static void DisplayMap()
        //{
        //    for (int mapRow = 0; mapRow < map.GetLength(0); mapRow++)
        //    {
        //        for(int mapColumn = 0; mapColumn < map.GetLength(1); mapColumn++)
        //        {
        //            Console.SetCursorPosition(mapColumn, mapRow);
        //            Console.Write(map[mapRow,mapColumn]);
                    
        //        }
        //    }
            
        //}

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

            if (playerYinput == -1 && map[playerYpos][playerXpos] == '~')
            {
                playerYpos += 1;
                


            }
            else if (playerYinput == 1 && map[playerYpos][playerXpos] == '~')
            {
                playerYpos -= 1;
            }
            else if (playerXinput == -1 && map[playerYpos][playerXpos] == '~')
            {
                playerXpos += 1;
            }
            else if (playerXinput == 1 && map[playerYpos][playerXpos] == '~')
            {
                playerXpos -= 1;
            }

            else if (playerYinput == -1 && map[playerYpos][playerXpos] == '^')
            {
                playerYpos += 1;



            }
            else if (playerYinput == 1 && map[playerYpos][playerXpos] == '^')
            {
                playerYpos -= 1;
            }
            else if (playerXinput == -1 && map[playerYpos][playerXpos] == '^')
            {
                playerXpos += 1;
            }
            else if (playerXinput == 1 && map[playerYpos][playerXpos] == '^')
            {
                playerXpos -= 1;
            }



            // Checks border of the map

            

        }

        static void PlayerDraw()
        {
            
            Console.SetCursorPosition(playerXpos, playerYpos);
            Console.Write("O");

            Console.SetCursorPosition(0, 0);
        }
    }
}
