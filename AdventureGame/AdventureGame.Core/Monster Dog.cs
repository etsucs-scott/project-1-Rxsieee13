using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame.Core
{
    public class Monster_Dog : ICatCharacter
    {
        private static Random _rng = Random();

        private static Random Random()
        {
            throw new NotImplementedException();
        }

        public int Health { get; set; } = 35;
        public int damage { get; } = 10;
        int ICatCharacter.damage { get => damage; set => throw new NotImplementedException(); }

        public void Attack(ICatCharacter target)
        {
            target.TakeDamage(damage);
        }
        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public int BaseDamage()
        {
            throw new NotImplementedException();
        }

        public void BaseDamage(int value)
        {
            throw new NotImplementedException();
        }
    }
}
