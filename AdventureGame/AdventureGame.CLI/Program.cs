using AdventureGame.Core;

static void Main(string[] args)
{
    GameEngine engine = new GameEngine();
    engine.InitializeMazeBuild(10, 10);

    string lastActionMessage = "Welcome to the Maze!";

    while (engine.IsRunning)
    {
        Console.Clear();

        RenderMaze(engine.Maze, engine.Player);
        Console.WriteLine(value: $"\nHP: {GetHealth(engine)}/150");
        Console.WriteLine($"Message: {lastActionMessage}");

        ConsoleKeyInfo input = Console.ReadKey(true);
        lastActionMessage = engine.ProcessInput(input.Key);
    }

    Console.WriteLine("Game Over! Press any key to exit.");
    Console.ReadKey();

    while (!gameOver)
    {
        Console.Clear();

        for (int y = 0; y < maze.Height; y++)
        {
            for (int x = 0; x < maze.Width; x++)
        }

        Console.WriteLine($"HP: {catplayer.Health} | Last Action: {messageFromCore}");

        ConsoleKeyInfo input = Console.ReadKey(true);

        messageFromCore = engine.ProcessInput(input.Key);
    }
}

static void RenderMaze(object maze, object player)
{
    throw new NotImplementedException();
}

static object GetHealth(GameEngine engine) 
{ 
    return engine.Player(GetHealth); 
}

