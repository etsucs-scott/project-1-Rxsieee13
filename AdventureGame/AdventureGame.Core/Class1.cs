namespace AdventureGame.Core
{
public interface ICharacter
{ 
    int Health { get; set; }
    int BaseDamage { get; set; }
    void Attack(ICharacter target);
    void TakeDamage(int amount);
}
public abstract class item
{
    public string Name { get; set; }
    public string PickupMessage { get; set; }

    protected item (string name, string pickupMassage)
    {
        Name = name;
        PickupMessage = pickupMassage;
    }
}
public class Weapon : item
{
    public int AttackModifier { get; set; }
    public Weapon (String name, string message, int modifier) : base (name, message) => AttackModifier = modifier;
}
public class Potion : item
{
    public int HealAmount {  get; set; }
    public int DamageAmount { get; set; }
    public Potion (string name, string message) : base (name, message) { }
}
public class Player : ICharacter
{
    public int Health { get; set; } = 100;
    public int MaxHealth { get; } = 150;
    public int BaseDamage { get; } = 10;
    public List<item> Inventory { get; private set; } = new List<item>();

    public void Attack(ICharacter target)
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
    public void DrinkPotion(Potion potion)
    {
        Health = Math.Min(MaxHealth, Health + potion.HealAmount);
        Inventory.Remove(potion);
    }
}


}
