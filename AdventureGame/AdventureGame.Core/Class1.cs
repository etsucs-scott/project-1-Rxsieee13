namespace AdventureGame.Core
{
    public interface ICatCharacter
    {
        int Health { get; set; }
        int BaseDamage { get; set; }
        void Attack(ICatCharacter target);
        void TakeDamage(int amount);
    }
    public abstract class items
    {
        public string Name { get; set; }
        public string PickupMessage { get; set; }

        protected items(string name, string pickupMessage)
        {
            Name = name;
            PickupMessage = pickupMessage;
        }
    }
    public class Weapon : items
    {
        public int AttackModifier { get; set; }
        public Weapon(String name, string message, int modifier) : base(name, message) => AttackModifier = modifier;
    }
    public class MilkPotion : items
    {
        public int HealAmount { get; } = 20;
        public int DamageAmount { get; set; }
        public MilkPotion(string name, string message) : base(name, message) { }
    }
    public class CatPlayer : ICatCharacter
    {
        public int Health { get; set; } = 100;
        public int MaxHealth { get; } = 150;
        public int BaseDamage { get; } = 10;
        public List<items> Inventory { get; private set; } = new List<items>();
        int ICatCharacter.BaseDamage { get => BaseDamage; set => throw new NotImplementedException(); }
        public static int X { get; internal set; }
        public static int Y { get; internal set; }

        public void Attack(ICatCharacter target)
        {
            int bestModifier = Inventory.OfType<Weapon>()
                .Select(w => w.AttackModifier)
                .DefaultIfEmpty(0)
                .Max();

            target.TakeDamage(BaseDamage + bestModifier);
        }
        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0)
            {
                Health = 0;
            }
        }
        public void DrinkMilkPotion(MilkPotion milkpotion)
        {
            Health = Math.Min(MaxHealth, Health + milkpotion.HealAmount);
            Inventory.Remove(milkpotion);
        }
    }

    public class MonsterDog : ICatCharacter
    {
        private static Random _rng = Random();

        private static Random Random()
        {
            throw new NotImplementedException();
        }

        public int Health { get; set; }
        public int BaseDamage { get; } = 10;
        int ICatCharacter.BaseDamage { get => BaseDamage; set => throw new NotImplementedException(); }

        public MonsterDog()
        {
            Health = _rng.Next(30, 50);
        }
        public void Attack(ICatCharacter target)
        {
            target.TakeDamage(BaseDamage);
        }
        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

    }

    public class Tile
    {
        public bool IsWall { get; set; }
        public bool IsExit { get; set; }
        public bool IsMonsterDog { get; set; }
        public bool Isitems { get; set; }
        public bool IsWalkable => !IsWall;
    }

    public class Maze
    {
        private Tile[,] _grid;
        public int Width { get; }
        public int Height { get; }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            _grid = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _grid[x, y] = new Tile();
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return null;
            return _grid[x, y];
        }
        public void Exit(int x, int y)
        {
            _grid[x, y].IsExit = true;
        }

    }

    public class GameEngine()
    {
        public bool GetItems() => items;

        public string MoveCat(int deltaX, int deltaY, CatPlayer catPlayer, bool item, bool item, bool items, Tile targetTile)
        {
            int newX = CatPlayer.X + deltaX;
            int newY = CatPlayer.Y + deltaY;

            Tile targetTile = GetTile(newX, newY);

            if (targetTile == null || targetTile.IsWall)
            {
                return $"You cannot move there!";
            }

            CatPlayer.X = newX;
            CatPlayer.Y = newY;

            if (targetTile.IsExit) return $"Win!";

            if (targetTile.Isitems != null)
            {
                catPlayer.Inventory.Add(targetTile.Isitems);
                targetTile.Isitems = null;

                if (targetTile.Isitems is MilkPotion p) CatPlayer.Health += p.HealAmount;
                return $"Picked up {Core.items.Name}! {Core.items.PickupMessage}";

            }

            if (targetTile.IsMonsterDog != null)
            {
                return StartBattle(targetTile.IsMonsterDog, targetTile);
            }

            return "Moved safely! :)";
        }

        private Tile GetTile(int newX, int newY)
        {
            throw new NotImplementedException();
        }    }
        private string StartBattle(MonsterDog monsterdog, Tile tile)
        {
            StringBuilder log = new StringBuilder("A monster appeared!");

            while (CatPlayer.Health > 0 && monsterdog.Health > 0)
            {
                int pDamage = 10 + GetBestWeaponModifier();
                monsterdog.TakeDamage(pDamage);
                log.AppendLine($"You deal {pDamage} damage.");

                if (monsterdog.Health <= 0)
                {
                    tile.MonsterDog = null;
                    log.AppendLine("Monster defeated!");
                    break;
                }

                CatPlayer.TakeDamage(monsterdog.BaseDamage);
                log.AppendLine($"Monster deals {monsterdog.BaseDamage} damage!");
            }

            if (CatPlayer.Health <= 0) return "GAME OVER!!!!";
            return log.ToString();
        }

        private int GetBestWeaponModifier()
        {
            return CatPlayer.Inventory.OfType<Weapon>()
                                   .Select(w => w.AttackModifier)
                                   .DefaultIfEmpty(0)
                                   .Max();
        }

        public void MazeBuild(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _grid[x, y] = new Tile { IsWall = true };
                }
            }

            Random rng = new Random();
            int wallX = 0, wallY = 0;
            _grid[wallX, wallY].IsWall = false;

            while (wallX < width - 1 || wallY < height - 1)
            {
                if (rng.Next(0, 2) == 0 && wallX < width - 1) wallX++;
                else if (wallY < height - 1) wallY++;

                _grid[wallX, wallY].IsWall = false;
            }
            _grid[width - 1, height - 1].IsExit = true;

            PopulateWorld(rng);
        }


        private void PopulateWorld(Random rng)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if ((x == 0 && y == 0) || _grid[x, y].IsExit) continue;

                    if (!_grid[x, y].IsWall && rng.Next(0, 100) < 10)
                    {
                        _grid[x, y].Monster = new Monster();
                    }
                    else if (!_grid[x, y].IsWall && rng.Next(0, 100) < 5)
                    {
                        _grid[x, y].Item = new Weapon("Iron Sword", "You found a sharp blade!", 20);
                    }
                }
            }
        }





    }
}
