using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajurgsBattleship
{
    class Game
    {
        Gameboard board;
        Ship carrier;
        Ship battleship;
        Ship submarine1;
        Ship submarine2;
        Ship destroyer1;
        Ship destroyer2;
        private bool hacks;
        Random rnd = new Random();
        List<Ship> shipList = new List<Ship>();
        private bool allShipsSunk = false;

        //main game method
        public void PlayGame()
        {
            board.Display(hacks);
            while (allShipsSunk == false)
            {
                Console.WriteLine("Where do you want to target Capitan?");
                Console.WriteLine("Enter two numbers 0-9 seperated by space, or enter hacks to toggle hacks");
                string input = Console.ReadLine();
                if ((input == "hacks")) //Toggle hacks.
                {
                    if (hacks == true)
                    {
                        hacks = false;
                    }
                    else if (hacks == false)
                    {
                        hacks = true;
                    }

                    board.Display(hacks);
                    Console.WriteLine($"hacks are {hacks}");
                }
                else
                {
                    string[] values = input.Split(' ');
                    int targetX = int.Parse(values[0]);
                    int targetY = int.Parse(values[1]);
                    Shoot(targetX, targetY);
                }
                allShipsSunk = AllSunk();//check if all ships are sunk;
            }
            if (allShipsSunk == true)
            {
                Console.WriteLine("You Have won Capitan!");
                Console.WriteLine("Would you like to play again? Y/N");
                string input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    board = new Gameboard();
                    hacks = false;
                    carrier = PlaceShip(5, "carrier");
                    battleship = PlaceShip(4, "battleship");
                    submarine1 = PlaceShip(3, "Submarine");
                    submarine2 = PlaceShip(3, "Submarine");
                    destroyer1 = PlaceShip(2, "Destroyer");
                    destroyer2 = PlaceShip(2, "Destroyer");
                    shipList.Add(carrier);
                    shipList.Add(battleship);
                    shipList.Add(submarine1);
                    shipList.Add(submarine2);
                    shipList.Add(destroyer1);
                    shipList.Add(destroyer2);
                }
                else if (input == "n" || input == "N")
                {
                    Console.WriteLine("Goodbye");
                }
            }
        }

        private bool AllSunk()
        {
            return shipList.TrueForAll(isSunk);
        }
        private bool isSunk(Ship i)
        {
            return i.Sunk;
        }

        private void Shoot(int x, int y)
        {
            if (board.CheckHit(x, y) == 0)
            {
                board.Miss(x, y);
                board.Display(hacks);
                Console.WriteLine("That's a miss Capitan!");
            }
            else if (board.CheckHit(x, y) == 1)
            {
                HitShip(x, y);
                board.Display(hacks);

            }
            else if (board.CheckHit(x, y) == 2)
            {
                // hit previous target
                board.Display(hacks);
                Console.WriteLine("You have already hit that spot Capitan!\nThings don't move in this game");
            }
        }

        private void HitShip(int x, int y)
        {
            for (int i = 0; i < shipList.Count; i++)
            {
                if (shipList[i].BowX == shipList[i].SternX)
                {
                    if (shipList[i].BowY > shipList[i].SternY)
                    {
                        for (int j = shipList[i].SternY; j <= shipList[i].BowY; j++)
                        {
                            if (shipList[i].BowX == x && j == y)
                            {
                                board.Hit(x, y);
                                bool sunk = shipList[i].shipSunk();
                                if (sunk == true)
                                {
                                    Console.WriteLine($"you have sunk a {shipList[i].Type}");
                                }
                                else
                                    Console.WriteLine("You have hit a ship Capitan");
                            }
                            else
                                continue;
                        }
                        

                    }
                    else if (shipList[i].SternY > shipList[i].BowY)
                    {
                        for (int j = shipList[i].BowY; j <= shipList[i].SternY; j++)
                        {
                            if (shipList[i].BowX == x && j == y)
                            {
                                board.Hit(x, y);
                                bool sunk = shipList[i].shipSunk();
                                if (sunk == true)
                                {
                                    Console.WriteLine($"you have sunk a {shipList[i].Type}");
                                }
                                else
                                    Console.WriteLine("You have hit a ship Capitan");
                            }
                            else
                                continue;
                            //Console.Write($"{shipList[i].bowX},{j} ");
                        }
                        continue;
                    }
                }
                else if (shipList[i].BowY == shipList[i].SternY)
                {
                    if (shipList[i].BowX > shipList[i].SternX)
                    {
                        for (int j = shipList[i].SternX; j <= shipList[i].BowX; j++)
                        {
                            if (j == x && shipList[i].BowY == y)
                            {
                                board.Hit(x, y);
                                bool sunk = shipList[i].shipSunk();
                                if (sunk == true)
                                {
                                    Console.WriteLine($"you have sunk a {shipList[i].Type}");
                                }
                                else
                                    Console.WriteLine("You have hit a ship Capitan");
                            }
                            else
                                continue;
                            //Console.Write($"{j},{shipList[i].bowY} ");
                        }
                        continue;
                    }
                    else if (shipList[i].SternX > shipList[i].BowX)
                    {
                        for (int j = shipList[i].BowX; j <= shipList[i].SternX; j++)
                        {
                            if (j == x && shipList[i].BowY == y)
                            {
                                board.Hit(x, y);
                                bool sunk = shipList[i].shipSunk();
                                if (sunk == true)
                                {
                                    Console.WriteLine($"you have sunk a {shipList[i].Type}");
                                }
                                else
                                    Console.WriteLine("You have hit a ship Capitan");
                            }
                            else
                                continue;
                            //Console.Write($"{j},{shipList[i].bowY} ");
                        }
                        continue;
                    }
                    else
                        Console.WriteLine( "Error");
                }
            }
        }

        // add a ship to gameboard
        private Ship PlaceShip(int length, string type)
        {
            int bowX = rnd.Next(10);
            int bowY = rnd.Next(10);
            int direction = rnd.Next(1, 5);
            bool validBow = board.FreeSpace(bowX, bowY);
            int stern;
            int sternX = -1;
            int sternY = -1;
            if (validBow == false)
            {
                while (validBow == false)
                {
                    bowX = rnd.Next(10);
                    bowY = rnd.Next(10);
                    direction = rnd.Next(1, 5);
                    validBow = board.FreeSpace(bowX, bowY);
                }
            }
            if (validBow == true)
            {
                switch (direction)
                {
                    case 1: // stern is north of the bow
                        stern = (bowY - length + 1);
                        if (stern >= 0)
                        {
                            for (int i = stern; i <= bowY; i++)
                            {
                                if (board.FreeSpace(bowX, i) == false)
                                {
                                    goto case 2;
                                }
                                else
                                {
                                    sternX = bowX;
                                    sternY = stern;
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            goto case 2;
                        }
                    case 2: // stern is east of the bow
                        stern = (bowX + length - 1);
                        if (stern <= 9)
                        {
                            for (int i = bowX; i <= stern; i++)
                            {
                                if (board.FreeSpace(i, bowY) == false)
                                {
                                    goto case 3;
                                }
                                else
                                {
                                    sternX = stern;
                                    sternY = bowY;
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            goto case 3;
                        }
                    case 3: // stern is south of the bow
                        stern = (bowY + length - 1);
                        if (stern <= 9)
                        {
                            for (int i = bowY; i <= stern; i++)
                            {
                                if (board.FreeSpace(bowX, i) == false)
                                {
                                    goto case 4;
                                }
                                else
                                {
                                    sternX = bowX;
                                    sternY = stern;
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            goto case 4;
                        }
                    case 4: // stern is west of the bow
                        stern = (bowX - length + 1);
                        if (stern >= 0)
                        {
                            for (int i = stern; stern <= bowX; i++)
                            {
                                if (board.FreeSpace(i, bowY) == false)
                                {
                                    goto default;
                                }
                                else
                                {
                                    sternX = stern;
                                    sternY = bowY;
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            goto default;
                        }
                    default:
                        if (direction != 1)
                        {
                            goto case 1;
                        }
                        else
                        {
                            sternX = 0;
                            sternY = 0;
                            Console.WriteLine("bow position failed to find valid positioning");
                            break;
                        }
                }
            }
            
            Ship temp = new Ship(length, bowX, bowY, sternX, sternY, type);
            board.AddShipIcon(bowX, bowY, sternX, sternY, temp);
            return temp;
        }

        //constructors
        public Game()
        {
            board = new Gameboard();
            hacks = false;
            carrier = PlaceShip(5, "carrier");
            battleship = PlaceShip(4, "battleship");
            submarine1 = PlaceShip(3, "Submarine");
            submarine2 = PlaceShip(3, "Submarine");
            destroyer1 = PlaceShip(2, "Destroyer");
            destroyer2 = PlaceShip(2, "Destroyer");
            shipList.Add(carrier);
            shipList.Add(battleship);
            shipList.Add(submarine1);
            shipList.Add(submarine2);
            shipList.Add(destroyer1);
            shipList.Add(destroyer2);
        }

    }
}
