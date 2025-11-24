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
        static Random random = new Random();

        static int playerHealth = 3;
        static int enemyHealth = 3;
        static int enemyHealth2 = 6;


        static int playerYpos = 3;
        static int playerXpos = 3;

        static int playerYinput = 0;
        static int playerXinput = 0;

        static int EnemyDirectionY = 0;
        static int EnemyDirectionX = 0;

        static int EnemyDirectionY2 = 0;
        static int EnemyDirectionX2 = 0;

        static int EnemyXpos = 20;
        static int EnemyXpos2 = 3;

        static int EnemyYpos = 3;
        static int EnemyYpos2 = 8;

        static int EnemyStartXpos = 20;
        static int EnemyStartYpos = 3;
        static int EnemyStartXpos2 = 3;
        static int EnemyStartYpos2 = 8;

        static int CollectableXpos = 0;
        static int CollectableYpos = 0;




        static int EnemoveCount = 0;

        static int EnemoveCount2 = 0;

        static int CollectableOnMap = 0;
        static int Collectables = 0;

        static bool Playing = true;
        static bool LoseState = false;
        static bool WinState = false;
        static bool EnemyAlive = true;
        static bool Enemy2Alive = true;


        static int mapOffset = 1;

        static string[] map;

        




        static string mapData = "mapData.txt";

        static string[] boarder = { "═", "║" };

        static void Main(string[] args)
        {
            
            Console.CursorVisible = false;
            if (File.Exists("mapData.txt"))
            {
                map = File.ReadAllLines("mapData.txt");
            }
               

            while (Playing == true)
            {
                
                PlayerHandler();
                EnemyHandler();
                CollectableHandler();
                PlayerDraw();
                DisplayMap();
                EnemyDraw();
                CollectableDraw();


                Thread.Sleep(17);

            }
            if (WinState == true)
            {
                Console.Clear();
                Console.SetCursorPosition(5, 20);
                Console.WriteLine("You Win!");
                Console.ReadKey();
                Console.ReadKey();
            }
            if (LoseState == true)
            {
                Console.Clear();
                Console.SetCursorPosition(5, 20);
                Console.WriteLine("GameOver");
                Console.ReadKey();
                Console.ReadKey();
            }
           


        }
        static void DisplayMap()
        {
            if (File.Exists("mapData.txt"))
            {
                
                

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
            
            if (map[playerYpos - mapOffset][playerXpos - mapOffset] == '#')
            {
                playerHealth -= 1;
                playerXpos = 3;
                playerYpos = 3;
                EnemyXpos = EnemyStartXpos;
                EnemyXpos2 = EnemyStartXpos2;
                EnemyYpos = EnemyStartYpos;
                EnemyYpos2 = EnemyStartYpos2;
            }
            if (EnemyAlive == true)
            {
                if (playerXpos == EnemyXpos && playerYpos == EnemyYpos)
                {
                    enemyHealth -= 1;

                    EnemyXpos = EnemyStartXpos;
                    EnemyYpos = EnemyStartYpos;


                }
                if (enemyHealth == 0)
                {
                    EnemyAlive = false;
                }
                if (enemyHealth < 1)
                {
                    enemyHealth = 0;
                    EnemyAlive = false;
                }

                if (playerHealth <= 0)
                {
                    LoseState = true;
                    Playing = false;
                    
                }
            }
            
            if (Enemy2Alive == true)
            {
                if (playerXpos == EnemyXpos2 && playerYpos == EnemyYpos2)
                {
                    enemyHealth2 -= 1;

                    EnemyXpos2 = EnemyStartXpos2;
                    EnemyYpos2 = EnemyStartYpos2;

                   


                }
                if (enemyHealth2 == 0)
                {
                    Enemy2Alive = false;
                }
                if (enemyHealth2 < 1)
                {
                    enemyHealth2 = 0;
                    Enemy2Alive = false;
                }
            }
           

        }

        static void EnemyHandler()
        {
            EnemoveCount += 1;
            EnemoveCount2 += 1;

            EnemyDirectionX = 0;
            EnemyDirectionY = 0;
            EnemyDirectionX2 = 0;
            EnemyDirectionY2 = 0;

            
            if (EnemoveCount == 20)
            {
                
                // moves enemy 1
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

                    EnemyDirectionX = 0;
                    EnemyDirectionY = 0;

                }
               

                EnemyXpos += EnemyDirectionX;
                EnemyYpos += EnemyDirectionY;
                

                EnemoveCount = 0;
            }
            
            if(EnemoveCount2 == 20)
            {
                if (playerXpos > EnemyXpos2)
                {
                    EnemyDirectionX2 += 1;
                }

                if (playerXpos < EnemyXpos2)
                {
                    EnemyDirectionX2 -= 1;
                }
                if (playerYpos > EnemyYpos2)
                {
                    EnemyDirectionY2 += 1;
                }

                if (playerYpos < EnemyYpos2)
                {
                    EnemyDirectionY2 -= 1;
                }

                if (map[EnemyYpos2 + EnemyDirectionY2 - mapOffset][EnemyXpos2 + EnemyDirectionX2 - mapOffset] != '`')
                {
                    EnemoveCount2 = 0;
                    EnemyDirectionX2 = 0;
                    EnemyDirectionY2 = 0;

                }


                EnemyXpos2 += EnemyDirectionX2;
                EnemyYpos2 += EnemyDirectionY2;

                EnemoveCount2 = 0;
            }

            if(EnemyXpos == EnemyXpos2 && EnemyYpos == EnemyYpos2)
            {
                EnemyXpos = EnemyStartXpos;
                EnemyXpos2 = EnemyStartXpos2;

                EnemyYpos = EnemyStartYpos;
                EnemyYpos2 = EnemyStartYpos2;

            }

            if (playerXpos == EnemyXpos && playerYpos == EnemyYpos)
            {
                playerHealth -= 1;
                playerXpos = 3;
                playerYpos = 3;
                EnemyXpos = EnemyStartXpos;
                EnemyXpos2 = EnemyStartXpos2;
                EnemyYpos = EnemyStartYpos;
                EnemyYpos2 = EnemyStartYpos2;
            }
            if (playerXpos == EnemyXpos2 && playerYpos == EnemyYpos2)
            {
                playerHealth -= 1;
                playerXpos = 3;
                playerYpos = 3;
                EnemyXpos = EnemyStartXpos;
                EnemyXpos2 = EnemyStartXpos2;
                EnemyYpos = EnemyStartYpos;
                EnemyYpos2 = EnemyStartYpos2;
            }
            if (playerHealth <= 0)
            {
                LoseState = true;
                Playing = false;
                
                
            }

            if (enemyHealth <= 0 && enemyHealth2 <= 0)
            {
                WinState = true;
                Playing = false;
                
            }
        }



        static void CollectableHandler()
        {
            
            if (CollectableOnMap == 0)
            {
                
                CollectableXpos = random.Next(1, map[0].Length - mapOffset);
                CollectableYpos = random.Next(1, map.Length - mapOffset);

                if (map[CollectableYpos - mapOffset][CollectableXpos - mapOffset] != '`')
                {

                    CollectableHandler();     
                }

                

                CollectableOnMap += 1;
            }
            

            if (playerXpos == CollectableXpos && playerYpos == CollectableYpos)
            {
                CollectableOnMap = 0;
                Collectables += 1;
            }

            if (Collectables >= 5)
            {
                WinState = true;
                Playing = false;
            }
        }
            

        static void PlayerDraw()
        {
            Console.SetCursorPosition(30, 0);
            Console.Write("Health: " + playerHealth);
            Console.SetCursorPosition(playerXpos,playerYpos);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("O");
            Console.ResetColor();

            Console.SetCursorPosition(0, 0);

           
        }

        static void EnemyDraw()
        {
            Console.SetCursorPosition(30, 2);
            Console.Write("Enemy 1 Health: " + enemyHealth);

            Console.SetCursorPosition(30, 3);
            Console.Write("Enemy 2 Health: " + enemyHealth2);
            if (EnemyAlive == true)
            {
                Console.SetCursorPosition(EnemyXpos, EnemyYpos);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
                Console.ResetColor();
            }


            if (Enemy2Alive == true)
            {
                Console.SetCursorPosition(EnemyXpos2, EnemyYpos2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
                Console.ResetColor();
            }
            

            Console.SetCursorPosition(0, 0);
        }

        static void CollectableDraw()
        {

            Console.SetCursorPosition(CollectableXpos, CollectableYpos);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("O");
            Console.ResetColor();

            Console.SetCursorPosition(30, 4);
            Console.Write("Collectables: " + Collectables);
            Console.SetCursorPosition(0, 0);
            
        }
    }
}
