using AdventureGame.CLI;
using AdventureGame.Core;

public class Program
{
    public static void Main()
    {
        Game_Engine game = new Game_Engine();
        game.GameLoop();
        Console.WriteLine(game.gameMessage);
    }
}
