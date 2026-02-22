namespace AdventureGame.Core
{
    public class Cat_Player : ICatCharacter
    {
        public const int baseHealth = 100;
        public int Health { get; set; }

        public int xPos { get; set; }
        public int yPos { get; set; }

        public int damage { get; set; }

        public int BaseDamage { get; private set; } = 10;
        int ICatCharacter.Health { get => Health; set => Health = value; }

        public Cat_Player()
        {
            Health = baseHealth;
            xPos = 1;
            yPos = 1;
            damage = BaseDamage;
        }

        public List<Items> Inventory = new List<Items>();

        public void Attack(ICatCharacter target)
        {
            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void PickUpItem(Items item)
        {
            if (item is Milk_Potions potion)
                HealAmount(potion);
            else
                Inventory.Add(item);
        }


        public int GetHighestModifier()
        {
            int highestMod = 0;
            foreach (Items item in Inventory)
            {
                if (item is Weapons weapon)
                {
                    if (weapon.AttackModifier > highestMod)
                        highestMod = weapon.AttackModifier;
                }
            }
            return highestMod;
        }

        public void HealAmount(Milk_Potions potion)
        {
            Health += potion.HealAmount;
            if (Health > baseHealth)
                Health = baseHealth;
        }

        int ICatCharacter.BaseDamage()
        {
            throw new NotImplementedException();
        }

        void ICatCharacter.BaseDamage(int value)
        {
            throw new NotImplementedException();
        }
    }
}
