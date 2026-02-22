

namespace AdventureGame.Core
{
    public interface ICatCharacter
    {
        int Health { get; set; }

        int BaseDamage();
        void BaseDamage(int value);

        int damage { get; set; }
        void Attack(ICatCharacter target);
        void TakeDamage(int amount);
    }
}
