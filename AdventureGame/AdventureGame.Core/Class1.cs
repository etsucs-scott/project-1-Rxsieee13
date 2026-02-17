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
    public MonsterDog? IsMonsterDog { get; set; }
    public items? Isitems { get; set; }
    public bool IsWalkable => !IsWall;

    public items Isitem { get; internal set; }
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
    public object Core { get; private set; }

    public bool GetItems(bool items) => items;

    public string MoveCat(int deltaX, int deltaY, CatPlayer catplayer, items item, Tile targetTile, items Isitem, CatPlayer catPlayer)
    {
        int newX = CatPlayer.X + deltaX;
        int newY = CatPlayer.Y + deltaY;

        Tile tile = GetTile(newX, newY);
        if (targetTile == null || targetTile.IsWall)
        {
            return $"You cannot move there!";
        }

        CatPlayer.X = newX;
        CatPlayer.Y = newY;

        if (targetTile.IsExit) return $"Win!";

        if (targetTile.Isitems != null)
        {
            catplayer.Inventory.Add(targetTile.Isitem);
            targetTile.Isitems = null;

            if (targetTile.Isitems is MilkPotion p) catplayer.Health += p.HealAmount;
            return $"Picked up {item.Name}! {item.PickupMessage}";

        }

        if (targetTile.IsMonsterDog != null)
        {
            return StartBattle(targetTile.IsMonsterDog, targetTile);
        }

        return "Moved safely! :)";
    }

    private string StartBattle(MonsterDog isMonsterDog, Tile targetTile)
    {
        throw new NotImplementedException();
    }

    private string StartBattle(bool isMonsterDog, Tile targetTile)
    {
        throw new NotImplementedException();
    }

    private Tile GetTile(int newX, int newY)
    {
        throw new NotImplementedException();
    }
}
}
