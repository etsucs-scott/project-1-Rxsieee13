using AdventureGame.Core;

namespace AdventureGame.CLI
{
    internal class Game_Engine
    {
        private Maze maze;
        private Cat_Player catplayer;
        private Monster_Dog monsterdog;
        private Milk_Potions milkpotions;
        private Weapons weapons;
        private bool GameOver = false;
        public string gameMessage = string.Empty;

        public void GameLoop()
        {
            maze = new Maze(10, 10);
            catplayer = new Core.Cat_Player();
            monsterdog = new Monster_Dog();
            milkpotions = new Milk_Potions();
            weapons = new Weapons(5);

            maze.PlacePlayer(catplayer);
            maze.PlaceExit();
            maze.PlaceMonsterDog();
            maze.PlaceMilkPotions();
            maze.PlaceWeapons();
            maze.PlaceInsideWalls();

            while (true)
            {
                Console.Clear();
                PrintMaze();
                Console.WriteLine(gameMessage);

                if (GameOver)
                {
                    gameMessage = string.Empty;
                    break;
                }

                gameMessage = string.Empty;

                Console.Write("Enter move using WASD or arrow keys: ");
                var key = Console.ReadKey(true);
                PlayerMoves(key.Key);
                CheckSpecialTile();
                maze.tiles[catplayer.xPos, catplayer.yPos] = '@';

            }

        }

        public void PrintMaze()
        {
            for (int y = 0; y < maze.height; y++)
            {
                for (int x = 0; x < maze.width; x++)
                {
                    Console.Write(maze.tiles[x, y] + " ");
                }
                Console.WriteLine();
            }
        }

        public void PlayerMoves(ConsoleKey Key)
        {
            int xPosition = catplayer.xPos;
            int yPosition = catplayer.yPos;

            switch (Key)
            {
                case ConsoleKey.W: yPosition--; break;
                case ConsoleKey.UpArrow: yPosition--; break;
                case ConsoleKey.A: xPosition--; break;
                case ConsoleKey.LeftArrow: xPosition--; break;
                case ConsoleKey.S: yPosition++; break;
                case ConsoleKey.DownArrow: yPosition++; break;
                case ConsoleKey.D: xPosition++; break;
                case ConsoleKey.RightArrow: xPosition++; break;

                default:
                    gameMessage = "Invalid key. Use WASD or arrow keys.";
                    return;
            }

            if (maze.tiles[xPosition, yPosition] == '#')
            {
                gameMessage = "Invalid input. Wall blocking path.";
                return;
            }

            maze.tiles[catplayer.xPos, catplayer.yPos] = '.';
            catplayer.xPos = xPosition;
            catplayer.yPos = yPosition;
        }

        public void Battle()
        {
            while (catplayer.Health > 0)
            {
                catplayer.Attack(monsterdog);
                gameMessage += "Cat Player attacked the dog! -" + catplayer.damage + "HP\n";
                if (monsterdog.Health <= 0)
                {
                    gameMessage += "Yay, You defeated the monster!\n";
                    maze.tiles[catplayer.xPos, catplayer.yPos] = '@';
                    break;
                }
                monsterdog.Attack(catplayer);
                gameMessage += "The Dog just attacked you. -" + monsterdog.damage + "HP\n";
                if (catplayer.Health <= 0)
                {
                    gameMessage += "OH NO The Dog killed you! Game over!\n";
                    GameOver = true;
                    break;
                }
            }
        }
        public void CheckSpecialTile()
        {
            char tile = maze.tiles[catplayer.xPos, catplayer.yPos];

            if (tile == 'E')
            {
                gameMessage = "You have reached the exit! Game over :) !\n";
                maze.tiles[catplayer.xPos, catplayer.yPos] = '@';
                GameOver = true;
            }
            else if (tile == 'M')
            {
                gameMessage = "Oh no! You ran into a DOG! Battle it to move on.\n";
                Battle();
            }
            else if (tile == 'P')
            {
                catplayer.PickUpItem(milkpotions);
                gameMessage = milkpotions.pickupMessage;
                catplayer.HealAmount(milkpotions);
                maze.tiles[catplayer.xPos, catplayer.yPos] = '@';
            }
            else if (tile == 'W')
            {
                catplayer.PickUpItem(weapons);
                gameMessage = weapons.pickupMessage;
                catplayer.damage += catplayer.GetHighestModifier();
                maze.tiles[ catplayer.xPos, catplayer.yPos] = '@';
            }
        }
    }
}
