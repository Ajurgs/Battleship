using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajurgsBattleship
{
    class Gameboard
    {
        // arrays for game board one hold the player shots and the other hold where the ships are located for hacks
        private char[,] playerShots = new char[10, 10];
        private char[,] shipLocations = new char[10, 10];


        //check to see if space is free
        public bool FreeSpace(int x, int y)
        {
            if (shipLocations[x, y] != 'S')
            {
                return true;
            }
            else
                return false;
        }

        //place ship on shipLocations
        public void AddShipIcon(int bowX, int bowY, int sternX, int sternY, Ship ship)
        {
            if (bowX == sternX)
            {
                if (bowY > sternY)
                {
                    for (int i = sternY; i <= bowY; i++)
                    {
                        shipLocations[bowX, i] = 'S';
                    }
                }
                else if (sternY > bowY)
                {
                    for (int i = bowY; i <= sternY; i++)
                    {
                        shipLocations[bowX, i] = 'S';
                    }
                }
            }
            else if (bowY == sternY)
            {
                if (bowX > sternX)
                {
                    for (int i = sternX; i <= bowX; i++)
                    {
                        shipLocations[i, sternY] = 'S';
                    }
                }
                else if (sternX > bowX)
                {
                    for (int i = bowX; i <= sternX; i++)
                    {
                        shipLocations[i, sternY] = 'S';
                    }
                }
            }
            else
                Console.WriteLine("Error with ships coordinates");
        }

        // method to display gameboard
        public void Display(bool hacks)
        {
            if (hacks == false)
            {
                Console.WriteLine("_ 0 1 2 3 4 5 6 7 8 9");
                for (int y = 0; y < 10; y++)
                {
                    Console.Write($"{y}");
                    for (int x = 0; x < 10; x++)
                    {
                        Console.Write($" {playerShots[x, y]}");
                    }
                    Console.Write("\n");
                }
            }
            else if (hacks == true)
            {
                Console.WriteLine("_ 0 1 2 3 4 5 6 7 8 9");
                for (int y = 0; y < 10; y++)
                {
                    Console.Write($"{y}");
                    for (int x = 0; x < 10; x++)
                    {
                        Console.Write($" {shipLocations[x, y]}");
                    }
                    Console.Write("\n");
                }
            }
        }

        //check if a shot hits
        public int CheckHit(int x, int y)
        {
            if (shipLocations[x, y] == 'S')
            {
                return 1; // hit
            }
            else if (shipLocations[x, y] == 'X' || shipLocations[x, y] == 'O')
            {
                return 2; // already hit this location
            }
            else
            {
                return 0; // miss
            }
        }
        
        // add X if miss
        public void Miss(int x, int y)
        {
            playerShots[x, y] = 'X';
            shipLocations[x, y] = 'X';
        }

        // add O if hit
        public void Hit(int x, int y)
        {
            playerShots[x, y] = 'O';
            shipLocations[x, y] = 'O';
        }

        // constuctors
        public Gameboard()
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    playerShots[x, y] = '_';
                    shipLocations[x, y] = '_';
                }
            }
        }


    }
}
