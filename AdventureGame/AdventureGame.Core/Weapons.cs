using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    public class Weapons : Items
    {
        private int modifier;

        public int AttackModifier { get; set; }
            public Weapons(String name, string message, int modifier)
            : base(name, message) => AttackModifier = modifier;
        public Weapons(int modifier) : base("New Weapon!", "You have picked up a weapon! +" + modifier + " damage.")
        {
            this.modifier = modifier;
        }
    }
}
