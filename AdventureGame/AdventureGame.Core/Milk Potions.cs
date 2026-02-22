using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    public class Milk_Potions : Items
        {
            public int HealAmount { get; set; } = 20;
            public int DamageAmount { get; set; }

        public Milk_Potions() : base("Holy Milk Potion!!!", "You have picked up a potion! +20 HP") { }
    }
}
